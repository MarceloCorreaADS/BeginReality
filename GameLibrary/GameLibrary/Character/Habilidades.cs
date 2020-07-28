using System;
using System.Reflection;
using System.Collections.Generic;
using EnumUtils;
using Utils;

namespace Character {
	public enum TipoHabilidades { OFENSIVA, HP, SP, HPSP }
	public class Habilidades {
		private Player Main;
		public List<Habilidade> habilidades { get; private set; }

		public Habilidades(Player Main) {
			this.Main = Main;
			habilidades = getHabilidadesUtilizaveis();
		}

		public Habilidade getHabilidadeByMethodName(string nomeLiteralHabilidade) {
			if (nomeLiteralHabilidade == null)
				return null;
			Habilidade habilidade;
			try {
				habilidade = GetType().GetProperty(nomeLiteralHabilidade).GetValue(this, null) as Habilidade;
			} catch (Exception) {
				// Adicionado para que o NPC busque habilidade pelo seu nome
				// Também pode ser utilizado pelo Jogador.
				habilidade = getHabilidadeByName(nomeLiteralHabilidade);
			}
			return habilidade;
		}
		public Habilidade getHabilidadeByName(string nomeHabilidade) {
			if (nomeHabilidade == null)
				return null;
			return habilidades.Find(habilidade => habilidade.nome == nomeHabilidade);
		}
		public List<Habilidade> getHabilidadesUtilizaveis() {
			List<PropertyInfo> propertyInfos = new List<PropertyInfo>((GetType().GetProperties() as PropertyInfo[]));
			// Verifica se o retorno é do mesmo tipo de Habilidade.
			// Assim outros metodos com retorno diferente são descartados
			object objec;
			propertyInfos = propertyInfos.FindAll(objeto => (objec = objeto.GetValue(this, null)) != null && objec.GetType().Equals(typeof(Habilidade)));
			objec = null;
			List<Habilidade> habilidades = new List<Habilidade>();
			foreach (PropertyInfo objeto in propertyInfos) {
				Habilidade habilidade = objeto.GetValue(this, null) as Habilidade;
				if (habilidade != null && habilidade.tipoClasse == Main.classe && habilidade.minLevel <= Main.atributos.Level)
					habilidades.Add(objeto.GetValue(this, null) as Habilidade);
			}
			return habilidades;
		}

		public bool checkStrings(string a, string b) {
			if (string.IsNullOrEmpty(a) || string.IsNullOrEmpty(b))
				return false;

			byte[] bytesA = System.Text.Encoding.GetEncoding("iso-8859-8").GetBytes(a.ToLower());
			byte[] bytesB = System.Text.Encoding.GetEncoding("iso-8859-8").GetBytes(b.ToLower());
			return System.Text.Encoding.UTF8.GetString(bytesA) == System.Text.Encoding.UTF8.GetString(bytesB);
		}
		//Guerreiro
		public Habilidade EspadaAfiada {
			get {
				return new Habilidade("Espada Afiada", "Afia-se sua espada bruscamente em seu giz abençoado e ataca o inimigo.", 3, 6, 1, 1, 0, 3, ClasseEnum.GUERREIRO, Main.atributos, 5, 0, 1, TipoHabilidades.OFENSIVA);
			}
		}
		public Habilidade Impacto {
			get {
				return new Habilidade("Impacto", "Pega o cabo da espada e da uma coronhada no inimigo.", 4, 6, 1, 1, 0, 6, ClasseEnum.GUERREIRO, Main.atributos, 5, 2, 1, TipoHabilidades.OFENSIVA);
			}
		}
		public Habilidade CorteVertical {
			get {
				return new Habilidade("Corte Vertical", "Realiza-se um corte na vertical que quebra as costlas do inimigo.", 3, 8, 1, 1, 0, 9, ClasseEnum.GUERREIRO, Main.atributos, 5, 5, 1, TipoHabilidades.OFENSIVA);
			}
		}
		public Habilidade RachaCuca {
			get {
				return new Habilidade("Racha Cuca", "Desfere-se um ataque com força total na cabeça do inimigo.", 4, 8, 1, 1, 0, 12, ClasseEnum.GUERREIRO, Main.atributos, 5, 7, 1, TipoHabilidades.OFENSIVA);
			}
		}
		public Habilidade EspadaFlamejante {
			get {
				return new Habilidade("Espada Flamejante", "Banha-se a Espada em Alcool 70% e à acende aumentando o dano.", 5, 10, 1, 1, 0, 15, ClasseEnum.GUERREIRO, Main.atributos, 5, 10, 1, TipoHabilidades.OFENSIVA);
			}
		}

