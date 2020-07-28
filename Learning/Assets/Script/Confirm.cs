using UnityEngine;
using UnityEngine.UI;

public class Confirm : MonoBehaviour {

	public Text txtConfirm;
	public Button btnOk;
	public Button btnCancel;
	public bool retorno;

	public void Create(string text) {
		txtConfirm.text = text;
		btnOk.onClick.AddListener(() => Confirmar());
		btnCancel.onClick.AddListener(() => DestroyConfirm());
	}

	public void Confirmar() {
		retorno = true;
	}

	public void DestroyConfirm() {
		Button[] btns = GameObject.Find("Canvas").GetComponentsInChildren<Button>();
		if (btns != null) {
			foreach (Button btn in btns) {
				if (btn != null)
					btn.interactable = true;
			}
		}
		Destroy(gameObject);
	}

	public static GameObject InstantiateConfirm() {
		Transform canvasTransform = GameObject.Find("Canvas").transform;
		GameObject confirmBox = Instantiate(Resources.Load<GameObject>("ConfirmBox"));
		confirmBox.name = "Confirm Box";
		confirmBox.transform.SetParent(canvasTransform, false);
		confirmBox.transform.position = canvasTransform.position;
		return confirmBox;
	}
}
