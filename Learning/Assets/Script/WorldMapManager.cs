using UnityEngine;
using Utils;
using Tiled2Unity;

public class WorldMapManager : MonoBehaviour {

	GameObject scenario = null;
	GameObject programmer = null;

	void Awake() {
		if (ApplicationModel.MapToLoadType.Equals(MapToLoadType.NEXT))
			ApplicationModel.CurrentWorldMap++;
		else if (ApplicationModel.MapToLoadType.Equals(MapToLoadType.PREVIUS))
			ApplicationModel.CurrentWorldMap--;
		ApplicationModel.BalanceDificult();
		scenario = Resources.Load<GameObject>("WorldMap/Scenario" + ApplicationModel.CurrentWorldMap);
		TiledMap tileMap = scenario.GetComponent<TiledMap>();
		scenario = Instantiate(scenario, new Vector3(0, tileMap.MapHeightInPixels / 100f, 0), Quaternion.identity) as GameObject;
		scenario.name = "Scenario" + ApplicationModel.CurrentWorldMap;
		// Se voltar de uma batalha contra boss e perder, volta para inicio do mapa.
		SaveLoadController saveLoadController = SaveLoadController.GetInstance();
		BattleReportList battleReportList = saveLoadController.Load(SaveTypes.BATTLEREPORT) as BattleReportList;
		if (battleReportList != null) {
			int battleNumber = UserLogged.GetInstance().battlesFought;
			BattleReportEntity battleReportEntity = battleReportList.getByBattleNumber(battleNumber);
			if (ApplicationModel.EndBattle) {
				battleReportEntity.Victory = ApplicationModel.Victory;
				battleReportEntity.DateBattleEnd = System.DateTime.Now.ToString("dd/MM/yyyy HH:mm");

				battleReportList.addPosition(battleNumber, battleReportEntity);

				saveLoadController.Save(SaveTypes.BATTLEREPORT, battleReportList);
				if (ApplicationModel.BattleType.Equals(BattleType.BOSS_BATTLE)) {
					if (ApplicationModel.Victory)
						KilledBoss();
					else
						Character.ProgrammerMove.DestroyInstance();
				}
			} else if (battleReportEntity != null && (battleReportEntity.DateBattleEnd == null || battleReportEntity.DateBattleEnd.Equals(""))) {
				battleReportEntity.Victory = false;
				battleReportEntity.DateBattleEnd = "";

				SavePlayers savePlayer = saveLoadController.Load(SaveTypes.PLAYER) as SavePlayers;
				foreach (PlayerData playerData in savePlayer.players) {
					playerData.byteCoin -= playerData.byteCoin * 30 / 100;
					playerData.experiencia -= 20;
					if (playerData.experiencia < 0)
						playerData.experiencia = 0;
				}
				saveLoadController.Save(SaveTypes.PLAYER, savePlayer);
				battleReportList.addPosition(battleNumber, battleReportEntity);

				saveLoadController.Save(SaveTypes.BATTLEREPORT, battleReportList);
			}
		}
	}

	void Start() {
		Character.ProgrammerMove programmerMove = Character.ProgrammerMove.getInstance();
		if (programmerMove == null) {
			programmer = Resources.Load<GameObject>("Programmer");
			Transform initPoint = scenario.transform.FindChild("InitPoint");
			if (ApplicationModel.MapToLoadType.Equals(MapToLoadType.PREVIUS))
				initPoint = scenario.transform.FindChild("InitPoint2");
			programmer = Instantiate(programmer, initPoint.position, Quaternion.identity) as GameObject;
			programmer.name = "Programmer";
		} else {
			programmer = programmerMove.gameObject;
			programmer.SetActive(true);
			programmerMove.setBlockMove(false);
			programmerMove.transform.position = programmerMove.programmerPos;
		}
		ApplicationModel.MapToLoadType = MapToLoadType.NORMAL;
		ApplicationModel.EndBattle = false;
		Camera.main.GetComponent<CameraWorldMap>().target = programmer.transform;
	}

	private void KilledBoss() {
		Accomplishment accomplishment = SaveLoadController.GetInstance().Load(SaveTypes.ACCOMPLISHMENT) as Accomplishment;
		if (accomplishment == null) {
			accomplishment = new Accomplishment();
		}
		accomplishment.setBossDead(ApplicationModel.CurrentWorldMap);
		SaveLoadController.GetInstance().Save(SaveTypes.ACCOMPLISHMENT, accomplishment);
		Character.ProgrammerMove.getInstance().transform.position = GameObject.Find("VictoryPoint").transform.position;
	}
}
