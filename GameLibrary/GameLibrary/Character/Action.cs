using System;
using UnityEngine;
using System.Collections;
using Utils;


namespace Character {
	public class Action {
		private Player Main;
		private Player Target;
		private Habilidades habilidades = null;
		public Habilidades Habilidades {
			get {
				if (habilidades == null)
					habilidades = new Habilidades(Main);
				return habilidades;
			}
			private set {
				habilidades = value;
			}
		}


		private int ModificadorAcerto;
		private int ModificadorEsquiva;
		private Board.Grid grid;
		private ConsoleButtons console;
		private Simulator simulator;
		private ActionOrder actionOrder;
		private GameObject gameManager;

		public Action(Player main) {
			this.Main = main;
			this.console = GameObject.Find("ConsoleButtons").GetComponent<ConsoleButtons>();
			actionOrder = ActionOrder.getInstance();
			gameManager = GameManagerUtil.Instance.GameManager;
			simulator = Simulator.getInstance();

		}

		private int Distancia {
			get {
				int contX = (int) Mathf.Abs(Target.transform.position.x - Main.transform.position.x);
				int contY = (int) Mathf.Abs(Target.transform.position.y - Main.transform.position.y);
				return contX + contY;
			}
		}

		private GameObject setTarget(string targetName) {
			return GameObject.Find(targetName);
		}

		public void Attack(string targetName) {
			GameObject target = setTarget(targetName);
			attack(target);
		}
		public void Attack() {
			int order = actionOrder.addOrder();
			new Task(this.attackAnyDir(order), true);
		}

		private IEnumerator attackAnyDir(int order) {
			yield return new WaitUntil(() => order == actionOrder.ActualOrder);
			setGrid();
			Player targetP = null;
			Board.BoardSpec boardSpec = Board.BoardSpec.getInstance();
			foreach (Board.Square square in boardSpec.GetNeighbours(boardSpec.SquareFromWorldPoint(Main.transform.position))) {
				if (square != null && square.character != null && square.character.tag != Main.tag) {
					if (targetP == null) {
						targetP = square.character.GetComponent<Player>();
					}
					if (targetP != null) {
						new Task(this.attack(null, targetP.gameObject, order), true);
						yield break;
					}
				}
			}
			actionOrder.ActualOrder++;
		}

		public void AttackUp() {
			int x = 0, y = 1;
			int order = actionOrder.addOrder();
			new Task(this.attackDir(x, y, order), true);
		}

		public void AttackDown() {
			int x = 0, y = -1;
			int order = actionOrder.addOrder();
			new Task(this.attackDir(x, y, order), true);
		}

		public void AttackLeft() {
			int x = -1, y = 0;
			int order = actionOrder.addOrder();
			new Task(this.attackDir(x, y, order), true);
		}

		public void AttackRight() {
			int x = 1, y = 0;
			int order = actionOrder.addOrder();
			checkSim(x, y);
			new Task(this.attackDir(x, y, order), true);
		}

		private IEnumerator attackDir(float x, float y, int order) {
			yield return new WaitUntil(() => order == actionOrder.ActualOrder);
			GameObject target = getTargetFromMainPos(Main.transform.position.x + x, Main.transform.position.y + y);
			if (target == null) {
				actionOrder.ActualOrder++;
				yield break;
			}
			new Task(this.attack(null, target, order), true);
		}

		private void attack(GameObject target) {
			if (target == null) {
				return;
			}
			int order = actionOrder.addOrder();
			new Task(this.attack(null, target, order), true);
		}

		private Habilidade getHabilidade(string abilityName) {
			if (abilityName == null || abilityName.Length == 0)
				return null;
			Habilidade habilidade = Habilidades.getHabilidadeByMethodName(abilityName);
			try {
				if (habilidade.tipoClasse == Main.classe) {
					if (habilidade.minLevel <= Main.atributos.Level) {
						return habilidade;
					}
					ConsoleReport.Log("Seu nível é muito baixo para utilizar esta Habilidade", ConsoleType.BATTLE);
				} else {
					ConsoleReport.Log("A classe " + Main.classe.ToString() + " não pode utilizar esta Habilidade!", ConsoleType.BATTLE);
				}
			} catch (NullReferenceException) {
				ConsoleReport.Log("Habilidade não encontrada!", ConsoleType.BATTLE);
			}
			return null;
		}
		public void Ability(string abilityName, string targetName) {
			GameObject target = setTarget(targetName);
			ability(abilityName, target);
		}

		private void ability(string abilityName, GameObject target) {
			if (target == null) {
				return;
			}
			int order = actionOrder.addOrder();
			new Task(this.attack(abilityName, target, order), true);
		}