		//Arqueiro
		public Habilidade TiroPreciso {
			get {
				return new Habilidade("Tiro Preciso", "Atira-se um flecha de uma curta distância para maximizar a precisão e dano", 2, 8, 2, 3, 0, 3, ClasseEnum.ARQUEIRO, Main.atributos, 5, 0, 1, TipoHabilidades.OFENSIVA);
			}
		}
		public Habilidade FlechaPerfurante {
			get {
				return new Habilidade("Flecha Perfurante", "Atira uma flecha com poder rotativo para perfurar e causar maior dano ao inimigo.", 3, 8, 2, 5, 0, 10, ClasseEnum.ARQUEIRO, Main.atributos, 5, 2, 1, TipoHabilidades.OFENSIVA);
			}
		}
		public Habilidade Recarregar {
			get {
				return new Habilidade("Recarregar", "Aproveitando-se de uma brecha em batalha, medita-se para recuperar um pouco de SP.", 6, 3, 1, 1, 0, 6, ClasseEnum.ARQUEIRO, Main.atributos, 5, 5, 1, TipoHabilidades.SP);
			}
		}
		public Habilidade FlechadaCerteira {
			get {
				return new Habilidade("Flechada Certeira", "Concentra-se e atira um flecha a longa distância com precisão de um sniper.", 4, 8, 4, 8, 0, 15, ClasseEnum.ARQUEIRO, Main.atributos, 5, 7, 1, TipoHabilidades.OFENSIVA);
			}
		}
		public Habilidade DisparoCarregado {
			get {
				return new Habilidade("Disparo Carregado", "Carrega a flecha mágica com muito mais magia que o normal transformando-a em uma flecha enorme e depois à dispara.", 5, 10, 2, 5, 0, 20, ClasseEnum.ARQUEIRO, Main.atributos, 5, 10, 1, TipoHabilidades.OFENSIVA);
			}
		}
		//Mago
		public Habilidade BolaDeFogo {
			get {
				return new Habilidade("Bola de Fogo", "Manipula-se o fogo e atira em direção ao inimigo.", 3, 6, 2, 5, 0, 10, ClasseEnum.MAGO, Main.atributos, 5, 0, 1, TipoHabilidades.OFENSIVA);
			}
		}
		public Habilidade PalavraMagica {
			get {
				return new Habilidade("Palavra Mágica", "Grita-se uma palavra ágica que irá causar dor e dano ao inimigo alvo.", 4, 6, 2, 5, 0, 15, ClasseEnum.MAGO, Main.atributos, 5, 2, 1, TipoHabilidades.OFENSIVA);
			}
		}
		public Habilidade Restabelecer {
			get {
				return new Habilidade("Restabelecer", "Envia-se magia ao companheiro de equipe alvo, curando um pouco de seu HP e restaurando parte de seu sp", 6, 4, 1, 4, 0, 20, ClasseEnum.MAGO, Main.atributos, 5, 5, 1, TipoHabilidades.HPSP);
			}
		}
		public Habilidade RajadaDirecionada {
			get {
				return new Habilidade("Rajada Direcionada", "Direciona-se rajada de magia ao inimigo causando danos mágicos.", 4, 8, 2, 5, 0, 25, ClasseEnum.MAGO, Main.atributos, 5, 7, 1, TipoHabilidades.OFENSIVA);
			}
		}
		public Habilidade Meteoro {
			get {
				return new Habilidade("Meteoro", "Concetra-se e invoca um meteoro envolto de magia e dereciona-o ao inimigo alvo.", 6, 8, 2, 5, 0, 30, ClasseEnum.MAGO, Main.atributos, 5, 10, 1, TipoHabilidades.OFENSIVA);
			}
		}

