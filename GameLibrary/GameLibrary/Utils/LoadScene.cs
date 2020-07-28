using System.Collections;
using UnityEngine.SceneManagement;

namespace Utils {
	public class LoadScene {

		public static IEnumerator LoadBattleScene(BattleType battleType) {
			ApplicationModel.BattleType = battleType;
			yield return null;
			SceneManager.LoadScene("Battle");
			yield break;
		}

		public static IEnumerator LoadWorldMap() {
			yield return null;
			SceneManager.LoadScene("WorldMap");
			yield break;
		}
	}
}
