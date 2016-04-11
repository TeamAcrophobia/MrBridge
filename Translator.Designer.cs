/*
 * Created by SharpDevelop.
 * User: Mama
 * Date: 3/4/2016
 * Time: 6:37 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Bridge
{
	partial class Translator
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtEditor;
		private System.Windows.Forms.Button btnTranslate;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label2;
		
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
			this.txtEditor = new System.Windows.Forms.TextBox();
			this.btnTranslate = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Aller Display", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.Gold;
			this.label1.Location = new System.Drawing.Point(21, 50);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(270, 41);
			this.label1.TabIndex = 0;
			this.label1.Text = "Phrase:";
			// 
			// txtEditor
			// 
			this.txtEditor.BackColor = System.Drawing.Color.DarkRed;
			this.txtEditor.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.txtEditor.ForeColor = System.Drawing.Color.Gold;
			this.txtEditor.Location = new System.Drawing.Point(21, 104);
			this.txtEditor.Multiline = true;
			this.txtEditor.Name = "txtEditor";
			this.txtEditor.Size = new System.Drawing.Size(423, 226);
			this.txtEditor.TabIndex = 1;
			this.txtEditor.TextChanged += new System.EventHandler(this.TxtEditorTextChanged);
			// 
			// btnTranslate
			// 
			this.btnTranslate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.btnTranslate.Location = new System.Drawing.Point(21, 362);
			this.btnTranslate.Name = "btnTranslate";
			this.btnTranslate.Size = new System.Drawing.Size(135, 41);
			this.btnTranslate.TabIndex = 2;
			this.btnTranslate.Text = "Translate!";
			this.btnTranslate.UseVisualStyleBackColor = true;
			this.btnTranslate.Click += new System.EventHandler(this.BtnTranslateClick);
			// 
			// textBox1
			// 
			this.textBox1.BackColor = System.Drawing.Color.DarkRed;
			this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.textBox1.ForeColor = System.Drawing.Color.Gold;
			this.textBox1.Location = new System.Drawing.Point(473, 104);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(358, 226);
			this.textBox1.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Aller Display", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.ForeColor = System.Drawing.Color.Gold;
			this.label2.Location = new System.Drawing.Point(473, 50);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(270, 41);
			this.label2.TabIndex = 4;
			this.label2.Text = "Log:";
			// 
			// Translator
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Maroon;
			this.ClientSize = new System.Drawing.Size(860, 432);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.btnTranslate);
			this.Controls.Add(this.txtEditor);
			this.Controls.Add(this.label1);
			this.MaximizeBox = false;
			this.Name = "Translator";
			this.Text = "Translator";
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
