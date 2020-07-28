using UnityEngine;

namespace Utils {
	public class GameManagerUtil {
		private static GameManagerUtil instance;
		private GameObject gameManager;
		
		private GameManagerUtil() { }

		public GameObject GameManager {
			get {
				if (gameManager == null)
					gameManager = GameObject.Find("GameManager");
				return gameManager;
			}

			private set {
				gameManager = value;
			}
		}

		public static GameManagerUtil Instance {
			get {
				if (instance == null)
					instance = new GameManagerUtil();
				return instance;
			}

			private set {
				instance = value;
			}
		}
	}
}
