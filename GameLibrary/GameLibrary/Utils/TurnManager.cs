using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Character;

namespace Utils {
    public class TurnManager : MonoBehaviour {
        public List<String> moves;
        public String teamTurn;
        public bool turnActive;
        [SerializeField]
        public List<Player> players = new List<Player>();
        public WinningConditions winningConditions;

        public Player getPlayer(String playerName) {
            Player player = null;
            String objectName = moves[0];
            GameObject gameObject = getGameObject(playerName);
            if (objectName == "Ally" && gameObject != null) {
                this.teamTurn = objectName;
                this.turnActive = true;
				Debug.Log("batatadoce");
				player = gameObject.GetComponent<Player>();
                player.turnFinished = false;
                player.move.resetParameters("iwanttoresetmove");
				if (players.Find(p => p.name == player.name) == null)
					players.Add(player);
            }
            return player;
        }

        private GameObject getFirstGameObject() {
            return GameObject.Find(moves[0]) as GameObject;
        }

        private GameObject getGameObject(String objectName) {
            return GameObject.Find(objectName) as GameObject;
        }

        public void EnemyTurn() {
            String objectName = moves[0];
            if (objectName == "Enemy") {
                enemyAction();
            }
        }

        private void enemyAction() {
            this.teamTurn = moves[0];
            List<GameObject> enemyObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag(teamTurn));
            new Task(EnemyTurn(enemyObjects), true);
        }

        private IEnumerator EnemyTurn(List<GameObject> enemyObjects) {
            List<GameObject> playerObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
            playerObjects.AddRange(GameObject.FindGameObjectsWithTag("Ally"));
            foreach (GameObject enemyObject in enemyObjects) {
                Player enemy = enemyObject.GetComponent<Player>();
				if (players.Find(p => p.name == enemy.name) == null)
					players.Add(enemy);
                enemy.move.resetParameters("iwanttoresetmove");
				winningConditions.checkCondition();
				if (winningConditions.checkVictory()) {
					break;
				}
				if (!enemy.status.isDead) {
					GameIA.ArtificialIntelligence gameIa = new GameIA.ArtificialIntelligence(enemy, playerObjects, GetComponent<Board.Grid>());
					Task artificialIntelligenceTask = null;
					try {
						artificialIntelligenceTask = new Task(gameIa.CalcularPossibleAttacks(), true);
					} catch (Exception) {
						Debug.Log("Ta dando erro na IA");
					}
					yield return new WaitWhile(() => artificialIntelligenceTask.Running);
				}
				this.turnActive = true;
			}
            Task task = new Task(NextTurn(), true);
			yield return new WaitWhile(() => task.Running);
			this.teamTurn = null;
			this.turnActive = false;
			yield break;
        }

        public void next() {
			ActionOrder.getInstance().resetActionOrder();
            String name = moves[0];
            this.moves.Remove(name);
            this.moves.Add(name);
			Debug.Log("alahuakbar");
            if (winningConditions.checkVictory()) {
                return;
            }
            String objectName = moves[0];
            if (objectName == "Enemy") {
                enemyAction();
            }
        }

        public IEnumerator NextTurn() {
			ActionOrder actionOrder = ActionOrder.getInstance();
			yield return new WaitWhile(() => actionOrder.QtyOrder > actionOrder.ActualOrder);
            if (players != null && players.Count > 0) {
                winningConditions.checkCondition();
                foreach (Player player in players) {
                    player.status.resetActionPoints();
                    player.turnFinished = true;
                }
            }
            next();
        }
    }
}

