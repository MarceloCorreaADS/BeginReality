using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Character {
	public class AtributosBase {
		private int forca = 0;
		private int constituicao = 0;
		private int destreza = 0;
		private int inteligencia = 0;

		public AtributosBase(ClassesEnum.TipoPlayer.ClasseInfo ClasseInfo) {
			this.forca = ClasseInfo.Forca;
			this.constituicao = ClasseInfo.Constituicao;
			this.destreza = ClasseInfo.Destreza;
			this.inteligencia = ClasseInfo.Inteligencia;
		}

		public AtributosBase(int forca, int constituicao, int destreza, int inteligencia) {
			this.forca = forca;
			this.constituicao = constituicao;
			this.destreza = destreza;
			this.inteligencia = inteligencia;
		}

		public int totalAtributos() {
			return Forca + Constituicao + Destreza + Inteligencia;
		}

		public int Forca {
			get {
				return forca;
			}

			private set {
				forca = value;
			}
		}

		public int Constituicao {
			get {
				return constituicao;
			}

			private set {
				constituicao = value;
			}
		}

		public int Destreza {
			get {
				return destreza;
			}

			private set {
				destreza = value;
			}
		}

		public int Inteligencia {
			get {
				return inteligencia;
			}

			private set {
				inteligencia = value;
			}
		}
	}
}
