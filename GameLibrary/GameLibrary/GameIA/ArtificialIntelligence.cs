using UnityEngine;
using Character;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Board;
using EnumUtils;
using Random = UnityEngine.Random;
using System;

namespace GameIA {
	public class ArtificialIntelligence {
		Player main;
		int pontosDeAcao;
		List<PossibleAttack> possibleAttacks;
		List<GameObject> gameObjects;
		Grid grid;
		Dificuldade dificuldade;
		PossibleAttack ataqueEscolhido = null;

		public ArtificialIntelligence(Player main, List<GameObject> playerObjects, Grid grid) {
			this.main = main;
			this.grid = grid;
			this.dificuldade = Utils.ApplicationModel.Dificuldade;
			setOrder(playerObjects);
			pontosDeAcao = main.status.pontosAcao;
			possibleAttacks = new List<PossibleAttack>();
		}

		private void setOrder(List<GameObject> playerObjects) {
			gameObjects = new List<GameObject>();
			BoardSpec boardSpec = BoardSpec.getInstance();
			Square mainSquare = boardSpec.SquareFromWorldPoint(main.transform.position);
			var items = new Dictionary<GameObject, int>();
			foreach (GameObject playerObject in playerObjects) {
				Player target = playerObject.GetComponent<Player>();
				Square targetSquare = boardSpec.SquareFromWorldPoint(target.transform.position);
				int distancia = BoardSpec.GetDistance(mainSquare, targetSquare);
				items.Add(playerObject, distancia);
			}

			foreach (var item in items.OrderBy(i => i.Value)) {
				gameObjects.Add(item.Key);
			}
		}

