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

using System;

namespace CSharpNetKey
{
	public class GoTiengViet
	{
		public struct Chu
		{
			public bool D9;

			public bool UODau;

			public bool UOA;

			public string amDau;

			public string nguyenAm;

			public string amCuoi;

			public int vitriDau;

			public int vitriThanhPhan;

			public int trungdau;

			public GoTiengViet.Dau dau;

			public char moc;

			public void Reset()
			{
				this.vitriThanhPhan = 0;
				this.D9 = (this.UODau = (this.UOA = false));
				this.amDau = (this.nguyenAm = (this.amCuoi = ""));
				this.dau = GoTiengViet.Dau.khongDau;
				this.moc = '-';
				this.vitriDau = (this.trungdau = -1);
			}
		}

		public enum Dau
		{
			khongDau,
			sac,
			huyen,
			nga,
			hoi,
			nang
		}

		private string[] van = new string[]
		{
			"a",
			"^",
			"a",
			"-",
			"a",
			"(",
			"ac",
			"^",
			"ac",
			"-",
			"ac",
			"(",
			"ach",
			"-",
			"ai",
			"-",
			"ak",
			"(",
			"am",
			"^",
			"am",
			"-",
			"am",
			"(",
			"an",
			"^",
			"an",
			"-",
			"an",
			"(",
			"ang",
			"^",
			"ang",
			"-",
			"ang",
			"(",
			"anh",
			"-",
			"ao",
			"-",
			"ap",
			"^",
			"ap",
			"-",
			"ap",
			"(",
			"at",
			"^",
			"at",
			"-",
			"at",
			"(",
			"au",
			"^",
			"au",
			"-",
			"ay",
			"^",
			"ay",
			"-",
			"e",
			"^",
			"e",
			"-",
			"ec",
			"-",
			"ech",
			"^",
			"em",
			"^",
			"em",
			"-",
			"en",
			"^",
			"en",
			"-",
			"eng",
			"-",
			"eng",
			"^",
			"enh",
			"^",
			"eo",
			"-",
			"ep",
			"^",
			"ep",
			"-",
			"et",
			"^",
			"et",
			"-",
			"eu",
			"^",
			"i",
			"-",
			"ia",
			"-",
			"ich",
			"-",
			"iec",
			"^",
			"iem",
			"^",
			"ien",
			"^",
			"ieng",
			"^",
			"iep",
			"^",
			"iet",
			"^",
			"ieu",
			"^",
			"im",
			"-",
			"in",
			"-",
			"inh",
			"-",
			"ip",
			"-",
			"it",
			"-",
			"iu",
			"-",
			"o",
			"^",
			"o",
			"-",
			"o",
			"*",
			"oa",
			"-",
			"oac",
			"-",
			"oac",
			"(",
			"oach",
			"-",
			"oai",
			"-",
			"oan",
			"-",
			"oan",
			"(",
			"oang",
			"-",
			"oang",
			"(",
			"oanh",
			"-",
			"oap",
			"-",
			"oat",
			"-",
			"oat",
			"-",
			"oat",
			"(",
			"oay",
			"-",
			"oc",
			"^",
			"oc",
			"-",
			"oe",
			"-",
			"oeo",
			"-",
			"oet",
			"-",
			"oi",
			"^",
			"oi",
			"-",
			"oi",
			"*",
			"om",
			"^",
			"om",
			"-",
			"om",
			"*",
			"on",
			"^",
			"on",
			"-",
			"on",
			"*",
			"ong",
			"^",
			"ong",
			"-",
			"ooc",
			"-",
			"op",
			"^",
			"op",
			"-",
			"op",
			"*",
			"ot",
			"^",
			"ot",
			"-",
			"ot",
			"*",
			"u",
			"-",
			"u",
			"*",
			"ua",
			"-",
			"ua",
			"*",
			"uan",
			"^",
			"uang",
			"^",
			"uat",
			"^",
			"uay",
			"^",
			"uc",
			"-",
			"uc",
			"*",
			"ue",
			"^",
			"uech",
			"^",
			"uenh",
			"^",
			"ui",
			"-",
			"ui",
			"*",
			"um",
			"-",
			"um",
			"*",
			"un",
			"-",
			"ung",
			"-",
			"ung",
			"*",
			"uo",
			"*",
			"uoc",
			"^",
			"uoc",
			"*",
			"uoi",
			"^",
			"uoi",
			"*",
			"uom",
			"^",
			"uom",
			"*",
			"uon",
			"^",
			"uon",
			"*",
			"uong",
			"^",
			"uong",
			"*",
			"uop",
			"*",
			"uot",
			"^",
			"uot",
			"*",
			"uou",
			"*",
			"up",
			"-",
			"ut",
			"-",
			"ut",
			"*",
			"uu",
			"*",
			"uy",
			"-",
			"uych",
			"-",
			"uyen",
			"^",
			"uyet",
			"^",
			"uynh",
			"-",
			"uyp",
			"-",
			"uyu",
			"-",
			"uyt",
			"-",
			"y",
			"-",
			"ych",
			"-",
			"yem",
			"^",
			"yen",
			"^",
			"yet",
			"^",
			"yeu",
			"^",
			"ynh",
			"-",
			"yt",
			"-"
		};

