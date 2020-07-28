using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Utils;
using System.Collections;

public class SceneChanger : MonoBehaviour {

	public Button button;
	public ScenesNames scenesNames = ScenesNames.None;
	public string sceneName;
	public int number;

	void Start() {
		if(button == null)
			button = GetComponent<Button>();
		button.onClick.AddListener(() => scene());
	}

	protected virtual void scene() {
		if (scenesNames != ScenesNames.None) { 
			sceneName = scenesNames.ToString();
			Character.ProgrammerMove programmer = Character.ProgrammerMove.getInstance();
			if (programmer != null) {
				if (scenesNames.Equals(ScenesNames.ManutencaoEquipe) || scenesNames.Equals(ScenesNames.CriarMetodo) || scenesNames.Equals(ScenesNames.RelatorioBatalha)) {
					programmer.gameObject.SetActive(false);
				} else if (scenesNames.Equals(ScenesNames.Login) || scenesNames.Equals(ScenesNames.MenuPrincipal)) {
					Destroy(programmer.gameObject);
				}
			}
		}

		if (sceneName == null || sceneName.Length == 0)
			SceneManager.LoadScene(number);
		else
			SceneManager.LoadScene(sceneName);

    }
}