		public IEnumerator CalcularPossibleAttacks() {
			BoardSpec boardSpec = BoardSpec.getInstance();
			foreach (GameObject gameObject in gameObjects) {
				Player target = gameObject.GetComponent<Player>();
				if (target != null && target.status.isDead)
					continue;
				Square mainSquare = boardSpec.SquareFromWorldPoint(main.transform.position);
				Square targetSquare = boardSpec.SquareFromWorldPoint(target.transform.position);
				int distanciaEstimadaInimigo = BoardSpec.GetSquareDistance(mainSquare, targetSquare);
				//Ataque Basico
				if (target.isAliado)
					CheckPossibleAttack(target, null, distanciaEstimadaInimigo);

				List<Habilidade> habilidades = main.action.Habilidades.habilidades;
				foreach (Habilidade habilidade in habilidades) {
					if (habilidade.custoPontoAcao > pontosDeAcao || habilidade.IsOfensiva && !target.isAliado || habilidade.custoSp > main.status.mana)
						continue;
					if (habilidade.minDistancia == 0 && habilidade.maxDistancia == 0 && target.name != main.name)
						continue;
					CheckPossibleAttack(target, habilidade, distanciaEstimadaInimigo);
				}
				yield return null;
			}
			Pathfinding pathfinding = null;
			List<Vector2> moves = null;
			bool apenasMovimenta = true;
			if (possibleAttacks.Count > 0) {
				apenasMovimenta = false;
				Player target = null;
				int cont = 0;
				int totalCost = 0;
				MyDictionary dict = new MyDictionary();
				PossibleAttack greatestAttack = null;
				foreach (PossibleAttack possibleAttack in possibleAttacks) {
					if (target == null || target.name != possibleAttack.targetName || possibleAttack.isRanged) {
						target = possibleAttack.Target;
						pathfinding = new Pathfinding();
						Vector2 pos = new Vector2();
						if (possibleAttack.isRangedInDanger) {
							pos = RunAndGun(target, possibleAttack.minDist);
						} else if (possibleAttack.isRanged) {
							pos = LongShot(target, possibleAttack.maxDist, possibleAttack.minDist);
						} else {
							pos = target.transform.position;
						}
						pathfinding.FindPath(main.transform.position, pos);
						yield return new WaitUntil(() => pathfinding.ready);
						moves = pathfinding.getMoveInstructions();
						yield return null;
					}
					// verificar se distancia no pathfinder é diferente que distanciaEstimada - 1, pois não conta o quadrado do alvo
					if ((possibleAttack.distanciaEstimada - 1) != moves.Count) {
						possibleAttack.distanciaEstimada = pathfinding.minSize;
						possibleAttack.CalcularCustoPontoAcaoEstimado();
						if (possibleAttack.actionPointCost <= pontosDeAcao) {
							possibleAttack.CalcularCusto(dificuldade);
						} else {
							cont++;
							continue;
						}
					}
					possibleAttack.moves = moves;
					dict.Add(cont, totalCost + 1, totalCost + possibleAttack.cost, possibleAttack, possibleAttack.cost);
					totalCost += possibleAttack.cost;
					cont++;
					if (greatestAttack == null || greatestAttack.cost < possibleAttack.cost)
						greatestAttack = possibleAttack;
				}
				if (dict.Count != 0 && totalCost > 0) {
					yield return null;
					if (dificuldade.Equals(Dificuldade.MEDIO)) {
						bool continueLoop = true;
						int cost = 60;
						while (continueLoop) {
							var listPossibleAttack = dict.Where(myval => myval.Value.cost >= cost);
							if (listPossibleAttack != null && listPossibleAttack.Count() > 0) {
								continueLoop = false;
								dict = new MyDictionary();
								cont = 0;
								int newTotalCost = 0;
								foreach (KeyValuePair<int, MyValue> item in listPossibleAttack.ToList()) {
									dict.Add(cont, newTotalCost + 1, newTotalCost + item.Value.cost, item.Value.possibleAttack, item.Value.cost);
									newTotalCost = item.Value.cost;
									cont++;
								}
								totalCost = newTotalCost;
							} else {
								cost -= 10;
							}
						}
					}
					if (!dificuldade.Equals(Dificuldade.DIFICIL)) {
						int random = Random.Range(1, totalCost);
						try {
							// Pega lista
							IEnumerable<KeyValuePair<int, MyValue>> asd = dict.Where(myval => myval.Value.pos1 <= random && myval.Value.pos2 >= random);
							var itens = asd.ToList();
							// Pega primeiro item da lista, pois a lista possui apenas 1 item
							ataqueEscolhido = itens[0].Value.possibleAttack;
						} catch (Exception e) {
							Debug.Log(e);
							Debug.Log(e.Message);
							Debug.Log(e.Data);
							yield break;
						}
					} else {
						ataqueEscolhido = greatestAttack;
					}
					if (ataqueEscolhido == null) {
						apenasMovimenta = true;
					} else {
						foreach (Vector2 position in ataqueEscolhido.moves) {
							main.move.NpcMove((int) position.x, (int) position.y);
						}
						main.move.Move();
						yield return null;
						if (ataqueEscolhido.isBasicAttack) {
							main.action.Attack(ataqueEscolhido.targetName);
						} else {
							main.action.Ability(ataqueEscolhido.habilidade.nome, ataqueEscolhido.targetName);
						}
						yield return null;
					}
				} else {
					apenasMovimenta = true;
				}
			}
			if (apenasMovimenta) {
				int qtyMoves = -1;
				foreach (GameObject gameObject in gameObjects) {
					Player target = gameObject.GetComponent<Player>();
					if (target != null && (target.status.isDead || !target.isAliado))
						continue;
					pathfinding = new Pathfinding();
					pathfinding.FindPath(main.transform.position, target.transform.position);
					yield return new WaitUntil(() => pathfinding.ready);

					if (qtyMoves == -1 || qtyMoves > pathfinding.minSize) {
						qtyMoves = pathfinding.minSize;
						moves = pathfinding.getMoveInstructions();
					}
					yield return null;
				}
				if (moves != null) {
					foreach (Vector2 position in moves) {
						main.move.NpcMove((int) position.x, (int) position.y);
					}
					main.move.Move();
				}
			}
			yield break;
		}

		private void CheckPossibleAttack(Player enemy, Habilidade habilidade, int distanciaEstimadaInimigo) {
			PossibleAttack possibleAttack = new PossibleAttack(main, enemy, habilidade, distanciaEstimadaInimigo);
			if (possibleAttack.actionPointCost <= pontosDeAcao) {
				possibleAttack.CalcularCusto(dificuldade);
				possibleAttacks.Add(possibleAttack);
			}
		}

