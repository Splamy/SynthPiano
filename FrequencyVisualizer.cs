using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SynthPiano
{
	public partial class FrequencyVisualizer : UserControl
	{
		PianoKey key = null;
		public PianoKey PianoKey { get { return key; } set { key = value?.Clone(); Invalidate(); } }

		public FrequencyVisualizer()
		{
			InitializeComponent();
		}

		private void FrequencyVisualizer_Paint(object sender, PaintEventArgs e)
		{
			if (PianoKey == null) return;
			PianoKey.FreqPos = 0;

			float prev = Height / 2;
			for (int i = 0; i < Width; i++)
			{
				double val = PianoKey.CalcWave();
				var next = (float)val * Height / 3 + 1 + Height / 2;
				e.Graphics.DrawLine(Pens.Black, i - 1, prev, i, next);
				prev = next;
			}
		}
	}
}
