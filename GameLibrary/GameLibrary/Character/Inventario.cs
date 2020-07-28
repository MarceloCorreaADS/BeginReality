using UnityEngine;
using System;
using System.Reflection;
using EnumUtils;
using ClassesEnum;
using System.Collections.Generic;

namespace Character {
    public enum TipoEquipamento { TODOS, ELMO, ARMADURA, ARMA, CALCA, BOTA }
    public enum TipoBonus { MAXDISTANCIA, HP, SP }
    public class Inventario {
        public ClasseEnum tipoClasse;
        public TipoPlayer.ClasseInfo Classe { get; private set; }
        private Equipamento elmo;
        private Equipamento	 armadura;
        private Equipamento arma;
        private Equipamento calca;
        private Equipamento bota;
		private List<Equipamento> equipamentos;
		public List<Equipamento> atributos { get; private set; }


	public Inventario(ClasseEnum tipoClasse) {
            this.tipoClasse = tipoClasse;
            if (tipoClasse != ClasseEnum.GUERREIRO && tipoClasse != ClasseEnum.MAGO && tipoClasse != ClasseEnum.ARQUEIRO)
                setMonsterInventory();
        }
		public Inventario(ClasseEnum tipoClasse, List<Equipamento> equipamentos) {
            this.tipoClasse = tipoClasse;
            int cont = 0;
            while (cont != equipamentos.Count) {
                switch (cont) {
                    case 0:
                        elmo = equipamentos[cont];
                        break;
                    case 1:
                        armadura = equipamentos[cont];
                        break;
                    case 2:
                        arma = equipamentos[cont];
                        break;
                    case 3:
                        calca = equipamentos[cont];
                        break;
                    case 4:
                        bota = equipamentos[cont];
                        break;
                }
                cont++;
            }
        }

