using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Movements;
using EnumUtils;
using Utils;

namespace Character {
	public class Player : MonoBehaviour {

		public Atributos atributos;
		public Status status;
		public Move move;
		public int cont = 0;
		public int p = 1;
		public Sprite playerFace;
		public ClasseEnum classe;
		public List<Equipamento> Equipamentos;
		public bool turnFinished = false;
		private Action playerAction;
		public bool isAliado;
		public static int qtyAlly = 0;
		public static int qtyEnemy = 0;
		private int orderAlly = 0;
		private int orderEnemy = 0;
		public GameObject InfoPlayer;

		void Awake() {
			this.gameObject.AddComponent<Status>();
			this.gameObject.AddComponent<Move>();
			this.atributos = new Atributos(classe);
			this.Equipamentos = new List<Equipamento>();
			this.move = GetComponent<Move>();
			this.move.blockingLayer = LayerMask.GetMask(LayerMask.LayerToName(8));
			this.status = GetComponent<Status>();
			this.InfoPlayer = Resources.Load<GameObject>("InfoBox");
			isAliado = true;
			if (gameObject.tag == "Enemy") {
				isAliado = false;
			}
			gameObject.name = name;
			gameObject.AddComponent<EffectsPlayer>();
			qtyAlly = 0;
			qtyEnemy = 0;
			orderAlly = 0;
			orderEnemy = 0;
		}
		// Use this for initialization
		void Start() {
			if (gameObject.scene.name == "Battle") {
				if (isAliado) {
					qtyAlly = qtyAlly + 1;
					if (orderAlly == 0)
						orderAlly = qtyAlly;
					status.order = orderAlly;
				} else {
					qtyEnemy++;
					if (orderEnemy == 0) 
						orderEnemy = qtyEnemy;
					status.order = orderEnemy;
				}

				setLifeObject();
			}

		}

        public int getAllys() {
			return qtyAlly;
		}

		public int getEnemies() {
			return qtyEnemy;
		}

		private void setLifeObject() {
			int order = 0;
			string objetoLocal = "";

			if (gameObject.tag == "Enemy") {
				objetoLocal = "Canvas/InfoEnemy/InfoBackgroundEnemy";
				order = orderEnemy;
			} else {
				objetoLocal = "Canvas/InfoAlly/InfoBackgroundAlly";
				order = orderAlly;
			}

			RectTransform InfoB = (RectTransform) GameObject.Find(objetoLocal).transform;
			InfoB.sizeDelta = new Vector2(130, 47 * order);

			Vector3 infoPosition = transform.position;
			InfoPlayer = Instantiate(InfoPlayer, infoPosition, Quaternion.identity) as GameObject;
			InfoPlayer.transform.SetParent(GameObject.Find(objetoLocal).transform);
			InfoPlayer.transform.localPosition = new Vector3(73, (-45 * order), 0);
			InfoPlayer.transform.localScale = new Vector3(1, 1, 1);

			Transform Character = InfoPlayer.transform.GetChild(0);
			Transform HpBar = InfoPlayer.transform.GetChild(1);
			Transform SpBar = InfoPlayer.transform.GetChild(2);

			if (gameObject.tag == "Ally") {
				InfoPlayer.transform.GetChild(0).gameObject.AddComponent<Button>();
				InfoPlayer.transform.GetChild(0).gameObject.AddComponent<ShowAbility>();
				InfoPlayer.transform.GetChild(0).gameObject.GetComponent<ShowAbility>().button = InfoPlayer.transform.GetChild(0).gameObject.GetComponent<Button>();
				InfoPlayer.transform.GetChild(0).gameObject.GetComponent<ShowAbility>().habilidades = action.Habilidades.getHabilidadesUtilizaveis();
			}

				Character.transform.GetChild(0).GetComponent<Text>().text = gameObject.name + " - Level :" + atributos.Level;
			Character.GetComponent<Image>().sprite = playerFace;

			transformBar(HpBar, status.vida, status.maxVida);
			transformBar(SpBar, status.mana, status.maxMana);

			InfoPlayer.name = "infoPlayer" + gameObject.name;
		}

		private void transformBar(Transform objeto, int valorAtual, int valorMax) {
			objeto.transform.GetChild(0).transform.localPosition = new Vector3(0, 0, 0);
			objeto.transform.GetChild(0).transform.localScale = new Vector3(1, 1, 1);
			objeto.transform.GetChild(1).transform.localPosition = new Vector3(0, 0, 0);
			objeto.transform.GetChild(1).GetComponent<Text>().text = valorAtual + "/" + valorMax;
		}

		private Text textHp {
			get {
				return this.InfoPlayer.transform.GetChild(1).transform.GetChild(1).GetComponent<Text>();
			}
		}

		private Transform transformHp {
			get {
				return this.InfoPlayer.transform.GetChild(1).transform.GetChild(0).GetComponent<Transform>();
			}
		}

		private Text textSp {
			get {
				return this.InfoPlayer.transform.GetChild(2).transform.GetChild(1).GetComponent<Text>();
			}
		}

		private Transform transformSp {
			get {
				return this.InfoPlayer.transform.GetChild(2).transform.GetChild(0).GetComponent<Transform>();
			}
		}

		public Action action {
			get {
				if (playerAction == null) {
					playerAction = new Action(this);
				}
				return playerAction;
			}
		}
	}
}