		private Vector2 RunAndGun(Player target, int minDist) {
			Vector2 targetPos = target.transform.position;
			List<Vector2> posicoesPossiveis = CirclePos(minDist, targetPos);
			List<GameObject> players = new List<GameObject>(gameObjects.FindAll(gameObject => !gameObject.GetComponent<Player>().status.isDead || gameObject.GetComponent<Player>().isAliado));
			Vector2 idealPos = new Vector2();
			int idealDistance = -1;
			foreach (Vector2 pos in posicoesPossiveis) {
				int cont = players.FindAll(gameObject => BoardSpec.GetSquareDistance(gameObject.transform.position, pos) < minDist).Count;
				if (cont == players.Count) {
					int distance = BoardSpec.GetSquareDistance(pos, main.transform.position);
					if (idealDistance < 0 || distance < idealDistance) {
						idealPos = pos;
						idealDistance = distance;
					}
				}
			}
			if (idealDistance > 0) {
				return idealPos;
			}
			foreach (Vector2 pos in posicoesPossiveis) {
				int distance = BoardSpec.GetSquareDistance(pos, main.transform.position);
				if (idealDistance < 0 || distance < idealDistance) {
					idealPos = pos;
					idealDistance = distance;
				}
			}
			return idealPos;
		}

		private Vector2 LongShot(Player target, int maxDist, int minDist) {
			Vector2 targetPos = target.transform.position;
			int playerToTarget = BoardSpec.GetSquareDistance(main.transform.position, targetPos);
			if (maxDist > playerToTarget) {
				while ((maxDist - playerToTarget) > 2) {
					maxDist--;
				}
			}
			List<Vector2> posicoesPossiveis = CirclePos(maxDist - 1, targetPos);
			List<GameObject> players = new List<GameObject>(gameObjects.FindAll(gameObject => !gameObject.GetComponent<Player>().status.isDead || gameObject.GetComponent<Player>().isAliado));
			Vector2 idealPos = new Vector2();
			int idealDistance = -1;
			foreach (Vector2 pos in posicoesPossiveis) {
				int cont = players.FindAll(gameObject => BoardSpec.GetSquareDistance(gameObject.transform.position, pos) < minDist).Count;
				if (cont == players.Count) {
					int distance = BoardSpec.GetSquareDistance(pos, main.transform.position);
					int distanceTarget = BoardSpec.GetSquareDistance(pos, targetPos);
					if (distanceTarget <= maxDist && (idealDistance < 0 || distance < idealDistance)) {
						idealPos = pos;
						idealDistance = distance;
					}
				}
			}
			if (idealDistance > 0) {
				return idealPos;
			}
			foreach (Vector2 pos in posicoesPossiveis) {
				int distance = BoardSpec.GetSquareDistance(pos, main.transform.position);
				if (idealDistance < 0 || distance < idealDistance) {
					idealPos = pos;
					idealDistance = distance;
				}
			}
			return idealPos;
		}

		private List<Vector2> CirclePos(int radius, Vector2 targetPos) {
			List<Vector2> posicoesPossiveis = new List<Vector2>();
			int qtdPosicoes = radius * 4;
			double slice = 360 / 12;
			BoardSpec boardSpec = BoardSpec.getInstance();
			for (int i = 0; i < qtdPosicoes; i++) {
				double angle = slice * i;
				double newX = targetPos.x + radius * Math.Cos(angle * (Math.PI / 180));
				double newY = targetPos.y + radius * Math.Sin(angle * (Math.PI / 180));
				Vector2 p = new Vector2((int) Math.Round(newX), (int) Math.Round(newY));
				if (p.x < 0 || p.y < 0 || p.x >= boardSpec.columns || p.y >= boardSpec.rows)
					continue;
				posicoesPossiveis.Add(p);
			}
			return posicoesPossiveis;
		}
	}

	public class PossibleAttack {
		Player main;
		public string targetName;
		public Habilidade habilidade { get; private set; }
		public bool isBasicAttack { get; set; }
		public int actionPointCost { get; private set; }
		public int cost { get; private set; }
		public int maxDist { get; private set; }
		public int minDist { get; private set; }
		public int distanciaEstimada { get; set; }
		public List<Vector2> moves;
		public bool moveToAttack { get; private set; }

