using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Linq;
using CSCore;
using CSCore.SoundOut;
using CSCore.CoreAudioAPI;

namespace SynthTest
{
	public partial class Form1 : Form, IWaveSource
	{
		//ISoundOut waveOut = new WaveOut(32);
		ISoundOut waveOut = new WasapiOut(true, AudioClientShareMode.Shared, 1);
		List<PianoKey> mixfreq = new List<PianoKey>();
		double volValue = 0.01;

		//int[] basetable = new[] { 26200, 29400, 33000, 34900, 39200, 44000, 49525, 52400, 58800, 66000 };

		public Form1()
		{
			InitializeComponent();

			using (var mmdeviceEnumerator = new MMDeviceEnumerator())
			{
				using (var mmdeviceCollection = mmdeviceEnumerator.EnumAudioEndpoints(DataFlow.Render, DeviceState.Active))
				{
					comboBox3.DataSource = mmdeviceCollection.ToList();
					comboBox3.DisplayMember = "FriendlyName";
					comboBox3.ValueMember = "DeviceID";
				}
			}

			SetStyle(ControlStyles.UserPaint, true);
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			SetStyle(ControlStyles.DoubleBuffer, true);

			comboBox1.DataSource = Enum.GetValues(typeof(WaveForm));
			comboBox2.DataSource = Enum.GetValues(typeof(WaveForm));
			comboBox1.SelectedIndex = (int)WaveForm.Sawtooth;
			comboBox2.SelectedIndex = (int)WaveForm.Sawtooth;

			tbarVolume_Scroll(tbarVolume, null);
			numericUpDown1.Value = piano1.Octave;
			numericUpDown2.Value = piano2.Octave;

			piano1.PianoKeyDown += k =>
			{
				PlayKey(k, true);
				frequencyVisualizer1.PianoKey = k;
			};
			piano1.PianoKeyUp += k => PlayKey(k, false);
			piano2.PianoKeyDown += k => PlayKey(k, true);
			piano2.PianoKeyUp += k => PlayKey(k, false);

			PrecalcKeys();
		}

		public void AutoPlay()
		{
			SelectDevice((MMDevice)comboBox3.SelectedItem);
		}

		private void SelectDevice(MMDevice device)
		{
			waveOut?.Dispose();

			var ws = new WasapiOut(true, AudioClientShareMode.Shared, 1) { Device = device };
			waveOut = ws;
			waveOut.Initialize(this);
			waveOut.Play();
		}

		private void buttonExit_Click(object sender, EventArgs e)
		{
			Close();
		}

		protected override void OnFormClosed(FormClosedEventArgs e)
		{
			waveOut.Dispose();
			base.OnFormClosed(e);
		}

		private void tbarVolume_Scroll(object sender, EventArgs e)
		{
			textVolume.Text = ((TrackBar)sender).Value.ToString();
			volValue = ((TrackBar)sender).Value / 100d;
		}

		private void PlayKey(PianoKey key, bool on)
		{
			key.Selected = on;
			bool cont = mixfreq.Contains(key);
			if (on && !cont)
			{
				key.WaveFin = false;
				mixfreq.Add(key);
			}
			else if (!on && cont)
			{
				key.WaveFin = true;
				//key.FreqPos = 0;
			}
			key.Parent.Invalidate(key.Bounds);
		}

		static readonly Keys[] KeyRow1B = new Keys[] { Keys.D2, Keys.D3, Keys.D5, Keys.D6, Keys.D7, Keys.D9, Keys.D0, Keys.Oemplus };
		static readonly Keys[] KeyRow1W = new Keys[] { Keys.Q, Keys.W, Keys.E, Keys.R, Keys.T, Keys.Y, Keys.U, Keys.I, Keys.O, Keys.P, Keys.Oem4, Keys.Oem6 };