		private string[] amDau = new string[]
		{
			"r",
			"d",
			"gi",
			"v",
			"ch",
			"tr",
			"s",
			"x",
			"l",
			"n",
			"qu",
			"b",
			"`c",
			"/k",
			"`g",
			"/gh",
			"h",
			"kh",
			"m",
			"`ng",
			"/ngh",
			"nh",
			"p",
			"ph",
			"t",
			"th"
		};

		private string[] phuAmCuoi = new string[]
		{
			"c",
			"ch",
			"m",
			"n",
			"ng",
			"nh",
			"p",
			"t"
		};

		private string[] sacNang = new string[]
		{
			"c",
			"ch",
			"p",
			"t"
		};

		public string[] bangMa;

		public char[] kieuGo;

		private int i;

		private int kq;

		public int ktChinhTa;

		private KieuGo viTriDauMoc;

		public GoTiengViet.Chu chu;

		private string s;

		private string value;

		private string stringChinhTa;

		public bool boDauKieuMoi;

		public bool vanChiIE;

		private char mocCu;

		public GoTiengViet(string[] ma, char[] go)
		{
			this.bangMa = ma;
			this.kieuGo = go;
			this.chu.Reset();
		}

		public bool KiemTraKetThucTu(char c)
		{
			return (c < 'A' || (c > 'Z' && c < 'a') || c > 'z') && this.TimKiemKieuGo(c) == KieuGo.Null && (c < '0' || c > '9');
		}

		public bool KiemTraDauMuD()
		{
			return this.chu.dau != GoTiengViet.Dau.khongDau || this.chu.moc != '-' || this.chu.D9;
		}

		public void Reset()
		{
			this.chu.Reset();
		}