        private void setMonsterInventory() {
            elmo = new Equipamento("Elmo Monstro", "O equipamento de um ser maligno", 0, tipoClasse, TipoEquipamento.ELMO);
            armadura = new Equipamento("Armadura Monstro", "O equipamento de um ser maligno", 0, tipoClasse, TipoEquipamento.ARMADURA);
            calca = new Equipamento("Calca Monstro", "O equipamento de um ser maligno", 0, tipoClasse, TipoEquipamento.CALCA);
            bota = new Equipamento("Bota Monstro", "O equipamento de um ser maligno", 0, tipoClasse, TipoEquipamento.BOTA);
			arma = new Equipamento("Mao", "Mao", 0, tipoClasse, TipoEquipamento.ARMA, 1, 6);
			switch (tipoClasse) {
				//Chefes
				case ClasseEnum.TROLLCHIEF:
					arma = new Equipamento("Maça", "Maça de combate do Chefe Troll", 0, tipoClasse, TipoEquipamento.ARMA, 3, 3);
					break;
				case ClasseEnum.PIRATACAPITAO:
					arma = new Equipamento("Scmitarra Esanguentada", "Uma espada horripilante banhada em sangue", 0, tipoClasse, TipoEquipamento.ARMA, 3, 4);
					break;
				case ClasseEnum.GRIFO:
					arma = new Equipamento("Garras", "As Garrasafiadas do grifo", 0, tipoClasse, TipoEquipamento.ARMA, 4, 6);
					break;
				case ClasseEnum.DRAGAO:
					arma = new Equipamento("Garras", "Enormes garras de um Dragão", 0, tipoClasse, TipoEquipamento.ARMA, 4, 6);
					break;
				case ClasseEnum.TINHODEMON:
                    arma = new Equipamento("Garras", "Garras demôniacos do Grande Demônio", 0, tipoClasse, TipoEquipamento.ARMA, 4, 10);
                    break;
				// Minions Floresta
				case ClasseEnum.TROLL:
					arma = new Equipamento("Bastão de Madeira", "O equipamento de um ser maligno", 0, tipoClasse, TipoEquipamento.ARMA, 2, 3);
					break;
				case ClasseEnum.GOBLIN:
					arma = new Equipamento("Adaga", "O equipamento de um ser maligno", 0, tipoClasse, TipoEquipamento.ARMA, 2, 3);
					break;
				case ClasseEnum.GOBLINXAMA:
					arma = new Equipamento("Cajado de Madeira", "O equipamento de um ser maligno", 0, tipoClasse, TipoEquipamento.ARMA, 2, 3);
					break;

				// Minions Praia
				case ClasseEnum.PIRATA:
                    arma = new Equipamento("Mãos", "O equipamento de um ser maligno", 0, tipoClasse, TipoEquipamento.ARMA, 2, 4);
                    break;
				case ClasseEnum.HARPIA:
					arma = new Equipamento("Asas", "O equipamento de um ser maligno", 0, tipoClasse, TipoEquipamento.ARMA, 2, 4);
					break;
				case ClasseEnum.BEHEMOTH:
					arma = new Equipamento("Mãos", "O equipamento de um ser maligno", 0, tipoClasse, TipoEquipamento.ARMA, 2, 4);
					break;
				//Minions RUinas
				case ClasseEnum.DIABOLICO:
					arma = new Equipamento("Garras", "O equipamento de um ser maligno", 0, tipoClasse, TipoEquipamento.ARMA, 2, 6);
					break;
				case ClasseEnum.SAPOAMARELO:
					arma = new Equipamento("Patas", "O equipamento de um ser maligno", 0, tipoClasse, TipoEquipamento.ARMA, 2, 6);
					break;
				case ClasseEnum.SAPOAZUL:
					arma = new Equipamento("Patas", "O equipamento de um ser maligno", 0, tipoClasse, TipoEquipamento.ARMA, 2, 6);
					break;
				//Minions Montanha
				case ClasseEnum.LICHE:
					arma = new Equipamento("Cajado Demoniaco", "O equipamento de um ser maligno", 0, tipoClasse, TipoEquipamento.ARMA, 2, 6);
					break;
				case ClasseEnum.WYRM:
					arma = new Equipamento("Garras", "O equipamento de um ser maligno", 0, tipoClasse, TipoEquipamento.ARMA, 2, 6);
					break;
				case ClasseEnum.WYRMMARROM:
					arma = new Equipamento("Garras", "O equipamento de um ser maligno", 0, tipoClasse, TipoEquipamento.ARMA, 2, 6);
					break;
				//Minions Do Vulcão
				case ClasseEnum.DRIADE:
					arma = new Equipamento("Mãos", "O equipamento de um ser maligno", 0, tipoClasse, TipoEquipamento.ARMA, 4, 8);
					break;
				case ClasseEnum.SUCCUBUS:
					arma = new Equipamento("Garras", "O equipamento de um ser maligno", 0, tipoClasse, TipoEquipamento.ARMA, 4, 8);
					break;
				case ClasseEnum.ZUMBI:
					arma = new Equipamento("Mãos", "O equipamento de um ser maligno", 0, tipoClasse, TipoEquipamento.ARMA, 4, 8);
					break;

				//Outros
				case ClasseEnum.ARANHA:
					arma = new Equipamento("Presas", "O equipamento de um ser maligno", 0, tipoClasse, TipoEquipamento.ARMA, 1, 6);
					break;
			}
        }

        public object this[string propertyName] {
            get { return this.GetType().GetProperty(propertyName).GetValue(this, null); }
            set { this.GetType().GetProperty(propertyName).SetValue(this, value, null); }
        }

		public List<Equipamento> Equipamentos {
			get {
				if (equipamentos != null) {
					return equipamentos;
				} else {
					equipamentos = new List<Equipamento>();
                    equipamentos.Add(Elmo);
					equipamentos.Add(Armadura);
					equipamentos.Add(Arma);
					equipamentos.Add(Calca);
					equipamentos.Add(Bota);
					return equipamentos;
				}
			}
			private set {
				Equipamentos = value;
			}
		}