		// Floresta
		//Troll Chief
		public Habilidade PancadaViolenta {
			get {
				return new Habilidade("Pancada Violenta", "Habilidade de um Monstro", 4, 3, 1, 1, 0, 4, ClasseEnum.TROLLCHIEF, Main.atributos, 5, 0, 1, TipoHabilidades.OFENSIVA);
			}
		}
		public Habilidade Porradao {
			get {
				return new Habilidade("Porradão", "Habilidade de um Monstro", 5, 3, 1, 1, 0, 6, ClasseEnum.TROLLCHIEF, Main.atributos, 5, 1, 1, TipoHabilidades.OFENSIVA);
			}
		}
		public Habilidade Esmagar {
			get {
				return new Habilidade("Esmagar", "Habilidade de um monstro", 3, 6, 1, 1, 0, 10, ClasseEnum.TROLLCHIEF, Main.atributos, 5, 1, 1, TipoHabilidades.OFENSIVA);
			}
		}
		//Goblin Xamã
		public Habilidade MandingaFlorestal {
			get {
				return new Habilidade("Mandinga Florestal", "Habilidade de um Monstro", 2, 6, 2, 5, 0, 10, ClasseEnum.GOBLINXAMA, Main.atributos, 5, 0, 1, TipoHabilidades.OFENSIVA);
			}
		}
		public Habilidade CuraComErvas {
			get {
				return new Habilidade("Cura com ervas", "Habilidade de um Monstro", 3, 4, 1, 5, 0, 15, ClasseEnum.GOBLINXAMA, Main.atributos, 5, 1, 1, TipoHabilidades.HP);
			}
		}

		//Troll
		public Habilidade TacapeViolento {
			get {
				return new Habilidade("Tacape Violento", "Habilidade de um Monstro", 2, 6, 1, 1, 0, 3, ClasseEnum.TROLL, Main.atributos, 5, 0, 1, TipoHabilidades.OFENSIVA);
			}
		}
		public Habilidade TapaTerremoto {
			get {
				return new Habilidade("Tapa Terremoto", "Habilidade de um Monstro", 3, 4, 1, 1, 0, 6, ClasseEnum.TROLL, Main.atributos, 5, 1, 1, TipoHabilidades.OFENSIVA);
			}
		}
		// Praia
		//Capitão Catupiry
		public Habilidade GarrafadaMortal {
			get {
				return new Habilidade("Garrafada Mortal", "Habilidade de um Monstro", 4, 4, 1, 1, 0, 3, ClasseEnum.PIRATACAPITAO, Main.atributos, 5, 1, 1, TipoHabilidades.OFENSIVA);
			}
		}
		public Habilidade CorteInfeccioso {
			get {
				return new Habilidade("Corte infeccioso", "Habilidade de um Monstro", 3, 6, 1, 1, 0, 6, ClasseEnum.PIRATACAPITAO, Main.atributos, 5, 2, 1, TipoHabilidades.OFENSIVA);
			}
		}
		public Habilidade Estocada {
			get {
				return new Habilidade("Estocada", "Habilidade de um Monstro", 5, 4, 1, 1, 0, 12, ClasseEnum.PIRATACAPITAO, Main.atributos, 5, 3, 1, TipoHabilidades.OFENSIVA);
			}
		}

		//Behemoth
		public Habilidade SocoDeAgua {
			get {
				return new Habilidade("Soco de Água", "Habilidade de um Monstro", 2, 6, 1, 1, 0, 3, ClasseEnum.BEHEMOTH, Main.atributos, 5, 1, 1, TipoHabilidades.OFENSIVA);
			}
		}
		public Habilidade MordidaViolenta {
			get {
				return new Habilidade("Mordida Violenta", "Habilidade de um Monstro", 2, 8, 1, 1, 0, 6, ClasseEnum.BEHEMOTH, Main.atributos, 5, 2, 1, TipoHabilidades.OFENSIVA);
			}
		}
		//Pirata
		public Habilidade GarrafadaDeBronze {
			get {
				return new Habilidade("Garrada de Bronze", "Habilidade de um Monstro", 2, 6, 1, 1, 0, 3, ClasseEnum.PIRATA, Main.atributos, 5, 1, 1, TipoHabilidades.OFENSIVA);
			}
		}
		public Habilidade Esganar {
			get {
				return new Habilidade("Esganar", "Habilidade de um Monstro", 2, 8, 1, 1, 0, 6, ClasseEnum.PIRATA, Main.atributos, 5, 2, 1, TipoHabilidades.OFENSIVA);
			}
		}
		//Harpia
		public Habilidade Arranhar {
			get {
				return new Habilidade("Arranhar", "Habilidade de um Monstro", 2, 6, 2, 5, 0, 3, ClasseEnum.HARPIA, Main.atributos, 5, 1, 1, TipoHabilidades.OFENSIVA);
			}
		}
		public Habilidade AtirarPenas {
			get {
				return new Habilidade("Atirar Penas", "Habilidade de um Monstro", 2, 8, 2, 5, 0, 6, ClasseEnum.HARPIA, Main.atributos, 5, 2, 1, TipoHabilidades.OFENSIVA);
			}
		}

