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
			this.buttonExit = new System.Windows.Forms.Button();
			this.label11 = new System.Windows.Forms.Label();
			this.tbarVolume = new System.Windows.Forms.TrackBar();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.label10 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.comboBox2 = new System.Windows.Forms.ComboBox();
			this.frequencyVisualizer1 = new SynthTest.FrequencyVisualizer();
			this.piano2 = new SynthTest.Piano();
			this.piano1 = new SynthTest.Piano();
			this.comboBox3 = new System.Windows.Forms.ComboBox();
			((System.ComponentModel.ISupportInitialize)(this.tbarVolume)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
			this.SuspendLayout();
			// 
			// textVolume
			// 
			this.textVolume.Location = new System.Drawing.Point(96, 343);
			this.textVolume.Name = "textVolume";
			this.textVolume.ReadOnly = true;
			this.textVolume.Size = new System.Drawing.Size(43, 20);
			this.textVolume.TabIndex = 1;
			this.textVolume.Text = "0";
			// 
			// buttonExit
			// 
			this.buttonExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonExit.Location = new System.Drawing.Point(15, 392);
			this.buttonExit.Name = "buttonExit";
			this.buttonExit.Size = new System.Drawing.Size(124, 25);
			this.buttonExit.TabIndex = 0;
			this.buttonExit.Text = "Exit";
			this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
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
			this.tbarVolume.Location = new System.Drawing.Point(176, 335);
			this.tbarVolume.Maximum = 100;
			this.tbarVolume.Name = "tbarVolume";
			this.tbarVolume.Size = new System.Drawing.Size(236, 45);
			this.tbarVolume.TabIndex = 4;
			this.tbarVolume.TickFrequency = 5;
			this.tbarVolume.Value = 20;
			this.tbarVolume.Scroll += new System.EventHandler(this.tbarVolume_Scroll);
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(140, 343);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(41, 20);
			this.label8.TabIndex = 2;
			this.label8.Text = "Low";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(407, 344);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(47, 20);
			this.label9.TabIndex = 2;
			this.label9.Text = "High";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.Location = new System.Drawing.Point(584, 344);
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
			this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(12, 343);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(73, 20);
			this.label10.TabIndex = 8;
			this.label10.Text = "Volume";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(505, 344);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(73, 20);
			this.label1.TabIndex = 9;
			this.label1.Text = "Top Octave";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(505, 373);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(73, 20);
			this.label2.TabIndex = 11;
			this.label2.Text = "Bot Octave";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// numericUpDown2
			// 
			this.numericUpDown2.Location = new System.Drawing.Point(584, 373);
			this.numericUpDown2.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.numericUpDown2.Name = "numericUpDown2";
			this.numericUpDown2.Size = new System.Drawing.Size(137, 20);
			this.numericUpDown2.TabIndex = 10;
			this.numericUpDown2.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numericUpDown2.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);
			// 
			// comboBox1
			// 
			this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new System.Drawing.Point(727, 344);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(121, 21);
			this.comboBox1.TabIndex = 12;
			this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
			// 
			// comboBox2
			// 
			this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox2.FormattingEnabled = true;
			this.comboBox2.Location = new System.Drawing.Point(727, 372);
			this.comboBox2.Name = "comboBox2";
			this.comboBox2.Size = new System.Drawing.Size(121, 21);
			this.comboBox2.TabIndex = 13;
			this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
			// 
			// frequencyVisualizer1
			// 
			this.frequencyVisualizer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.frequencyVisualizer1.Location = new System.Drawing.Point(861, 335);
			this.frequencyVisualizer1.Name = "frequencyVisualizer1";
			this.frequencyVisualizer1.PianoKey = null;
			this.frequencyVisualizer1.Size = new System.Drawing.Size(224, 81);
			this.frequencyVisualizer1.TabIndex = 14;
			// 
			// piano2
			// 
			this.piano2.Dock = System.Windows.Forms.DockStyle.Top;
			this.piano2.Location = new System.Drawing.Point(0, 166);
			this.piano2.Name = "piano2";
			this.piano2.Octave = 3;
			this.piano2.Size = new System.Drawing.Size(1097, 155);
			this.piano2.TabIndex = 6;
			this.piano2.WaveForm = SynthTest.WaveForm.Sawtooth;
			// 
			// piano1
			// 
			this.piano1.Dock = System.Windows.Forms.DockStyle.Top;
			this.piano1.Location = new System.Drawing.Point(0, 0);
			this.piano1.Name = "piano1";
			this.piano1.Octave = 2;
			this.piano1.Size = new System.Drawing.Size(1097, 166);
			this.piano1.TabIndex = 5;
			this.piano1.WaveForm = SynthTest.WaveForm.Sawtooth;
			// 
			// comboBox3
			// 
			this.comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox3.FormattingEnabled = true;
			this.comboBox3.Location = new System.Drawing.Point(176, 395);
			this.comboBox3.Name = "comboBox3";
			this.comboBox3.Size = new System.Drawing.Size(236, 21);
			this.comboBox3.TabIndex = 15;
			this.comboBox3.SelectedIndexChanged += new System.EventHandler(this.comboBox3_SelectedIndexChanged);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.buttonExit;
			this.ClientSize = new System.Drawing.Size(1097, 442);
			this.Controls.Add(this.comboBox3);
			this.Controls.Add(this.frequencyVisualizer1);
			this.Controls.Add(this.comboBox2);
			this.Controls.Add(this.comboBox1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.numericUpDown2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.numericUpDown1);
			this.Controls.Add(this.piano2);
			this.Controls.Add(this.piano1);
			this.Controls.Add(this.textVolume);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.tbarVolume);
			this.Controls.Add(this.buttonExit);
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.Text = "Dot Net Piano";
			((System.ComponentModel.ISupportInitialize)(this.tbarVolume)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonExit;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.TrackBar tbarVolume;
		private System.Windows.Forms.TextBox textVolume;
		private Piano piano1;
		private Piano piano2;
		private System.Windows.Forms.NumericUpDown numericUpDown1;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown numericUpDown2;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.ComboBox comboBox2;
		private FrequencyVisualizer frequencyVisualizer1;
		private System.Windows.Forms.ComboBox comboBox3;
	}
}

