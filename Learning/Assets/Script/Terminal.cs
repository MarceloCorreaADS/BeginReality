using UnityEngine;
using UnityEngine.UI;

public class Terminal : MonoBehaviour {
	
	void OnCollisionEnter2D(Collision2D collision) {
		Accomplishment accomplishment = SaveLoadController.GetInstance().Load(SaveTypes.ACCOMPLISHMENT) as Accomplishment;
		string alertMsg = "Você precisa derrotar o protetor do portal antes de ativar.";
		if (accomplishment != null) {
			bool bossDead = accomplishment.checkBossDead(Utils.ApplicationModel.CurrentWorldMap);
			bool terminalActive = accomplishment.checkTerminalActivated(Utils.ApplicationModel.CurrentWorldMap);
			if (terminalActive) {
				alertMsg = "Portal já está ativado!";
			} else if (bossDead) {
				accomplishment.setTerminalActivated(Utils.ApplicationModel.CurrentWorldMap);
				SaveLoadController.GetInstance().Save(SaveTypes.ACCOMPLISHMENT, accomplishment);
				alertMsg = "Portal ativado!";
			}
		}
		GameObject alertBox = Alert.InstantiateAlert();
		Alert alert = alertBox.GetComponent<Alert>();
		alert.Create(alertMsg, new Button[0], false);
	}
}