		public string Convert(string nguon)
		{
			this.s = (this.value = "");
			this.chu.Reset();
			this.i = 0;
			while (this.i < nguon.Length)
			{
				if (this.chu.vitriThanhPhan == 0)
				{
					this.viTriDauMoc = this.TimKiemKieuGo(nguon[this.i]);
					if (this.viTriDauMoc == KieuGo.DThanhD)
					{
						if (this.chu.amDau.ToLower() == "d")
						{
							this.chu.D9 = true;
							this.chu.vitriThanhPhan = 1;
						}
						else
						{
							this.chu.amDau = this.chu.amDau + nguon[this.i];
						}
					}
					else if (this.viTriDauMoc == KieuGo.UThanh7OThanh7AThanh8)
					{
						this.chu.vitriThanhPhan = 1;
						this.chu.moc = '*';
						if (nguon[this.i].ToString() == nguon[this.i].ToString().ToLower())
						{
							this.chu.nguyenAm = "u";
						}
						else
						{
							this.chu.nguyenAm = "U";
						}
						this.chu.UOA = true;
					}
					else if (this.KiemTraNguyenAm(nguon[this.i]))
					{
						this.chu.vitriThanhPhan = 1;
						this.chu.nguyenAm = nguon[this.i].ToString();
					}
					else
					{
						this.chu.amDau = this.chu.amDau + nguon[this.i];
					}
				}
				else if (this.chu.vitriThanhPhan == 1)
				{
					this.viTriDauMoc = this.TimKiemKieuGo(nguon[this.i]);
					if ((this.chu.nguyenAm.Length > 0 && this.KiemTraNguyenAm(nguon[this.i])) || (this.viTriDauMoc == KieuGo.UThanh7OThanh7AThanh8 && this.chu.nguyenAm.Length == 1))
					{
						if ((this.chu.amDau == "g" || this.chu.amDau == "G") && (this.chu.nguyenAm[0] == 'i' || this.chu.nguyenAm[0] == 'I'))
						{
							this.chu.amDau = this.chu.amDau + this.chu.nguyenAm[0];
							this.chu.nguyenAm = this.chu.nguyenAm.Substring(1);
						}
						else if ((this.chu.amDau == "q" || this.chu.amDau == "Q") && (this.chu.nguyenAm[0] == 'u' || this.chu.nguyenAm[0] == 'U'))
						{
							this.chu.amDau = this.chu.amDau + this.chu.nguyenAm[0];
							this.chu.nguyenAm = this.chu.nguyenAm.Substring(1);
						}
					}
					if (this.viTriDauMoc == KieuGo.UThanh7OThanh7AThanh8 && this.chu.nguyenAm.Length == 0)
					{
						if (nguon[this.i].ToString() == nguon[this.i].ToString().ToLower())
						{
							this.chu.nguyenAm = "u";
						}
						else
						{
							this.chu.nguyenAm = "U";
						}
						this.chu.UOA = true;
					}
					if (this.viTriDauMoc >= KieuGo.khongDau && (this.TimKiemAmDau() || this.ktChinhTa != 2))
					{
						if (this.viTriDauMoc == KieuGo.DThanhD)
						{
							if (this.chu.amDau == "d" || this.chu.amDau == "D")
							{
								if (this.chu.D9)
								{
									this.chu.D9 = false;
									this.chu.trungdau = this.i;
									this.chu.vitriThanhPhan = 2;
									this.chu.amCuoi = nguon[this.i].ToString();
								}
								else
								{
									this.chu.D9 = true;
								}
							}
							else
							{
								this.chu.vitriThanhPhan = 2;
								this.chu.amCuoi = this.chu.amCuoi + nguon[this.i];
							}
						}
						else if (!this.ThemVaoChu(this.viTriDauMoc, this.i))
						{
							if (this.KiemTraNguyenAm(nguon[this.i]))
							{
								this.chu.nguyenAm = this.chu.nguyenAm + nguon[this.i];
							}
							else
							{
								this.chu.vitriThanhPhan = 2;
								this.chu.amCuoi = this.chu.amCuoi + nguon[this.i];
							}
						}
					}
					else if (this.KiemTraNguyenAm(nguon[this.i]))
					{
						this.chu.nguyenAm = this.chu.nguyenAm + nguon[this.i];
					}
					else
					{
						this.chu.vitriThanhPhan = 2;
						this.chu.amCuoi = this.chu.amCuoi + nguon[this.i];
					}
					if (this.chu.trungdau >= 0)
					{
						this.chu.vitriThanhPhan = 2;
						this.chu.amCuoi = nguon[this.i].ToString();
					}
				}
				else if (this.chu.vitriThanhPhan == 2)
				{
					this.viTriDauMoc = this.TimKiemKieuGo(nguon[this.i]);
					if (this.viTriDauMoc >= KieuGo.khongDau && (this.TimKiemAmDau() || this.ktChinhTa == 0) && this.TimKiemPhuAmCuoi() && this.chu.trungdau == -1)
					{
						if (this.viTriDauMoc == KieuGo.DThanhD)
						{
							if (this.chu.amDau == "d" || this.chu.amDau == "D")
							{
								if (this.chu.D9)
								{
									this.chu.D9 = false;
									this.chu.trungdau = this.i;
									this.chu.vitriThanhPhan = 2;
									this.chu.amCuoi = this.chu.amCuoi + nguon[this.i];
								}
								else
								{
									this.chu.D9 = true;
								}
							}
							else
							{
								this.chu.amCuoi = this.chu.amCuoi + nguon[this.i];
							}
						}
						else if (!this.ThemVaoChu(this.viTriDauMoc, this.i) || this.chu.trungdau >= 0)
						{
							this.chu.amCuoi = this.chu.amCuoi + nguon[this.i];
						}
					}
					else
					{
						this.chu.amCuoi = this.chu.amCuoi + nguon[this.i];
					}
					if (this.ktChinhTa != 0 && this.chu.trungdau < 0 && this.chu.amCuoi.Length > 0 && this.KiemTraNguyenAm(this.chu.amCuoi[this.chu.amCuoi.Length - 1]))
					{
						return nguon;
					}
				}
				this.i++;
			}
			if (this.chu.D9)
			{
				if (this.chu.amDau == "D")
				{
					this.s = this.bangMa[145];
				}
				else
				{
					if (!(this.chu.amDau == "d"))
					{
						return nguon;
					}
					this.s = this.bangMa[72];
				}
			}
			else
			{
				this.s = this.chu.amDau;
			}
			this.value = this.ChuyenSangTiengViet();
			if (this.value != null)
			{
				return this.s + this.value + this.chu.amCuoi;
			}
			if (this.chu.nguyenAm == "")
			{
				return this.s + this.chu.amCuoi;
			}
			return nguon;
		}

