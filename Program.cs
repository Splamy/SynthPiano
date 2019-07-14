using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SynthTest
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
			InterceptKeys.SetHook();
			Application.Run(frm);
			InterceptKeys.RemoveHook();
		}
	}
}
