/*
 * Created by SharpDevelop.
 * User: Mama
 * Date: 3/3/2016
 * Time: 11:35 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;

namespace Bridge
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		private Translator trans;
		private Help help;
		private About about;
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			trans = new Translator();
			trans.FormClosing += OnTranslatorClose;
			help = new Help();
			help.FormClosing += OnHelpClose;
			about = new About();
			about.FormClosing += OnAboutClose;
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void OnAboutClose(object sender, FormClosingEventArgs e)
		{
			about.Hide ();
			about.Parent = null;
			e.Cancel = true;
			btnAbout.Enabled = true;
		}
		void OnTranslatorClose(object sender, FormClosingEventArgs e)
		{
			trans.Hide();
			trans.Parent = null;
			e.Cancel = true;
			btnTranslator.Enabled = true;
		}

		void OnHelpClose(object sender, FormClosingEventArgs e)
		{
			help.Hide();
			help.Parent = null;
			e.Cancel = true;
			btnHelp.Enabled = true;
		}
		void BtnTranslatorClick(object sender, EventArgs e)
		{
			btnTranslator.Enabled = false;
			trans.Owner = this;
			trans.Show();
		}
		void BtnHelpClick(object sender, EventArgs e)
		{
			btnHelp.Enabled = false;
			help.Owner = this;
			help.Show();
		}
		void BtnAboutClick(object sender, EventArgs e)
		{
			btnAbout.Enabled = false;
			about.Owner = this;
			about.Show();
		}
	}
}
