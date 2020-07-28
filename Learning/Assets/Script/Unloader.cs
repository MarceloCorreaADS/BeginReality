using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Unloader : MonoBehaviour {
	[HideInInspector]
	public Button button;

	void Start() {
		button = GetComponent<Button>();
		button.onClick.AddListener(() => Unload());
	}
	private void Unload() {
		SceneManager.UnloadScene("ManutencaoEquipe");
	}
}