		//Griffin
		public Habilidade Bicada {
			get {
				return new Habilidade("Bicada", "Habilidade de um Monstro", 6, 4, 1, 1, 0, 3, ClasseEnum.GRIFO, Main.atributos, 5, 4, 1, TipoHabilidades.OFENSIVA);
			}
		}
		public Habilidade AsaCortante {
			get {
				return new Habilidade("Asa Cortante", "Habilidade de um Monstro", 5, 6, 1, 1, 0, 6, ClasseEnum.GRIFO, Main.atributos, 5, 5, 1, TipoHabilidades.OFENSIVA);
			}
		}
		public Habilidade GarrasMortais {
			get {
				return new Habilidade("Garras Mortais", "Habilidade de um Monstro", 6, 6, 1, 1, 0, 12, ClasseEnum.GRIFO, Main.atributos, 5, 7, 1, TipoHabilidades.OFENSIVA);
			}
		}
		//Meta-Sapo Azul
		public Habilidade Trombada {
			get {
				return new Habilidade("Trombada", "Habilidade de um Monstro", 2, 8, 1, 1, 0, 3, ClasseEnum.SAPOAZUL, Main.atributos, 5, 1, 1, TipoHabilidades.OFENSIVA);
			}
		}
		public Habilidade EnforcamentoComLingua {
			get {
				return new Habilidade("Enforcamento com Lingua", "Habilidade de um Monstro", 3, 6, 1, 1, 0, 6, ClasseEnum.SAPOAZUL, Main.atributos, 5, 2, 1, TipoHabilidades.OFENSIVA);
			}
		}

		//Meta-Sapo Amarelo
		public Habilidade JatoDeAgua {
			get {
				return new Habilidade("Jato de água", "Habilidade de um Monstro", 2, 8, 2, 5, 0, 3, ClasseEnum.SAPOAMARELO, Main.atributos, 5, 4, 1, TipoHabilidades.OFENSIVA);
			}
		}
		public Habilidade CuspeEnvenenado {
			get {
				return new Habilidade("Cuspe Envenenado", "Habilidade de um Monstro", 3, 6, 2, 5, 0, 6, ClasseEnum.SAPOAMARELO, Main.atributos, 5, 5, 1, TipoHabilidades.OFENSIVA);
			}
		}
		//Diabolico
		public Habilidade CorteDemoniaco {
			get {
				return new Habilidade("Corte Demoniaco", "Habilidade de um Monstro", 2, 8, 2, 5, 0, 10, ClasseEnum.DIABOLICO, Main.atributos, 5, 4, 1, TipoHabilidades.OFENSIVA);
			}
		}
		public Habilidade CuraDemoniaca {
			get {
				return new Habilidade("Cura Demoniaca", "Habilidade de um Monstro", 4, 6, 1, 5, 0, 15, ClasseEnum.DIABOLICO, Main.atributos, 5, 5, 1, TipoHabilidades.HPSP);
			}
		}

