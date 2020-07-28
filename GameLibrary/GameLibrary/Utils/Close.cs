using UnityEngine;
using UnityEngine.UI;

namespace GameLibrary.Utils {
	public class Close : MonoBehaviour {
		public Button button;


		void Start() {
			button = gameObject.GetComponent<Button>();
			button.onClick.AddListener(() => Show());
		}

		void Show() {
			GameObject destruir;
			destruir = button.transform.parent.gameObject;
			if (destruir != null)
				Destroy(destruir);
		}
	}
}