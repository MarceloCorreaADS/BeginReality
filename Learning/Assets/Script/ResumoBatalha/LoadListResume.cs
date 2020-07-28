using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Utils;

public class LoadListResume : MonoBehaviour {
	MapMethods mapMethods;
	// Use this for initialization
	void Start () {
		gameObject.AddComponent<MapMethods>();
		mapMethods = GetComponent<MapMethods>();

		GameObject content = gameObject.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
        BattleReportList resumeBattle = new BattleReportList();
		resumeBattle = SaveLoadController.GetInstance().Load(SaveTypes.BATTLEREPORT) as BattleReportList;
		if (resumeBattle != null) {
			int count = 0;
			Debug.Log(resumeBattle.BattleReportEntityList.Count);
			for (int i = resumeBattle.BattleReportEntityList.Count;  i > 0; i--) { 
				content.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(213, 75 + ((count) * 50));
				instanciaObjetoLista(0, 0, 123, 25, Resources.Load<GameObject>("GenericButton"), content.transform, resumeBattle.BattleReportEntityList[i-1], count);
				count++;
			}
		}
	}
	void instanciaObjetoLista(float posX, int posY, int width, int height, GameObject prefab, Transform pai, BattleReportEntity resumeBattle, int index) {
		GameObject objeto;
		objeto = Instancia(posX, posY, width, height, prefab, pai);
		objeto.transform.GetChild(0).GetComponent<Text>().text = setNameBattle(resumeBattle);
		objeto.AddComponent<ControllerResume>();
		objeto.GetComponent<ControllerResume>().button = objeto.GetComponent<Button>();
		objeto.GetComponent<ControllerResume>().resumeBattle = resumeBattle;

		objeto.name = "Batalha"+resumeBattle.BattleNumber;
	}
	public string setNameBattle(BattleReportEntity resume) {
		string name = "";

		name += "Batalha: " + resume.BattleNumber ;
		name += " - " + mapMethods.mapName(resume.Map, false);
		name += " - " + resume.DateBattleStart;
		name += " - " + (resume.Victory?"Vitória":"Derrota");

		return name;
	}

	GameObject Instancia(float posX, int posY, int width, int height, GameObject prefab, Transform pai) {
		GameObject objeto;
		Vector3 infoPosition = transform.position;
		objeto = Instantiate(prefab, infoPosition, Quaternion.identity) as GameObject;
		objeto.transform.SetParent(pai);
		objeto.transform.localPosition = new Vector3(posX, posY, 1);
		objeto.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
		objeto.transform.localScale = new Vector3(1, 1, 1);

		objeto.name = prefab.name;

		return objeto;
	}
}
