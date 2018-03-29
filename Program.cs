﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SynthPiano
{
	static class Program
	{
		/// <summary>
		/// Der Haupteinstiegspunkt für die Anwendung.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			var frm = new Form1();
			frm.AutoPlay();
			Application.Run(frm);
		}
	}
}