		public Equipamento Elmo {
            get {
                if (elmo != null)
                    return elmo;
                if (this.tipoClasse == ClasseEnum.MAGO)
                    return ElmoDoMago;
                else if (this.tipoClasse == ClasseEnum.GUERREIRO)
                    return ElmoDoGuerreiro;
                else if (this.tipoClasse == ClasseEnum.ARQUEIRO)
                    return ElmoDoArqueiro;

                return null;
            }
            private set {
                if (value.tipoEquipamento == TipoEquipamento.ELMO) {
                    if (this.tipoClasse == ClasseEnum.MAGO)
                        ElmoDoMago = value;
                    else if (this.tipoClasse == ClasseEnum.GUERREIRO)
                        ElmoDoGuerreiro = value;
                    else if (this.tipoClasse == ClasseEnum.ARQUEIRO)
                        ElmoDoArqueiro = value;
                }
            }
        }
        public Equipamento Armadura {
            get {
                if (armadura != null) {
                    return armadura;
                }
                if (this.tipoClasse == ClasseEnum.MAGO)
                    return ArmaduraDoMago;
                else if (this.tipoClasse == ClasseEnum.GUERREIRO)
                    return ArmaduraDoGuerreiro;
                else if (this.tipoClasse == ClasseEnum.ARQUEIRO)
                    return ArmaduraDoArqueiro;
                return null;
            }
            private set {
                if (value.tipoEquipamento == TipoEquipamento.ARMADURA) {
                    if (this.tipoClasse == ClasseEnum.MAGO)
                        ArmaduraDoMago = value;
                    else if (this.tipoClasse == ClasseEnum.GUERREIRO)
                        ArmaduraDoGuerreiro = value;
                    else if (this.tipoClasse == ClasseEnum.ARQUEIRO)
                        ArmaduraDoArqueiro = value;
                }
            }
        }
        public Equipamento Arma {
            get {
                if (arma != null) {
                    return arma;
                }
                if (this.tipoClasse == ClasseEnum.MAGO)
                    return ArmaDoMago;
                else if (this.tipoClasse == ClasseEnum.GUERREIRO)
                    return ArmaDoGuerreiro;
                else if (this.tipoClasse == ClasseEnum.ARQUEIRO)
                    return ArmaDoArqueiro;
                return null;
            }
            private set {
                if (value.tipoEquipamento == TipoEquipamento.ARMA) {
                    if (this.tipoClasse == ClasseEnum.MAGO)
                        ArmaDoMago = value;
                    else if (this.tipoClasse == ClasseEnum.GUERREIRO)
                        ArmaDoGuerreiro = value;
                    else if (this.tipoClasse == ClasseEnum.ARQUEIRO)
                        ArmaDoArqueiro = value;
                }
            }
        }
        public Equipamento Calca {
            get {
                if (calca != null) {
                    return calca;
                }
                if (this.tipoClasse == ClasseEnum.MAGO)
                    return CalcaDoMago;
                else if (this.tipoClasse == ClasseEnum.GUERREIRO)
                    return CalcaDoGuerreiro;
                else if (this.tipoClasse == ClasseEnum.ARQUEIRO)
                    return CalcaDoArqueiro;
                return null;
            }
            private set {
                if (value.tipoEquipamento == TipoEquipamento.CALCA) {
                    if (this.tipoClasse == ClasseEnum.MAGO)
                        CalcaDoMago = value;
                    else if (this.tipoClasse == ClasseEnum.GUERREIRO)
                        CalcaDoGuerreiro = value;
                    else if (this.tipoClasse == ClasseEnum.ARQUEIRO)
                        CalcaDoArqueiro = value;
                }
            }
        }

        public Equipamento Bota {
            get {
                if (bota != null) {
                    return bota;
                }
                if (this.tipoClasse == ClasseEnum.MAGO)
                    return BotaDoMago;
                else if (this.tipoClasse == ClasseEnum.GUERREIRO)
                    return BotaDoGuerreiro;
                else if (this.tipoClasse == ClasseEnum.ARQUEIRO)
                    return BotaDoArqueiro;
                return null;
            }
            private set {
                if (value.tipoEquipamento == TipoEquipamento.BOTA) {
                    if (this.tipoClasse == ClasseEnum.MAGO)
                        BotaDoMago = value;
                    else if (this.tipoClasse == ClasseEnum.GUERREIRO)
                        BotaDoGuerreiro = value;
                    else if (this.tipoClasse == ClasseEnum.ARQUEIRO)
                        BotaDoArqueiro = value;
                }
            }
        }