		public string ConvertNguoc()
		{
			if (this.chu.vitriDau >= this.chu.nguyenAm.Length || this.chu.vitriDau < 0 || this.chu.dau == GoTiengViet.Dau.khongDau)
			{
				this.chu.vitriDau = -1;
				this.chu.dau = GoTiengViet.Dau.khongDau;
				this.value = this.chu.amDau + this.chu.nguyenAm;
			}
			else
			{
				this.value = this.chu.amDau + this.chu.nguyenAm + this.kieuGo[(int)this.chu.dau];
			}
			if (this.chu.D9 && (this.chu.amDau == "d" || this.chu.amDau == "D"))
			{
				this.value += this.kieuGo[14];
			}
			if (this.chu.moc == '-')
			{
				return this.value + this.chu.amCuoi;
			}
			if (this.chu.moc == '(')
			{
				if (this.chu.nguyenAm.IndexOf('a') >= 0 || this.chu.nguyenAm.IndexOf('A') >= 0)
				{
					return this.value + this.kieuGo[13] + this.chu.amCuoi;
				}
				return this.value + this.chu.amCuoi;
			}
			else if (this.chu.moc == '*')
			{
				char[] tam = "uUoO".ToCharArray();
				if (this.chu.nguyenAm.IndexOfAny(tam) < 0)
				{
					return this.value + this.chu.amCuoi;
				}
				if (this.kieuGo[12] != ' ')
				{
					return this.value + this.kieuGo[12] + this.chu.amCuoi;
				}
				return this.value + this.kieuGo[10] + this.chu.amCuoi;
			}
			else
			{
				char[] tam2 = "aAoOeE".ToCharArray();
				if (this.chu.nguyenAm.IndexOfAny(tam2) < 0)
				{
					return this.value + this.chu.amCuoi;
				}
				if (this.kieuGo[6] != ' ')
				{
					return this.value + this.kieuGo[6] + this.chu.amCuoi;
				}
				if (this.chu.nguyenAm.IndexOf('a') >= 0)
				{
					return this.value + this.kieuGo[7] + this.chu.amCuoi;
				}
				if (this.chu.nguyenAm.IndexOf('e') >= 0)
				{
					return this.value + this.kieuGo[8] + this.chu.amCuoi;
				}
				return this.value + this.kieuGo[9] + this.chu.amCuoi;
			}
		}

		private bool ThemVaoChu(KieuGo dnkg, int trungDau)
		{
			if (dnkg < KieuGo.khongDau || dnkg > KieuGo.nang)
			{
				this.mocCu = this.chu.moc;
				if (dnkg == KieuGo.DauMuChungChoAOE)
				{
					char[] c = new char[]
					{
						'a',
						'A',
						'o',
						'O',
						'e',
						'E'
					};
					if (this.chu.nguyenAm.IndexOfAny(c) < 0)
					{
						return false;
					}
					this.chu.moc = '^';
				}
				else if (dnkg == KieuGo.AThanh6)
				{
					char[] c2 = new char[]
					{
						'a',
						'A'
					};
					char[] c3 = new char[]
					{
						'o',
						'O',
						'e',
						'E'
					};
					if (this.chu.nguyenAm.IndexOfAny(c2) < 0 || this.chu.nguyenAm.IndexOfAny(c3) >= 0)
					{
						return false;
					}
					this.chu.moc = '^';
				}
				else if (dnkg == KieuGo.EThanh6)
				{
					char[] c4 = new char[]
					{
						'E',
						'e'
					};
					char[] c5 = new char[]
					{
						'a',
						'A',
						'o',
						'O'
					};
					if (this.chu.nguyenAm.IndexOfAny(c4) < 0 || this.chu.nguyenAm.IndexOfAny(c5) >= 0)
					{
						return false;
					}
					this.chu.moc = '^';
				}
				else if (dnkg == KieuGo.OThanh6)
				{
					char[] c6 = new char[]
					{
						'O',
						'o'
					};
					char[] c7 = new char[]
					{
						'a',
						'A',
						'e',
						'E'
					};
					if (this.chu.nguyenAm.IndexOfAny(c6) < 0 || this.chu.nguyenAm.IndexOfAny(c7) >= 0)
					{
						return false;
					}
					this.chu.moc = '^';
				}
				else if (dnkg == KieuGo.UThanh7OThanh7AThanh8 || dnkg == KieuGo.UOASimple)
				{
					char[] d = new char[]
					{
						'a',
						'A'
					};
					if (this.chu.nguyenAm.Length == 0 || this.chu.nguyenAm[0] == 'u' || this.chu.nguyenAm[0] == 'U' || (this.chu.nguyenAm.IndexOfAny(d) < 0 && this.chu.nguyenAm[0] == 'o') || this.chu.nguyenAm[0] == 'O')
					{
						char[] c8 = new char[]
						{
							'u',
							'o',
							'U',
							'O'
						};
						if (this.chu.nguyenAm.IndexOfAny(c8) < 0)
						{
							return false;
						}
						if (this.chu.nguyenAm.Length == 1)
						{
							this.chu.UODau = true;
						}
						this.chu.moc = '*';
					}
					else
					{
						if (this.chu.nguyenAm.IndexOfAny(d) < 0)
						{
							return false;
						}
						this.chu.moc = '(';
					}
				}
				else if (dnkg == KieuGo.UThanh7OThanh7)
				{
					char[] c9 = new char[]
					{
						'u',
						'o',
						'U',
						'O'
					};
					if (this.chu.nguyenAm.IndexOfAny(c9) < 0)
					{
						return false;
					}
					if (this.chu.nguyenAm.Length == 1)
					{
						this.chu.UODau = true;
					}
					this.chu.moc = '*';
				}
				else if (dnkg == KieuGo.AThanh8)
				{
					char[] c10 = new char[]
					{
						'a',
						'A'
					};
					if (this.chu.nguyenAm.IndexOfAny(c10) < 0)
					{
						return false;
					}
					this.chu.moc = '(';
				}
				if (this.chu.moc == '*' && this.mocCu == '*' && this.chu.UODau && this.chu.nguyenAm.Length == 2)
				{
					this.mocCu = '-';
					this.chu.UODau = false;
				}
				if (this.chu.moc == this.mocCu)
				{
					this.chu.trungdau = trungDau;
					this.chu.moc = '-';
					this.chu.vitriThanhPhan = 2;
					if (this.mocCu == '*' && dnkg == KieuGo.UThanh7OThanh7AThanh8)
					{
						if (this.chu.nguyenAm[0] == 'U')
						{
							this.chu.amCuoi = this.chu.amCuoi + this.kieuGo[10].ToString().ToUpper();
						}
						else
						{
							this.chu.amCuoi = this.chu.amCuoi + this.kieuGo[10].ToString().ToLower();
						}
						if (this.chu.UOA)
						{
							this.chu.nguyenAm = this.chu.nguyenAm.Substring(1);
						}
					}
				}
				return true;
			}
			this.KiemTraViTriDau();
			if (this.chu.vitriDau == -1 || this.chu.nguyenAm.Length == 0)
			{
				return false;
			}
			if (this.chu.dau == (GoTiengViet.Dau)dnkg)
			{
				if (this.chu.dau == GoTiengViet.Dau.khongDau)
				{
					return false;
				}
				this.chu.trungdau = trungDau;
				this.chu.dau = GoTiengViet.Dau.khongDau;
				this.chu.vitriThanhPhan = 2;
			}
			if (this.chu.trungdau == -1)
			{
				this.chu.dau = (GoTiengViet.Dau)dnkg;
			}
			return true;
		}

