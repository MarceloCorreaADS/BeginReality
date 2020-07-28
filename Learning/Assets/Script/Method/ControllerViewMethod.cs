using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ControllerViewMethod : MonoBehaviour {
	public Button button;
	public Method method;
	void Start() {
		button.onClick.AddListener(() => Show());
	}

	public void Show() {
		GameObject content = GameObject.Find("Canvas/MethodView/InfoBoxDescScroll/Viewport/Content");
		Transform desc = content.transform.GetChild(0);
		desc.GetComponent<Text>().text = method.methodFormatted;
		StartCoroutine(Gambi());
	}
	private IEnumerator Gambi() {
		yield return null;
		GameObject content = GameObject.Find("Canvas/MethodView/InfoBoxDescScroll/Viewport/Content");
		Transform desc = content.transform.GetChild(0);
		content.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(desc.GetComponent<RectTransform>().sizeDelta.x, 20 + desc.GetComponent<RectTransform>().sizeDelta.y);
		yield break;
	}
}