        // Equipamentos do Mago
        private Equipamento ElmoDoMago {
            get {
                if (elmo == null)
                    return elmo = new Equipamento("Chapéu Reforçado", "Chapéu especial confeccionado à mão com um tecido mágico, ajustável a cabeça.", 0, ClasseEnum.MAGO, TipoEquipamento.ELMO);
                else
                    return elmo;
            }
            set {
                elmo = value;
            }
        }
        private Equipamento ArmaduraDoMago {
            get {
                if (armadura == null)
                    return armadura = new Equipamento("Manto de 8000 Anos", "Manto inteiramente conservado, utilizado pelos primeiros magos do mundo. Reza a lenda que possui mais de 8000 anos, mas nada confirmado.", 0, ClasseEnum.MAGO, TipoEquipamento.ARMADURA);
                else
                    return armadura;
            }
            set {
                armadura = value;
            }
        }
        private Equipamento ArmaDoMago {
            get {
                if (arma == null)
                    return new Equipamento("Báculo Lunar", "Um báculo entregue especialmente pelo Líder Lunar ao mago. Possui recursos de iluminação noturna.", 0, ClasseEnum.MAGO, TipoEquipamento.ARMA, 2, 6);
                else
                    return arma;
            }
            set {
                arma = value;
            }
        }
        private Equipamento CalcaDoMago {
            get {
                if (calca == null)
                    return new Equipamento("O Anel", "Anel poderoso que contém um espirito mágico movido a ByteCoins.", 0, ClasseEnum.MAGO, TipoEquipamento.CALCA);
                else
                    return calca;
            }
            set {
                calca = value;
            }
        }
        private Equipamento BotaDoMago {
            get {
                if (bota == null)
                    return new Equipamento("Sapatos do Crocodilo", "Sapatos desenvolvidos especialmente por crocodilos mágicos especialistas no ramo.", 0, ClasseEnum.MAGO, TipoEquipamento.BOTA);
                else
                    return bota;
            }
            set {
                bota = value;
            }
        }
        // Equipamentos do Arqueiro
        private Equipamento ElmoDoArqueiro {
            get {
                if (elmo == null)
                    return new Equipamento("Tiara Natural", "Tiara criada por seres mágicos da natureza, entregue apenas aos elfos mais nobres.", 0, ClasseEnum.ARQUEIRO, TipoEquipamento.ELMO);
                else
                    return elmo;
            }
            set {
                elmo = value;
            }
        }
        private Equipamento ArmaduraDoArqueiro {
            get {
                if (armadura == null)
                    return new Equipamento("Vestido de Couro", "Vestido feito com couro especial, fornecido pelos animais protetores que vivem na floresta. Livre de material de origem animal.", 0, ClasseEnum.ARQUEIRO, TipoEquipamento.ARMADURA);
                else
                    return armadura;
            }
            set {
                armadura = value;
            }
        }
        private Equipamento ArmaDoArqueiro {
            get {
                if (arma == null)
                    return new Equipamento("Arco Élfico", "Um arco especial, utilizado apenas por elfos. Atira flechas mágicas invisíveis.", 0, ClasseEnum.ARQUEIRO, TipoEquipamento.ARMA, 2, 6);
                else
                    return arma;
            }
            set {
                arma = value;
            }
        }
        private Equipamento CalcaDoArqueiro {
            get {
                if (calca == null)
                    return new Equipamento("Aljava Mágica ", "Uma aljava que fornece a magia ao arco para que possa ser usadas as flechas mágicas invisíveis. São invisíveis e com enorme capacidade.", 0, ClasseEnum.ARQUEIRO, TipoEquipamento.CALCA);
                else
                    return calca;
            }
            set {
                calca = value;
            }
        }
        private Equipamento BotaDoArqueiro {
            get {
                if (bota == null)
                    return new Equipamento("Botas Ligeiras ", "OBotas especiais para correr em qualquer terreno. Última moda élfica.", 0, ClasseEnum.ARQUEIRO, TipoEquipamento.BOTA);
                else
                    return bota;
            }
            set {
                bota = value;
            }
        }
        // Equipamentos do Guerreiro
        private Equipamento ElmoDoGuerreiro {
            get {
                if (elmo == null)
                    return new Equipamento("Capacete de Titânio", "Um capacete sagrado de titânio muito resistente. Nem um coco pode afetá-lo.", 0, ClasseEnum.GUERREIRO, TipoEquipamento.ELMO);
                else
                    return elmo;
            }
            set {
                elmo = value;
            }
        }
        private Equipamento ArmaduraDoGuerreiro {
            get {
                if (armadura == null)
                    return new Equipamento("Cota de Malha Sagrada", "Especial para guerreiros, a cota de malha é abençoada em aguas digitais.", 0, ClasseEnum.GUERREIRO, TipoEquipamento.ARMADURA);
                else
                    return armadura;
            }
            set {
                armadura = value;
            }
        }
        private Equipamento ArmaDoGuerreiro {
            get {
                if (arma == null)
                    return new Equipamento("Espada de Dogeyrion", "Espada forjada com o aço mais nobre do mundo, encontrada nas terras dos primeiros cães existentes.", 0, ClasseEnum.GUERREIRO, TipoEquipamento.ARMA, 2, 6);
                else
                    return arma;
            }
            set {
                arma = value;
            }
        }
        private Equipamento CalcaDoGuerreiro {
            get {
                if (calca == null)
                    return new Equipamento("Luvas Reforçadas", "Luvas especiais que protegem do frio e encaixam a espada perfeitamente nas mãos.", 0, ClasseEnum.GUERREIRO, TipoEquipamento.CALCA);
                else
                    return calca;
            }
            set {
                calca = value;
            }
        }
        private Equipamento BotaDoGuerreiro {
            get {
				if (bota == null)
                    return new Equipamento("Botas de Segurança", "Botas especiais revestidas com bicos de titânio. Ideal para as batalhas e no trabalho.", 0, ClasseEnum.GUERREIRO, TipoEquipamento.BOTA);
                else
                    return bota;
            }
            set {
                bota = value;
            }
        }