		private void KiemTraViTriDau()
		{
			if (this.chu.nguyenAm.Length == 1)
			{
				this.chu.vitriDau = 0;
				return;
			}
			if (this.chu.nguyenAm.Length == 2)
			{
				if (this.chu.nguyenAm[0] == 'a' || this.chu.nguyenAm[0] == 'A')
				{
					if ((this.chu.nguyenAm[1] != 'e' && this.chu.nguyenAm[1] != 'E') || (this.chu.nguyenAm[1] != 'a' && this.chu.nguyenAm[1] != 'A'))
					{
						this.chu.vitriDau = 0;
						return;
					}
					this.chu.vitriDau = -1;
					return;
				}
				else if (this.chu.nguyenAm[0] == 'e' || this.chu.nguyenAm[0] == 'E')
				{
					if (this.chu.nguyenAm[1] == 'o' || this.chu.nguyenAm[1] == 'O' || this.chu.nguyenAm[1] == 'u' || this.chu.nguyenAm[1] == 'U' || this.chu.nguyenAm[1] == 'y' || this.chu.nguyenAm[1] == 'Y')
					{
						this.chu.vitriDau = 0;
						return;
					}
					this.chu.vitriDau = -1;
					return;
				}
				else if (this.chu.nguyenAm[0] == 'i' || this.chu.nguyenAm[0] == 'I')
				{
					if (this.chu.nguyenAm[1] == 'e' || this.chu.nguyenAm[1] == 'E')
					{
						this.chu.vitriDau = 1;
						return;
					}
					if (this.chu.nguyenAm[1] == 'u' || this.chu.nguyenAm[1] == 'U' || this.chu.nguyenAm[1] == 'a' || this.chu.nguyenAm[1] == 'A')
					{
						this.chu.vitriDau = 0;
						return;
					}
					this.chu.vitriDau = -1;
					return;
				}
				else if (this.chu.nguyenAm[0] == 'o' || this.chu.nguyenAm[0] == 'O')
				{
					if (this.chu.nguyenAm[1] == 'a' || this.chu.nguyenAm[1] == 'A' || this.chu.nguyenAm[1] == 'e' || this.chu.nguyenAm[1] == 'E')
					{
						if (this.chu.amCuoi.Length > 0 || this.boDauKieuMoi || this.chu.moc == '^' || this.chu.moc == '(')
						{
							this.chu.vitriDau = 1;
							return;
						}
						this.chu.vitriDau = 0;
						return;
					}
					else
					{
						if (this.chu.nguyenAm[1] == 'i' || this.chu.nguyenAm[1] == 'I')
						{
							this.chu.vitriDau = 0;
							return;
						}
						if (this.chu.nguyenAm[1] == 'o' || this.chu.nguyenAm[1] == 'O')
						{
							this.chu.vitriDau = 1;
							return;
						}
						this.chu.vitriDau = -1;
						return;
					}
				}
				else if (this.chu.nguyenAm[0] == 'u' || this.chu.nguyenAm[0] == 'U')
				{
					if (this.chu.nguyenAm[1] == 'a' || this.chu.nguyenAm[1] == 'A')
					{
						if (this.chu.amCuoi.Length > 0 || this.chu.moc == '^' || this.chu.moc == '(')
						{
							this.chu.vitriDau = 1;
							return;
						}
						this.chu.vitriDau = 0;
						return;
					}
					else
					{
						if (this.chu.nguyenAm[1] == 'e' || this.chu.nguyenAm[1] == 'E')
						{
							this.chu.vitriDau = 1;
							return;
						}
						if (this.chu.nguyenAm[1] == 'i' || this.chu.nguyenAm[1] == 'I' || this.chu.nguyenAm[1] == 'u' || this.chu.nguyenAm[1] == 'U')
						{
							this.chu.vitriDau = 0;
							return;
						}
						if (this.chu.nguyenAm[1] == 'y' || this.chu.nguyenAm[1] == 'Y')
						{
							if (this.chu.amCuoi.Length > 0 || this.boDauKieuMoi)
							{
								this.chu.vitriDau = 1;
								return;
							}
							this.chu.vitriDau = 0;
							return;
						}
						else
						{
							if (this.chu.nguyenAm[1] == 'o' || this.chu.nguyenAm[1] == 'O')
							{
								this.chu.vitriDau = 1;
								return;
							}
							this.chu.vitriDau = -1;
							return;
						}
					}
				}
				else
				{
					if (this.chu.nguyenAm[0] == 'y' || this.chu.nguyenAm[0] == 'Y')
					{
						this.chu.vitriDau = 1;
						return;
					}
					this.chu.vitriDau = -1;
					return;
				}
			}
			else
			{
				if (this.chu.nguyenAm.Length != 3)
				{
					this.chu.vitriDau = -1;
					return;
				}
				string tam = this.chu.nguyenAm.Substring(0, 3).ToLower();
				if (tam == "uye")
				{
					this.chu.vitriDau = 2;
					return;
				}
				if (tam == "uou" || tam == "ieu" || tam == "oai" || tam == "uay" || tam == "oay" || tam == "uoi" || tam == "uya" || tam == "yeu" || tam == "oeo" || tam == "uyu")
				{
					this.chu.vitriDau = 1;
					return;
				}
				this.chu.vitriDau = -1;
				return;
			}
		}

