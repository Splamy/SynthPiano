using System;
using System.Drawing;
using System.Windows.Forms;

namespace SynthTest
{
	public partial class Sequencer : UserControl
	{
		readonly TickWorker ticker;

		public Sequencer()
		{
			InitializeComponent();

			ticker = TickPool.RegisterTick(Tick, TimeSpan.FromSeconds(1), false);
		}

		public const int SeqSize = 16;
		public float BPM = 90;
		public int step = 0;

		public Form1 parent;

		public PianoKey[] Channels { get; } = new PianoKey[SeqSize];

		public void SetBpm(float bpm)
		{
			ticker.Interval = TimeSpan.FromMinutes(1 / (bpm * 4));
		}

		public void CreateChannel()
		{
			//Channels.Add(new PianoKey[SeqSize]);
		}

		public void SetP(PianoKey key)
		{
			Channels[step] = key;
			Step();
		}

		public void Step() => step = (step + 1) % SeqSize;

		public void Tick()
		{
			if (Channels[step] != null)
				parent.PlayKey(Channels[step], false);
			Step();
			if (Channels[step] != null)
				parent.PlayKey(Channels[step], true);
		}

		public void Start()
		{
			step = 0;
			ticker.Active = true;
		}

		public void Stop()
		{
			ticker.Active = false;
			parent.PlayKey(Channels[step], false);
		}

		private void Sequencer_Paint(object sender, PaintEventArgs e)
		{
			for (int i = 0; i < SeqSize; i++)
			{
				if (Channels[i] != null)
					e.Graphics.DrawRectangle(Pens.Red, i * 10, (int)(Channels[i].Frequency / 10), 10, 5);
			}
			// e.Graphics
		}
	}
}
