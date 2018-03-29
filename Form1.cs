﻿using SynthPiano.AudioBackend;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace SynthPiano
{
	public partial class Form1 : Form
	{
		private List<PianoKey> mixfreq = new List<PianoKey>();
		private double volValue = 0.01;

		private IAudioBackend audioBackend;

		public Form1()
		{
			InitializeComponent();

			audioBackend = new BassBackend();

			audioBackend.Read = Read;
			audioBackend.Init();

			FormClosed += Form1_FormClosed;

			var devList = audioBackend.GetDevices().ToArray();
			comboBox3.DataSource = devList;
			comboBox3.DisplayMember = nameof(DeviceId.Name);
			comboBox3.ValueMember = nameof(DeviceId.Id);

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
			audioBackend.Dispose();
		}

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

		public int Read(byte[] buffer, int offset, int count)
		{
			int read;
			var sw = Stopwatch.StartNew();
			switch (Global.Bits)
			{
			case 8: read = Read_8bit(buffer, offset, count); break;
			case 16: read = Read_16bit(buffer, offset, count); break;
			default: throw new ArgumentOutOfRangeException();
			}
			sw.Stop();
			Debug.WriteLine("Get: {0}, took {1}", count, sw.ElapsedMilliseconds);
			return read;
		}

		public unsafe int Read_16bit(byte[] buffer, int offset, int count)
		{
			int minOf = Math.Min(buffer.Length, count);
			if (minOf % 2 != 0)
				throw new ArgumentException();
			var mflocal = mixfreq.ToArray();

			fixed (byte* bytePointer = buffer)
			{
				short* shortPointer = (short*)bytePointer;

				for (int i = 0; i < minOf / 2; i++)
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
			return minOf;
		}

		public int Read_8bit(byte[] buffer, int offset, int count)
		{
			int minOf = Math.Min(buffer.Length, count);
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
			return minOf;
		}

		private void numericUpDown1_ValueChanged(object sender, EventArgs e) { piano1.Octave = (int)numericUpDown1.Value; PrecalcKeys(); }
		private void numericUpDown2_ValueChanged(object sender, EventArgs e) { piano2.Octave = (int)numericUpDown2.Value; PrecalcKeys(); }

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) { piano1.WaveForm = (WaveForm)comboBox1.SelectedItem; }
		private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) { piano2.WaveForm = (WaveForm)comboBox2.SelectedItem; }

		private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
		{
			audioBackend.SetDevice(((DeviceId)comboBox3.SelectedItem).Id);
		}
	}
}