		public bool TimKiemAmDau()
		{
			if (this.chu.amDau.ToLower() == "gi" && this.chu.nguyenAm.Length > 0 && "iI".IndexOf(this.chu.nguyenAm[0]) >= 0)
			{
				return false;
			}
			if (this.chu.amDau.ToLower() == "qu" && this.chu.nguyenAm.Length > 0 && "uU".IndexOf(this.chu.nguyenAm[0]) >= 0)
			{
				return false;
			}
			this.kq = -1;
			if (this.chu.amDau == "")
			{
				this.kq = 0;
			}
			else if ((this.chu.amDau == "k" || this.chu.amDau == "K") && this.chu.nguyenAm != "" && (this.chu.nguyenAm[0] == 'y' || this.chu.nguyenAm[0] == 'Y'))
			{
				this.kq = 3;
			}
			else
			{
				for (int i = 0; i < this.amDau.Length; i++)
				{
					if (this.amDau[i][0] == '/' && this.chu.amDau.ToLower() == this.amDau[i].Substring(1).ToLower())
					{
						this.kq = 1;
						break;
					}
					if (this.amDau[i][0] == '`' && this.chu.amDau.ToLower() == this.amDau[i].Substring(1).ToLower())
					{
						this.kq = 2;
						break;
					}
					if (this.chu.amDau.ToLower() == this.amDau[i].ToLower())
					{
						this.kq = 0;
						break;
					}
				}
			}
			if ((this.chu.amDau == "g" || this.chu.amDau == "G") && (this.chu.nguyenAm == "i" || this.chu.nguyenAm == "I"))
			{
				this.kq = 0;
			}
			return this.kq >= 0 && !(this.chu.nguyenAm == "") && (this.kq != 1 || "iIeE".IndexOf(this.chu.nguyenAm[0]) >= 0) && (this.kq != 2 || "iIeE".IndexOf(this.chu.nguyenAm[0]) < 0);
		}

