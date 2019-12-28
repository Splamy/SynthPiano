using CSCore;
using CSCore.CoreAudioAPI;
using CSCore.SoundOut;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SynthTest
{
	public partial class Form1 : Form, IWaveSource
	{
		ISoundOut waveOut = null;
		readonly List<PianoKey> mixfreq = new List<PianoKey>();
		double volValue = 0.01;
		bool interceptSlider = true;
		int fade = 0;

		public Form1()
		{
			InitializeComponent();
			comboBox1.DataSource = Enum.GetValues(typeof(WaveForm)); // TODO fix saving type
			sequencer1.parent = this;

			using (var mmdeviceEnumerator = new MMDeviceEnumerator())
			using (var mmdeviceCollection = mmdeviceEnumerator.EnumAudioEndpoints(DataFlow.Render, DeviceState.Active))
			{
				var mmdeviceList = mmdeviceCollection.ToList();
				comboBox3.SelectedIndexChanged -= ComboBox3_SelectedIndexChanged;
				comboBox3.DataSource = mmdeviceList;
				comboBox3.SelectedIndexChanged += ComboBox3_SelectedIndexChanged;
				comboBox3.DisplayMember = "FriendlyName";
				comboBox3.ValueMember = "DeviceID";
				comboBox3.SelectedItem = mmdeviceList.FirstOrDefault(x => x.FriendlyName == Config.Default.LastAudioDevice);
			}

			SetStyle(ControlStyles.UserPaint, true);
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			SetStyle(ControlStyles.DoubleBuffer, true);

			comboBox1.SelectedItem = Config.Default.LastWave1;

			TbarVolume_Scroll(tbarVolume, null);
			piano1.Octave = Config.Default.LastOctave1;
			numericUpDown1.Value = piano1.Octave;

			piano1.PianoKeyDown += k =>
			{
				if (recordSeq)
				{
					sequencer1.SetP(k);
					sequencer1.Invalidate();
				}
				PlayKey(k, true);
				frequencyVisualizer1.PianoKey = k;
			};
			piano1.PianoKeyUp += k => PlayKey(k, false);

			PrecalcKeys();

			InterceptKeys.KeyEvent = InterceptKeys_KeyEvent;
		}

		private bool InterceptKeys_KeyEvent(bool down, Keys e)
		{
			const double mult = 0.003;

			if (!down || !interceptSlider || (e != Keys.VolumeUp && e != Keys.VolumeDown))
				return false;

			var signedMult = e == Keys.VolumeUp ? 1 + mult : 1 - mult;

			lock (mixfreq)
			{
				foreach (var key in mixfreq)
				{
					if (!key.IsWaveFading && !key.IsWaveFinalizing)
					{
						key.Frequency *= signedMult;
					}
				}
			}

			return true;
		}

		public void AutoPlay()
		{
			if (comboBox3.SelectedItem is MMDevice device)
				SelectDevice(device);
		}

		private void SelectDevice(MMDevice device)
		{
			CloseDevice();

			var ws = new WasapiOut(true, AudioClientShareMode.Shared, 1) { Device = device };

			waveOut = ws;
			waveOut.Initialize(this);
			waveOut.Play();
		}

		private void CloseDevice()
		{
			Config.Default.Save();
			waveOut?.Dispose();
		}

		protected override void OnFormClosed(FormClosedEventArgs e)
		{
			CloseDevice();
			base.OnFormClosed(e);
		}

		private void TbarVolume_Scroll(object sender, EventArgs e)
		{
			textVolume.Text = ((TrackBar)sender).Value.ToString();
			volValue = ((TrackBar)sender).Value / 100d;
		}

		private void TbarFade_Scroll(object sender, EventArgs e)
		{
			const double fshort = 0.05;
			const double flong = 0.9;

			var rawValue = ((TrackBar)sender).Value;
			var rawMax = ((TrackBar)sender).Maximum;
			fade = ((TrackBar)sender).Value;
			Global.FadePower = (rawValue / (double)rawMax) * (flong - fshort) + fshort;
		}

		public void PlayKey(PianoKey key, bool on)
		{
			lock (mixfreq)
			{
				key.Selected = on;
				bool cont = mixfreq.Contains(key);
				if (on)
				{
					key.Reset();
					if (!cont)
					{
						mixfreq.Add(key);
					}
				}
				else if (!on && cont)
				{
					if (fade == 0)
						key.FinalizeWave();
					else
						key.Fade();
				}
			}
			key.Parent.Invalidate(key.Bounds);
		}

		static readonly Keys[] KeyRow1B = new Keys[] { Keys.D2, Keys.D3, Keys.D5, Keys.D6, Keys.D7, Keys.D9, Keys.D0, Keys.Oemplus };
		static readonly Keys[] KeyRow1W = new Keys[] { Keys.Q, Keys.W, Keys.E, Keys.R, Keys.T, Keys.Y, Keys.U, Keys.I, Keys.O, Keys.P, Keys.Oem4, Keys.Oem6 };

		static readonly Keys[] KeyRow2B = new Keys[] { Keys.A, Keys.S, Keys.F, Keys.G, Keys.H, Keys.K, Keys.L, Keys.Oem7 };
		static readonly Keys[] KeyRow2W = new Keys[] { Keys.Oem102, Keys.Z, Keys.X, Keys.C, Keys.V, Keys.B, Keys.N, Keys.M, Keys.Oemcomma, Keys.OemPeriod, Keys.Oem2 };

		private static readonly Keys[][] KeyMatrixV2 = {
			new[] { Keys.D1, Keys.D2, Keys.D3, Keys.D4, Keys.D5, Keys.D6, Keys.D7, Keys.D8, Keys.D9, Keys.D0, Keys.OemMinus /*?*/, Keys.Oemplus },
			new[] { Keys.Q, Keys.W, Keys.E, Keys.R, Keys.T, Keys.Y, Keys.U, Keys.I, Keys.O, Keys.P, Keys.Oem4, Keys.Oem6 },
			new[] { Keys.A, Keys.S, Keys.D, Keys.F, Keys.G, Keys.H, Keys.J, Keys.K, Keys.L, Keys.Oem1, Keys.Oem7, Keys.Oem5 },
			new[] { Keys.Z, Keys.X, Keys.C, Keys.V, Keys.B, Keys.N, Keys.M, Keys.Oemcomma, Keys.OemPeriod, Keys.Oem2 },
		};

		private void PrecalcKeys()
		{
			//PrecalcKeys(KeyRow1B, KeyRow1W, piano1);
			//PrecalcKeys(KeyRow2B, KeyRow2W, piano2);

			PrecalcKeysV2();
		}
		private void PrecalcKeys(Keys[] rowb, Keys[] roww, Piano piano)
		{
			for (int i = 0; i < rowb.Length; i++)
				keyIndex[(int)rowb[i]] = piano.BlackKeys[i];
			for (int i = 0; i < roww.Length; i++)
				keyIndex[(int)roww[i]] = piano.WhiteKeys[i];
		}

		private void PrecalcKeysV2()
		{
			for (int i = 0; i < 4 * 12; i++)
			{
				try { keyIndex[(int)KeyMatrixV2[i % 4][i / 4]] = piano1.AllKeys[i]; } catch { } // TODO not-lazy :P
			}
		}

		readonly PianoKey[] keyIndex = new PianoKey[255];
		readonly bool[] down = new bool[255];

		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			int keyint = (int)e.KeyCode;
			if (keyint < 0 || keyint >= 255 || down[keyint] || keyIndex[keyint] == null) return;
			down[keyint] = true;
			PlayKey(keyIndex[keyint], true);
		}

		protected override void OnKeyUp(KeyEventArgs e)
		{
			base.OnKeyUp(e);
			int keyint = (int)e.KeyCode;
			if (keyint < 0 || keyint >= 255 || keyIndex[keyint] == null) return;
			down[keyint] = false;
			PlayKey(keyIndex[keyint], false);
		}

		public bool CanSeek => false;
		public long Length => -1;
		private long shortPos = 0;
		public long Position { get { return shortPos * 2; } set { shortPos = value / 2; } }

		public WaveFormat WaveFormat { get; } = new WaveFormat(Global.Bitrate, 16, 1);

		public int Read(byte[] buffer, int offset, int count)
		{
			if (WaveFormat.BitsPerSample == 8)
				return Read_Nbit<sbyte>(buffer, offset, count);
			else if (WaveFormat.BitsPerSample == 16)
				return Read_Nbit<short>(buffer, offset, count);

			throw new NotSupportedException();
		}

		public unsafe int Read_Nbit<T>(byte[] buffer, int offset, int count) where T : unmanaged
		{
			if (offset > buffer.Length)
				throw new ArgumentOutOfRangeException(nameof(offset));
			int minOf = Math.Min(buffer.Length - offset, count);
			if (minOf % sizeof(T) != 0)
				throw new ArgumentException();
			minOf /= sizeof(T);

			lock (mixfreq)
			{
				fixed (byte* bytePointer = buffer)
				{
					var samplePointer = (T*)(bytePointer + offset);

					for (int i = 0; i < minOf; i++)
					{
						double value = 0;

						for (int j = 0; j < mixfreq.Count; j++)
						{
							var key = mixfreq[j];
							if (key == null) continue;
							var addval = key.CalcWave();

							if (key.IsWaveFinalizing && key.IsFinal(addval))
							{
								mixfreq[j] = null;
							}

							if (key.IsWaveFading)
							{
								var fadeMul = key.GetFadeMul();
								if (!key.IsWaveFinalizing && fadeMul <= 0.001)
								{
									key.FinalizeWave();
								}
								value += addval * fadeMul;
							}
							else
							{
								value += addval;
							}
						}

						int maxVal = (1 << (8 * sizeof(T) - 1)) - 1;
						samplePointer[i] = ConvNum<T>(value * volValue * maxVal); // / Math.Sqrt(mixfreq.Count) 
					}
				}

				mixfreq.RemoveAll(x => x is null);
			}

			shortPos += minOf;
			return count;
		}

		public static T ConvNum<T>(double val)
		{
			if (typeof(T) == typeof(sbyte)) return (T)(object)(sbyte)val;
			else if (typeof(T) == typeof(short)) return (T)(object)(short)val;
			else if (typeof(T) == typeof(int)) return (T)(object)(int)val;
			else throw new NotSupportedException();
		}

		private void NumericUpDown1_ValueChanged(object sender, EventArgs e)
		{
			piano1.Octave = (int)numericUpDown1.Value;
			PrecalcKeys();
			Config.Default.LastOctave1 = piano1.Octave;
		}

		private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			piano1.WaveForm = (WaveForm)comboBox1.SelectedItem;
			Config.Default.LastWave1 = piano1.WaveForm;
		}

		private void ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBox3.SelectedItem is MMDevice device)
			{
				Config.Default.LastAudioDevice = device.FriendlyName;
				Config.Default.Save();

				SelectDevice(device);
			}
		}

		private void Form1_Deactivate(object sender, EventArgs e)
		{
			interceptSlider = false;
		}

		private void Form1_Activated(object sender, EventArgs e)
		{
			interceptSlider = true;
		}


		private void Button1_Click(object sender, EventArgs e)
		{
			sequencer1.Start();
		}

		private void Button2_Click(object sender, EventArgs e)
		{
			sequencer1.Stop();
		}

		bool recordSeq = false;

		private void Button3_Click(object sender, EventArgs e)
		{
			recordSeq = !recordSeq;
		}

		private void NumericUpDown2_ValueChanged(object sender, EventArgs e)
		{
			sequencer1.SetBpm((float)numericUpDown2.Value);
		}
	}
}
