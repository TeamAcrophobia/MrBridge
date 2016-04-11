/*
 * Created by SharpDevelop.
 * User: Mama
 * Date: 3/3/2016
 * Time: 11:35 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Bridge
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnTranslator;
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.Button btnAbout;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.btnTranslator = new System.Windows.Forms.Button();
			this.btnHelp = new System.Windows.Forms.Button();
			this.btnAbout = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Aller Display", 30F, System.Drawing.FontStyle.Bold);
			this.label1.ForeColor = System.Drawing.Color.Gold;
			this.label1.Location = new System.Drawing.Point(12, 42);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(513, 167);
			this.label1.TabIndex = 0;
			this.label1.Text = "Bridge: Text to Filipino Sign Language Translator";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btnTranslator
			// 
			this.btnTranslator.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.btnTranslator.Location = new System.Drawing.Point(195, 243);
			this.btnTranslator.Name = "btnTranslator";
			this.btnTranslator.Size = new System.Drawing.Size(149, 47);
			this.btnTranslator.TabIndex = 1;
			this.btnTranslator.Text = "Translator";
			this.btnTranslator.UseVisualStyleBackColor = true;
			this.btnTranslator.Click += new System.EventHandler(this.BtnTranslatorClick);
			// 
			// btnHelp
			// 
			this.btnHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.btnHelp.Location = new System.Drawing.Point(196, 296);
			this.btnHelp.Name = "btnHelp";
			this.btnHelp.Size = new System.Drawing.Size(148, 47);
			this.btnHelp.TabIndex = 2;
			this.btnHelp.Text = "Help";
			this.btnHelp.UseVisualStyleBackColor = true;
			this.btnHelp.Click += new System.EventHandler(this.BtnHelpClick);
			// 
			// btnAbout
			// 
			this.btnAbout.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.btnAbout.Location = new System.Drawing.Point(195, 349);
			this.btnAbout.Name = "btnAbout";
			this.btnAbout.Size = new System.Drawing.Size(148, 47);
			this.btnAbout.TabIndex = 3;
			this.btnAbout.Text = "About";
			this.btnAbout.UseVisualStyleBackColor = true;
			this.btnAbout.Click += new System.EventHandler(this.BtnAboutClick);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Maroon;
			this.ClientSize = new System.Drawing.Size(539, 432);
			this.Controls.Add(this.btnAbout);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.btnTranslator);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.Text = "Bridge";
			this.ResumeLayout(false);

		}
	}
}