		private string ChuyenSangTiengViet()
		{
			this.KiemTraViTriDau();
			string s = "";
			bool tv = false;
			if (this.chu.moc == '^')
			{
				char[] c = new char[]
				{
					'a',
					'A'
				};
				char[] c2 = new char[]
				{
					'o',
					'O'
				};
				char[] c3 = new char[]
				{
					'e',
					'E'
				};
				char[] c4 = new char[]
				{
					'a',
					'A',
					'o',
					'O',
					'e',
					'E'
				};
				if (this.chu.nguyenAm.IndexOfAny(c) >= 0 && this.chu.nguyenAm.IndexOfAny(c2) >= 0)
				{
					return null;
				}
				if (this.chu.nguyenAm.IndexOfAny(c2) >= 0 && this.chu.nguyenAm.IndexOfAny(c3) >= 0)
				{
					return null;
				}
				if (this.chu.nguyenAm.IndexOfAny(c3) >= 0 && this.chu.nguyenAm.IndexOfAny(c) >= 0)
				{
					return null;
				}
				if (this.chu.nguyenAm.IndexOfAny(c4) < 0)
				{
					return null;
				}
				for (int i = 0; i < this.chu.nguyenAm.Length; i++)
				{
					int value = -1;
					if (this.chu.nguyenAm[i] == 'a')
					{
						value = 12;
					}
					else if (this.chu.nguyenAm[i] == 'A')
					{
						value = 85;
					}
					else if (this.chu.nguyenAm[i] == 'o')
					{
						value = 42;
					}
					else if (this.chu.nguyenAm[i] == 'O')
					{
						value = 115;
					}
					else if (this.chu.nguyenAm[i] == 'e')
					{
						value = 24;
					}
					else if (this.chu.nguyenAm[i] == 'E')
					{
						value = 97;
					}
					if (value >= 0)
					{
						if (this.chu.vitriDau == i)
						{
							value = (int)(value + this.chu.dau);
						}
						s += this.bangMa[value];
						tv = true;
					}
					else
					{
						s += this.chu.nguyenAm[i];
					}
				}
				if (tv)
				{
					return s;
				}
				return null;
			}
			else if (this.chu.moc == '*')
			{
				char[] c5 = new char[]
				{
					'u',
					'o',
					'U',
					'O'
				};
				if (this.chu.nguyenAm.IndexOfAny(c5) < 0)
				{
					return null;
				}
				int dem = 0;
				for (int j = 0; j < this.chu.nguyenAm.Length; j++)
				{
					int value = -1;
					if (this.chu.nguyenAm[j] == 'o')
					{
						value = 48;
					}
					else if (this.chu.nguyenAm[j] == 'O')
					{
						value = 121;
					}
					else if (this.chu.nguyenAm[j] == 'u')
					{
						if (this.chu.nguyenAm.ToLower() == "uo" && this.chu.amCuoi.Length == 0 && this.chu.amDau.Length > 0)
						{
							value = 54;
						}
						else
						{
							value = 60;
						}
						dem++;
					}
					else if (this.chu.nguyenAm[j] == 'U')
					{
						if (this.chu.nguyenAm.ToLower() == "uo" && this.chu.amCuoi.Length == 0 && this.chu.amDau.Length > 0)
						{
							value = 127;
						}
						else
						{
							value = 133;
						}
						dem++;
					}
					if (value >= 0 && dem < 2)
					{
						if (this.chu.vitriDau == j)
						{
							value = (int)(value + this.chu.dau);
						}
						s += this.bangMa[value];
						tv = true;
					}
					else
					{
						s += this.chu.nguyenAm[j];
					}
				}
				if (tv)
				{
					return s;
				}
				return null;
			}
			else if (this.chu.moc == '(')
			{
				char[] c6 = new char[]
				{
					'a',
					'A'
				};
				if (this.chu.nguyenAm.IndexOfAny(c6) < 0)
				{
					return null;
				}
				for (int k = 0; k < this.chu.nguyenAm.Length; k++)
				{
					int value = -1;
					if (this.chu.nguyenAm[k] == 'a')
					{
						value = 6;
					}
					else if (this.chu.nguyenAm[k] == 'A')
					{
						value = 79;
					}
					if (value >= 0)
					{
						if (this.chu.vitriDau == k)
						{
							value = (int)(value + this.chu.dau);
						}
						s += this.bangMa[value];
						this.chu.moc = '(';
						tv = true;
					}
					else
					{
						s += this.chu.nguyenAm[k];
					}
				}
				if (tv)
				{
					return s;
				}
				return null;
			}
			else
			{
				for (int l = 0; l < this.chu.nguyenAm.Length; l++)
				{
					int value = -1;
					if (this.chu.nguyenAm[l] == 'a')
					{
						value = 0;
					}
					else if (this.chu.nguyenAm[l] == 'A')
					{
						value = 73;
					}
					else if (this.chu.nguyenAm[l] == 'o')
					{
						value = 36;
					}
					else if (this.chu.nguyenAm[l] == 'O')
					{
						value = 109;
					}
					else if (this.chu.nguyenAm[l] == 'e')
					{
						value = 18;
					}
					else if (this.chu.nguyenAm[l] == 'E')
					{
						value = 91;
					}
					else if (this.chu.nguyenAm[l] == 'i')
					{
						value = 30;
					}
					else if (this.chu.nguyenAm[l] == 'I')
					{
						value = 103;
					}
					else if (this.chu.nguyenAm[l] == 'u')
					{
						value = 54;
					}
					else if (this.chu.nguyenAm[l] == 'U')
					{
						value = 127;
					}
					else if (this.chu.nguyenAm[l] == 'y')
					{
						value = 66;
					}
					else if (this.chu.nguyenAm[l] == 'Y')
					{
						value = 139;
					}
					if (value >= 0)
					{
						if (this.chu.vitriDau == l)
						{
							value = (int)(value + this.chu.dau);
						}
						s += this.bangMa[value];
						tv = true;
					}
					else
					{
						s += this.chu.nguyenAm[l];
					}
				}
				if (tv)
				{
					return s;
				}
				return null;
			}
		}