		//Montanha
		//HammerTail
		public Habilidade VentoCortante {
			get {
				return new Habilidade("Vento Cortante", "Habilidade de um Monstro", 6, 4, 2, 5, 0, 9, ClasseEnum.DRAGAO, Main.atributos, 5, 4, 1, TipoHabilidades.OFENSIVA);
			}
		}
		public Habilidade MarteladaDeCauda {
			get {
				return new Habilidade("Martelada de Cauda", "Habilidade de um Monstro", 5, 6, 2, 3, 0, 15, ClasseEnum.DRAGAO, Main.atributos, 5, 5, 1, TipoHabilidades.OFENSIVA);
			}
		}
		public Habilidade BafoFlamejante {
			get {
				return new Habilidade("Bafo Flamejante", "Habilidade de um Monstro", 6, 6, 2, 5, 0, 20, ClasseEnum.DRAGAO, Main.atributos, 5, 7, 1, TipoHabilidades.OFENSIVA);
			}
		}

		//Wyrm
		public Habilidade Mordida {
			get {
				return new Habilidade("Mordida", "Habilidade de um Monstro", 2, 8, 1, 1, 0, 6, ClasseEnum.WYRM, Main.atributos, 5, 4, 1, TipoHabilidades.OFENSIVA);
			}
		}
		public Habilidade AgarrarCortante {
			get {
				return new Habilidade("Agarra Cortante", "Habilidade de um Monstro", 3, 6, 1, 1, 0, 9, ClasseEnum.WYRM, Main.atributos, 5, 5, 1, TipoHabilidades.OFENSIVA);
			}
		}
		
		//Wyrm Marrom
		public Habilidade ArranharEnvenenado {
			get {
				return new Habilidade("Arranhar Envenenado", "Habilidade de um Monstro", 2, 8, 1, 1, 0, 6, ClasseEnum.WYRMMARROM, Main.atributos, 5, 4, 1, TipoHabilidades.OFENSIVA);
			}
		}
		public Habilidade LambidaAcida {
			get {
				return new Habilidade("Lambida Ácida", "Habilidade de um Monstro", 3, 6, 1, 1, 0, 9, ClasseEnum.WYRMMARROM, Main.atributos, 5, 5, 1, TipoHabilidades.HPSP);
			}
		}
		
		//Liche
		public Habilidade Amaldicoar {
			get {
				return new Habilidade("Amaldicoar", "Habilidade de um Monstro", 2, 8, 2, 5, 0, 15, ClasseEnum.LICHE, Main.atributos, 5, 4, 1, TipoHabilidades.OFENSIVA);
			}
		}
		public Habilidade MaldicaoDaRestauracao {
			get {
				return new Habilidade("Maldicao da Restauração", "Habilidade de um Monstro", 4, 6, 2, 5, 0, 20, ClasseEnum.LICHE, Main.atributos, 5, 5, 1, TipoHabilidades.HPSP);
			}
		}

		//Vulcão
		// Tinho Demon
		public Habilidade AbracoDoDemonio {
			get {
				return new Habilidade("Abraço do Demonio", "Habilidade de um Monstro", 8, 6, 1, 1, 0, 20, ClasseEnum.TINHODEMON, Main.atributos, 5, 7, 1, TipoHabilidades.OFENSIVA);
			}
		}
		public Habilidade CorteDoDemonio {
			get {
				return new Habilidade("Corte do Demonio", "Habilidade de um Monstro", 10, 6, 1, 1, 0, 25, ClasseEnum.TINHODEMON, Main.atributos, 5, 8, 1, TipoHabilidades.OFENSIVA);
			}
		}
		public Habilidade SocoDemoniaco {
			get {
				return new Habilidade("Soco Demoniaco", "Habilidade de um Monstro", 7, 10, 1, 1, 0, 30, ClasseEnum.TINHODEMON, Main.atributos, 5, 10, 1, TipoHabilidades.OFENSIVA);
			}
		}
		//Driade
		public Habilidade EnforcamentoPorGalho {
			get {
				return new Habilidade("Enforcamento por Galho", "Habilidade de um Monstro", 4, 10, 2, 5, 0, 20, ClasseEnum.DRIADE, Main.atributos, 5, 7, 1, TipoHabilidades.OFENSIVA);
			}
		}
		public Habilidade BencaoDoCarvalho {
			get {
				return new Habilidade("Benção do Carvalho", "Habilidade de um Monstro", 6, 6, 1, 5, 0, 25, ClasseEnum.DRIADE, Main.atributos, 5, 8, 1, TipoHabilidades.HPSP);
			}
		}
		//Zumbi
		public Habilidade Morder {
			get {
				return new Habilidade("Morder", "Habilidade de um Monstro", 4, 10, 1, 1, 0, 6, ClasseEnum.ZUMBI, Main.atributos, 5, 7, 1, TipoHabilidades.OFENSIVA);
			}
		}
		public Habilidade Infectar {
			get {
				return new Habilidade("Infectar", "Habilidade de um Monstro", 6, 8, 1, 1, 0, 9, ClasseEnum.ZUMBI, Main.atributos, 5, 8, 1, TipoHabilidades.OFENSIVA);
			}
		}
		//Succubo
		public Habilidade SugarEnergiaVital {
			get {
				return new Habilidade("Sugar Energia Vital", "Habilidade de um Monstro", 4, 10, 1, 1, 0, 6, ClasseEnum.SUCCUBUS, Main.atributos, 5, 7, 1, TipoHabilidades.OFENSIVA);
			}
		}
		public Habilidade CortarGarganta {
			get {
				return new Habilidade("Cortar Garganta", "Habilidade de um Monstro", 6, 8, 1, 1, 0, 9, ClasseEnum.SUCCUBUS, Main.atributos, 5, 8, 1, TipoHabilidades.OFENSIVA);
			}
		}

	}

