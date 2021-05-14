// Copyright (c) 2020 DuyThanhSoftwares. All right reserved
// Copyright (c) 2010-2012 Published by Bùi Đức Tiến. All right reserved

// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at

//    http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using CSharpNetKey;
using CSharpNetKey.Properties;
using gma.System.Windows;
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace CSharpNetKey
{
	public class Form1 : Form
	{
		private enum InputType
		{
			INPUT_MOUSE,
			INPUT_KEYBOARD,
			INPUT_HARDWARE
		}

		[Flags]
		private enum MOUSEEVENTF
		{
			MOVE = 1,
			LEFTDOWN = 2,
			LEFTUP = 4,
			RIGHTDOWN = 8,
			RIGHTUP = 16,
			MIDDLEDOWN = 32,
			MIDDLEUP = 64,
			XDOWN = 128,
			XUP = 256,
			WHEEL = 2048,
			VIRTUALDESK = 16384,
			ABSOLUTE = 32768
		}

		[Flags]
		private enum KEYEVENTF
		{
			EXTENDEDKEY = 1,
			KEYUP = 2,
			UNICODE = 4,
			SCANCODE = 8
		}

		private struct MOUSEINPUT
		{
			public int dx;

			public int dy;

			public int mouseData;

			public int dwFlags;

			public int time;

			public IntPtr dwExtraInfo;
		}

		private struct KEYBDINPUT
		{
			public short wVk;

			public short wScan;

			public int dwFlags;

			public int time;

			public IntPtr dwExtraInfo;
		}

		private struct HARDWAREINPUT
		{
			public int uMsg;

			public short wParamL;

			public short wParamH;
		}

		[StructLayout(LayoutKind.Explicit)]
		private struct INPUT
		{
			[FieldOffset(0)]
			public int type;

			[FieldOffset(4)]
			public Form1.MOUSEINPUT mi;

			[FieldOffset(4)]
			public Form1.KEYBDINPUT ki;

			[FieldOffset(4)]
			public Form1.HARDWAREINPUT hi;
		}

		[StructLayout(LayoutKind.Explicit)]
		private struct INPUT64
		{
			[FieldOffset(0)]
			public int type;

			[FieldOffset(8)]
			public Form1.MOUSEINPUT mi;

			[FieldOffset(8)]
			public Form1.KEYBDINPUT ki;

			[FieldOffset(8)]
			public Form1.HARDWAREINPUT hi;
		}

		public enum CF
		{
			Text = 1,
			Bitmap,
			MetaFilePict,
			Sylk,
			Dif,
			Tiff,
			OemText,
			Dib,
			Palette,
			Pendata,
			Riff,
			Wave,
			UnicodeText,
			EnhMetaFile,
			HDrop,
			Locale,
			Dibv5,
			OwnerDisplay = 128,
			DspText,
			DspBitmap,
			DspMetaFilePict,
			DspEnhMetaFile = 142,
			PrivateFirst = 512,
			PrivateLast = 767,
			GdiObjFirst,
			GdiObjLast = 1023
		}

		private enum KeyModifiers : uint
		{
			None,
			Alt,
			Control,
			Shift = 4u
		}

		private const string version = "1.5";

		private const int cross = 4;

		private const int cross64 = 8;

		private RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

		private string[] unicodeDungSan = new string[]
		{
			"a",
			"á",
			"à",
			"ã",
			"ả",
			"ạ",
			"ă",
			"ắ",
			"ằ",
			"ẵ",
			"ẳ",
			"ặ",
			"â",
			"ấ",
			"ầ",
			"ẫ",
			"ẩ",
			"ậ",
			"e",
			"é",
			"è",
			"ẽ",
			"ẻ",
			"ẹ",
			"ê",
			"ế",
			"ề",
			"ễ",
			"ể",
			"ệ",
			"i",
			"í",
			"ì",
			"ĩ",
			"ỉ",
			"ị",
			"o",
			"ó",
			"ò",
			"õ",
			"ỏ",
			"ọ",
			"ô",
			"ố",
			"ồ",
			"ỗ",
			"ổ",
			"ộ",
			"ơ",
			"ớ",
			"ờ",
			"ỡ",
			"ở",
			"ợ",
			"u",
			"ú",
			"ù",
			"ũ",
			"ủ",
			"ụ",
			"ư",
			"ứ",
			"ừ",
			"ữ",
			"ử",
			"ự",
			"y",
			"ý",
			"ỳ",
			"ỹ",
			"ỷ",
			"ỵ",
			"đ",
			"A",
			"Á",
			"À",
			"Ã",
			"Ả",
			"Ạ",
			"Ă",
			"Ắ",
			"Ằ",
			"Ẵ",
			"Ẳ",
			"Ặ",
			"Â",
			"Ấ",
			"Ầ",
			"Ẫ",
			"Ẩ",
			"Ậ",
			"E",
			"É",
			"È",
			"Ẽ",
			"Ẻ",
			"Ẹ",
			"Ê",
			"Ế",
			"Ề",
			"Ễ",
			"Ể",
			"Ệ",
			"I",
			"Í",
			"Ì",
			"Ĩ",
			"Ỉ",
			"Ị",
			"O",
			"Ó",
			"Ò",
			"Õ",
			"Ỏ",
			"Ọ",
			"Ô",
			"Ố",
			"Ồ",
			"Ỗ",
			"Ổ",
			"Ộ",
			"Ơ",
			"Ớ",
			"Ờ",
			"Ỡ",
			"Ở",
			"Ợ",
			"U",
			"Ú",
			"Ù",
			"Ũ",
			"Ủ",
			"Ụ",
			"Ư",
			"Ứ",
			"Ừ",
			"Ữ",
			"Ử",
			"Ự",
			"Y",
			"Ý",
			"Ỳ",
			"Ỹ",
			"Ỷ",
			"Ỵ",
			"Đ"
		};

		private string[] duongDan;

		private char[] vni = new char[]
		{
			'0',
			'1',
			'2',
			'4',
			'3',
			'5',
			'6',
			' ',
			' ',
			' ',
			' ',
			' ',
			'7',
			'8',
			'9'
		};

		private char[] telex = new char[]
		{
			'z',
			's',
			'f',
			'x',
			'r',
			'j',
			' ',
			'a',
			'e',
			'o',
			' ',
			'w',
			' ',
			' ',
			'd'
		};

		private char[] telexMoRong = new char[]
		{
			'z',
			's',
			'f',
			'x',
			'r',
			'j',
			' ',
			'a',
			'e',
			'o',
			'w',
			' ',
			' ',
			' ',
			'd'
		};

		private char[] VIQR = new char[]
		{
			'0',
			'\'',
			'`',
			'~',
			'?',
			'.',
			'^',
			' ',
			' ',
			' ',
			' ',
			' ',
			'+',
			'(',
			'd'
		};

		private GoTiengViet a;

		private string s = "";

		private string s1 = "";

		private string s2 = "";

		private string cache = "";

		private string gotat = "";

		private string theoDoi1 = "";

		private string theoDoi2 = "";

		private bool controlV;

		private bool viet = true;

		private bool alt;

		private bool shiftL;

		private bool shiftR;

		private bool another;

		private bool control;

		private bool dragging;

		private bool luuGoTat;

		private bool shiftTruoc;

		private bool CtrlShift;

		private Point pointClicked;

		private int i;

		private int xuat;

		private int ngatTamThoi = -1;

		private Pen pen = new Pen(Color.Black, 6f);

		private WebClient web;

		private IContainer components;

		private NotifyIcon notifyIcon1;

		private ContextMenuStrip contextMenuStrip1;

		private CheckBox chbBoDauKieuMoi;

		private CheckBox chbKhoiDongCungWin;

		private ToolStripMenuItem thoátToolStripMenuItem;

		private CheckBox chbBatHoiThoaiKhiKhoiDong;

		private TabControl tabControl1;

		private TabPage tabPage1;

		private TabPage tabPage2;

		private TabPage tabPage3;

		private Button button1;

		private ToolStripMenuItem tsmThoat;

		private RichTextBox richTextBox1;

		private Label label1;

		private Label label3;

		private Label label2;

		private ListBox lstKieuGo;

		private ListBox lstBangMa;

		private ToolStripMenuItem tsmdotNetKey;

		private ToolStripMenuItem tsmHuongDan;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripComboBox tscBangMa;

		private ToolStripComboBox tscKieuGo;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripMenuItem tsmBoDauKieuMoi;

		private ToolStripMenuItem tsmKhoiDongCungWin;

		private TabPage tabPage5;

		private DataGridView dataGridView1;

		private TextBox txtBang;

		private TextBox txtThayThe;

		private Button btnXoa;

		private Button btnThem;

		private Button btnSua;

		private DataGridViewTextBoxColumn Column1;

		private DataGridViewTextBoxColumn Column2;

		private CheckBox chbKhongCanCach;

		private CheckBox chbGoTatKhiTatTiengViet;

		private CheckBox chbChoPhepGoTat;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripMenuItem tsmBangGoTat;

		private CheckBox chbSuDungSendInput;

		private GroupBox groupBox3;

		private GroupBox groupBox2;

		private GroupBox groupBox1;

		private ToolStripMenuItem tsmSuDungSendInput;

		private ToolStripMenuItem tsmTuyChonKhac;

		private CheckBox chbBoDauTrongAutoComplete;

		private ToolStripMenuItem tsmboDauTrongAutocomplete;

		private Button btnChuyenMa;

		private Button btnHuongDan;

		private ToolStripMenuItem tmsChuyenMa;

		private GroupBox groupBox4;

		private RadioButton rdbAltZ;

		private RadioButton rdbCtrlShift;

		private ComboBox cbbPhimTat;

		private System.Windows.Forms.Timer timer1;

		private ComboBox cbbKTChinhTa;

		private Label label4;

		private ToolStripComboBox tscKTChinhTa;
		private CheckBox chbBatTheoDoi;
		private TabPage tabPage4;
		private TextBox textBox3;
		private TextBox textBox2;
		private TextBox textBox1;
		private Label label5;
		private TabPage tabPage6;
		private Button btnLuu;
		private ComboBox cbbSetting;
		private Button btnThuMucThietLap;
		private Button btnThietLapSan1;
		private TextBox txtKiemTraCapNhat;
		private Button btnKiemTraCapNhat;
		private ToolStripMenuItem khởiĐộngLạiỨngDụngToolStripMenuItem;

		[DllImport("user32.dll")]
		private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

		[DllImport("user32.dll", SetLastError = true)]
		private static extern uint SendInput(uint nInputs, Form1.INPUT[] pInputs, int cbSize);

		[DllImport("user32.dll", SetLastError = true)]
		private static extern uint SendInput(uint nInputs, Form1.INPUT64[] pInputs, int cbSize);

		[DllImport("User32.dll")]
		public static extern bool OpenClipboard(IntPtr hWnd);

		[DllImport("User32.dll")]
		public static extern IntPtr SetClipboardData(Form1.CF Format, IntPtr hMem);

		[DllImport("User32.dll")]
		public static extern bool EmptyClipboard();

		[DllImport("User32.dll")]
		public static extern bool CloseClipboard();

		private void sendUnicode(string s)
		{
			if (IntPtr.Size == 4)
			{
				Form1.INPUT[] input = new Form1.INPUT[s.Length * 2];
				for (int i = 0; i < s.Length; i++)
				{
					input[i * 2].type = 1;
					input[i * 2].ki.wVk = 0;
					input[i * 2].ki.wScan = (short)s[i];
					input[i * 2].ki.dwFlags = 4;
					input[i * 2 + 1] = input[i * 2];
					input[i * 2 + 1].ki.dwFlags = 6;
				}
				Form1.SendInput((uint)input.Length, input, Marshal.SizeOf(input[0]));
				return;
			}
			Form1.INPUT64[] input2 = new Form1.INPUT64[s.Length * 2];
			for (int j = 0; j < s.Length; j++)
			{
				input2[j * 2].type = 1;
				input2[j * 2].ki.wVk = 0;
				input2[j * 2].ki.wScan = (short)s[j];
				input2[j * 2].ki.dwFlags = 4;
				input2[j * 2 + 1] = input2[j * 2];
				input2[j * 2 + 1].ki.dwFlags = 6;
			}
			Form1.SendInput((uint)input2.Length, input2, Marshal.SizeOf(input2[0]));
		}

		private void sendAnsi(Keys k)
		{
			if (IntPtr.Size == 4)
			{
				Form1.INPUT input_down = default(Form1.INPUT);
				input_down.type = 1;
				input_down.ki.wVk = (short)k;
				Form1.INPUT input_up = input_down;
				input_up.ki.dwFlags = 2;
				Form1.INPUT[] input = new Form1.INPUT[]
				{
					input_down,
					input_up
				};
				Form1.SendInput(2u, input, Marshal.SizeOf(input_down));
				return;
			}
			Form1.INPUT64 input_down2 = default(Form1.INPUT64);
			input_down2.type = 1;
			input_down2.ki.wVk = (short)k;
			Form1.INPUT64 input_up2 = input_down2;
			input_up2.ki.dwFlags = 2;
			Form1.INPUT64[] input2 = new Form1.INPUT64[]
			{
				input_down2,
				input_up2
			};
			Form1.SendInput(2u, input2, Marshal.SizeOf(input_down2));
		}

		private void SetClipboardAPI(string s)
		{
			Form1.OpenClipboard(IntPtr.Zero);
			Form1.EmptyClipboard();
			Form1.SetClipboardData(Form1.CF.UnicodeText, Marshal.StringToCoTaskMemUni(s));
			Form1.CloseClipboard();
		}

		public Form1()
		{
			this.InitializeComponent();
			UserActivityHook actHook = new UserActivityHook(false, true);
			actHook.KeyDown += new KeyEventHandler(this.MyKeyDown);
			actHook.KeyPress += new KeyPressEventHandler(this.MyKeyPress);
			actHook.KeyUp += new KeyEventHandler(this.MyKeyUp);
			Control.CheckForIllegalCrossThreadCalls = false;
			this.a = new GoTiengViet(this.unicodeDungSan, this.vni);
			this.lstBangMa.SelectedIndex = 0;
			this.lstKieuGo.SelectedIndex = 0;
			this.notifyIcon1.Icon = Resource1.V;
			this.cbbSetting.SelectedIndex = 0;
			if (Directory.Exists(Application.StartupPath + "\\bangMa"))
			{
				this.duongDan = Directory.GetFiles(Application.StartupPath + "\\bangMa\\", "*.txt");
				Array.Sort<string>(this.duongDan);
				this.i = 0;
				while (this.i < this.duongDan.Length)
				{
					this.lstBangMa.Items.Add(Path.GetFileNameWithoutExtension(this.duongDan[this.i]));
					this.tscBangMa.Items.Add(Path.GetFileNameWithoutExtension(this.duongDan[this.i]));
					this.i++;
				}
			}
			this.cbbPhimTat.SelectedIndex = this.cbbPhimTat.Items.Count - 1;
			try
			{
				if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\CSharpKey"))
				{
					Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\CSharpKey");
				}
				this.s1 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\CSharpKey\\CSharpKey.xml";
				XmlDocument xml = new XmlDocument();
				xml.Load(this.s1);
				if (xml.DocumentElement["bat"].InnerText == "0")
				{
					this.viet = false;
					this.notifyIcon1.Icon = Resource1.E;
				}
				this.lstKieuGo.SelectedIndex = int.Parse(xml.DocumentElement["kieuGo"].InnerText);
				this.lstBangMa.SelectedIndex = int.Parse(xml.DocumentElement["bangMa"].InnerText);
				if (xml.DocumentElement["boDauKieuMoi"].InnerText != "0")
				{
					this.chbBoDauKieuMoi.Checked = true;
				}
				else
				{
					this.chbBoDauKieuMoi.Checked = false;
				}
				if (xml.DocumentElement["batHoiThoaiKhiKhoiDong"].InnerText != "0")
				{
					this.chbBatHoiThoaiKhiKhoiDong.Checked = true;
				}
				else
				{
					this.chbBatHoiThoaiKhiKhoiDong.Checked = false;
				}
				if (xml.DocumentElement["kiemTraChinhTa"].InnerText == "0")
				{
					this.cbbKTChinhTa.SelectedIndex = 0;
				}
				else if (xml.DocumentElement["kiemTraChinhTa"].InnerText == "1")
				{
					this.cbbKTChinhTa.SelectedIndex = 1;
				}
				else
				{
					this.cbbKTChinhTa.SelectedIndex = 2;
				}
				if (xml.DocumentElement["choPhepGoTat"].InnerText == "0")
				{
					this.chbChoPhepGoTat.Checked = false;
				}
				else
				{
					this.chbChoPhepGoTat.Checked = true;
				}
				if (xml.DocumentElement["goTatKhiTatTiengViet"].InnerText != "0")
				{
					this.chbGoTatKhiTatTiengViet.Checked = true;
				}
				else
				{
					this.chbGoTatKhiTatTiengViet.Checked = false;
				}
				if (xml.DocumentElement["khongCanCach"].InnerText != "0")
				{
					this.chbKhongCanCach.Checked = true;
				}
				else
				{
					this.chbKhongCanCach.Checked = false;
				}
				if (xml.DocumentElement["sendInput"].InnerText != "0")
				{
					this.chbSuDungSendInput.Checked = true;
				}
				else
				{
					this.chbSuDungSendInput.Checked = false;
				}
				if (xml.DocumentElement["boDauTrongAutocomplete"].InnerText != "0")
				{
					this.chbBoDauTrongAutoComplete.Checked = true;
				}
				else
				{
					this.chbBoDauTrongAutoComplete.Checked = false;
				}
				if (xml.DocumentElement["tatTheoDoi"].InnerText != "0")
				{
					this.chbBatTheoDoi.Checked = true;
				}
				else
				{
					this.chbBatTheoDoi.Checked = false;
				}
				if (xml.DocumentElement["phimChuyen"].InnerText != "0")
				{
					this.rdbAltZ.Checked = true;
					int tam;
					if (int.TryParse(xml.DocumentElement["phimChuyen"].InnerText, out tam) && tam > 0 && tam <= this.cbbPhimTat.Items.Count)
					{
						this.cbbPhimTat.SelectedIndex = tam - 1;
					}
				}
				else
				{
					this.rdbCtrlShift.Checked = true;
				}
			}
			catch
			{
				this.chbKhoiDongCungWin.Checked = true;
				this.cbbKTChinhTa.SelectedIndex = 1;
			}
			if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\CSharpKey\\GoTat.dat"))
			{
				File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\CSharpKey\\GoTat.dat", "chxh\tCộng hòa xã hội chủ nghĩa Việt Nam\r\ndltd\tĐộc lập - Tự do - Hạnh phúc");
			}
			StreamReader str = new StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\CSharpKey\\GoTat.dat");
			while ((this.s = str.ReadLine()) != null)
			{
				this.dataGridView1.Rows.Add(this.s.Split(new char[]
				{
					'\t'
				}));
			}
			str.Close();
		}

		private void MyKeyPress(object sender, KeyPressEventArgs e)
		{
			try
			{
				if (this.goTat(e.KeyChar))
				{
					e.Handled = true;
				}
				else if (this.controlV || !this.viet)
				{
					this.controlV = false;
				}
				else
				{
					if (!this.textBox3.Focused)
					{
						if (this.theoDoi1.Length >= 100)
						{
							this.theoDoi1 = this.theoDoi1.Substring(50, this.theoDoi1.Length - 50);
						}
						if (e.KeyChar == '\b')
						{
							this.theoDoi1 += "\\b";
						}
						else if (e.KeyChar == '\t')
						{
							this.theoDoi1 += "\\t";
						}
						else if (e.KeyChar == '\r')
						{
							this.theoDoi1 += "\\n";
						}
						else if (e.KeyChar == '\\')
						{
							this.theoDoi1 += "\\\\";
						}
						else
						{
							this.theoDoi1 += e.KeyChar;
						}
						if (this.tabControl1.SelectedIndex == 4 && base.Visible && this.chbBatTheoDoi.Checked)
						{
							this.textBox3.Text = this.theoDoi1;
						}
					}
					if (!this.control && (this.shiftL || this.shiftR) && !this.alt && e.KeyChar == ' ' && !this.shiftTruoc)
					{
						if (this.s2 != null && this.s2 != "" && this.s2 != this.s1)
						{
							e.Handled = true;
							this.XuatTiengViet(this.s2, this.s1, true);
							this.s1 = (this.cache = (this.s2 = (this.gotat = "")));
							this.ngatTamThoi = 0;
						}
					}
					else
					{
						if (this.shiftL || this.shiftR)
						{
							this.shiftTruoc = true;
						}
						else
						{
							this.shiftTruoc = false;
						}
						if (this.a.KiemTraKetThucTu(e.KeyChar))
						{
							if (e.KeyChar == '\r')
							{
								if (this.cbbKTChinhTa.SelectedIndex != 0 && !this.a.KiemTraChinhTa() && this.s1 != this.s2 && (this.a.chu.trungdau < 0 || this.a.KiemTraDauMuD()))
								{
									this.XuatTiengViet(this.s2, this.s1, false);
								}
								this.cache = (this.s1 = (this.s2 = (this.gotat = "")));
								this.ngatTamThoi = -1;
							}
							else if (e.KeyChar == '\b')
							{
								if (!this.control && !this.alt && (this.shiftL || this.shiftR))
								{
									this.s = (this.s1 = (this.s2 = (this.cache = (this.gotat = ""))));
								}
								else if (this.s2 == this.s1 && this.s2 != "")
								{
									this.s2 = this.s2.Remove(this.s2.Length - 1);
									this.s = this.a.Convert(this.s2);
									e.Handled = true;
									this.XuatTiengViet(this.s, this.s1, true);
									this.s1 = this.s;
								}
								else if (this.s2 == "")
								{
									this.i = this.cache.Length - 2;
									while (this.i >= 0 && !this.a.KiemTraKetThucTu(this.cache[this.i]))
									{
										this.i--;
									}
									if (this.i < 0)
									{
										if (this.cache != "")
										{
											this.s2 = this.cache.Remove(this.cache.Length - 1);
											this.cache = "";
											this.gotat = this.s2;
										}
										else
										{
											this.cache = (this.s2 = "");
										}
									}
									else
									{
										this.s2 = this.cache.Substring(this.i + 1, this.cache.Length - this.i - 2);
										this.cache = this.cache.Remove(this.i + 1);
										this.gotat = this.s2;
									}
									this.s1 = this.a.Convert(this.s2);
									if (this.cbbKTChinhTa.SelectedIndex != 0 && !this.a.KiemTraChinhTa() && (this.a.chu.trungdau < 0 || this.a.KiemTraDauMuD()))
									{
										e.Handled = true;
										this.XuatTiengViet(this.s1, this.s2 + ' ', true);
									}
								}
								else if (this.a.chu.amCuoi != "")
								{
									int value = this.s2.LastIndexOf(this.a.chu.amCuoi[this.a.chu.amCuoi.Length - 1]);
									if (value >= 0)
									{
										this.s2 = this.s2.Remove(value, 1);
									}
									this.s = this.a.Convert(this.s2);
									e.Handled = true;
									this.XuatTiengViet(this.s, this.s1, true);
									this.s1 = this.s;
								}
								else if (this.a.chu.nguyenAm != "")
								{
									this.a.chu.nguyenAm = this.a.chu.nguyenAm.Remove(this.a.chu.nguyenAm.Length - 1);
									if (this.a.chu.nguyenAm == "" && (this.a.chu.amDau.ToLower() == "gi" || this.a.chu.amDau.ToLower() == "qu"))
									{
										this.a.chu.nguyenAm = this.a.chu.amDau[1].ToString();
										this.a.chu.amDau = this.a.chu.amDau[0].ToString();
									}
									this.s2 = this.a.ConvertNguoc();
									this.s = this.a.Convert(this.s2);
									e.Handled = true;
									this.XuatTiengViet(this.s, this.s1, true);
									this.s1 = this.s;
								}
								else if (this.a.chu.amDau != "")
								{
									this.a.chu.amDau = this.a.chu.amDau.Remove(this.a.chu.amDau.Length - 1);
									this.s2 = this.a.ConvertNguoc();
									this.s = this.a.Convert(this.s2);
									e.Handled = true;
									this.XuatTiengViet(this.s, this.s1, true);
									this.s1 = this.s;
								}
								else
								{
									this.a.chu.amDau = (this.s1 = (this.s2 = ""));
								}
								if (this.ngatTamThoi >= 0)
								{
									this.ngatTamThoi--;
								}
							}
							else
							{
								this.cache = this.cache + this.s2 + e.KeyChar;
								if (this.cache.Length > 100)
								{
									this.cache = this.cache.Substring(50);
								}
								if (this.cbbKTChinhTa.SelectedIndex != 0 && !this.a.KiemTraChinhTa() && this.s1 != this.s2 && (this.a.chu.trungdau < 0 || this.a.KiemTraDauMuD()))
								{
									this.XuatTiengViet(this.s2, this.s1, true);
								}
								this.s = (this.s1 = (this.s2 = ""));
								this.ngatTamThoi = -1;
							}
						}
						else if (this.ngatTamThoi >= 0)
						{
							this.ngatTamThoi++;
						}
						else if (this.a.kieuGo[14].ToString().ToLower() == e.KeyChar.ToString().ToLower() && this.s1.Length >= 2 && !this.a.KiemTraKetThucTu(this.s1[this.s1.Length - 2]) && !this.a.KiemTraNguyenAm(this.s1[this.s1.Length - 2]) && "dD".IndexOf(this.s1[this.s1.Length - 1]) >= 0)
						{
							if (this.s1[this.s1.Length - 1] == 'd')
							{
								this.s = this.a.bangMa[72];
							}
							else if (this.s1[this.s1.Length - 1] == 'D')
							{
								this.s = this.a.bangMa[145];
							}
							e.Handled = true;
							this.XuatTiengViet(this.s, "d", true);
							this.a.chu.D9 = true;
							this.a.chu.amDau = this.s1[this.s1.Length - 1].ToString();
							this.s2 = (this.cache = (this.gotat = this.s1[this.s1.Length - 1].ToString() + e.KeyChar));
							this.s1 = this.s;
							this.a.Convert(this.s2);
						}
						else if (this.a.kieuGo[14].ToString().ToLower() == e.KeyChar.ToString().ToLower() && this.s2.Length >= 2 && this.s2.Substring(this.s2.Length - 2).ToLower() == "dd")
						{
							e.Handled = true;
							this.XuatTiengViet(this.s2[this.s2.Length - 2] + e.KeyChar.ToString(), " ", true);
							this.s2 = (this.s1 = (this.cache = (this.gotat = e.KeyChar.ToString())));
							this.a.Convert(this.s2);
						}
						else
						{
							this.s2 += e.KeyChar;
							this.s = this.a.Convert(this.s2);
							if (this.cbbKTChinhTa.SelectedIndex == 0 && this.a.KiemTraNguyenAm(e.KeyChar) && (this.a.chu.amCuoi != "" || this.a.chu.trungdau >= 0) && this.s == this.s1 + e.KeyChar)
							{
								this.s = (this.s1 = (this.s2 = e.KeyChar.ToString()));
								this.cache = "";
								this.a.Convert(this.s);
							}
							else if (this.s != null && this.s != "" && this.s != this.s1 + e.KeyChar)
							{
								e.Handled = true;
								this.XuatTiengViet(this.s, this.s1, true);
								this.s1 = this.s;
							}
							else
							{
								this.s1 += e.KeyChar;
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.s = string.Concat(new string[]
				{
					"Xuất hiện lỗi trong quá trình xử lý CSharpKey 1.5 !!!\r\n",
					ex.Source,
					"\r\n",
					ex.Message,
					"\r\n",
					ex.StackTrace,
					"\r\n",
					ex.TargetSite.ToString(),
					"\r\n",
					this.theoDoi1,
					"\r\n",
					this.theoDoi2
				});
				File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\CSharpKey\\Error.txt", this.s);
				Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\CSharpKey\\Error.txt");
				Application.Exit();
			}
		}

		private void MyKeyDown(object sender, KeyEventArgs e)
		{
			string s = e.KeyData.ToString();
			if (s.Contains("ControlKey") && !this.controlV)
			{
				this.control = true;
			}
			else if (s.Contains("LShiftKey"))
			{
				this.shiftL = true;
			}
			else if (s.Contains("RShiftKey"))
			{
				this.shiftR = true;
			}
			else if (s.Contains("Menu"))
			{
				this.alt = true;
			}
			else
			{
				this.another = true;
			}
			if (s == "Home" || s == "End" || s == "Delete" || s == "PageUp" || s == "Next" || s == "Up" || s == "Down" || s == "Left" || s == "Right")
			{
				this.s1 = (this.s2 = (this.cache = (this.gotat = "")));
			}
			else if (s.Contains("NumPad"))
			{
				this.controlV = true;
			}
			if (this.control && !this.shiftL && !this.shiftR && !this.alt)
			{
				this.s1 = (this.s2 = (this.cache = (this.gotat = "")));
			}
		}

		private void MyKeyUp(object sender, KeyEventArgs e)
		{
			this.Inbox();
			if (this.control && this.alt && !this.shiftL && !this.shiftR)
			{
				if (e.KeyCode == Keys.F1)
				{
					this.cbbSetting.SelectedIndex = 0;
					this.s1 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\CSharpKey\\CSharpKey1.xml";
					if (File.Exists(this.s1))
					{
						this.notifyIcon1.BalloonTipText = "Chuyển chế độ Setting 1";
					}
					else
					{
						this.notifyIcon1.BalloonTipText = "Chưa lưu Setting 1";
					}
					this.notifyIcon1.ShowBalloonTip(1);
				}
				else if (e.KeyCode == Keys.F2)
				{
					this.cbbSetting.SelectedIndex = 1;
					this.s1 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\CSharpKey\\CSharpKey2.xml";
					if (File.Exists(this.s1))
					{
						this.notifyIcon1.BalloonTipText = "Chuyển chế độ Setting 2";
					}
					else
					{
						this.notifyIcon1.BalloonTipText = "Chưa lưu Setting 2";
					}
					this.notifyIcon1.ShowBalloonTip(1);
				}
				else if (e.KeyCode == Keys.F3)
				{
					this.cbbSetting.SelectedIndex = 2;
					this.s1 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\CSharpKey\\CSharpKey3.xml";
					if (File.Exists(this.s1))
					{
						this.notifyIcon1.BalloonTipText = "Chuyển chế độ Setting 3";
					}
					else
					{
						this.notifyIcon1.BalloonTipText = "Chưa lưu Setting 3";
					}
					this.notifyIcon1.ShowBalloonTip(1);
				}
				else if (e.KeyCode == Keys.F4)
				{
					this.cbbSetting.SelectedIndex = 3;
					this.s1 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\CSharpKey\\CSharpKey4.xml";
					if (File.Exists(this.s1))
					{
						this.notifyIcon1.BalloonTipText = "Chuyển chế độ Setting 4";
					}
					else
					{
						this.notifyIcon1.BalloonTipText = "Chưa lưu Setting 4";
					}
					this.notifyIcon1.ShowBalloonTip(1);
				}
				else if (e.KeyCode == Keys.F5)
				{
					this.cbbSetting.SelectedIndex = 4;
					this.s1 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\CSharpKey\\CSharpKey5.xml";
					if (File.Exists(this.s1))
					{
						this.notifyIcon1.BalloonTipText = "Chuyển chế độ Setting 5";
					}
					else
					{
						this.notifyIcon1.BalloonTipText = "Chưa lưu Setting 5";
					}
					this.notifyIcon1.ShowBalloonTip(1);
				}
				try
				{
					XmlDocument xml = new XmlDocument();
					xml.Load(this.s1);
					if (xml.DocumentElement["bat"].InnerText == "0")
					{
						this.viet = false;
						this.notifyIcon1.Icon = Resource1.E;
					}
					else
					{
						this.viet = true;
						this.notifyIcon1.Icon = Resource1.V;
					}
					this.lstKieuGo.SelectedIndex = int.Parse(xml.DocumentElement["kieuGo"].InnerText);
					this.lstBangMa.SelectedIndex = int.Parse(xml.DocumentElement["bangMa"].InnerText);
					if (xml.DocumentElement["boDauKieuMoi"].InnerText != "0")
					{
						this.chbBoDauKieuMoi.Checked = true;
					}
					else
					{
						this.chbBoDauKieuMoi.Checked = false;
					}
					if (xml.DocumentElement["batHoiThoaiKhiKhoiDong"].InnerText != "0")
					{
						this.chbBatHoiThoaiKhiKhoiDong.Checked = true;
					}
					else
					{
						this.chbBatHoiThoaiKhiKhoiDong.Checked = false;
					}
					if (xml.DocumentElement["kiemTraChinhTa"].InnerText == "0")
					{
						this.cbbKTChinhTa.SelectedIndex = 0;
					}
					else if (xml.DocumentElement["kiemTraChinhTa"].InnerText == "1")
					{
						this.cbbKTChinhTa.SelectedIndex = 1;
					}
					else
					{
						this.cbbKTChinhTa.SelectedIndex = 2;
					}
					if (xml.DocumentElement["choPhepGoTat"].InnerText == "0")
					{
						this.chbChoPhepGoTat.Checked = false;
					}
					else
					{
						this.chbChoPhepGoTat.Checked = true;
					}
					if (xml.DocumentElement["goTatKhiTatTiengViet"].InnerText != "0")
					{
						this.chbGoTatKhiTatTiengViet.Checked = true;
					}
					else
					{
						this.chbGoTatKhiTatTiengViet.Checked = false;
					}
					if (xml.DocumentElement["khongCanCach"].InnerText != "0")
					{
						this.chbKhongCanCach.Checked = true;
					}
					else
					{
						this.chbKhongCanCach.Checked = false;
					}
					if (xml.DocumentElement["sendInput"].InnerText != "0")
					{
						this.chbSuDungSendInput.Checked = true;
					}
					else
					{
						this.chbSuDungSendInput.Checked = false;
					}
					if (xml.DocumentElement["boDauTrongAutocomplete"].InnerText != "0")
					{
						this.chbBoDauTrongAutoComplete.Checked = true;
					}
					else
					{
						this.chbBoDauTrongAutoComplete.Checked = false;
					}
					if (xml.DocumentElement["tatTheoDoi"].InnerText != "0")
					{
						this.chbBatTheoDoi.Checked = true;
					}
					else
					{
						this.chbBatTheoDoi.Checked = false;
					}
					if (xml.DocumentElement["phimChuyen"].InnerText != "0")
					{
						this.rdbAltZ.Checked = true;
					}
					else
					{
						this.rdbCtrlShift.Checked = true;
					}
					if (xml.DocumentElement["phimChuyen"].InnerText != "0")
					{
						this.rdbAltZ.Checked = true;
						int tam;
						if (int.TryParse(xml.DocumentElement["phimChuyen"].InnerText, out tam) && tam > 0 && tam <= this.cbbPhimTat.Items.Count)
						{
							this.cbbPhimTat.SelectedIndex = tam - 1;
						}
					}
					else
					{
						this.rdbCtrlShift.Checked = true;
					}
					this.LuuFile("CSharpKey");
				}
				catch
				{
					this.chbKhoiDongCungWin.Checked = true;
				}
				this.s = (this.s1 = (this.s2 = (this.cache = (this.gotat = ""))));
			}
			if (this.rdbAltZ.Checked && this.cbbPhimTat.SelectedIndex != -1 && e.KeyCode == (Keys)this.cbbPhimTat.Items[this.cbbPhimTat.SelectedIndex].ToString()[0] && !this.control && !this.shiftL && !this.shiftR && this.alt)
			{
				if (this.viet)
				{
					this.notifyIcon1.Icon = Resource1.E;
					this.s1 = (this.s2 = (this.cache = ""));
				}
				else
				{
					this.notifyIcon1.Icon = Resource1.V;
				}
				this.viet = !this.viet;
			}
			if (e.KeyData.ToString().Contains("ControlKey"))
			{
				this.control = false;
				if ((this.shiftL || this.shiftR) && !this.another)
				{
					this.CtrlShift = true;
				}
			}
			else if (e.KeyData.ToString().Contains("LShiftKey"))
			{
				this.shiftL = false;
				if (this.control && !this.another)
				{
					this.CtrlShift = true;
				}
			}
			else if (e.KeyData.ToString().Contains("RShiftKey"))
			{
				this.shiftR = false;
				if (this.control && !this.another)
				{
					this.CtrlShift = true;
				}
			}
			else if (e.KeyData.ToString().Contains("Menu"))
			{
				this.alt = false;
			}
			if (this.rdbCtrlShift.Checked && (e.KeyData.ToString().Contains("ControlKey") || e.KeyData.ToString().Contains("ShiftKey")) && !this.control && !this.shiftL && !this.shiftR && this.CtrlShift && !this.alt && !this.another)
			{
				if (this.viet)
				{
					this.notifyIcon1.Icon = Resource1.E;
					this.s1 = (this.s2 = (this.cache = ""));
				}
				else
				{
					this.notifyIcon1.Icon = Resource1.V;
				}
				this.viet = !this.viet;
				this.control = (this.shiftL = (this.shiftR = (this.alt = (this.another = (this.CtrlShift = false)))));
			}
			if (!this.control && !this.shiftL && !this.shiftR)
			{
				this.another = false;
			}
		}

		private void MouseMoved(object sender, EventArgs e)
		{
			if (Control.MouseButtons != MouseButtons.None)
			{
				this.s1 = (this.s2 = (this.cache = (this.gotat = "")));
				this.ngatTamThoi = -1;
				this.Inbox();
			}
		}

		private void XuatTiengViet(string s, string s1, bool autocomplete)
		{
			if (s != s1)
			{
				this.xuat = 0;
				this.i = 0;
				while (this.i < s.Length && this.i < s1.Length && s[this.i] == s1[this.i])
				{
					this.xuat++;
					this.i++;
				}
				if (s.Length == s1.Length - 1 && this.gotat != null && this.gotat.Length > 0)
				{
					this.gotat = this.gotat.Remove(this.gotat.Length - 1);
				}
				if (!this.chbSuDungSendInput.Checked)
				{
					if (autocomplete && this.chbBoDauTrongAutoComplete.Checked)
					{
						this.controlV = true;
						Form1.keybd_event(192, 0, 0u, UIntPtr.Zero);
						Form1.keybd_event(192, 0, 2u, UIntPtr.Zero);
						this.i = 0;
						while (this.i < s1.Length - this.xuat + 1)
						{
							this.controlV = true;
							Form1.keybd_event(8, 0, 0u, UIntPtr.Zero);
							Form1.keybd_event(8, 0, 2u, UIntPtr.Zero);
							this.i++;
						}
					}
					else
					{
						this.i = 0;
						while (this.i < s1.Length - this.xuat)
						{
							this.controlV = true;
							Form1.keybd_event(8, 0, 0u, UIntPtr.Zero);
							Form1.keybd_event(8, 0, 2u, UIntPtr.Zero);
							this.i++;
						}
					}
					if (this.xuat != s.Length)
					{
						this.gotat = null;
						this.SetClipboardAPI(s.Substring(this.xuat, s.Length - this.xuat));
						if (this.shiftR)
						{
							Form1.keybd_event(16, 0, 0u, UIntPtr.Zero);
							Form1.keybd_event(45, 0, 1u, UIntPtr.Zero);
							Form1.keybd_event(45, 0, 2u, UIntPtr.Zero);
							Form1.keybd_event(16, 0, 2u, UIntPtr.Zero);
							return;
						}
						if (this.shiftL)
						{
							this.controlV = true;
							Form1.keybd_event(160, 0, 2u, UIntPtr.Zero);
							Form1.keybd_event(17, 0, 0u, UIntPtr.Zero);
							Form1.keybd_event(86, 0, 0u, UIntPtr.Zero);
							Form1.keybd_event(86, 0, 2u, UIntPtr.Zero);
							Form1.keybd_event(17, 0, 2u, UIntPtr.Zero);
							Form1.keybd_event(160, 0, 0u, UIntPtr.Zero);
							return;
						}
						this.controlV = true;
						Form1.keybd_event(17, 0, 0u, UIntPtr.Zero);
						Form1.keybd_event(86, 0, 0u, UIntPtr.Zero);
						Form1.keybd_event(86, 0, 2u, UIntPtr.Zero);
						Form1.keybd_event(17, 0, 2u, UIntPtr.Zero);
						return;
					}
				}
				else
				{
					if (autocomplete && this.chbBoDauTrongAutoComplete.Checked)
					{
						this.sendUnicode("`");
						this.i = 0;
						while (this.i < s1.Length - this.xuat + 1)
						{
							this.controlV = true;
							this.sendAnsi(Keys.Back);
							this.i++;
						}
					}
					else
					{
						this.i = 0;
						while (this.i < s1.Length - this.xuat)
						{
							this.controlV = true;
							this.sendAnsi(Keys.Back);
							this.i++;
						}
					}
					if (this.xuat != s.Length)
					{
						this.gotat = null;
						this.sendUnicode(s.Substring(this.xuat));
					}
				}
			}
		}

		private void chbBoDauKieuMoi_CheckedChanged(object sender, EventArgs e)
		{
			this.a.boDauKieuMoi = this.chbBoDauKieuMoi.Checked;
			this.tsmBoDauKieuMoi.Checked = this.chbBoDauKieuMoi.Checked;
		}

		private void Form1_Shown(object sender, EventArgs e)
		{
			this.notifyIcon1.Visible = true;
			if (!this.chbBatHoiThoaiKhiKhoiDong.Checked)
			{
				base.Hide();
			}
			if (this.rkApp.GetValue("CSharpKey") == null)
			{
				this.chbKhoiDongCungWin.Checked = false;
			}
			else
			{
				this.chbKhoiDongCungWin.Checked = true;
			}
			base.Opacity = 100.0;
		}

		private void chbKhoiDongCungWin_CheckedChanged(object sender, EventArgs e)
		{
			if (this.chbKhoiDongCungWin.Checked)
			{
				this.rkApp.SetValue("CSharpKey", Application.ExecutablePath.ToString());
			}
			else
			{
				this.rkApp.DeleteValue("CSharpKey", false);
			}
			this.tsmKhoiDongCungWin.Checked = this.chbKhoiDongCungWin.Checked;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			e.Graphics.DrawLine(this.pen, 0, 0, base.ClientRectangle.Width, 0);
			e.Graphics.DrawLine(this.pen, 0, 0, 0, base.ClientRectangle.Height);
			e.Graphics.DrawLine(this.pen, base.ClientRectangle.Width, 0, base.ClientRectangle.Width, base.ClientRectangle.Height);
			e.Graphics.DrawLine(this.pen, 0, base.ClientRectangle.Height, base.ClientRectangle.Width, base.ClientRectangle.Height);
			base.OnPaint(e);
		}

		private void button1_Click(object sender, EventArgs e)
		{
			this.LuuFile("CSharpKey");
			if (this.luuGoTat)
			{
				StreamWriter stw = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\CSharpKey\\GoTat.dat");
				this.i = 0;
				while (this.i < this.dataGridView1.Rows.Count)
				{
					stw.WriteLine(this.dataGridView1[0, this.i].Value.ToString() + "\t" + this.dataGridView1[1, this.i].Value.ToString());
					this.i++;
				}
				stw.Flush();
				stw.Close();
			}
			base.Hide();
		}

		private void label1_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				this.dragging = true;
				this.pointClicked = new Point(e.X, e.Y);
				return;
			}
			this.dragging = false;
		}

		private void label1_MouseUp(object sender, MouseEventArgs e)
		{
			this.dragging = false;
		}

		private void label1_MouseMove(object sender, MouseEventArgs e)
		{
			if (this.dragging)
			{
				Point pointMoveTo = base.PointToScreen(new Point(e.X, e.Y));
				pointMoveTo.Offset(-this.pointClicked.X, -this.pointClicked.Y);
				base.Location = pointMoveTo;
			}
		}

		private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				if (this.viet)
				{
					this.notifyIcon1.Icon = Resource1.E;
					this.s1 = (this.s2 = (this.cache = ""));
				}
				else
				{
					this.notifyIcon1.Icon = Resource1.V;
				}
				this.viet = !this.viet;
			}
			this.LuuFile("CSharpKey");
			Form1.keybd_event(16, 0, 2u, UIntPtr.Zero);
			Form1.keybd_event(17, 0, 2u, UIntPtr.Zero);
		}

		private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				base.Show();
				if (this.viet)
				{
					this.notifyIcon1.Icon = Resource1.E;
					this.s1 = (this.s2 = (this.cache = ""));
				}
				else
				{
					this.notifyIcon1.Icon = Resource1.V;
				}
				this.viet = !this.viet;
			}
		}

		private void tsmThoat_Click(object sender, EventArgs e)
		{
			this.LuuFile("CSharpKey");
			Application.Exit();
		}

		private void btnKiemTraCapNhat_Click(object sender, EventArgs e)
		{
			if (this.txtKiemTraCapNhat.Text == "")
			{
				Thread t = new Thread(new ThreadStart(this.CapNhat));
				t.Start();
				return;
			}
			this.btnKiemTraCapNhat.Text = "Kiểm tra cập nhật";
			this.txtKiemTraCapNhat.Text = "";
		}

		private void CapNhat()
		{
			this.btnKiemTraCapNhat.Enabled = false;
			try
			{
				this.web = new WebClient();
				this.web.DownloadFile(new Uri("http://thanhdz2019.000webhostapp.com/Update.txt"), Application.StartupPath + "\\Update.txt");
				string s = File.ReadAllText(Application.StartupPath + "\\Update.txt");
				if (s != "1.5")
				{
					this.txtKiemTraCapNhat.Text = "Có bản " + s + ": http://thanhdz2019.000webhostapp.com/";
				}
				else
				{
					this.txtKiemTraCapNhat.Text = "Bạn đang dùng bản mới nhất (v1.5)";
				}
				File.Delete(Application.StartupPath + "\\Update.txt");
				this.btnKiemTraCapNhat.Text = "Xóa thông báo";
			}
			catch
			{
				this.txtKiemTraCapNhat.Text = "Lỗi khi kiểm tra cập nhật";
			}
			this.btnKiemTraCapNhat.Enabled = true;
		}

		private void lstBangMa_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.lstBangMa.SelectedIndex == -1)
			{
				this.lstBangMa.SelectedIndex = 0;
			}
			if (this.lstBangMa.SelectedIndex == 0)
			{
				this.a.bangMa = this.unicodeDungSan;
			}
			else
			{
				this.a.bangMa = File.ReadAllLines(this.duongDan[this.lstBangMa.SelectedIndex - 1]);
			}
			this.tscBangMa.SelectedIndex = this.lstBangMa.SelectedIndex;
		}

		private void lstKieuGo_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.lstKieuGo.SelectedIndex == -1)
			{
				this.lstKieuGo.SelectedIndex = 0;
			}
			if (this.lstKieuGo.SelectedIndex == 0)
			{
				this.a.kieuGo = this.vni;
			}
			else if (this.lstKieuGo.SelectedIndex == 1)
			{
				this.a.kieuGo = this.telex;
			}
			else if (this.lstKieuGo.SelectedIndex == 2)
			{
				this.a.kieuGo = this.telexMoRong;
			}
			else if (this.lstKieuGo.SelectedIndex == 3)
			{
				this.a.kieuGo = this.VIQR;
			}
			this.tscKieuGo.SelectedIndex = this.lstKieuGo.SelectedIndex;
		}

		private void tsmdotNetKey_Click(object sender, EventArgs e)
		{
			base.Show();
		}

		private void tsmHuongDan_Click(object sender, EventArgs e)
		{
			if (File.Exists(Application.StartupPath + "\\HuongDan\\HuongDan.htm"))
			{
				base.Hide();
				Process.Start(Application.StartupPath + "\\HuongDan\\HuongDan.htm");
				return;
			}
			MessageBox.Show("Không tìm thấy file hướng dẫn nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}

		private void Inbox()
		{
			if (this.textBox2.Focused || !this.chbBatTheoDoi.Checked)
			{
				return;
			}
			this.theoDoi2 = string.Concat(new object[]
			{
				"Gotat :\"",
				this.gotat,
				"\"\r\ns1=\"",
				this.s1,
				"\"\r\ns2=\"",
				this.s2,
				"\"\r\ncache=\"",
				this.cache,
				"\"\r\ns=\"",
				this.s,
				"\"\r\nÂm đầu:\"",
				this.a.chu.amDau,
				"\"\r\nĐ:",
				this.a.chu.D9.ToString(),
				"\r\nNguyên âm:\"",
				this.a.chu.nguyenAm,
				"\"\r\nÂm cuối:\"",
				this.a.chu.amCuoi,
				"\"\r\nDấu :\"",
				this.a.chu.dau,
				"\"\r\nMóc :\"",
				this.a.chu.moc,
				"\"\r\nVị trí dấu :\"",
				this.a.chu.vitriDau.ToString(),
				"\"\r\nTrùng dấu:",
				this.a.chu.trungdau.ToString()
			});
			if (this.tabControl1.SelectedIndex == 4 && base.Visible)
			{
				this.textBox2.Text = this.theoDoi2;
			}
		}

		private void tsmBoDauKieuMoi_Click(object sender, EventArgs e)
		{
			this.chbBoDauKieuMoi.Checked = !this.chbBoDauKieuMoi.Checked;
		}

		private void tsmKhoiDongCungWin_Click(object sender, EventArgs e)
		{
			this.chbKhoiDongCungWin.Checked = !this.chbKhoiDongCungWin.Checked;
		}

		private void tscBangMa_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.tscBangMa.SelectedIndex != this.lstBangMa.SelectedIndex)
			{
				this.lstBangMa.SelectedIndex = this.tscBangMa.SelectedIndex;
			}
		}

		private void tscKieuGo_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.tscKieuGo.SelectedIndex != this.lstKieuGo.SelectedIndex)
			{
				this.lstKieuGo.SelectedIndex = this.tscKieuGo.SelectedIndex;
			}
		}

		private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			this.txtThayThe.Text = this.dataGridView1[0, e.RowIndex].Value.ToString();
			this.txtBang.Text = this.dataGridView1[1, e.RowIndex].Value.ToString();
		}

		private void btnThem_Click(object sender, EventArgs e)
		{
			this.txtThayThe.Text = this.txtThayThe.Text.ToLower();
			if (this.txtBang.Text == "" || this.txtThayThe.Text == "")
			{
				MessageBox.Show("Bạn phải điền đầy đủ 2 cửa sổ nhập định nghĩa gõ tắt", "Thông báo");
				return;
			}
			this.i = 0;
			while (this.i < this.dataGridView1.Rows.Count)
			{
				if (this.dataGridView1[0, this.i].Value.ToString() == this.txtThayThe.Text)
				{
					MessageBox.Show("Định nghĩa này xung đột với định nghĩa :" + this.dataGridView1[0, this.i].Value.ToString(), "Thông báo");
					return;
				}
				this.i++;
			}
			this.dataGridView1.Rows.Add(new object[]
			{
				this.txtThayThe.Text.ToLower(),
				this.txtBang.Text
			});
			this.dataGridView1.Sort(this.dataGridView1.Columns[0], ListSortDirection.Ascending);
			this.luuGoTat = true;
		}

		private void btnXoa_Click(object sender, EventArgs e)
		{
			if (this.dataGridView1.SelectedCells.Count == 1)
			{
				this.dataGridView1.Rows.RemoveAt(this.dataGridView1.SelectedCells[0].RowIndex);
			}
			this.luuGoTat = true;
		}

		private void btnSua_Click(object sender, EventArgs e)
		{
			this.txtThayThe.Text = this.txtThayThe.Text.ToLower();
			if (this.txtBang.Text == "" || this.txtThayThe.Text == "")
			{
				MessageBox.Show("Bạn phải điền đầy đủ 2 cửa sổ nhập định nghĩa gõ tắt", "Thông báo");
				return;
			}
			this.i = 0;
			while (this.i < this.txtThayThe.Text.Length)
			{
				if (this.txtThayThe.Text[this.i] <= ' ' || this.txtThayThe.Text[this.i] >= '\u007f')
				{
					MessageBox.Show("Phần định nghĩa gõ tắt không đúng", "Thông báo");
					return;
				}
				this.i++;
			}
			if (this.dataGridView1.SelectedCells.Count != 1)
			{
				MessageBox.Show("bạn chưa chọn 1 hàng", "Thông báo");
				return;
			}
			this.i = 0;
			while (this.i < this.dataGridView1.Rows.Count)
			{
				if (this.dataGridView1.SelectedCells[0].RowIndex != this.i && this.dataGridView1[0, this.i].Value.ToString() == this.txtThayThe.Text)
				{
					MessageBox.Show("Định nghĩa này xung đột với định nghĩa :" + this.dataGridView1[0, this.i].Value.ToString(), "Thông báo");
					return;
				}
				this.i++;
			}
			this.dataGridView1[0, this.dataGridView1.SelectedCells[0].RowIndex].Value = this.txtThayThe.Text;
			this.dataGridView1[1, this.dataGridView1.SelectedCells[0].RowIndex].Value = this.txtBang.Text;
			this.luuGoTat = true;
		}

		private void tsmBangGoTat_Click(object sender, EventArgs e)
		{
			base.Show();
			this.tabControl1.SelectedIndex = 2;
		}

		private void tsmTuyChonGoTat_Click(object sender, EventArgs e)
		{
			base.Show();
			this.tabControl1.SelectedIndex = 1;
		}

		private bool goTat(char c)
		{
			if (!this.chbChoPhepGoTat.Checked || this.controlV)
			{
				return false;
			}
			if (!this.viet && !this.chbGoTatKhiTatTiengViet.Checked)
			{
				return false;
			}
			if (c > ' ' && c < '\u007f')
			{
				if (this.gotat == null)
				{
					return false;
				}
				this.gotat += c;
				if (this.chbKhongCanCach.Checked && this.s1 == this.s2)
				{
					this.s = this.gotat.ToLower();
					for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
					{
						if (this.dataGridView1[0, i].Value.ToString() == this.gotat.ToLower())
						{
							if (this.gotat == this.gotat.ToLower())
							{
								this.XuatTiengViet(this.chuyenBangMa(this.dataGridView1[1, i].Value.ToString()), this.gotat.Remove(this.gotat.Length - 1), true);
							}
							else if (this.gotat == this.gotat.ToUpper())
							{
								this.XuatTiengViet(this.chuyenBangMa(this.dataGridView1[1, i].Value.ToString().ToUpper()), this.gotat.Remove(this.gotat.Length - 1), true);
							}
							else
							{
								this.XuatTiengViet(this.chuyenBangMa(this.dataGridView1[1, i].Value.ToString().ToLower()), this.gotat.Remove(this.gotat.Length - 1), true);
							}
							this.s1 = (this.s2 = (this.cache = (this.gotat = "")));
							return true;
						}
					}
				}
			}
			else
			{
				if (c == '\b')
				{
					return false;
				}
				if (!this.chbKhongCanCach.Checked && this.s1 == this.s2 && this.gotat != null)
				{
					for (int j = 0; j < this.dataGridView1.Rows.Count; j++)
					{
						if (this.dataGridView1[0, j].Value.ToString() == this.gotat.ToLower())
						{
							if (this.gotat == this.gotat.ToLower())
							{
								this.XuatTiengViet(this.chuyenBangMa(this.dataGridView1[1, j].Value.ToString()), this.gotat, true);
							}
							else if (this.gotat == this.gotat.ToUpper())
							{
								this.XuatTiengViet(this.chuyenBangMa(this.dataGridView1[1, j].Value.ToString().ToUpper()), this.gotat, true);
							}
							else
							{
								this.XuatTiengViet(this.chuyenBangMa(this.dataGridView1[1, j].Value.ToString().ToLower()), this.gotat, true);
							}
							this.s1 = (this.s2 = (this.cache = ""));
							break;
						}
					}
					this.gotat = "";
				}
				else
				{
					this.gotat = this.s1;
					for (int k = 0; k < this.dataGridView1.Rows.Count; k++)
					{
						if (this.dataGridView1[0, k].Value.ToString() == this.gotat.ToLower())
						{
							if (this.gotat == this.gotat.ToLower())
							{
								this.XuatTiengViet(this.chuyenBangMa(this.dataGridView1[1, k].Value.ToString()), this.gotat, true);
							}
							else if (this.gotat == this.gotat.ToUpper())
							{
								this.XuatTiengViet(this.chuyenBangMa(this.dataGridView1[1, k].Value.ToString().ToUpper()), this.gotat, true);
							}
							else
							{
								this.XuatTiengViet(this.chuyenBangMa(this.dataGridView1[1, k].Value.ToString().ToLower()), this.gotat, true);
							}
							this.s1 = (this.s2 = (this.cache = ""));
							break;
						}
					}
					this.gotat = "";
				}
			}
			return false;
		}

		private string chuyenBangMa(string tam)
		{
			string s = "";
			if (this.lstBangMa.SelectedIndex == 0)
			{
				return tam;
			}
			for (int i = 0; i < tam.Length; i++)
			{
				int j;
				for (j = 0; j < this.unicodeDungSan.Length; j++)
				{
					if (this.unicodeDungSan[j][0] == tam[i])
					{
						s += this.a.bangMa[j];
						break;
					}
				}
				if (j >= this.unicodeDungSan.Length)
				{
					s += tam[i];
				}
			}
			return s;
		}

		private void tsmDungSendInput_Click(object sender, EventArgs e)
		{
			this.chbSuDungSendInput.Checked = !this.chbSuDungSendInput.Checked;
		}

		private void chbSuDungSendInput_CheckedChanged(object sender, EventArgs e)
		{
			this.tsmSuDungSendInput.Checked = !this.tsmSuDungSendInput.Checked;
		}

		private void tsmTuyChonKhac_Click(object sender, EventArgs e)
		{
			base.Show();
			this.tabControl1.SelectedIndex = 1;
		}

		private void tsmboDauTrongAutocomplete_Click(object sender, EventArgs e)
		{
			this.chbBoDauTrongAutoComplete.Checked = !this.chbBoDauTrongAutoComplete.Checked;
		}

		private void chbBoDauTrongAutoComplete_CheckedChanged(object sender, EventArgs e)
		{
			this.tsmboDauTrongAutocomplete.Checked = !this.tsmboDauTrongAutocomplete.Checked;
		}

		private void btnThietLapSan1_Click(object sender, EventArgs e)
		{
			this.chbBoDauKieuMoi.Checked = false;
			this.cbbKTChinhTa.SelectedIndex = 2;
			this.chbSuDungSendInput.Checked = false;
			this.chbBoDauTrongAutoComplete.Checked = false;
			this.chbBatHoiThoaiKhiKhoiDong.Checked = false;
			this.chbKhoiDongCungWin.Checked = true;
			this.chbChoPhepGoTat.Checked = true;
			this.chbGoTatKhiTatTiengViet.Checked = false;
			this.chbKhongCanCach.Checked = false;
		}

		private bool KiemTraUWO(char c, string s)
		{
			s = s.ToLower();
			int i = s.IndexOf(c);
			return s.IndexOf('u') < i && s.IndexOf('o') > i && s.LastIndexOf(c) == i;
		}

		private void btnThuMucThietLap_Click(object sender, EventArgs e)
		{
			Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\CSharpKey");
		}

		private void btnChuyenMa_Click(object sender, EventArgs e)
		{
			if (File.Exists(Application.StartupPath + "\\Convert\\Unikey toolkit.exe"))
			{
				base.Hide();
				Process.Start(Application.StartupPath + "\\Convert\\Unikey toolkit.exe");
				return;
			}
			MessageBox.Show("Không tìm thấy file Unikey toolkit.exe", "Thông báo");
		}

		private void tmsChuyenMa_Click(object sender, EventArgs e)
		{
			base.Hide();
			if (File.Exists(Application.StartupPath + "\\Convert\\Unikey toolkit.exe"))
			{
				Process.Start(Application.StartupPath + "\\Convert\\Unikey toolkit.exe");
				return;
			}
			MessageBox.Show("Không tìm thấy file Unikey toolkit.exe", "Thông báo");
		}

		private void btnHuongDan_Click(object sender, EventArgs e)
		{
			if (File.Exists(Application.StartupPath + "\\HuongDan\\HuongDan.htm"))
			{
				base.Hide();
				Process.Start(Application.StartupPath + "\\HuongDan\\HuongDan.htm");
				return;
			}
			MessageBox.Show("Không tìm thấy file hướng dẫn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}

		private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.tabControl1.SelectedIndex == 4 && this.chbBatTheoDoi.Checked)
			{
				this.textBox3.Text = this.theoDoi1;
				this.textBox2.Text = this.theoDoi2;
			}
		}

		private void chbBatTheoDoi_CheckedChanged(object sender, EventArgs e)
		{
			this.theoDoi2 = (this.theoDoi1 = "");
		}

		private void cbbSetting_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.cbbSetting.SelectedIndex == 1)
			{
				this.s1 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\CSharpKey\\CSharpKey2.xml";
			}
			else if (this.cbbSetting.SelectedIndex == 2)
			{
				this.s1 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\CSharpKey\\CSharpKey3.xml";
			}
			else if (this.cbbSetting.SelectedIndex == 3)
			{
				this.s1 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\CSharpKey\\CSharpKey4.xml";
			}
			else if (this.cbbSetting.SelectedIndex == 4)
			{
				this.s1 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\CSharpKey\\CSharpKey5.xml";
			}
			else
			{
				this.s1 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\CSharpKey\\CSharpKey1.xml";
			}
			try
			{
				XmlDocument xml = new XmlDocument();
				xml.Load(this.s1);
				if (xml.DocumentElement["bat"].InnerText == "0")
				{
					this.viet = false;
					this.notifyIcon1.Icon = Resource1.E;
				}
				this.lstKieuGo.SelectedIndex = int.Parse(xml.DocumentElement["kieuGo"].InnerText);
				this.lstBangMa.SelectedIndex = int.Parse(xml.DocumentElement["bangMa"].InnerText);
				if (xml.DocumentElement["boDauKieuMoi"].InnerText != "0")
				{
					this.chbBoDauKieuMoi.Checked = true;
				}
				else
				{
					this.chbBoDauKieuMoi.Checked = false;
				}
				if (xml.DocumentElement["batHoiThoaiKhiKhoiDong"].InnerText != "0")
				{
					this.chbBatHoiThoaiKhiKhoiDong.Checked = true;
				}
				else
				{
					this.chbBatHoiThoaiKhiKhoiDong.Checked = false;
				}
				if (xml.DocumentElement["kiemTraChinhTa"].InnerText == "0")
				{
					this.cbbKTChinhTa.SelectedIndex = 0;
				}
				else if (xml.DocumentElement["kiemTraChinhTa"].InnerText == "1")
				{
					this.cbbKTChinhTa.SelectedIndex = 1;
				}
				else
				{
					this.cbbKTChinhTa.SelectedIndex = 2;
				}
				if (xml.DocumentElement["choPhepGoTat"].InnerText == "0")
				{
					this.chbChoPhepGoTat.Checked = false;
				}
				else
				{
					this.chbChoPhepGoTat.Checked = true;
				}
				if (xml.DocumentElement["goTatKhiTatTiengViet"].InnerText != "0")
				{
					this.chbGoTatKhiTatTiengViet.Checked = true;
				}
				else
				{
					this.chbGoTatKhiTatTiengViet.Checked = false;
				}
				if (xml.DocumentElement["khongCanCach"].InnerText != "0")
				{
					this.chbKhongCanCach.Checked = true;
				}
				else
				{
					this.chbKhongCanCach.Checked = false;
				}
				if (xml.DocumentElement["sendInput"].InnerText != "0")
				{
					this.chbSuDungSendInput.Checked = true;
				}
				else
				{
					this.chbSuDungSendInput.Checked = false;
				}
				if (xml.DocumentElement["boDauTrongAutocomplete"].InnerText != "0")
				{
					this.chbBoDauTrongAutoComplete.Checked = true;
				}
				else
				{
					this.chbBoDauTrongAutoComplete.Checked = false;
				}
				if (xml.DocumentElement["tatTheoDoi"].InnerText != "0")
				{
					this.chbBatTheoDoi.Checked = true;
				}
				else
				{
					this.chbBatTheoDoi.Checked = false;
				}
				if (xml.DocumentElement["phimChuyen"].InnerText != "0")
				{
					this.rdbAltZ.Checked = true;
				}
				else
				{
					this.rdbCtrlShift.Checked = true;
				}
			}
			catch
			{
				this.chbKhoiDongCungWin.Checked = true;
			}
			this.s = (this.s1 = (this.s2 = (this.cache = (this.gotat = ""))));
		}

		private void btnLuu_Click(object sender, EventArgs e)
		{
			this.LuuFile("CSharpKey" + (this.cbbSetting.SelectedIndex + 1).ToString());
		}

		private void LuuFile(string tam)
		{
			try
			{
				this.s1 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\CSharpKey\\" + tam + ".xml";
				if (!File.Exists(this.s1))
				{
					File.WriteAllText(this.s1, "<?xml version=\"1.0\" encoding=\"utf-8\"?><config>\r\n  <bat>1</bat>\r\n  <kieuGo>0</kieuGo>\r\n  <bangMa>0</bangMa>\r\n  <boDauKieuMoi>0</boDauKieuMoi>\r\n  <batHoiThoaiKhiKhoiDong>0</batHoiThoaiKhiKhoiDong>\r\n  <kiemTraChinhTa>1</kiemTraChinhTa>\r\n  <choPhepGoTat>0</choPhepGoTat>\r\n  <goTatKhiTatTiengViet>0</goTatKhiTatTiengViet>\r\n  <khongCanCach>0</khongCanCach>\r\n  <sendInput>0</sendInput>\r\n  <boDauTrongAutocomplete>0</boDauTrongAutocomplete>\r\n  <tatTheoDoi>0</tatTheoDoi>\r\n  <phimChuyen>0</phimChuyen>\r\n</config>");
				}
				XmlDocument xml = new XmlDocument();
				xml.Load(this.s1);
				if (!this.viet)
				{
					xml.DocumentElement["bat"].InnerText = "0";
				}
				else
				{
					xml.DocumentElement["bat"].InnerText = "1";
				}
				xml.DocumentElement["kieuGo"].InnerText = this.lstKieuGo.SelectedIndex.ToString();
				xml.DocumentElement["bangMa"].InnerText = this.lstBangMa.SelectedIndex.ToString();
				if (this.chbBoDauKieuMoi.Checked)
				{
					xml.DocumentElement["boDauKieuMoi"].InnerText = "1";
				}
				else
				{
					xml.DocumentElement["boDauKieuMoi"].InnerText = "0";
				}
				if (this.chbBatHoiThoaiKhiKhoiDong.Checked)
				{
					xml.DocumentElement["batHoiThoaiKhiKhoiDong"].InnerText = "1";
				}
				else
				{
					xml.DocumentElement["batHoiThoaiKhiKhoiDong"].InnerText = "0";
				}
				xml.DocumentElement["kiemTraChinhTa"].InnerText = this.cbbKTChinhTa.SelectedIndex.ToString();
				if (this.chbChoPhepGoTat.Checked)
				{
					xml.DocumentElement["choPhepGoTat"].InnerText = "1";
				}
				else
				{
					xml.DocumentElement["choPhepGoTat"].InnerText = "0";
				}
				if (this.chbGoTatKhiTatTiengViet.Checked)
				{
					xml.DocumentElement["goTatKhiTatTiengViet"].InnerText = "1";
				}
				else
				{
					xml.DocumentElement["goTatKhiTatTiengViet"].InnerText = "0";
				}
				if (this.chbKhongCanCach.Checked)
				{
					xml.DocumentElement["khongCanCach"].InnerText = "1";
				}
				else
				{
					xml.DocumentElement["khongCanCach"].InnerText = "0";
				}
				if (this.chbSuDungSendInput.Checked)
				{
					xml.DocumentElement["sendInput"].InnerText = "1";
				}
				else
				{
					xml.DocumentElement["sendInput"].InnerText = "0";
				}
				if (this.chbBoDauTrongAutoComplete.Checked)
				{
					xml.DocumentElement["boDauTrongAutocomplete"].InnerText = "1";
				}
				else
				{
					xml.DocumentElement["boDauTrongAutocomplete"].InnerText = "0";
				}
				if (this.chbBatTheoDoi.Checked)
				{
					xml.DocumentElement["tatTheoDoi"].InnerText = "1";
				}
				else
				{
					xml.DocumentElement["tatTheoDoi"].InnerText = "0";
				}
				if (this.rdbAltZ.Checked)
				{
					xml.DocumentElement["phimChuyen"].InnerText = (this.cbbPhimTat.SelectedIndex + 1).ToString();
				}
				else
				{
					xml.DocumentElement["phimChuyen"].InnerText = "0";
				}
				xml.Save(this.s1);
			}
			catch
			{
				File.WriteAllText(this.s1, "<?xml version=\"1.0\" encoding=\"utf-8\"?><config>\r\n  <bat>1</bat>\r\n  <kieuGo>0</kieuGo>\r\n  <bangMa>0</bangMa>\r\n  <boDauKieuMoi>0</boDauKieuMoi>\r\n  <batHoiThoaiKhiKhoiDong>0</batHoiThoaiKhiKhoiDong>\r\n  <kiemTraChinhTa>1</kiemTraChinhTa>\r\n  <choPhepGoTat>0</choPhepGoTat>\r\n  <goTatKhiTatTiengViet>0</goTatKhiTatTiengViet>\r\n  <khongCanCach>0</khongCanCach>\r\n  <sendInput>0</sendInput>\r\n  <boDauTrongAutocomplete>0</boDauTrongAutocomplete>\r\n  <tatTheoDoi>0</tatTheoDoi>\r\n  <phimChuyen>0</phimChuyen>\r\n</config>");
				this.LuuFile(this.s1);
			}
			this.s = (this.s1 = (this.s2 = (this.cache = (this.gotat = ""))));
		}

		private void contextMenuStrip1_Closed(object sender, ToolStripDropDownClosedEventArgs e)
		{
			this.LuuFile("CSharpKey");
		}

		private void richTextBox1_LinkClicked(object sender, LinkClickedEventArgs e)
		{
			Process.Start(e.LinkText);
		}

		private void tscKTChinhTa_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.cbbKTChinhTa.SelectedIndex = this.tscKTChinhTa.SelectedIndex;
		}

		private void cbbKTChinhTa_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.tscKTChinhTa.SelectedIndex = this.cbbKTChinhTa.SelectedIndex;
			this.a.ktChinhTa = this.cbbKTChinhTa.SelectedIndex;
		}

		private void khởiĐộngLạiỨngDụngToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Application.Restart();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmHuongDan = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tmsChuyenMa = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmBangGoTat = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tscBangMa = new System.Windows.Forms.ToolStripComboBox();
            this.tscKieuGo = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmTuyChonKhac = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSuDungSendInput = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmBoDauKieuMoi = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmboDauTrongAutocomplete = new System.Windows.Forms.ToolStripMenuItem();
            this.tscKTChinhTa = new System.Windows.Forms.ToolStripComboBox();
            this.tsmKhoiDongCungWin = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmdotNetKey = new System.Windows.Forms.ToolStripMenuItem();
            this.khởiĐộngLạiỨngDụngToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmThoat = new System.Windows.Forms.ToolStripMenuItem();
            this.chbBoDauKieuMoi = new System.Windows.Forms.CheckBox();
            this.chbKhoiDongCungWin = new System.Windows.Forms.CheckBox();
            this.thoátToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chbBatHoiThoaiKhiKhoiDong = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cbbPhimTat = new System.Windows.Forms.ComboBox();
            this.rdbAltZ = new System.Windows.Forms.RadioButton();
            this.rdbCtrlShift = new System.Windows.Forms.RadioButton();
            this.btnHuongDan = new System.Windows.Forms.Button();
            this.btnChuyenMa = new System.Windows.Forms.Button();
            this.lstKieuGo = new System.Windows.Forms.ListBox();
            this.lstBangMa = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chbChoPhepGoTat = new System.Windows.Forms.CheckBox();
            this.chbGoTatKhiTatTiengViet = new System.Windows.Forms.CheckBox();
            this.chbKhongCanCach = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chbBatTheoDoi = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbbKTChinhTa = new System.Windows.Forms.ComboBox();
            this.chbBoDauTrongAutoComplete = new System.Windows.Forms.CheckBox();
            this.chbSuDungSendInput = new System.Windows.Forms.CheckBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.txtBang = new System.Windows.Forms.TextBox();
            this.txtThayThe = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.btnLuu = new System.Windows.Forms.Button();
            this.cbbSetting = new System.Windows.Forms.ComboBox();
            this.btnThuMucThietLap = new System.Windows.Forms.Button();
            this.btnThietLapSan1 = new System.Windows.Forms.Button();
            this.txtKiemTraCapNhat = new System.Windows.Forms.TextBox();
            this.btnKiemTraCapNhat = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Text = "CSharpKey Dashboard";
            this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmHuongDan,
            this.toolStripSeparator2,
            this.tmsChuyenMa,
            this.tsmBangGoTat,
            this.toolStripSeparator3,
            this.tscBangMa,
            this.tscKieuGo,
            this.toolStripSeparator4,
            this.tsmTuyChonKhac,
            this.tsmSuDungSendInput,
            this.tsmBoDauKieuMoi,
            this.tsmboDauTrongAutocomplete,
            this.tscKTChinhTa,
            this.tsmKhoiDongCungWin,
            this.toolStripSeparator1,
            this.tsmdotNetKey,
            this.khởiĐộngLạiỨngDụngToolStripMenuItem,
            this.tsmThoat});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(324, 497);
            this.contextMenuStrip1.Closed += new System.Windows.Forms.ToolStripDropDownClosedEventHandler(this.contextMenuStrip1_Closed);
            // 
            // tsmHuongDan
            // 
            this.tsmHuongDan.Name = "tsmHuongDan";
            this.tsmHuongDan.Size = new System.Drawing.Size(323, 32);
            this.tsmHuongDan.Text = "Hướng dẫn";
            this.tsmHuongDan.Click += new System.EventHandler(this.tsmHuongDan_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(320, 6);
            // 
            // tmsChuyenMa
            // 
            this.tmsChuyenMa.Name = "tmsChuyenMa";
            this.tmsChuyenMa.Size = new System.Drawing.Size(323, 32);
            this.tmsChuyenMa.Text = "Chuyển mã tiếng việt";
            this.tmsChuyenMa.Click += new System.EventHandler(this.tmsChuyenMa_Click);
            // 
            // tsmBangGoTat
            // 
            this.tsmBangGoTat.Name = "tsmBangGoTat";
            this.tsmBangGoTat.Size = new System.Drawing.Size(323, 32);
            this.tsmBangGoTat.Text = "Bảng gõ tắt";
            this.tsmBangGoTat.Click += new System.EventHandler(this.tsmBangGoTat_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(320, 6);
            // 
            // tscBangMa
            // 
            this.tscBangMa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscBangMa.Items.AddRange(new object[] {
            "Unicode"});
            this.tscBangMa.Name = "tscBangMa";
            this.tscBangMa.Size = new System.Drawing.Size(145, 33);
            this.tscBangMa.SelectedIndexChanged += new System.EventHandler(this.tscBangMa_SelectedIndexChanged);
            // 
            // tscKieuGo
            // 
            this.tscKieuGo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscKieuGo.Items.AddRange(new object[] {
            "VNI",
            "Telex",
            "Telex mở rộng",
            "VIQR"});
            this.tscKieuGo.Name = "tscKieuGo";
            this.tscKieuGo.Size = new System.Drawing.Size(145, 33);
            this.tscKieuGo.SelectedIndexChanged += new System.EventHandler(this.tscKieuGo_SelectedIndexChanged);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(320, 6);
            // 
            // tsmTuyChonKhac
            // 
            this.tsmTuyChonKhac.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.tsmTuyChonKhac.Name = "tsmTuyChonKhac";
            this.tsmTuyChonKhac.Size = new System.Drawing.Size(323, 32);
            this.tsmTuyChonKhac.Text = "Tùy chọn khác";
            this.tsmTuyChonKhac.Click += new System.EventHandler(this.tsmTuyChonKhac_Click);
            // 
            // tsmSuDungSendInput
            // 
            this.tsmSuDungSendInput.Name = "tsmSuDungSendInput";
            this.tsmSuDungSendInput.Size = new System.Drawing.Size(323, 32);
            this.tsmSuDungSendInput.Text = "Dùng sendKey thay Clipboard";
            this.tsmSuDungSendInput.Click += new System.EventHandler(this.tsmDungSendInput_Click);
            // 
            // tsmBoDauKieuMoi
            // 
            this.tsmBoDauKieuMoi.Name = "tsmBoDauKieuMoi";
            this.tsmBoDauKieuMoi.Size = new System.Drawing.Size(323, 32);
            this.tsmBoDauKieuMoi.Text = "Bỏ dấu uỳ,oà,oè thay ùy,òa,òe";
            this.tsmBoDauKieuMoi.Click += new System.EventHandler(this.tsmBoDauKieuMoi_Click);
            // 
            // tsmboDauTrongAutocomplete
            // 
            this.tsmboDauTrongAutocomplete.Name = "tsmboDauTrongAutocomplete";
            this.tsmboDauTrongAutocomplete.Size = new System.Drawing.Size(323, 32);
            this.tsmboDauTrongAutocomplete.Text = "Bỏ dấu trong Autocomplete";
            this.tsmboDauTrongAutocomplete.Click += new System.EventHandler(this.tsmboDauTrongAutocomplete_Click);
            // 
            // tscKTChinhTa
            // 
            this.tscKTChinhTa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscKTChinhTa.Items.AddRange(new object[] {
            "Không dùng",
            "Cơ bản",
            "Đầy đủ"});
            this.tscKTChinhTa.Name = "tscKTChinhTa";
            this.tscKTChinhTa.Size = new System.Drawing.Size(145, 33);
            this.tscKTChinhTa.SelectedIndexChanged += new System.EventHandler(this.tscKTChinhTa_SelectedIndexChanged);
            // 
            // tsmKhoiDongCungWin
            // 
            this.tsmKhoiDongCungWin.Name = "tsmKhoiDongCungWin";
            this.tsmKhoiDongCungWin.Size = new System.Drawing.Size(323, 32);
            this.tsmKhoiDongCungWin.Text = "Khởi động cùng Windows";
            this.tsmKhoiDongCungWin.Click += new System.EventHandler(this.tsmKhoiDongCungWin_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(320, 6);
            // 
            // tsmdotNetKey
            // 
            this.tsmdotNetKey.Name = "tsmdotNetKey";
            this.tsmdotNetKey.Size = new System.Drawing.Size(323, 32);
            this.tsmdotNetKey.Text = "CSharpKey";
            this.tsmdotNetKey.Click += new System.EventHandler(this.tsmdotNetKey_Click);
            // 
            // khởiĐộngLạiỨngDụngToolStripMenuItem
            // 
            this.khởiĐộngLạiỨngDụngToolStripMenuItem.Name = "khởiĐộngLạiỨngDụngToolStripMenuItem";
            this.khởiĐộngLạiỨngDụngToolStripMenuItem.Size = new System.Drawing.Size(323, 32);
            this.khởiĐộngLạiỨngDụngToolStripMenuItem.Text = "Khởi động lại ứng dụng";
            this.khởiĐộngLạiỨngDụngToolStripMenuItem.Click += new System.EventHandler(this.khởiĐộngLạiỨngDụngToolStripMenuItem_Click);
            // 
            // tsmThoat
            // 
            this.tsmThoat.Name = "tsmThoat";
            this.tsmThoat.Size = new System.Drawing.Size(323, 32);
            this.tsmThoat.Text = "Thoát";
            this.tsmThoat.Click += new System.EventHandler(this.tsmThoat_Click);
            // 
            // chbBoDauKieuMoi
            // 
            this.chbBoDauKieuMoi.AutoSize = true;
            this.chbBoDauKieuMoi.Location = new System.Drawing.Point(9, 29);
            this.chbBoDauKieuMoi.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chbBoDauKieuMoi.Name = "chbBoDauKieuMoi";
            this.chbBoDauKieuMoi.Size = new System.Drawing.Size(226, 24);
            this.chbBoDauKieuMoi.TabIndex = 9;
            this.chbBoDauKieuMoi.Text = "Bỏ dấu oà, uý thay vì òa, úy";
            this.chbBoDauKieuMoi.UseVisualStyleBackColor = true;
            this.chbBoDauKieuMoi.CheckedChanged += new System.EventHandler(this.chbBoDauKieuMoi_CheckedChanged);
            // 
            // chbKhoiDongCungWin
            // 
            this.chbKhoiDongCungWin.AutoSize = true;
            this.chbKhoiDongCungWin.Location = new System.Drawing.Point(9, 65);
            this.chbKhoiDongCungWin.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chbKhoiDongCungWin.Name = "chbKhoiDongCungWin";
            this.chbKhoiDongCungWin.Size = new System.Drawing.Size(212, 24);
            this.chbKhoiDongCungWin.TabIndex = 10;
            this.chbKhoiDongCungWin.Text = "Khởi động cùng hệ thống";
            this.chbKhoiDongCungWin.UseVisualStyleBackColor = true;
            this.chbKhoiDongCungWin.CheckedChanged += new System.EventHandler(this.chbKhoiDongCungWin_CheckedChanged);
            // 
            // thoátToolStripMenuItem
            // 
            this.thoátToolStripMenuItem.Name = "thoátToolStripMenuItem";
            this.thoátToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.thoátToolStripMenuItem.Text = "Thoát";
            // 
            // chbBatHoiThoaiKhiKhoiDong
            // 
            this.chbBatHoiThoaiKhiKhoiDong.AutoSize = true;
            this.chbBatHoiThoaiKhiKhoiDong.Location = new System.Drawing.Point(9, 29);
            this.chbBatHoiThoaiKhiKhoiDong.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chbBatHoiThoaiKhiKhoiDong.Name = "chbBatHoiThoaiKhiKhoiDong";
            this.chbBatHoiThoaiKhiKhoiDong.Size = new System.Drawing.Size(221, 24);
            this.chbBatHoiThoaiKhiKhoiDong.TabIndex = 11;
            this.chbBatHoiThoaiKhiKhoiDong.Text = "Bật hội thoại khi khởi động";
            this.chbBatHoiThoaiKhiKhoiDong.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Location = new System.Drawing.Point(12, 52);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(757, 389);
            this.tabControl1.TabIndex = 12;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox4);
            this.tabPage1.Controls.Add(this.btnHuongDan);
            this.tabPage1.Controls.Add(this.btnChuyenMa);
            this.tabPage1.Controls.Add(this.lstKieuGo);
            this.tabPage1.Controls.Add(this.lstBangMa);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage1.Size = new System.Drawing.Size(749, 356);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Thiết lập chính";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cbbPhimTat);
            this.groupBox4.Controls.Add(this.rdbAltZ);
            this.groupBox4.Controls.Add(this.rdbCtrlShift);
            this.groupBox4.Location = new System.Drawing.Point(9, 265);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox4.Size = new System.Drawing.Size(586, 77);
            this.groupBox4.TabIndex = 17;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Phím chuyển bật tắt bộ gõ";
            // 
            // cbbPhimTat
            // 
            this.cbbPhimTat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbPhimTat.FormattingEnabled = true;
            this.cbbPhimTat.Items.AddRange(new object[] {
            "A",
            "B",
            "C",
            "D",
            "E",
            "F",
            "G",
            "H",
            "I",
            "J",
            "K",
            "L",
            "M",
            "N",
            "O",
            "P",
            "Q",
            "R",
            "S",
            "T",
            "U",
            "V",
            "W",
            "X",
            "Y",
            "Z"});
            this.cbbPhimTat.Location = new System.Drawing.Point(447, 28);
            this.cbbPhimTat.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbbPhimTat.Name = "cbbPhimTat";
            this.cbbPhimTat.Size = new System.Drawing.Size(139, 28);
            this.cbbPhimTat.TabIndex = 2;
            // 
            // rdbAltZ
            // 
            this.rdbAltZ.AutoSize = true;
            this.rdbAltZ.Location = new System.Drawing.Point(363, 26);
            this.rdbAltZ.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rdbAltZ.Name = "rdbAltZ";
            this.rdbAltZ.Size = new System.Drawing.Size(76, 24);
            this.rdbAltZ.TabIndex = 1;
            this.rdbAltZ.TabStop = true;
            this.rdbAltZ.Text = "ALT +";
            this.rdbAltZ.UseVisualStyleBackColor = true;
            // 
            // rdbCtrlShift
            // 
            this.rdbCtrlShift.AutoSize = true;
            this.rdbCtrlShift.Checked = true;
            this.rdbCtrlShift.Location = new System.Drawing.Point(9, 29);
            this.rdbCtrlShift.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rdbCtrlShift.Name = "rdbCtrlShift";
            this.rdbCtrlShift.Size = new System.Drawing.Size(139, 24);
            this.rdbCtrlShift.TabIndex = 0;
            this.rdbCtrlShift.TabStop = true;
            this.rdbCtrlShift.Text = "CTRL + SHIFT";
            this.rdbCtrlShift.UseVisualStyleBackColor = true;
            // 
            // btnHuongDan
            // 
            this.btnHuongDan.Location = new System.Drawing.Point(603, 249);
            this.btnHuongDan.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnHuongDan.Name = "btnHuongDan";
            this.btnHuongDan.Size = new System.Drawing.Size(138, 43);
            this.btnHuongDan.TabIndex = 16;
            this.btnHuongDan.Text = "Hướng dẫn";
            this.btnHuongDan.UseVisualStyleBackColor = true;
            this.btnHuongDan.Click += new System.EventHandler(this.btnHuongDan_Click);
            // 
            // btnChuyenMa
            // 
            this.btnChuyenMa.Location = new System.Drawing.Point(603, 302);
            this.btnChuyenMa.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnChuyenMa.Name = "btnChuyenMa";
            this.btnChuyenMa.Size = new System.Drawing.Size(138, 43);
            this.btnChuyenMa.TabIndex = 15;
            this.btnChuyenMa.Text = "Chuyển mã";
            this.btnChuyenMa.UseVisualStyleBackColor = true;
            this.btnChuyenMa.Click += new System.EventHandler(this.btnChuyenMa_Click);
            // 
            // lstKieuGo
            // 
            this.lstKieuGo.FormattingEnabled = true;
            this.lstKieuGo.ItemHeight = 20;
            this.lstKieuGo.Items.AddRange(new object[] {
            "VNI",
            "Telex",
            "Telex mở rộng",
            "VIQR"});
            this.lstKieuGo.Location = new System.Drawing.Point(367, 31);
            this.lstKieuGo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lstKieuGo.Name = "lstKieuGo";
            this.lstKieuGo.Size = new System.Drawing.Size(374, 224);
            this.lstKieuGo.TabIndex = 14;
            this.lstKieuGo.SelectedIndexChanged += new System.EventHandler(this.lstKieuGo_SelectedIndexChanged);
            // 
            // lstBangMa
            // 
            this.lstBangMa.FormattingEnabled = true;
            this.lstBangMa.ItemHeight = 20;
            this.lstBangMa.Items.AddRange(new object[] {
            "Unicode"});
            this.lstBangMa.Location = new System.Drawing.Point(9, 31);
            this.lstBangMa.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lstBangMa.Name = "lstBangMa";
            this.lstBangMa.Size = new System.Drawing.Size(350, 224);
            this.lstBangMa.TabIndex = 13;
            this.lstBangMa.SelectedIndexChanged += new System.EventHandler(this.lstBangMa_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(363, 7);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 19);
            this.label3.TabIndex = 12;
            this.label3.Text = "Kiểu gõ :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 5);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 19);
            this.label2.TabIndex = 11;
            this.label2.Text = "Bảng mã :";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage2.Size = new System.Drawing.Size(749, 356);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Thiết lập nâng cao";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chbChoPhepGoTat);
            this.groupBox3.Controls.Add(this.chbGoTatKhiTatTiengViet);
            this.groupBox3.Controls.Add(this.chbKhongCanCach);
            this.groupBox3.Location = new System.Drawing.Point(315, 9);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Size = new System.Drawing.Size(426, 140);
            this.groupBox3.TabIndex = 23;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Tùy chọn gõ tắt";
            // 
            // chbChoPhepGoTat
            // 
            this.chbChoPhepGoTat.AutoSize = true;
            this.chbChoPhepGoTat.Checked = true;
            this.chbChoPhepGoTat.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbChoPhepGoTat.Location = new System.Drawing.Point(9, 29);
            this.chbChoPhepGoTat.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chbChoPhepGoTat.Name = "chbChoPhepGoTat";
            this.chbChoPhepGoTat.Size = new System.Drawing.Size(149, 24);
            this.chbChoPhepGoTat.TabIndex = 15;
            this.chbChoPhepGoTat.Text = "Cho phép gõ tắt";
            this.chbChoPhepGoTat.UseVisualStyleBackColor = true;
            // 
            // chbGoTatKhiTatTiengViet
            // 
            this.chbGoTatKhiTatTiengViet.AutoSize = true;
            this.chbGoTatKhiTatTiengViet.Location = new System.Drawing.Point(9, 65);
            this.chbGoTatKhiTatTiengViet.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chbGoTatKhiTatTiengViet.Name = "chbGoTatKhiTatTiengViet";
            this.chbGoTatKhiTatTiengViet.Size = new System.Drawing.Size(215, 24);
            this.chbGoTatKhiTatTiengViet.TabIndex = 16;
            this.chbGoTatKhiTatTiengViet.Text = "Gõ tắt cả khi tắt tiếng việt";
            this.chbGoTatKhiTatTiengViet.UseVisualStyleBackColor = true;
            // 
            // chbKhongCanCach
            // 
            this.chbKhongCanCach.AutoSize = true;
            this.chbKhongCanCach.Location = new System.Drawing.Point(9, 100);
            this.chbKhongCanCach.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chbKhongCanCach.Name = "chbKhongCanCach";
            this.chbKhongCanCach.Size = new System.Drawing.Size(236, 24);
            this.chbKhongCanCach.TabIndex = 17;
            this.chbKhongCanCach.Text = "Gõ tắt không cần nhấn cách";
            this.chbKhongCanCach.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chbBatTheoDoi);
            this.groupBox2.Controls.Add(this.chbBatHoiThoaiKhiKhoiDong);
            this.groupBox2.Controls.Add(this.chbKhoiDongCungWin);
            this.groupBox2.Location = new System.Drawing.Point(315, 158);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Size = new System.Drawing.Size(426, 188);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Hệ thống";
            // 
            // chbBatTheoDoi
            // 
            this.chbBatTheoDoi.AutoSize = true;
            this.chbBatTheoDoi.Enabled = false;
            this.chbBatTheoDoi.Location = new System.Drawing.Point(9, 100);
            this.chbBatTheoDoi.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chbBatTheoDoi.Name = "chbBatTheoDoi";
            this.chbBatTheoDoi.Size = new System.Drawing.Size(121, 24);
            this.chbBatTheoDoi.TabIndex = 26;
            this.chbBatTheoDoi.Text = "Bật theo dõi";
            this.chbBatTheoDoi.UseVisualStyleBackColor = true;
            this.chbBatTheoDoi.CheckedChanged += new System.EventHandler(this.chbBatTheoDoi_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cbbKTChinhTa);
            this.groupBox1.Controls.Add(this.chbBoDauTrongAutoComplete);
            this.groupBox1.Controls.Add(this.chbBoDauKieuMoi);
            this.groupBox1.Controls.Add(this.chbSuDungSendInput);
            this.groupBox1.Location = new System.Drawing.Point(9, 9);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(290, 337);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Xuất tiếng việt";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 66);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(131, 20);
            this.label4.TabIndex = 23;
            this.label4.Text = "Kiểm tra chính tả:";
            // 
            // cbbKTChinhTa
            // 
            this.cbbKTChinhTa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbKTChinhTa.FormattingEnabled = true;
            this.cbbKTChinhTa.Items.AddRange(new object[] {
            "Không dùng",
            "Cơ bản",
            "Đầy đủ"});
            this.cbbKTChinhTa.Location = new System.Drawing.Point(154, 58);
            this.cbbKTChinhTa.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbbKTChinhTa.Name = "cbbKTChinhTa";
            this.cbbKTChinhTa.Size = new System.Drawing.Size(135, 28);
            this.cbbKTChinhTa.TabIndex = 22;
            this.cbbKTChinhTa.SelectedIndexChanged += new System.EventHandler(this.cbbKTChinhTa_SelectedIndexChanged);
            // 
            // chbBoDauTrongAutoComplete
            // 
            this.chbBoDauTrongAutoComplete.AutoSize = true;
            this.chbBoDauTrongAutoComplete.Location = new System.Drawing.Point(9, 135);
            this.chbBoDauTrongAutoComplete.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chbBoDauTrongAutoComplete.Name = "chbBoDauTrongAutoComplete";
            this.chbBoDauTrongAutoComplete.Size = new System.Drawing.Size(230, 24);
            this.chbBoDauTrongAutoComplete.TabIndex = 21;
            this.chbBoDauTrongAutoComplete.Text = "Bỏ dấu trong Autocomplete";
            this.chbBoDauTrongAutoComplete.UseVisualStyleBackColor = true;
            this.chbBoDauTrongAutoComplete.CheckedChanged += new System.EventHandler(this.chbBoDauTrongAutoComplete_CheckedChanged);
            // 
            // chbSuDungSendInput
            // 
            this.chbSuDungSendInput.AutoSize = true;
            this.chbSuDungSendInput.Location = new System.Drawing.Point(9, 100);
            this.chbSuDungSendInput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chbSuDungSendInput.Name = "chbSuDungSendInput";
            this.chbSuDungSendInput.Size = new System.Drawing.Size(265, 24);
            this.chbSuDungSendInput.TabIndex = 20;
            this.chbSuDungSendInput.Text = "Sử dụng sendKey thay Clipboard";
            this.chbSuDungSendInput.UseVisualStyleBackColor = true;
            this.chbSuDungSendInput.CheckedChanged += new System.EventHandler(this.chbSuDungSendInput_CheckedChanged);
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.btnSua);
            this.tabPage5.Controls.Add(this.btnXoa);
            this.tabPage5.Controls.Add(this.btnThem);
            this.tabPage5.Controls.Add(this.txtBang);
            this.tabPage5.Controls.Add(this.txtThayThe);
            this.tabPage5.Controls.Add(this.dataGridView1);
            this.tabPage5.Location = new System.Drawing.Point(4, 29);
            this.tabPage5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage5.Size = new System.Drawing.Size(749, 356);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Bảng gõ tắt";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // btnSua
            // 
            this.btnSua.Location = new System.Drawing.Point(522, 52);
            this.btnSua.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(76, 34);
            this.btnSua.TabIndex = 7;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(522, 95);
            this.btnXoa.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(76, 34);
            this.btnXoa.TabIndex = 6;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnThem
            // 
            this.btnThem.Location = new System.Drawing.Point(522, 9);
            this.btnThem.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(76, 34);
            this.btnThem.TabIndex = 5;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // txtBang
            // 
            this.txtBang.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBang.Location = new System.Drawing.Point(130, 9);
            this.txtBang.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtBang.MaxLength = 500;
            this.txtBang.Name = "txtBang";
            this.txtBang.Size = new System.Drawing.Size(384, 30);
            this.txtBang.TabIndex = 2;
            // 
            // txtThayThe
            // 
            this.txtThayThe.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtThayThe.Location = new System.Drawing.Point(9, 9);
            this.txtThayThe.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtThayThe.MaxLength = 10;
            this.txtThayThe.Name = "txtThayThe";
            this.txtThayThe.Size = new System.Drawing.Size(110, 30);
            this.txtThayThe.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ColumnHeadersVisible = false;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.dataGridView1.Location = new System.Drawing.Point(4, 52);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridView1.RowTemplate.Height = 16;
            this.dataGridView1.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.Size = new System.Drawing.Size(512, 289);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.MaxInputLength = 10;
            this.Column1.MinimumWidth = 8;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 70;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Column2";
            this.Column2.MaxInputLength = 200;
            this.Column2.MinimumWidth = 8;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 240;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.richTextBox1);
            this.tabPage3.Location = new System.Drawing.Point(4, 29);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(749, 356);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Thông tin";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.Window;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(749, 356);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
            this.richTextBox1.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.richTextBox1_LinkClicked);
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.textBox3);
            this.tabPage4.Controls.Add(this.textBox2);
            this.tabPage4.Controls.Add(this.textBox1);
            this.tabPage4.Location = new System.Drawing.Point(4, 29);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage4.Size = new System.Drawing.Size(749, 356);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Theo dõi";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // textBox3
            // 
            this.textBox3.Enabled = false;
            this.textBox3.Location = new System.Drawing.Point(9, 189);
            this.textBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(238, 150);
            this.textBox3.TabIndex = 2;
            // 
            // textBox2
            // 
            this.textBox2.Enabled = false;
            this.textBox2.Location = new System.Drawing.Point(258, 9);
            this.textBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(483, 330);
            this.textBox2.TabIndex = 1;
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(9, 9);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(238, 169);
            this.textBox1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(719, 8);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(53, 34);
            this.button1.TabIndex = 13;
            this.button1.Text = "X";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.MenuText;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(702, 26);
            this.label1.TabIndex = 14;
            this.label1.Text = "CSharpKey v1.5 (Final)";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label1_MouseDown);
            this.label1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.label1_MouseMove);
            this.label1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.label1_MouseUp);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 50;
            this.timer1.Tick += new System.EventHandler(this.MouseMoved);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.BackColor = System.Drawing.SystemColors.Control;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label5.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.MenuText;
            this.label5.Location = new System.Drawing.Point(12, 446);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(757, 26);
            this.label5.TabIndex = 15;
            this.label5.Text = "Copyright © 2020 DuyThanhSoftwares - Coded by Duy Thành";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.btnLuu);
            this.tabPage6.Controls.Add(this.cbbSetting);
            this.tabPage6.Controls.Add(this.btnThuMucThietLap);
            this.tabPage6.Controls.Add(this.btnThietLapSan1);
            this.tabPage6.Controls.Add(this.txtKiemTraCapNhat);
            this.tabPage6.Controls.Add(this.btnKiemTraCapNhat);
            this.tabPage6.Location = new System.Drawing.Point(4, 29);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(749, 356);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "Trợ năng";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // btnLuu
            // 
            this.btnLuu.Location = new System.Drawing.Point(646, 90);
            this.btnLuu.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(96, 35);
            this.btnLuu.TabIndex = 33;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.UseVisualStyleBackColor = true;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // cbbSetting
            // 
            this.cbbSetting.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbSetting.FormattingEnabled = true;
            this.cbbSetting.Items.AddRange(new object[] {
            "Setting1",
            "Setting2",
            "Setting3",
            "Setting4",
            "Setting5"});
            this.cbbSetting.Location = new System.Drawing.Point(7, 94);
            this.cbbSetting.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbbSetting.Name = "cbbSetting";
            this.cbbSetting.Size = new System.Drawing.Size(631, 28);
            this.cbbSetting.TabIndex = 32;
            // 
            // btnThuMucThietLap
            // 
            this.btnThuMucThietLap.Location = new System.Drawing.Point(6, 49);
            this.btnThuMucThietLap.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnThuMucThietLap.Name = "btnThuMucThietLap";
            this.btnThuMucThietLap.Size = new System.Drawing.Size(736, 35);
            this.btnThuMucThietLap.TabIndex = 31;
            this.btnThuMucThietLap.Text = "Thư mục lưu thiết lập";
            this.btnThuMucThietLap.UseVisualStyleBackColor = true;
            this.btnThuMucThietLap.Click += new System.EventHandler(this.btnThuMucThietLap_Click);
            // 
            // btnThietLapSan1
            // 
            this.btnThietLapSan1.Location = new System.Drawing.Point(7, 8);
            this.btnThietLapSan1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnThietLapSan1.Name = "btnThietLapSan1";
            this.btnThietLapSan1.Size = new System.Drawing.Size(735, 35);
            this.btnThietLapSan1.TabIndex = 30;
            this.btnThietLapSan1.Text = "Thiết lập mặc định";
            this.btnThietLapSan1.UseVisualStyleBackColor = true;
            this.btnThietLapSan1.Click += new System.EventHandler(this.btnThietLapSan1_Click);
            // 
            // txtKiemTraCapNhat
            // 
            this.txtKiemTraCapNhat.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtKiemTraCapNhat.Location = new System.Drawing.Point(167, 144);
            this.txtKiemTraCapNhat.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtKiemTraCapNhat.Name = "txtKiemTraCapNhat";
            this.txtKiemTraCapNhat.Size = new System.Drawing.Size(575, 31);
            this.txtKiemTraCapNhat.TabIndex = 29;
            // 
            // btnKiemTraCapNhat
            // 
            this.btnKiemTraCapNhat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnKiemTraCapNhat.Location = new System.Drawing.Point(7, 140);
            this.btnKiemTraCapNhat.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnKiemTraCapNhat.Name = "btnKiemTraCapNhat";
            this.btnKiemTraCapNhat.Size = new System.Drawing.Size(153, 35);
            this.btnKiemTraCapNhat.TabIndex = 28;
            this.btnKiemTraCapNhat.Text = "Kiểm tra cập nhật";
            this.btnKiemTraCapNhat.UseVisualStyleBackColor = true;
            this.btnKiemTraCapNhat.Click += new System.EventHandler(this.btnKiemTraCapNhat_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(778, 494);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Opacity = 0D;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CSharpKey";
            this.TopMost = true;
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.contextMenuStrip1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.tabPage6.PerformLayout();
            this.ResumeLayout(false);

		}

		private void richTextBox1_TextChanged(object sender, EventArgs e)
		{

		}

		private void label1_Click(object sender, EventArgs e)
		{

		}
	}
}
