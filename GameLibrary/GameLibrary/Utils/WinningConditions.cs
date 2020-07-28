using UnityEngine;
using System.Collections.Generic;
using Character;

namespace Utils {
    public enum Conditions { KILL_EVERYONE, KILL_BOSS, ALL_TEAM_ALIVE, DESTROY_OBJECTIVE, IF, FOR, WHILE }

    public class WinningConditions : MonoBehaviour {
        [SerializeField]
        public List<WinningCondition> winningConditions;
        public int qtyObrigatory;
        public int totalCompleted;
		public Reward.RewardController rewardController;
		public bool endGame = false;

        public void addCondition(Conditions condition = Conditions.KILL_EVERYONE, int quantity = 0, bool obrigatory = true) {
            if (obrigatory)
                qtyObrigatory++;
            winningConditions.Add(new WinningCondition(condition, quantity, obrigatory));
        }

        public bool checkVictory() {
            int qty = winningConditions.FindAll(winningCondition => winningCondition.obrigatory == true && winningCondition.complete == true).Count;
            if (qty == qtyObrigatory) {
                gameEnd(true);
                return true;
            }
            List<GameObject> allysObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Ally"));
            List<Player> allyPlayers = new List<Player>();
            allysObjects.ForEach(allyObject => allyPlayers.Add(allyObject.GetComponent<Player>()));
            List<Player> deadAllies = allyPlayers.FindAll(player => player.status.isDead);
            if (deadAllies != null && deadAllies.Count == allysObjects.Count) {
                gameEnd(false);
                return true;
            }
            return false;
        }

		public void setLose() {
			gameEnd(false);
		}

        private void gameEnd(bool isPlayerVictory) {
			if (!endGame) {
				endGame = true;
				rewardController.End(isPlayerVictory);
			}
        }

        public void checkCondition() {
            foreach (WinningCondition winningCondition in winningConditions) {
                List<GameObject> enemyObjects = new List<GameObject>();
                switch (winningCondition.Condition) {
                    case Conditions.KILL_EVERYONE:
                        enemyObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
                        List<Player> enemyPlayers = new List<Player>();
                        enemyObjects.ForEach(enemyObject => enemyPlayers.Add(enemyObject.GetComponent<Player>()));
                        List<Player> deadEnemies = enemyPlayers.FindAll(player => player.status.isDead);
                        if (deadEnemies.Count > 0 && deadEnemies[0].getEnemies() == deadEnemies.Count) {
                            winningCondition.complete = true;
                        }
                        break;
                    case Conditions.KILL_BOSS:
                        enemyObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
                        Player enemyPlayer = null;
                        enemyObjects.ForEach(enemyObject => {
                            if (enemyObject.GetComponent<Player>().status.isBoos)
                                enemyPlayer = enemyObject.GetComponent<Player>();
                        });
                        if (enemyPlayer.status.vida == 0)
                            winningCondition.completeQuantity++;
                        break;
                    case Conditions.DESTROY_OBJECTIVE:
                        break;
                    case Conditions.ALL_TEAM_ALIVE:
                        List<GameObject> allysObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Ally"));
                        List<Player> allyPlayers = new List<Player>();
                        allysObjects.ForEach(allyObject => allyPlayers.Add(allyObject.GetComponent<Player>()));
                        List<Player> deadAllies = allyPlayers.FindAll(player => player.status.isDead);
                        if (deadAllies.Count > 0) {
                            winningCondition.complete = false;
                            gameEnd(false);
                            return;
                        }
                        break;
                }
                if (winningCondition.quantity > 0 && winningCondition.quantity <= winningCondition.completeQuantity)
                    winningCondition.complete = true;
            }
        }
    }

    [System.Serializable]
    public class WinningCondition {
        public Conditions condition;
        public int quantity;
        public int completeQuantity;
        public bool obrigatory;
        public bool complete;

        public WinningCondition() {
            condition = Conditions.KILL_EVERYONE;
            quantity = 0;
            completeQuantity = 0;
            obrigatory = true;
            complete = false;
        }

        public WinningCondition(Conditions condition = Conditions.KILL_EVERYONE, int quantity = 0, bool obrigatory = true) {
            this.condition = condition;
            this.quantity = quantity;
            completeQuantity = 0;
            if (condition == Conditions.KILL_BOSS || condition == Conditions.DESTROY_OBJECTIVE) {
                obrigatory = true;
            }
            this.obrigatory = obrigatory;
            complete = false;
            if (condition == Conditions.ALL_TEAM_ALIVE)
                complete = true;
        }

        public Conditions Condition {
            get {
                return condition;
            }
            private set {
                condition = value;
            }
        }
        public int Quantity {
            get {
                return quantity;
            }
            private set {
                quantity = value;
            }
        }
        public bool Obrigatory {
            get {
                return obrigatory;
            }
            private set {
                obrigatory = value;
            }
        }
        public bool Complete {
            get {
                return complete;
            }
            set {
                complete = value;
            }
        }

        public void checkQuantity() {
            if (quantity > 0 && completeQuantity >= quantity) {
                complete = true;
            }
        }
    }
}