		public bool KiemTraNguyenAm(char c)
		{
			return "aAeEiIoOuUyY".IndexOf(c) >= 0;
		}

		public KieuGo TimKiemKieuGo(char tim)
		{
			for (int i = 0; i < this.kieuGo.Length; i++)
			{
				if (this.kieuGo[i] != ' ' && this.kieuGo[i].ToString().ToLower() == tim.ToString().ToLower())
				{
					return (KieuGo)i;
				}
			}
			return KieuGo.Null;
		}

		public bool TimKiemPhuAmCuoi()
		{
			if (this.chu.amCuoi == "")
			{
				return true;
			}
			this.stringChinhTa = this.chu.amCuoi.ToLower();
			for (int i = 0; i < this.phuAmCuoi.Length; i++)
			{
				if (this.stringChinhTa == this.phuAmCuoi[i])
				{
					return true;
				}
			}
			return this.stringChinhTa == "k" || (this.ktChinhTa != 2 && (this.stringChinhTa == "h" || this.stringChinhTa == "k"));
		}

		public bool ChiSacNang()
		{
			this.stringChinhTa = this.chu.amCuoi.ToLower();
			for (int i = 0; i < this.sacNang.Length; i++)
			{
				if (this.stringChinhTa == this.sacNang[i])
				{
					return true;
				}
			}
			return false;
		}

		public bool KiemTraChinhTa()
		{
			this.stringChinhTa = this.chu.nguyenAm.ToLower() + this.chu.amCuoi.ToLower();
			if (this.chu.nguyenAm == "" || (this.chu.D9 && this.chu.nguyenAm != "" && !this.KiemTraNguyenAm(this.chu.nguyenAm[0])))
			{
				return true;
			}
			if (this.chu.amDau.ToLower() == "gi" && "iI".IndexOf(this.chu.nguyenAm[0]) >= 0)
			{
				return false;
			}
			if (this.chu.amDau.ToLower() == "qu" && "uU".IndexOf(this.chu.nguyenAm[0]) >= 0)
			{
				return false;
			}
			this.kq = -1;
			if (this.ktChinhTa == 1 && this.TimKiemPhuAmCuoi())
			{
				return true;
			}
			if (this.chu.amDau == "")
			{
				this.kq = 0;
			}
			else if ((this.chu.amDau == "k" || this.chu.amDau == "K") && this.chu.nguyenAm != "" && (this.chu.nguyenAm[0] == 'y' || this.chu.nguyenAm[0] == 'Y'))
			{
				this.kq = 3;
			}
			else
			{
				this.i = 0;
				while (this.i < this.amDau.Length)
				{
					if (this.amDau[this.i][0] == '/' && this.chu.amDau.ToLower() == this.amDau[this.i].Substring(1).ToLower())
					{
						this.kq = 1;
						break;
					}
					if (this.amDau[this.i][0] == '`' && this.chu.amDau.ToLower() == this.amDau[this.i].Substring(1).ToLower())
					{
						this.kq = 2;
						break;
					}
					if (this.chu.amDau.ToLower() == this.amDau[this.i].ToLower())
					{
						this.kq = 0;
						break;
					}
					this.i++;
				}
			}
			if ((this.chu.amDau == "g" || this.chu.amDau == "G") && (this.chu.nguyenAm == "i" || this.chu.nguyenAm == "I"))
			{
				this.kq = 0;
			}
			if (this.kq < 0 || this.chu.nguyenAm == "" || (this.kq == 1 && "iIeE".IndexOf(this.chu.nguyenAm[0]) < 0) || (this.kq == 2 && "iIeE".IndexOf(this.chu.nguyenAm[0]) >= 0))
			{
				return false;
			}
			for (int i = 0; i < this.van.Length; i += 2)
			{
				if (this.stringChinhTa == this.van[i].ToLower())
				{
					if (this.van[i] == "a" && (this.chu.moc == '^' || this.chu.moc == '(') && this.chu.amDau.Length > 0 && this.chu.amCuoi.Length == 0)
					{
						return false;
					}
					if (this.van[i + 1][0] == this.chu.moc && ((this.ChiSacNang() && (this.chu.dau == GoTiengViet.Dau.sac || this.chu.dau == GoTiengViet.Dau.nang)) || !this.ChiSacNang()))
					{
						return true;
					}
				}
			}
			return false;
		}
	}
}