		static readonly Keys[] KeyRow2B = new Keys[] { Keys.A, Keys.S, Keys.F, Keys.G, Keys.H, Keys.K, Keys.L, Keys.Oem7 };
		static readonly Keys[] KeyRow2W = new Keys[] { Keys.Oem102, Keys.Z, Keys.X, Keys.C, Keys.V, Keys.B, Keys.N, Keys.M, Keys.Oemcomma, Keys.OemPeriod, Keys.Oem2 };

		private void PrecalcKeys()
		{
			PrecalcKeys(KeyRow1B, KeyRow1W, piano1);
			PrecalcKeys(KeyRow2B, KeyRow2W, piano2);
		}
		private void PrecalcKeys(Keys[] rowb, Keys[] roww, Piano piano)
		{
			for (int i = 0; i < rowb.Length; i++)
				keyIndex[(int)rowb[i]] = piano.BlackKeys[i];
			for (int i = 0; i < roww.Length; i++)
				keyIndex[(int)roww[i]] = piano.WhiteKeys[i];
		}

		PianoKey[] keyIndex = new PianoKey[255];
		bool[] down = new bool[255];
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

		System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
		public int Read(byte[] buffer, int offset, int count)
		{
			try
			{
				if (WaveFormat.BitsPerSample == 8)
					return Read_8bit(buffer, offset, count);
				else if (WaveFormat.BitsPerSample == 16)
					return Read_16bit(buffer, offset, count);
				else
					return 0;
			}
			catch (Exception e) { return 0; }
		}

		public unsafe int Read_16bit(byte[] buffer, int offset, int count)
		{
			sw.Restart();

			int minOf = Math.Min(buffer.Length, count);
			if (minOf % 2 != 0)
				throw new ArgumentException();
			minOf /= 2;
			var mflocal = mixfreq.ToArray();

			fixed (byte* bytePointer = buffer)
			{
				short* shortPointer = (short*)bytePointer;

				for (int i = 0; i < minOf; i++)
				{
					double value = 0;
					for (int j = 0; j < mflocal.Length; j++)
					{
						var key = mflocal[j];
						if (key == null) continue;
						var addval = key.CalcWave();
						value += addval;
						if (key.WaveFin && key.IsFin(addval))
						{
							mixfreq.Remove(key);
							key.WaveFin = false;
							key.FreqPos = 0;
							mflocal[j] = null;
						}
					}
					shortPointer[i] = (short)(value / Math.Sqrt(mflocal.Length + 1) * volValue * short.MaxValue);
				}
			}
			sw.Stop();
			if (mflocal.Length == 0)
			{

			}

			shortPos += minOf;
			return count;
		}

		public unsafe int Read_8bit(byte[] buffer, int offset, int count)
		{
			sw.Restart();

			int minOf = Math.Min(buffer.Length, count);
			if (minOf % 2 != 0)
				throw new ArgumentException();
			minOf /= 2;
			var mflocal = mixfreq.ToArray();

			fixed (byte* bytePointer = buffer)
			{
				sbyte* sbytePointer = (sbyte*)bytePointer;

				for (int i = 0; i < minOf; i++)
				{
					double value = 0;
					foreach (var key in mflocal)
					{
						value += key.CalcWave();
					}
					sbytePointer[i] = (sbyte)(value / Math.Sqrt(mflocal.Length) * volValue * sbyte.MaxValue);
				}
			}
			sw.Stop();

			shortPos += minOf;
			return count;
		}

		private void numericUpDown1_ValueChanged(object sender, EventArgs e) { piano1.Octave = (int)numericUpDown1.Value; PrecalcKeys(); }
		private void numericUpDown2_ValueChanged(object sender, EventArgs e) { piano2.Octave = (int)numericUpDown2.Value; PrecalcKeys(); }

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) { piano1.WaveForm = (WaveForm)comboBox1.SelectedItem; }
		private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) { piano2.WaveForm = (WaveForm)comboBox2.SelectedItem; }

		private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
		{
			SelectDevice((MMDevice)comboBox3.SelectedItem);
		}
	}
}
