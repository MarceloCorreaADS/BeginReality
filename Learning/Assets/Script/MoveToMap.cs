using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MoveToMap : MonoBehaviour {

	public bool nextMap;

	void OnCollisionEnter2D(Collision2D collision) {
		bool canLoad = false;
		if (nextMap) {
			Accomplishment accomplishment = SaveLoadController.GetInstance().Load(SaveTypes.ACCOMPLISHMENT) as Accomplishment;
			if (accomplishment != null && accomplishment.checkTerminalActivated(Utils.ApplicationModel.CurrentWorldMap)) {
				canLoad = true;
				Utils.ApplicationModel.MapToLoadType = Utils.MapToLoadType.NEXT;
			}
		} else {
			canLoad = true;
			Utils.ApplicationModel.MapToLoadType = Utils.MapToLoadType.PREVIUS;
		}
		if (canLoad) {
			Utils.ApplicationModel.SetBattleInfoDefaultOptions();
			Character.ProgrammerMove.DestroyInstance();
			SceneManager.LoadScene("WorldMap");
		} else {
			GameObject alertBox = Alert.InstantiateAlert();
			Alert alert = alertBox.GetComponent<Alert>();
			alert.Create("Você precisa ativar o Terminal para passar de mapa.", new Button[0], false);
		}
	}
}
