using System.Collections.Generic;
using UnityEngine;

namespace Character {
	public class Atributos {
		private AtributosBase atributosBase;
		private int level = 0;
		private int experiencia = 0;
		private int byteCoin = 0;
		private int ca = 0;
		private int acertoBase = 0;
		public EnumUtils.ClasseEnum tipoClasse;
		public ClassesEnum.TipoPlayer.ClasseInfo Classe { get; private set; }
		public int modificadorPrincipal { get; private set; }
		public Inventario inventario;

		public Atributos(EnumUtils.ClasseEnum tipoClasse) {
			this.Classe = new ClassesEnum.TipoPlayer().getClasseInfo(tipoClasse);
			this.inventario = new Inventario(tipoClasse);
			this.tipoClasse = tipoClasse;
			settings();
		}

		public Atributos(int level, int experiencia, int byteCoin, EnumUtils.ClasseEnum tipoClasse, List<Equipamento> equipamentos) {
			Classe = new ClassesEnum.TipoPlayer().getClasseInfo(tipoClasse);
			this.tipoClasse = tipoClasse;
			this.level = level;
			this.inventario = new Inventario(tipoClasse, equipamentos);
			settings();
			this.experiencia = experiencia;
			this.byteCoin = byteCoin;
		}

		private void settings() {
			addAtributos();
			setModificadorPrincipal();
		}

		private void setModificadorPrincipal() {
			if (Classe.tipoAtaque == EnumUtils.TipoAtaque.CORPO_A_CORPO) { // verifica qual é o tipo de ataque e atribui o valor do modificador principal
				this.modificadorPrincipal = Utils.CalculosUtil.ModificadorAtributo(ForcaTotal);
			} else if (Classe.tipoAtaque == EnumUtils.TipoAtaque.ATAQUE_A_DISTANCIA) {
				this.modificadorPrincipal = Utils.CalculosUtil.ModificadorAtributo(DestrezaTotal);
			} else if (Classe.tipoAtaque == EnumUtils.TipoAtaque.MAGICO) {
				this.modificadorPrincipal = Utils.CalculosUtil.ModificadorAtributo(InteligenciaTotal);
			}
		}

		private void addAtributos() {
			atributosBase = new AtributosBase(Classe);
			if (level > 0) {
				if (Classe.isNPC) {
					if (Classe.isBoss) {
						atributosBase = DistribuidorAtributos.distribuirPontosBoss(level, atributosBase, Classe);
					} else {
						atributosBase = DistribuidorAtributos.distribuirPontosMinion(level, atributosBase, Classe);
					}
				} else {
					atributosBase = DistribuidorAtributos.distribuirPontosPlayer(level, atributosBase, Classe);
				}
			}
		}

		public int Level {
			get {
				if (level > 0)
					return level;
				return Classe.level;
			}
			private set {
				level = value;
			}
		}

		public int Experiencia {
			get {
				return experiencia;
			}
			private set {
				experiencia = value;
			}
		}
		public int ByteCoin {
			get {
				return byteCoin;
			}
			private set {
				byteCoin = value;
			}
		}

		public int hp {
			get {
				int level = this.level;
				if (level == 0)
					level++;
				return (int) Classe.hp + ((Utils.CalculosUtil.ModificadorAtributo(ConstituicaoTotal) * ConstituicaoTotal) / 2) * level;
			}
		}

		public int sp {
			get {
				int level = this.level;
				if (level == 0)
					level++;
				return (int) (Classe.sp + Utils.CalculosUtil.ModificadorAtributo(InteligenciaTotal)) * level;
			}
		}

		//Atributos base

		public int Forca {
			get {
				return atributosBase.Forca;
			}
		}

		public int Constituicao {
			get {
				return atributosBase.Constituicao;
			}
		}

		public int Destreza {
			get {
				return atributosBase.Destreza;
			}
		}

		public int Inteligencia {
			get {
				return atributosBase.Inteligencia;
			}
		}

		// Atributos Totais (com a soma dos bonus de equipamento)
		public int ForcaTotal {
			get {
				return Forca + BonusForcaEquipamento;
			}
		}

		public int ConstituicaoTotal {
			get {
				return Constituicao + BonusConstituicaoEquipamento;
			}
		}

		public int DestrezaTotal {
			get {
				return Destreza + BonusDestrezaEquipamento;
			}
		}

		public int InteligenciaTotal {
			get {
				return Inteligencia + BonusInteligenciaEquipamento;
			}
		}

		//Custo de Pontos de Ação
		public int CustoAtaqueBasico {
			get {
				return 5;
			}
		}