		private IEnumerator attack(string abilityName, GameObject target, int order) {
			yield return new WaitUntil(() => order == actionOrder.ActualOrder);
			if (Main.status.acao > 0 && Main.status.pontosAcao >= 5) {
				if (MainIsDead) {
					ConsoleReport.Log("PERSONAGEM ATACANTE ESTÁ MORTO!", ConsoleType.BATTLE);
					actionOrder.ActualOrder++;
					yield break;
				}
				this.Target = target.GetComponent<Player>();
				this.ModificadorEsquiva = Target.atributos.CA;
				if (TargetIsDead) {
					ConsoleReport.Log("O ALVO SELECIONADO ESTÁ MORTO!", ConsoleType.BATTLE);
					actionOrder.ActualOrder++;
					yield break;
				}
				int maxDistancia = Main.atributos.DistAtqMax;
				int minDistancia = Main.atributos.DistAtqMin;
				int bonusAcerto = 0;
				int custoPontosAcao = Main.atributos.CustoAtaqueBasico;
				Habilidade habilidade = getHabilidade(abilityName);
				if (habilidade != null) {
					ConsoleReport.LogBattle(habilidade.nome);
					maxDistancia = habilidade.maxDistancia;
					minDistancia = habilidade.minDistancia;
					bonusAcerto = habilidade.bonusAcerto;
					custoPontosAcao = habilidade.custoPontoAcao;
				} else if (abilityName != null && abilityName.Length > 0) {
					yield break;
				}
				if (verificaDistancia(maxDistancia, minDistancia)) {
					yield break;
				}
				if (habilidade != null && !Main.status.UseSp(habilidade.custoSp)) {
					ConsoleReport.Log("VOCÊ NÃO TEM SP SUFICIENTE PARA UTILIZAR ESTÁ HABILIDADE! SEU SP: " + Main.status.mana + " | SP NECESSÁRIA: " + habilidade.custoSp, ConsoleType.BATTLE);
					actionOrder.ActualOrder++;
					yield break;
				}
				this.Main.status.acao--;
				this.Main.status.pontosAcao -= custoPontosAcao;
				int damage = 0;
				bool isOfensiva = true;
				if (habilidade != null) {
					isOfensiva = habilidade.IsOfensiva;
					if (habilidade.tipoHabilidade.Equals(TipoHabilidades.HP)) {
						this.Target.status.Heal(habilidade.Cura);
					} else if (habilidade.tipoHabilidade.Equals(TipoHabilidades.SP)) {
						this.Target.status.Rejuvenate(habilidade.Rejuvenescer);
					} else if (habilidade.tipoHabilidade.Equals(TipoHabilidades.HPSP)) {
						this.Target.status.Heal(habilidade.Cura);
						this.Target.status.Rejuvenate(habilidade.Rejuvenescer);
					} else if (habilidade.tipoHabilidade.Equals(TipoHabilidades.OFENSIVA)) {
						damage = habilidade.Dano;
					}
				}
				if (isOfensiva) {
					this.ModificadorAcerto = Main.atributos.AcertoBase + bonusAcerto;
					if (hit) {
						if (damage.Equals(0)) {
							damage = Main.atributos.Dano;
						}
						if (damage < 0) {
							damage = 0;
						}
						ConsoleReport.Log("ModificadorAcerto: " + ModificadorAcerto + "| ModificadorEsquiva: " + ModificadorEsquiva + "| DANO: " + damage, ConsoleType.BATTLE);
						Main.status.attBarras();
						this.Target.status.Damage(damage);
					} else {
                        SoundManager.instance.Sfx("Miss");
                        ConsoleReport.Log("Errou", ConsoleType.BATTLE);
						ConsoleReport.Log("ModificadorAcerto: " + ModificadorAcerto + "| ModificadorEsquiva: " + ModificadorEsquiva, ConsoleType.BATTLE);
						Target.GetComponent<EffectsPlayer>().show(tipoEffect.DANO, "Errou", Target.GetComponent<Transform>().position);
					}
                    Main.status.attBarras();
				}
			}
			actionOrder.ActualOrder++;
			yield break;
		}

		private GameObject getTargetFromMainPos(float x, float y) {
			setGrid();
			if (x < 0 || y < 0) {
				return null;
			}
			return this.grid.getCharacterObject(x, y);
		}

		private void setGrid() {
			if (this.grid == null) {
				this.grid = gameManager.GetComponent<Board.Grid>();
			}
		}

		private bool MainIsDead {
			get {
				return Main.status.isDead;
			}
		}

		private bool TargetIsDead {
			get {
				return Target.status.isDead;
			}
		}

		private bool hit {
			get {
				return (ModificadorAcerto) > (ModificadorEsquiva);
			}
		}

		private void checkSim(int x, int y) {
			return;
			try {
				if (simulator.QtySimulacoes == 0)
					return;
				Vector2 targetPos = Main.move.getEnd(x, y, Main.move.getStart);
				GameObject target = getTargetFromMainPos(targetPos.x, targetPos.y);
				simulator.Check(TipoSimulacao.ATAQUE, target != null);
			} catch (Exception) {
				Debug.Log("Action - Erro ao achar Simulator.");
			}
		}

		private bool verificaDistancia(int maxDistancia, int minDistancia) {
			if (Distancia > maxDistancia) {
				ConsoleReport.Log("VOCÊ ESTÁ MUITO LONGE PARA ACERTAR! DISTANCIA: " + Distancia, ConsoleType.BATTLE);
				actionOrder.ActualOrder++;
				return true;
			}
			if (Distancia < minDistancia) {
				ConsoleReport.Log("VOCÊ ESTÁ MUITO PERTO PARA ACERTAR! DISTANCIA: " + Distancia, ConsoleType.BATTLE);
				actionOrder.ActualOrder++;
				return true;
			}
			if (Distancia < minDistancia) {
				ConsoleReport.Log("VOCÊ ESTÁ MUITO PERTO PARA ACERTAR! DISTANCIA: " + Distancia, ConsoleType.BATTLE);
				actionOrder.ActualOrder++;
				return true;
			}
			return false;
		}

		//public void setHabilidades() {
		//    if (habilidades == null)
		//        habilidades = new Habilidades(Main);
		//}
	}
}