/*
 * Created by SharpDevelop.
 * User: Mama
 * Date: 3/4/2016
 * Time: 6:37 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace Bridge
{
	/// <summary>
	/// Description of Translator.
	/// </summary>
	public partial class 
        Translator : Form
	{

		struct Salita {
			public string salita;
			public string bahagi;
			public string uri;
			public string atbp;
		}

		public Translator()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}

		OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Diksyunaryo.accdb");
		OleDbCommand cmd;
		OleDbDataReader read;

		int index;
		string strTexto;
		bool mali;

		readonly char[] chrBantas = {'.', '!', '?', ',', '-'};
		readonly char[] chrAlfabeto = {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'Ñ', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'ñ', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'};
		readonly char[] chrBilang = {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9'};

		List<Salita> pangungusap = new List<Salita>();
		Stack<string> parser = new Stack<string>();

		/// <summary>
		/// Starts Lexical Analysis
		/// </summary>
		/// <returns>true or false</returns>
		bool Leksiko()
		{
			textBox1.AppendText("Getting Text...\n");
			strTexto = txtEditor.Text;
			conn.Open();
			for (index = 0; index < strTexto.Length; index++) {
				var s = new Salita();
				if (IsBantas(strTexto[index]) && strTexto[index] != '-') {
					s.salita += strTexto[index];
					cmd = new OleDbCommand("SELECT * FROM tblBantas WHERE Bantas = '" + s.salita + "'", conn);
					read = cmd.ExecuteReader();
					s.bahagi = "bantas";
					s.uri = read.Read() ? read["Pangalan"].ToString() : "Error";
					textBox1.AppendText("Bantas: \'" + s.salita + "\' Uri: " + s.uri + "\n");
					pangungusap.Add(s);
				} else if (IsAlfaBil(strTexto[index])) {
					s.salita = getSalita();
					textBox1.AppendText("Salita: \"" + s.salita + "\" ");
					cmd = new OleDbCommand("SELECT * FROM tblDiksyunaryo WHERE Salita = '" + s.salita.ToLower() + "'", conn);
					read = cmd.ExecuteReader();
					if (read.Read()) {
						s.bahagi = read["Bahagi"].ToString();
						s.uri = read["Uri"].ToString();
						s.atbp = read["Others"].ToString();
					} else if (Char.IsUpper(s.salita[0])) {
						s.bahagi = "pangngalan";
						s.uri = "pantangi";
					} else {
						s.bahagi = "Error";
						mali = true;
					}

					textBox1.AppendText("Bahagi: " + s.bahagi + " Uri: " + s.uri + " Other: " + s.atbp + "\n");
					pangungusap.Add(s);
				}
			}
			conn.Close();
			if (!mali) {
				textBox1.AppendText("Accepted.\n***** Process finished. ***************\n");
				ShowPangungusap();
				return true;
			}
			textBox1.AppendText("Rejected.\n***** Process unfinished. ***************\n");
			return false;
		}

		/// <summary>
		/// Gets words from input in txtEditor
		/// excludes spaces and known symbols (except dashes)
		/// </summary>
		/// <returns>string: word</returns>
		string getSalita() {
			string salita = "";
			while (strTexto[index] != ' ' && (IsAlfaBil(strTexto[index]) || strTexto[index] == '-' )) {
				salita += strTexto[index];
				if (index + 1 != strTexto.Length && (IsAlfaBil(strTexto[index+1]) || strTexto[index+1] == '-')) index++;
				else break;
			}
			return salita;
		}

		/// <summary>
		/// Checks if current character is an alphabet or number
		/// </summary>
		/// <param name="ch">character</param>
		/// <returns>true or false</returns>
		bool IsAlfaBil(char ch) {
			foreach(char c in chrAlfabeto) {
				if(ch == c) return true;
			}
			foreach(char c in chrBilang) {
				if(ch == c) return true;
			}
			return false;
		}

		/// <summary>
		/// Checks if current charcter is a pronunciation mark
		/// </summary>
		/// <param name="ch">character</param>
		/// <returns>true or false</returns>
		bool IsBantas(char ch)
		{
			foreach(char c in chrBantas) {
				if(ch == c) return true;
			} return false;
		}

		/// <summary>
		/// Shows current words listed in "pangungusap" List
		/// </summary>
		void ShowPangungusap() {
			foreach(Salita s in pangungusap) {
				textBox1.AppendText(s.salita + " ");
			}
			textBox1.AppendText("\n");
		}

		/// <summary>
		/// Starts Syntax Analysis
		/// </summary>
		/// <returns>true or false</returns>
		bool Sintaks() {
			textBox1.AppendText("\nAnalyzing structure...\n");
			if(Pangungusap()) {
				textBox1.AppendText("Accepted.\n***** Process finished. ***************\n");
				return true;
			} else {
				textBox1.AppendText("Errors found.\n***** Process finished. ***************\n");
				return false;
			}
		}

		/// <summary>
		/// Loops the pangungusap List and checks if sentence input is correct.
		/// Accepts multiple sentences.
		/// </summary>
		/// <returns>true or false</returns>
		bool Pangungusap() {
			int tanggal = 0; index = 0;
			while(index < pangungusap.Count) {
				if(pangungusap[index].bahagi == "bantas" && pangungusap[index].uri != "kuwit") {
					Ilagay(pangungusap[index].bahagi); index++; tanggal++;
					if(mali) return false;
					Alisin(tanggal); Ilagay("<Pangungusap>"); tanggal = 0;
					continue;
				}
				if(Sugnay()) {
					tanggal++;
				} else mali = true;
			}
			return !mali;
		}

		bool Sugnay() {
			int tanggal = 0;
			if(pangungusap[index].bahagi == "panghalip" && pangungusap[index].uri == "pananong") {
				Ilagay(pangungusap[index].uri); index++; tanggal++;
				if(pangungusap[index].bahagi == "pangngalan" || pangungusap[index].bahagi == "panghalip" || pangungusap[index].bahagi == "pantukoy") {
					if(Simuno()) {
						tanggal++;
						if(pangungusap[index].bahagi == "pandiwa") {
							Ilagay(pangungusap[index].bahagi); index++; tanggal++;
							if(pangungusap[index].bahagi != "bantas") {
								if(PanagUri()) tanggal++;
								else mali = true;
							}
						} else mali = true;
					} else mali = true;
				} else mali = true;
			} else if(pangungusap[index].bahagi == "pangngalan" || pangungusap[index].bahagi == "panghalip" || pangungusap[index].bahagi == "pantukoy") {
				Simuno(); tanggal++;
				if(pangungusap[index].bahagi == "pangawing") {
					Ilagay(pangungusap[index].bahagi); index++; tanggal++;
					if(pangungusap[index].bahagi == "pandiwa") {
						Ilagay(pangungusap[index].bahagi); index++; tanggal++;
						if(pangungusap[index].bahagi == "pangatnig") {
							if(!mali) {
								Alisin(tanggal); Ilagay("<Sugnay>"); tanggal = 1;
							}
							Ilagay(pangungusap[index].bahagi); index++; tanggal++;
							if(Sugnay()) {
								Alisin(1); Ilagay("<Sugnay>"); tanggal++;
							} else mali = true;
						} else if(pangungusap[index].bahagi != "bantas") {
							if(PanagUri()) {
								tanggal++;
							} else mali = true;
						}
					} else mali = true;
				} else mali = true;
			} else if(pangungusap[index].bahagi == "pandiwa") {
				Ilagay(pangungusap[index].bahagi); index++; tanggal++;
				if(pangungusap[index].bahagi == "pangngalan" || pangungusap[index].bahagi == "panghalip" || pangungusap[index].bahagi == "pantukoy") {
					if(Simuno()) {
						tanggal++;
						if(PanagUri()) {
							tanggal++;
							if(pangungusap[index].bahagi == "pangatnig") {
								if(!mali) {
									Alisin(tanggal); Ilagay("<Sugnay>"); tanggal = 1;
								}
								Ilagay(pangungusap[index].bahagi); index++; tanggal++;
								if(Sugnay()) {
									Alisin(1); Ilagay("<Sugnay>"); tanggal++;
								} else mali = true;
							}
						} else mali = true;
					} else mali = true;
				} else if(pangungusap[index].bahagi == "pang-ukol" || pangungusap[index].bahagi == "pang-abay") {
					if(PanagUri()) {
						tanggal++;
						if(pangungusap[index].bahagi == "pangngalan" || pangungusap[index].bahagi == "panghalip" || pangungusap[index].bahagi == "pantukoy") {
							if(Simuno()) {
								tanggal++;
								if(pangungusap[index].bahagi == "pangatnig") {
									if(!mali) {
										Alisin(tanggal); Ilagay("<Sugnay>"); tanggal = 1;
									}
									Ilagay(pangungusap[index].bahagi); index++; tanggal++;
									if(Sugnay()) {
										Alisin(1); Ilagay("<Sugnay>"); tanggal++;
									} else mali = true;
								}
							} else mali = true;
						} else mali = true;
					} else mali = true;
				} else mali = true;
			} else if (pangungusap[index].bahagi == "pang-uri") {
				if(PangUri()) {
					tanggal++;
					if(pangungusap[index].bahagi == "pangngalan") {
						Pangngalan(); tanggal++;
						if(pangungusap[index].bahagi != "bantas") {
							if(PanagUri()) {
								tanggal++;
							} else mali = true;
						}
					} else if(pangungusap[index].bahagi == "pangngalan" || pangungusap[index].bahagi == "panghalip" || pangungusap[index].bahagi == "pantukoy") {
						if(Simuno()) {
							tanggal++;
							if(pangungusap[index].bahagi != "bantas") {
								if(PanagUri()) {
									tanggal++;
								} else mali = true;
							}
						} else mali = true;
					}
				} else mali = true;
			}
			if(mali) return false;
			Alisin(tanggal); Ilagay("<Sugnay>");
			return true;
		}

		bool Simuno() {
			int tanggal = 0;
			switch (pangungusap[index].bahagi) {
			case "pangngalan":
				Pangngalan(); tanggal++;
				break;
			case "panghalip":
				Panghalip(); tanggal++;
				break;
			case "pantukoy":
				if (Pantukoy()) {
					tanggal++;
					if (pangungusap[index].bahagi == "pangngalan") {
						Pangngalan(); tanggal++;
					} else mali = true;
				} else mali = true;
				break;
			default:
				mali = true;
				break;
			}
			if(mali) return false;
			Alisin(tanggal); Ilagay("<Simuno>");
			return true;
		}

		bool PanagUri() {
			int tanggal = 0;
			while(pangungusap[index].bahagi == "pang-ukol" || pangungusap[index].bahagi == "pang-abay" || pangungusap[index].bahagi == "panghalip") {
				switch (pangungusap[index].bahagi) {
				case "pang-ukol":
					if (PangUkol()) {
						tanggal++;
						if (pangungusap[index].bahagi == "pangngalan") {
							Pangngalan(); tanggal++;
						} else if (pangungusap[index].bahagi == "panghalip") {
							Panghalip(); tanggal++;
						} else mali = true;
					} else mali = true;
					break;
				case "pang-abay":
					if (PangAbay()) tanggal++;
					else mali = true;
					break;
				case "panghalip":
					Panghalip(); tanggal++;
					break;
				default:
					mali = true;
					break;
				}
			}
			if(mali) return false;
			Alisin(tanggal); Ilagay("<Panaguri>");
			return true;
		}

		void Pangngalan() {
			int tanggal = 0;
			while(pangungusap[index].bahagi == "pangngalan") {
				Ilagay(pangungusap[index].bahagi); tanggal++;
				if(index + 1 >= pangungusap.Count) break;
				index++;
			}
			Alisin(tanggal); Ilagay("<Pangngalan>");
		}

		void Panghalip() {
			Ilagay(pangungusap[index].bahagi); index++;
			Alisin(1); Ilagay("<Panghalip>");
		}

		bool PangUri() {
			int tanggal = 0;
			if(pangungusap[index].bahagi == "pang-uri") {
				Ilagay(pangungusap[index].bahagi); index++; tanggal++;
			} else mali = true;
			if(mali) return false;
			Alisin(tanggal); Ilagay("<Pang-uri>");
			return true;
		}

		bool PangAbay() {
			int tanggal = 0;
			Ilagay(pangungusap[index].bahagi); index++; tanggal++;
			if(pangungusap[index-1].uri == "pamanahon" && pangungusap[index-1].atbp == "mayroon" && pangungusap[index].bahagi == "pangngalan") {
				Ilagay(pangungusap[index].bahagi); index++; tanggal++;
			} else mali = true;
			if(mali) return false;
			Alisin(tanggal); Ilagay("<Pang-abay>");
			return true;
		}

		bool PangUkol() {
			int tanggal = 0;
			Ilagay(pangungusap[index].bahagi); index++; tanggal++;
			if(pangungusap[index-1].atbp == "sa_kay_kina") {
				if(pangungusap[index].salita == "sa" || pangungusap[index].salita == "kay" || pangungusap[index].salita == "kina") {
					Ilagay(pangungusap[index].bahagi); index++; tanggal++;
				} else mali = true;
			}
			if(pangungusap[index-1].atbp == "sa-ng_mga" && pangungusap[index].salita == "mga") {
				Ilagay(pangungusap[index].bahagi); index++; tanggal++;
			}
			if(mali) return false;
			Alisin(tanggal); Ilagay("<PangUkol>");
			return true;
		}

		bool Pantukoy() {
			int tanggal = 0;
			Ilagay(pangungusap[index].bahagi); index++; tanggal++;
			if(pangungusap[index-1].salita == "ang" && pangungusap[index].salita == "mga") {
				Ilagay(pangungusap[index].bahagi); index++; tanggal++;
			} else mali = true;
			if(mali) return false;
			Alisin(tanggal); Ilagay("<Pantukoy>");
			return true;
		}

		/// <summary>
		/// Similar to Push Stack function,
		/// but also does ShowParser()
		/// </summary>
		/// <param name="salita">string: word to be inserted</param>
		void Ilagay(string salita) {
			parser.Push(salita);
			textBox1.AppendText("Push: ");
			ShowParser();
		}

		/// <summary>
		/// Pops parser stack
		/// </summary>
		/// <param name="tanggal">int: how many times to pop stack</param>
		void Alisin(int tanggal) {
			textBox1.AppendText("Pop (" + tanggal + "), ");
			while(tanggal != 0) {
				parser.Pop();
				tanggal--;
			}
		}

		/// <summary>
		/// Shows parser stack
		/// </summary>
		void ShowParser() {
			string[] tmp = parser.ToArray();
			Array.Reverse(tmp);
			foreach(string s in tmp) {
				textBox1.AppendText(s + " ");
			}
			textBox1.AppendText("\n");
		}

		void BtnTranslateClick(object sender, EventArgs e)
		{
			textBox1.Clear();
			pangungusap.Clear();
			parser.Clear();
			if(Leksiko()) {
				if(Sintaks()) {
					foreach(Salita s in pangungusap){
						if(s.salita=="Ako" || s.salita=="ako" || s.salita=="ko"){
							Thread.Sleep(1100);
							Process me = Process.Start(@"Animation\\I.mp4");
							Thread.Sleep(1490);
							me.Kill();
						}
						else if(s.salita=="kumakain"||s.salita=="kain"){
							Thread.Sleep(1100);
							Process kain = Process.Start(@"Animation\\Eat.mp4");
							Thread.Sleep(2500);
							kain.Kill();
						}
						else if(s.salita=="isda"){
							Thread.Sleep(1100);
							Process isda = Process.Start(@"Animation\\Fish.mp4");
							Thread.Sleep(2800);
							isda.Kill();
						}
						else if(s.salita == "Saan"){
							Thread.Sleep(1100);
							Process saan = Process.Start(@"Animation\\Where.mp4");
							Thread.Sleep(1700);
							saan.Kill();
						}
						else if(s.salita == "ka"||s.salita=="ikaw"||s.salita=="inyo"||s.salita=="kayo"){
							Thread.Sleep(1100);
							Process ikaw = Process.Start(@"Animation\\You.mp4");
							Thread.Sleep(1300);
							ikaw.Kill();
						}
						else if(s.salita == "punta"||s.salita=="pupunta"){
							Thread.Sleep(1100);
							Process punta = Process.Start(@"Animation\\Go.mp4");
							Thread.Sleep(1200);
							punta.Kill();
						}
						else if(s.salita=="umaga"){
							Thread.Sleep(1100);
							Process umaga = Process.Start(@"Animation\\GoodMorning.mp4");
							Thread.Sleep(4300);
							umaga.Kill();
						}
						else if(s.salita=="nila"||s.salita=="ito"||s.salita=="siya"||s.salita=="sina"||s.salita=="nina"||s.salita=="nila"){
                            Thread.Sleep(1150);
							Process it = Process.Start(@"Animation\\He_She_It.mp4");
                            Thread.Sleep(0070);
							it.Kill();
						}
						else if(s.salita=="Magbabasa"||s.salita=="basa"){
							Thread.Sleep(1100);
							Process basa = Process.Start(@"Animation\\He_She_It.mp4");
                            Thread.Sleep(1550);
							basa.Kill();
						}
						else if(s.salita=="libro"){
							Thread.Sleep(1100);
							Process book = Process.Start(@"Animation\\Book.mp4");
							Thread.Sleep(1550);
							book.Kill();
						}
						else if(s.salita=="mamayang"||s.salita=="mamaya"){
							Thread.Sleep(1100);
							Process maya = Process.Start(@"Animation\\Later.mp4");
                            Thread.Sleep(2550);
							maya.Kill();
						}
						else if(s.salita=="hapon"){
                            Thread.Sleep(1150);
							Process book = Process.Start(@"Animation\\Afternoon.mp4");
                            Thread.Sleep(2150);
							book.Kill();
						}
						else if(s.salita=="Mahal"||s.salita=="mahal"){
							Thread.Sleep(1100);
							Process love = Process.Start(@"Animation\\Love.mp4");
                            Thread.Sleep(2150);
							love.Kill();
						}
						else if(s.salita=="nagawa"||s.salita=="gawa"){
							Thread.Sleep(1100);
							Process gawa = Process.Start(@"Animation\\Done.mp4");
							Thread.Sleep(1150);
							gawa.Kill();
						}
					}
				}
			}
			textBox1.AppendText("\n***** Program terminated. ***************\n");
		}
		void TxtEditorTextChanged(object sender, EventArgs e)
		{
			btnTranslate.Enabled = txtEditor.Text!="";
		}
	}
}
