using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ControllerHelp : MonoBehaviour {
	public Button button;
	public HelpDescription helpDescription;
	void Start() {
		button.onClick.AddListener(() => Show());
	}

	public void Show() {
		GameObject content = GameObject.Find("Canvas/Help/InfoBoxDescScroll/Viewport/Content");
		Transform titulo = content.transform.GetChild(0);
		Transform desc = content.transform.GetChild(1);
		titulo.GetComponent<Text>().text = helpDescription.title + "\n";
		desc.GetComponent<Text>().text = "";
		foreach (HelpDescriptionSub h in helpDescription.helpDescriptionSub) {
			desc.GetComponent<Text>().text += h.title + "\n\n";
			desc.GetComponent<Text>().text += h.description;
			desc.GetComponent<Text>().text += "\n\n---------------------------------------\n\n";
		}
		StartCoroutine(Gambi());
	}
	private IEnumerator Gambi() {
		yield return null;
		GameObject content = GameObject.Find("Canvas/Help/InfoBoxDescScroll/Viewport/Content");
		Transform desc = content.transform.GetChild(1);
		content.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(desc.GetComponent<RectTransform>().sizeDelta.x, 20 + desc.GetComponent<RectTransform>().sizeDelta.y);
		yield break;
	}
}