	public class Habilidade {
		public string nome;
		public string descricao;
		public int qtyDados;
		public int valorDado;
		public int minDistancia;
		public int maxDistancia;
		public int bonusAcerto;
		public int custoSp;
		public ClasseEnum tipoClasse;
		private Atributos atributos;
		public int custoPontoAcao;
		public int minLevel;
		public int multiplicador;
		public TipoHabilidades tipoHabilidade;
		Equipamento arma;

		public Habilidade(string nome, string descricao, int qtyDados, int valorDado, int minDistancia, int maxDistancia,
			int bonusAcerto, int custoSp, ClasseEnum tipoClasse, Atributos atributos, int custoPontoAcao = 5,
			int minLevel = 0, int multiplicador = 1, TipoHabilidades tipoHabilidade = TipoHabilidades.OFENSIVA) {
			this.nome = nome;
			this.descricao = descricao;
			this.qtyDados = qtyDados;
			this.valorDado = valorDado;
			this.minDistancia = minDistancia;
			this.maxDistancia = maxDistancia;
			this.bonusAcerto = bonusAcerto;
			this.custoSp = custoSp;
			this.tipoClasse = tipoClasse;
			this.custoPontoAcao = custoPontoAcao;
			this.minLevel = minLevel;
			this.multiplicador = multiplicador;
			this.tipoHabilidade = tipoHabilidade;
			this.atributos = atributos;
			arma = atributos.inventario.Arma;
		}

		public bool IsOfensiva {
			get {
				return tipoHabilidade.Equals(TipoHabilidades.OFENSIVA);
			}
		}

		public int Dano {
			get {
				return CalculosUtil.CalculoDano(qtyDados + arma.qtyDadosDano, valorDado + arma.valorDadoDano, atributos.DanoEquipamento, atributos.modificadorPrincipal);
			}
		}

		public int DanoMedio {
			get {
				return CalculosUtil.CalculoDanoMedio(qtyDados + arma.qtyDadosDano, valorDado + arma.valorDadoDano, atributos.DanoEquipamento, atributos.modificadorPrincipal);
			}
		}

		public int Cura {
			get {
				return CalculosUtil.CalculoDado(qtyDados + arma.qtyDadosDano, valorDado + arma.valorDadoDano);
			}
		}

		public int CuraMedia {
			get {
				return CalculosUtil.CalculoDadoMedio(qtyDados + arma.qtyDadosDano, valorDado + arma.valorDadoDano);
			}
		}

		public int Rejuvenescer {
			get {
				return CalculosUtil.CalculoDado(qtyDados + arma.qtyDadosDano, valorDado + arma.valorDadoDano);
			}
		}

		public int RejuvenescerMedio {
			get {
				return CalculosUtil.CalculoDadoMedio(qtyDados + arma.qtyDadosDano, valorDado + arma.valorDadoDano);
			}
		}

	}
}