		public List<AttributeName> Atributos(TipoEquipamento tipoEquip) {
			FieldInfo[] fields = typeof(Equipamento).GetFields();
			List<AttributeName> atributos = new List<AttributeName>();
			AttributeName atributo;
			foreach (FieldInfo field in fields) {
				atributo = null;
				object[] objects = field.GetCustomAttributes(true);
				if (objects.Length > 0) {
					atributo = objects[0] as AttributeName;
					if(atributo.Item == tipoEquip || atributo.Item == TipoEquipamento.TODOS)
						atributos.Add(atributo);
				}
			}
			return atributos;
		}

		public List<AttributeName> bonusAleatorio(TipoEquipamento tipoEquip) {
			FieldInfo[] fields = typeof(Equipamento).GetFields();
			List<AttributeName> atributosAleatorios = new List<AttributeName>();
			AttributeName atributo;
            foreach (FieldInfo field in fields) {
				atributo = null;
				object[] objects = field.GetCustomAttributes(true);
				if (objects.Length > 0) {
					atributo = objects[0] as AttributeName;
					atributo = verificaBonus(atributo, tipoEquip);
					if (atributo != null) {
						atributosAleatorios.Add(atributo);
					}
				}
			}
			return atributosAleatorios;
		}
		AttributeName verificaBonus(AttributeName atributo, TipoEquipamento tipoEquip) {

			if((atributo.Item == TipoEquipamento.TODOS || atributo.Item == tipoEquip) && (atributo.nameOriginal != "valorDadoDano" && atributo.nameOriginal != "level"))
				atributo = verificaMinMax(atributo);
			else
				atributo = null;

			return atributo;
		}
		AttributeName verificaMinMax(AttributeName atributo) {
			int min = 1;
			int max = 0;
			if (atributo.Classe == ClasseEnum.TODOS || atributo.Classe == tipoClasse)
				max = 3;
			else
				max = 1;

			atributo = new AttributeName(atributo.nameOriginal, atributo.name, atributo.Classe, atributo.Item, min, max);

			return atributo;
		}
	}

