namespace SynthTest
{
	partial class Form1
	{
		/// <summary>
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Verwendete Ressourcen bereinigen.
		/// </summary>
		/// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Vom Windows Form-Designer generierter Code

		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung.
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
			this.textVolume = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.tbarVolume = new System.Windows.Forms.TrackBar();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.label10 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.comboBox3 = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.tbarFade = new System.Windows.Forms.TrackBar();
			this.frequencyVisualizer1 = new SynthTest.FrequencyVisualizer();
			this.piano1 = new SynthTest.Piano();
			this.sequencer1 = new SynthTest.Sequencer();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
			((System.ComponentModel.ISupportInitialize)(this.tbarVolume)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.tbarFade)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
			this.SuspendLayout();
			// 
			// textVolume
			// 
			this.textVolume.Location = new System.Drawing.Point(96, 169);
			this.textVolume.Name = "textVolume";
			this.textVolume.ReadOnly = true;
			this.textVolume.Size = new System.Drawing.Size(43, 20);
			this.textVolume.TabIndex = 1;
			this.textVolume.Text = "0";
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(6, 13);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(73, 15);
			this.label11.TabIndex = 2;
			this.label11.Text = "Focus";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// tbarVolume
			// 
			this.tbarVolume.Location = new System.Drawing.Point(176, 170);
			this.tbarVolume.Maximum = 100;
			this.tbarVolume.Name = "tbarVolume";
			this.tbarVolume.Size = new System.Drawing.Size(236, 45);
			this.tbarVolume.TabIndex = 4;
			this.tbarVolume.TickFrequency = 5;
			this.tbarVolume.TickStyle = System.Windows.Forms.TickStyle.None;
			this.tbarVolume.Value = 10;
			this.tbarVolume.Scroll += new System.EventHandler(this.TbarVolume_Scroll);
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(140, 169);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(41, 20);
			this.label8.TabIndex = 2;
			this.label8.Text = "Low";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(407, 170);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(47, 20);
			this.label9.TabIndex = 2;
			this.label9.Text = "High";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.Location = new System.Drawing.Point(584, 170);
			this.numericUpDown1.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size(137, 20);
			this.numericUpDown1.TabIndex = 7;
			this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numericUpDown1.ValueChanged += new System.EventHandler(this.NumericUpDown1_ValueChanged);
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(12, 169);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(73, 20);
			this.label10.TabIndex = 8;
			this.label10.Text = "Volume";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(505, 170);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(73, 20);
			this.label1.TabIndex = 9;
			this.label1.Text = "Top Octave";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// comboBox1
			// 
			this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new System.Drawing.Point(727, 170);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(121, 21);
			this.comboBox1.TabIndex = 12;
			this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.ComboBox1_SelectedIndexChanged);
			// 
			// comboBox3
			// 
			this.comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox3.FormattingEnabled = true;
			this.comboBox3.Location = new System.Drawing.Point(15, 261);
			this.comboBox3.Name = "comboBox3";
			this.comboBox3.Size = new System.Drawing.Size(236, 21);
			this.comboBox3.TabIndex = 15;
			this.comboBox3.SelectedIndexChanged += new System.EventHandler(this.ComboBox3_SelectedIndexChanged);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(12, 209);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(73, 20);
			this.label3.TabIndex = 20;
			this.label3.Text = "Fade";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(140, 209);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(41, 20);
			this.label4.TabIndex = 17;
			this.label4.Text = "Fast";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(407, 210);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(47, 20);
			this.label5.TabIndex = 18;
			this.label5.Text = "Slow";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// tbarFade
			// 
			this.tbarFade.Location = new System.Drawing.Point(176, 210);
			this.tbarFade.Maximum = 100;
			this.tbarFade.Name = "tbarFade";
			this.tbarFade.Size = new System.Drawing.Size(236, 45);
			this.tbarFade.TabIndex = 19;
			this.tbarFade.TickFrequency = 10;
			this.tbarFade.TickStyle = System.Windows.Forms.TickStyle.None;
			this.tbarFade.Value = 1;
			this.tbarFade.Scroll += new System.EventHandler(this.TbarFade_Scroll);
			// 
			// frequencyVisualizer1
			// 
			this.frequencyVisualizer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.frequencyVisualizer1.Location = new System.Drawing.Point(1164, 169);
			this.frequencyVisualizer1.Name = "frequencyVisualizer1";
			this.frequencyVisualizer1.PianoKey = null;
			this.frequencyVisualizer1.Size = new System.Drawing.Size(224, 81);
			this.frequencyVisualizer1.TabIndex = 14;
			// 
			// piano1
			// 
			this.piano1.Dock = System.Windows.Forms.DockStyle.Top;
			this.piano1.Location = new System.Drawing.Point(0, 0);
			this.piano1.Name = "piano1";
			this.piano1.Octave = 2;
			this.piano1.Size = new System.Drawing.Size(1400, 166);
			this.piano1.TabIndex = 5;
			this.piano1.WaveForm = SynthTest.WaveForm.Sawtooth;
			// 
			// sequencer1
			// 
			this.sequencer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.sequencer1.Location = new System.Drawing.Point(12, 307);
			this.sequencer1.Name = "sequencer1";
			this.sequencer1.Size = new System.Drawing.Size(979, 270);
			this.sequencer1.TabIndex = 21;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(1015, 307);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(116, 28);
			this.button1.TabIndex = 22;
			this.button1.Text = "button1";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.Button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(1015, 341);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(116, 30);
			this.button2.TabIndex = 23;
			this.button2.Text = "button2";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.Button2_Click);
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(1015, 428);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(116, 60);
			this.button3.TabIndex = 24;
			this.button3.Text = "button3";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.Button3_Click);
			// 
			// numericUpDown2
			// 
			this.numericUpDown2.Location = new System.Drawing.Point(1015, 494);
			this.numericUpDown2.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
			this.numericUpDown2.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.numericUpDown2.Name = "numericUpDown2";
			this.numericUpDown2.Size = new System.Drawing.Size(120, 20);
			this.numericUpDown2.TabIndex = 25;
			this.numericUpDown2.Value = new decimal(new int[] {
            90,
            0,
            0,
            0});
			this.numericUpDown2.ValueChanged += new System.EventHandler(this.NumericUpDown2_ValueChanged);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(1400, 589);
			this.Controls.Add(this.numericUpDown2);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.sequencer1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.tbarFade);
			this.Controls.Add(this.comboBox3);
			this.Controls.Add(this.frequencyVisualizer1);
			this.Controls.Add(this.comboBox1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.numericUpDown1);
			this.Controls.Add(this.piano1);
			this.Controls.Add(this.textVolume);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.tbarVolume);
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.Text = "Dot Net Piano";
			this.Activated += new System.EventHandler(this.Form1_Activated);
			this.Deactivate += new System.EventHandler(this.Form1_Deactivate);
			((System.ComponentModel.ISupportInitialize)(this.tbarVolume)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.tbarFade)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.TrackBar tbarVolume;
		private System.Windows.Forms.TextBox textVolume;
		private Piano piano1;
		private System.Windows.Forms.NumericUpDown numericUpDown1;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox comboBox1;
		private FrequencyVisualizer frequencyVisualizer1;
		private System.Windows.Forms.ComboBox comboBox3;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TrackBar tbarFade;
		private Sequencer sequencer1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.NumericUpDown numericUpDown2;
	}
}

