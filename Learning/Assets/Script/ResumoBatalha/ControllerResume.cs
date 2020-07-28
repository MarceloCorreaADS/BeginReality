using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using Utils;

public class ControllerResume : MonoBehaviour {
	public Button button;
	public BattleReportEntity resumeBattle;
	MapMethods mapMethods;

	void Start() {
		gameObject.AddComponent<MapMethods>();
		mapMethods = GetComponent<MapMethods>();
		button.onClick.AddListener(() => Show());
	}

	void Show() {
		Transform objetos = GameObject.Find("Canvas/RightObjects").transform;
		Transform codText = objetos.GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(0);

		objetos.GetChild(0).GetComponent<Image>().sprite = mapMethods.mapSprite(resumeBattle.Map, resumeBattle.BossBattle);
		objetos.GetChild(1).GetComponent<Text>().text = setDescBattle();
		objetos.GetChild(2).GetComponent<Text>().text = setRelBattle();
		codText.GetComponent<Text>().text = setCodBattle();
		StartCoroutine(Gambi());
	}

	string setCodBattle() {
		string cod = "";
		foreach (string c in resumeBattle.PlayerActions) {
			cod += c + "\n----------\n";
		}
		
		if (cod == "") {
			cod += "Nenhum código foi usado nesta batalha!";
		}

		return cod;
	}
	string setRelBattle() {
		string rel = "";

		return rel;
	}
	string setDescBattle() {
		string desc = "";

		desc += "Batalha: " + resumeBattle.BattleNumber + "\n";
		desc += "Mapa: " + mapMethods.mapName(resumeBattle.Map, false) + "\n";
		desc += (resumeBattle.BossBattle ? "Batalha Contra Chefe\n" : "");
		desc += "Data inicio: " + resumeBattle.DateBattleStart + "\n";
		desc += "Data Fim: " + resumeBattle.DateBattleEnd + "\n";
		desc += "Turnos: " + resumeBattle.QtyTurns + "\n";
		desc += "Resultado: " + (resumeBattle.Victory ? "Vitória" : "Derrota") + "\n";
		
		return desc;
	}
	private IEnumerator Gambi() {
		yield return null;
		GameObject content = GameObject.Find("Canvas/RightObjects/CodeDesc/InfoBoxScroll/Viewport/Content");
		Transform desc = content.transform.GetChild(0);
        content.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(desc.GetComponent<RectTransform>().sizeDelta.x, desc.GetComponent<RectTransform>().sizeDelta.y);
		yield break;
	}
}
