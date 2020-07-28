using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Character;
using Utils;
using System.Collections;

namespace Reward {
	public class RewardController : MonoBehaviour {
		public GameObject rewardObject;
		public GameObject rewardComponent;
		public Text text;
		public Text rewardTitle;
		public Text txtName;
		public Text level;
		public Text xp;
		public Text byteCoin;
		public Image backGround1;

		public void End(bool victory) {
			text.text = victory ? "Você Venceu!" : "Você Perdeu!";
			ApplicationModel.EndBattle = true;
			ApplicationModel.Victory = victory;
			text.enabled = true;
			rewardTitle.enabled = true;
			txtName.enabled = true;
			level.enabled = true;
			xp.enabled = true;
			byteCoin.enabled = true;
			backGround1.enabled = true;
			setRewards();
			new Task(Wait(victory), true);
		}

		private IEnumerator Wait(bool victory) {
			yield return new WaitForSeconds(5F);
			StartCoroutine(LoadScene.LoadWorldMap());
			yield break;
		}

		private void AddReward(string name, string level, string xp, string byteCoin) {
			GameObject newRewardObject = Instantiate(rewardObject, new Vector3(0, 0), Quaternion.identity) as GameObject;
			newRewardObject.name = "RewardItem";
			newRewardObject.transform.SetParent(rewardComponent.transform);
			newRewardObject.transform.localScale = new Vector3(1, 1);
			RewardItem rewardItem = newRewardObject.GetComponent<RewardItem>();
			rewardItem.setReward(name, level, xp, byteCoin);
		}

		private void setRewards() {
			List<GameObject> enemyObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
			List<LevelScalator> levelList = new List<LevelScalator>();
			if (ApplicationModel.Victory) {
				foreach (GameObject enemyObject in enemyObjects) {
					Player player = enemyObject.GetComponent<Player>();
					int xp = player.atributos.Classe.xp;
					int level = player.atributos.Level;
					xp += Mathf.FloorToInt(level * 0.5f);
					levelList.Add(new LevelScalator(xp, level, player.atributos.ByteCoin));
				}
			}

			List<GameObject> allyObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Ally"));
			foreach (GameObject allyObject in allyObjects) {
				Player player = allyObject.GetComponent<Player>();
				string name = player.name;
				int levelAtual = player.atributos.Level;
				int xpAtual = player.atributos.Experiencia;
				int byteCoinAtual = player.atributos.ByteCoin;

				int xpGanha = 0;
				int byteCoinGanha = 0;

				string levelStr = levelAtual.ToString();
				int xpFinal = xpAtual;
				int levelFinal = levelAtual;
				if (ApplicationModel.Victory) {
					foreach (LevelScalator levelScalator in levelList) {
						int xp = levelScalator.xp;
						int diferencaLevel = levelAtual - levelScalator.level;
						if (diferencaLevel > 0) {
							xp = xp - diferencaLevel;
							if (xp < 1)
								xp = 1;
						}
						xpGanha += xp;
						byteCoinGanha += levelScalator.byteCoin;
					}

					if (player.status.isDead) {
						xpGanha = Mathf.CeilToInt(xpGanha / 2);
						byteCoinGanha = Mathf.CeilToInt(byteCoinGanha / 2);
					}

					//fixo 10 de xp por ganhar a batalha
					xpFinal += xpGanha + 10;
					if (xpFinal >= 100) {
						xpFinal = 0;
						levelFinal++;
						if (levelFinal <= 10) {
							levelStr += " ~> " + levelFinal;
						}
					}
				} else {
					xpFinal -= Mathf.FloorToInt(xpFinal * 30 / 100);
					byteCoinGanha -= Mathf.FloorToInt(byteCoinAtual * 30 / 100);
				}
				int byteCoinFinal = byteCoinAtual + byteCoinGanha;

				string xpStr = xpAtual + " ~> " + xpFinal;
				string byteCoinStr = byteCoinAtual + " ~> " + byteCoinFinal;
				player.atributos = new Atributos(levelFinal, xpFinal, byteCoinFinal, player.atributos.tipoClasse, player.atributos.inventario.Equipamentos);
				AddReward(name, levelStr, xpStr, byteCoinStr);
			}
		}
	}

	class LevelScalator {
		public int xp;
		public int level;
		public int byteCoin;

		public LevelScalator(int xp, int level, int byteCoin) {
			this.xp = xp;
			this.level = level;
			this.byteCoin = byteCoin;
		}
	}


}
