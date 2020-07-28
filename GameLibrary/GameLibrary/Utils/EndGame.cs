using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Utils {
    public class EndGame : MonoBehaviour {
        public Text text;
        public Image background;

        public void End(bool victory) {
			text.text = victory ? "Você Venceu!" : "Você Perdeu!";
            text.enabled = true;
            background.enabled = true;
            new Task(Wait(victory), true);
		}
        public IEnumerator Wait(bool victory) {
			ApplicationModel.EndBattle = true;
			ApplicationModel.Victory = victory;
			yield return new WaitForSeconds(3F);
			StartCoroutine(LoadScene.LoadWorldMap());
            yield break;
        }
    }
}
