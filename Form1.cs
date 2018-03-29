using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Un4seen.Bass;

namespace SynthTest
{
	public partial class Form1 : Form
	{
		private const int sampleRateSec = 48000;
		private const BASSFlag sampleTypeFlag = BASSFlag.BASS_DEFAULT;

		private int bassStream;
		private List<PianoKey> mixfreq = new List<PianoKey>();
		private double volValue = 0.01;
		private int streamBufferSize;

		private STREAMPROC soundCreator;

		//int[] basetable = new[] { 26200, 29400, 33000, 34900, 39200, 44000, 49525, 52400, 58800, 66000 };

		public Form1()
		{
			InitializeComponent();

			InitBass();

			FormClosed += Form1_FormClosed;

			// TODO
			//			comboBox3.DataSource = mmdeviceCollection.ToList();
			//			comboBox3.DisplayMember = "FriendlyName";
			//			comboBox3.ValueMember = "DeviceID";

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

		private void Form1_FormClosed(object sender, FormClosedEventArgs e)
		{
			Bass.BASS_StreamFree(bassStream);
			Bass.BASS_Free();
		}

		private void InitBass()
		{
			Bass.BASS_Init(-1, sampleRateSec, BASSInit.BASS_DEVICE_DEFAULT | BASSInit.BASS_DEVICE_LATENCY, IntPtr.Zero);
			BASS_INFO info = Bass.BASS_GetInfo();
			Console.WriteLine($@"Minimal buffer size: {info.minbuf}, letency: {info.latency}");
			Bass.BASS_SetConfig(BASSConfig.BASS_CONFIG_BUFFER, info.minbuf + 6);
			Bass.BASS_SetConfig(BASSConfig.BASS_CONFIG_UPDATEPERIOD, 5);
			Bass.BASS_SetConfig(BASSConfig.BASS_CONFIG_NET_BUFFER, info.minbuf / 2);

			IntPtr pointer = new IntPtr();
			soundCreator = GetSoundBytes;
			bassStream = Bass.BASS_StreamCreate(sampleRateSec, 1, sampleTypeFlag, soundCreator, pointer);
			Debug.Assert(bassStream != 0, "Stream creation failed.");

			// play
			Bass.BASS_ChannelPlay(bassStream, false);
		}

		public void AutoPlay()
		{
			//			SelectDevice((MMDevice)comboBox3.SelectedItem);
		}

		//		private void SelectDevice(MMDevice device)
		//		{
		//			var ws = new WasapiOut(true, AudioClientShareMode.Shared, 1) { Device = device };
		//			waveOut = ws;
		//			waveOut.Initialize(this);
		//			waveOut.Play();
		//		}

		private void buttonExit_Click(object sender, EventArgs e)
		{
			Close();
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

		static readonly Keys[] KeyRow1B = { Keys.D2, Keys.D3, Keys.D5, Keys.D6, Keys.D7, Keys.D9, Keys.D0, Keys.Oemplus };
		static readonly Keys[] KeyRow1W = { Keys.Q, Keys.W, Keys.E, Keys.R, Keys.T, Keys.Y, Keys.U, Keys.I, Keys.O, Keys.P, Keys.Oem4, Keys.Oem6 };

		static readonly Keys[] KeyRow2B = { Keys.A, Keys.S, Keys.F, Keys.G, Keys.H, Keys.K, Keys.L, Keys.Oem7 };
		static readonly Keys[] KeyRow2W = { Keys.Oem102, Keys.Z, Keys.X, Keys.C, Keys.V, Keys.B, Keys.N, Keys.M, Keys.Oemcomma, Keys.OemPeriod, Keys.Oem2 };

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
		public long Position { get => shortPos * 2; set => shortPos = value / 2; }

		//		public WaveFormat WaveFormat { get; } = new WaveFormat(Global.Bitrate, 16, 1);

		private int GetSoundBytes(int handle, IntPtr buffer, int length, IntPtr user)
		{
			byte[] dataArray = new byte[length];
			int readBytes = Read(dataArray, 0, length);
			Marshal.Copy(dataArray, 0, buffer, readBytes);
			Debug.WriteLine("Get: {0}, took {1}", length, sw.ElapsedMilliseconds);
			return readBytes;
		}

		Stopwatch sw = new Stopwatch();
		public int Read(byte[] buffer, int offset, int count)
		{
			try
			{
				switch (sampleTypeFlag)
				{
					case BASSFlag.BASS_SAMPLE_8BITS: return Read_8bit(buffer, offset, count);
					case BASSFlag.BASS_DEFAULT: return Read_16bit(buffer, offset, count);
					default: return 0;
				}
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

			shortPos += minOf;
			return count;
		}

		public int Read_8bit(byte[] buffer, int offset, int count)
		{
			sw.Restart();

			int minOf = Math.Min(buffer.Length, count);
			if (minOf % 2 != 0)
				throw new ArgumentException();
			minOf /= 2;
			var mflocal = mixfreq.ToArray();


			for (int i = 0; i < minOf; i++)
			{
				double value = 0;
				foreach (var key in mflocal)
				{
					value += key.CalcWave();
				}
				buffer[i] = (byte)(value / Math.Sqrt(mflocal.Length) * volValue * sbyte.MaxValue);
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
			//			SelectDevice((MMDevice)comboBox3.SelectedItem);
		}
	}
}