		//Distacia de Atk
		public int DistAtqMin {
			get {
				return Classe.DistAtqMin;
			}
		}

		public int DistAtqMax {
			get {
				return Classe.DistAtqMax + BonusDistAtqMaxEquipamento;
			}
		}

		// CA Total
		public int CA {
			get {
				if (ca == 0) {
					ca = Utils.CalculosUtil.CA(CAEquipamento, DestrezaTotal, CAExtra);
				}
				return ca;
			}
		}

		// AcertoBase
		public int AcertoBase {
			get {
				if (acertoBase == 0) {
					acertoBase = Utils.CalculosUtil.AcertoBase(this);
				}
				return acertoBase + Utils.CalculosUtil.d(1, 20);
			}
		}

		// Classe de Armadura dos Equipamentos
		public int CAEquipamento {
			get {
				return (int) inventario.Elmo.CA + inventario.Armadura.CA + inventario.Arma.CA + inventario.Calca.CA + inventario.Bota.CA;
			}
		}

		// Bonus de ataque Básico
		public int BAB {
			get {
				return inventario.Arma.bonusAtaqueBase;
			}
		}

		// Bonus de Acerto Extra
		public int BAE {
			get {
				return (int) inventario.Elmo.bonusAcerto + inventario.Armadura.bonusAcerto + inventario.Arma.bonusAcerto + inventario.Calca.bonusAcerto + inventario.Bota.bonusAcerto;
			}
		}

		public int Dano {
			get {
				return Utils.CalculosUtil.CalculoDano(inventario.Arma.qtyDadosDano, inventario.Arma.valorDadoDano, DanoEquipamento, modificadorPrincipal);
			}
		}

		public int DanoMedio {
			get {
				return Utils.CalculosUtil.CalculoDanoMedio(inventario.Arma.qtyDadosDano, inventario.Arma.valorDadoDano, DanoEquipamento, modificadorPrincipal);
			}
		}

		// Bonus de Equipamentos

		public int BonusHPEquipamento {
			get {
				return (int) inventario.Elmo.bonusHP + inventario.Armadura.bonusHP + inventario.Arma.bonusHP + inventario.Calca.bonusHP + inventario.Bota.bonusHP;
			}
		}

		public int BonusSPEquipamento {
			get {
				return (int) inventario.Elmo.bonusSP + inventario.Armadura.bonusSP + inventario.Arma.bonusSP + inventario.Calca.bonusSP + inventario.Bota.bonusSP;
			}
		}


		//Bonus de equipamento para os atributos
		public int BonusForcaEquipamento {
			get {
				return (int) inventario.Elmo.bonusForca + inventario.Armadura.bonusForca + inventario.Arma.bonusForca + inventario.Calca.bonusForca + inventario.Bota.bonusForca; // mais força Equipamento
			}
		}

		public int BonusConstituicaoEquipamento {
			get {
				return (int) inventario.Elmo.bonusConstituicao + inventario.Armadura.bonusConstituicao + inventario.Arma.bonusConstituicao + inventario.Calca.bonusConstituicao + inventario.Bota.bonusConstituicao; // mais força Equipamento
			}
		}

		public int BonusDestrezaEquipamento {
			get {
				return (int) inventario.Elmo.bonusDestreza + inventario.Armadura.bonusDestreza + inventario.Arma.bonusDestreza + inventario.Calca.bonusDestreza + inventario.Bota.bonusDestreza; // mais força Equipamento
			}
		}

		public int BonusInteligenciaEquipamento {
			get {
				return (int) inventario.Elmo.bonusInteligencia + inventario.Armadura.bonusInteligencia + inventario.Arma.bonusInteligencia + inventario.Calca.bonusInteligencia + inventario.Bota.bonusInteligencia; // mais força Equipamento
			}
		}


		public int DanoEquipamento {
			get {
				return (int) inventario.Elmo.bonusDanoBase + inventario.Armadura.bonusDanoBase + inventario.Arma.bonusDanoBase + inventario.Calca.bonusDanoBase + inventario.Bota.bonusDanoBase;
			}
		}

		public int CAExtra {
			get {
				return (int) inventario.Elmo.bonusCA + inventario.Armadura.bonusCA + inventario.Arma.bonusCA + inventario.Calca.bonusCA + inventario.Bota.bonusCA;
			}
		}

		public int BonusDistAtqMaxEquipamento {
			get {
				return (int) inventario.Elmo.bonusMaxDistancia + inventario.Armadura.bonusMaxDistancia + inventario.Arma.bonusMaxDistancia + inventario.Calca.bonusMaxDistancia + inventario.Bota.bonusMaxDistancia;// mais força Equipamento
			}
		}
	}
}