		public PossibleAttack(Player main, Player target, Habilidade habilidade, int distanciaEstimada) {
			this.main = main;
			this.targetName = target.name;
			this.habilidade = habilidade;
			this.distanciaEstimada = distanciaEstimada;
			this.cost = 0;
			if (habilidade == null) {
				isBasicAttack = true;
				actionPointCost = main.atributos.CustoAtaqueBasico;
				maxDist = main.atributos.DistAtqMax;
				minDist = main.atributos.DistAtqMin;
			} else {
				isBasicAttack = false;
				actionPointCost = habilidade.custoPontoAcao;
				maxDist = habilidade.maxDistancia;
				minDist = habilidade.minDistancia;
			}
			CalcularCustoPontoAcaoEstimado();
		}

		public void CalcularCustoPontoAcaoEstimado() {
			moveToAttack = false;
			if (minDist > distanciaEstimada) {
				moveToAttack = true;
				actionPointCost += Utils.CalculosUtil.CalculoPontosAcaoMovimento(main.atributos.Classe.tipoAtaque, minDist - distanciaEstimada);
			} else if (maxDist < distanciaEstimada) {
				moveToAttack = true;
				actionPointCost += Utils.CalculosUtil.CalculoPontosAcaoMovimento(main.atributos.Classe.tipoAtaque, minDist + distanciaEstimada);
			}
		}

		public void CalcularCusto(Dificuldade dificuldade) {
			//  Habilidade de Cura
			Player target = Target;
			float porcentagemVidaTotal = 100f / (float) target.status.maxVida;
			float porcentagemVidaRestante = target.status.vida * porcentagemVidaTotal;
			if (!isBasicAttack && !habilidade.IsOfensiva) {
				if (habilidade.tipoHabilidade.Equals(TipoHabilidades.HP)) {
					cost = 100 - Mathf.FloorToInt(porcentagemVidaRestante);
					// Se for Fácil, pode acontecer de curar um aliado com pouca necessidade de cura
					if ((dificuldade == Dificuldade.FACIL && cost > 15) || (dificuldade != Dificuldade.FACIL && cost > 50)) {
						float porcentagemVida = (target.status.vida + habilidade.CuraMedia) * porcentagemVidaTotal;
						if (porcentagemVida > 100f)
							porcentagemVida = 100f;
						cost += Mathf.FloorToInt(porcentagemVida) / 2;
					}
				}
			} else {
				float pontoAcerto = CalcularChanceAcerto();
				if (pontoAcerto > 0) {
					int danoMedio;
					if (isBasicAttack) {
						danoMedio = main.atributos.DanoMedio;
					} else {
						danoMedio = habilidade.DanoMedio;
					}
					float porcentagemDano = danoMedio * porcentagemVidaTotal;
					int pontoDano = 0;
					int qtyVidaRestante = Mathf.FloorToInt(porcentagemVidaRestante - porcentagemDano);
					if (qtyVidaRestante <= 0) {
						pontoDano = 50;
					} else {
						pontoDano = Mathf.FloorToInt(porcentagemDano * 100 / porcentagemVidaRestante);
					}
					cost = Mathf.FloorToInt(pontoAcerto + pontoDano / 2);
				}
			}
		}

		private float CalcularChanceAcerto() {
			Player target = Target;
			int restoEsquiva = target.atributos.CA - target.atributos.AcertoBase;
			if (!isBasicAttack) {
				restoEsquiva -= habilidade.bonusAcerto;
			}
			if (restoEsquiva > 20) {
				return 0f;
			} else if (restoEsquiva <= 0) {
				return 100f;
			}
			return restoEsquiva * 100f / 20f;
		}

		public bool CheckAttack(int distancia) {
			return maxDist < distancia || minDist > distancia;
		}

		public Player Target {
			get {
				return GameObject.Find(targetName).GetComponent<Player>();
			}
		}

		public bool isRangedInDanger {
			get {
				return moveToAttack && minDist > 1 && minDist > distanciaEstimada;
			}
		}

		public bool isRanged {
			get {
				return minDist > 1 || maxDist > 1;
			}
		}

	}

	public struct MyValue {
		public int pos1;
		public int pos2;
		public PossibleAttack possibleAttack;
		public int cost;
	}
	public class MyDictionary : Dictionary<int, MyValue> {
		public void Add(int key, int value1, int value2, PossibleAttack possibleAttack, int cost) {
			MyValue val;
			val.pos1 = value1;
			val.pos2 = value2;
			val.possibleAttack = possibleAttack;
			val.cost = cost;
			this.Add(key, val);
		}
	}

}
