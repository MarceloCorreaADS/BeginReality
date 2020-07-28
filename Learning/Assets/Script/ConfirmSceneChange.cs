using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Collections;

public class ConfirmSceneChange : SceneChanger {

	public string message;

	protected override void scene() {
		Button[] btns = GameObject.Find("Canvas").GetComponentsInChildren<Button>();
		if (btns != null) {
			foreach (Button btn in btns) {
				if (btn != null)
					btn.interactable = false;
			}
		}
		GameObject confirmObject = Confirm.InstantiateConfirm();
		Confirm confirm = confirmObject.GetComponent<Confirm>();
		confirm.Create(message);
		StartCoroutine(ConfirmClick(confirm));
	}
	
	IEnumerator ConfirmClick(Confirm confirm) {
		while (confirm != null && confirm.gameObject != null && !confirm.retorno) {
			yield return null;
		}
		if (confirm != null && confirm.gameObject != null && confirm.retorno == true) {
			if (gameObject.scene.name.Equals("Battle")) {
				GameManager.instance.setLose(scenesNames);
				yield return null;
				Destroy(GameObject.Find("Confirm Box"));
				yield return null;
				Destroy(GameObject.Find("MenuBattle"));
			} else {
				base.scene();
			}
		}
		yield break;
	}
}