    [Serializable]
    public class Equipamento {
        public string nome;
		public string descricao;
		[AttributeName("level", "Level:", ClasseEnum.TODOS, TipoEquipamento.TODOS)]
		public int level = 0;
		public ClasseEnum tipoClasse;
        public TipoEquipamento tipoEquipamento;
		[AttributeName("CA", "CA:", ClasseEnum.TODOS, TipoEquipamento.TODOS)]
		public int CA = 0;
		[AttributeName("bonusAtaqueBase","Ataque Base:", ClasseEnum.TODOS, TipoEquipamento.ARMA)]
		public int bonusAtaqueBase = 0;
		[AttributeName("bonusMaxDistancia", "Distancia Máxima:", ClasseEnum.TODOS, TipoEquipamento.ARMA)]
		public int bonusMaxDistancia = 0;
		[AttributeName("bonusDanoBase", "Dano Base:", ClasseEnum.TODOS, TipoEquipamento.ARMA)]
		public int bonusDanoBase = 0;
		[AttributeName("bonusCA", "CA Extra:", ClasseEnum.GUERREIRO, TipoEquipamento.TODOS)]
		public int bonusCA = 0;
		[AttributeName("bonusAcerto", "Acerto:", ClasseEnum.ARQUEIRO, TipoEquipamento.ARMA)]
		public int bonusAcerto = 0;
		[AttributeName("bonusSP", "SP:", ClasseEnum.MAGO, TipoEquipamento.ELMO)]
		public int bonusSP = 0;
		[AttributeName("bonusHP", "Hp:", ClasseEnum.GUERREIRO, TipoEquipamento.ARMADURA)]
		public int bonusHP = 0;
		[AttributeName("bonusForca", "Força:", ClasseEnum.GUERREIRO, TipoEquipamento.TODOS)]
		public int bonusForca = 0;
		[AttributeName("bonusConstituicao", "Constituição:", ClasseEnum.TODOS, TipoEquipamento.TODOS)]
		public int bonusConstituicao = 0;
		[AttributeName("bonusDestreza", "Destreza:", ClasseEnum.ARQUEIRO, TipoEquipamento.TODOS)]
		public int bonusDestreza = 0;
		[AttributeName("bonusInteligencia", "Inteligência:", ClasseEnum.MAGO, TipoEquipamento.TODOS)]
		public int bonusInteligencia = 0;
		[AttributeName("qtyDadosDano", "Dados:", ClasseEnum.TODOS, TipoEquipamento.ARMA)]
		public int qtyDadosDano = 0;
		[AttributeName("valorDadoDano", "Valor do Dado:", ClasseEnum.TODOS, TipoEquipamento.ARMA)]
		public int valorDadoDano = 0;

		public object this[string fieldName] {
			get { return this.GetType().GetField(fieldName).GetValue(this); }
			set { this.GetType().GetField(fieldName).SetValue(this, value); }
		}

		public Equipamento(string nome, string descricao, int level, ClasseEnum tipoClasse, TipoEquipamento tipoEquipamento, int CA = 0, int bonusAtaqueBase = 0, int bonusMaxDistancia = 0, int bonusDanoBase = 0, int bonusCA = 0, int bonusAcerto = 0, int bonusSP = 0, int bonusHP = 0, int bonusForca = 0, int bonusConstituicao = 0, int bonusDestreza = 0, int bonusInteligencia = 0, int qtyDadosDano = 0, int valorDadoDano = 0) {
            this.nome = nome;
			this.descricao = descricao;
			this.level = level;
            this.tipoClasse = tipoClasse;
            this.tipoEquipamento = tipoEquipamento;
            this.CA = CA;
            this.bonusAtaqueBase = bonusAtaqueBase;
            this.bonusMaxDistancia = bonusMaxDistancia;
            this.bonusDanoBase = bonusDanoBase;
            this.bonusCA = bonusCA;
            this.bonusAcerto = bonusAcerto;
            this.bonusSP = bonusSP;
            this.bonusHP = bonusHP;
            this.bonusForca = bonusForca;
            this.bonusConstituicao = bonusConstituicao;
            this.bonusDestreza = bonusDestreza;
            this.bonusInteligencia = bonusInteligencia;
            this.qtyDadosDano = qtyDadosDano;
            this.valorDadoDano = valorDadoDano;
        }
        public Equipamento(string nome, string descricao, int level, ClasseEnum tipoClasse, TipoEquipamento tipoEquipamento, int qtyDadosDano = 0, int valorDadoDano = 0) {
            this.nome = nome;
			this.descricao = descricao;
			this.level = level;
			this.tipoClasse = tipoClasse;
            this.tipoEquipamento = tipoEquipamento;
            this.qtyDadosDano = qtyDadosDano;
            this.valorDadoDano = valorDadoDano;
        }
        public Equipamento(string nome, string descricao, int level, ClasseEnum tipoClasse, TipoEquipamento tipoEquipamento) {
            this.nome = nome;
			this.descricao = descricao;
			this.level = level;
			this.tipoClasse = tipoClasse;
            this.tipoEquipamento = tipoEquipamento;
        }
    }
}