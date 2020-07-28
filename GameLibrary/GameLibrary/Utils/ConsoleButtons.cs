using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Utils {
	public class ConsoleButtons : MonoBehaviour {
		public Button btnReport;
		public bool reportBlink = false;
		public Button btnBattle;
		public bool battleBlink = false;
		public Button btnHistory;
		public bool historyBlink = false;
		public Button btnMission;
		public bool missionBlink = false;
		public Button btnMenu;
		private Button btnSelected;
		public Text text;
		public ScrollRect scrollRect;
		private ConsoleType consoleTypeSelected;
		ColorBlock colorBlock;

		void Start() {
			ConsoleReport.Settings(text, scrollRect, this);
			btnReport.onClick.AddListener(() => Console(ConsoleType.REPORT, btnReport));
			btnBattle.onClick.AddListener(() => Console(ConsoleType.BATTLE, btnBattle));
			btnHistory.onClick.AddListener(() => Console(ConsoleType.HISTORY, btnHistory));
			btnMission.onClick.AddListener(() => Console(ConsoleType.MISSION, btnMission));
			consoleTypeSelected = ConsoleType.REPORT;
			btnReport.Select();
			colorBlock = btnReport.colors;
			btnSelected = btnReport;
		}

		private void Console(ConsoleType consoleType, Button button) {
			consoleTypeSelected = consoleType;
			ConsoleReport.SetConsole(consoleType);
			Canvas.ForceUpdateCanvases();
			scrollRect.verticalNormalizedPosition = 0f;
			ColorBlock color = button.colors;
			color.normalColor = Color.black;
			btnSelected.colors = color;
			color.normalColor = Color.gray;
			button.colors = color;
			btnSelected = button;
		}

		public void blinkButton(ConsoleType consoleType) {
			new Task(BlinkText(consoleType), true);
		}
		int cont = 1;
		public IEnumerator BlinkText(ConsoleType consoleType) {
			Button button = null;
			switch (consoleType) {
				case (ConsoleType.REPORT):
					if (!reportBlink) {
						reportBlink = true;
						button = btnReport;
					}
					break;
				case (ConsoleType.BATTLE):
					if (!battleBlink) {
						battleBlink = true;
						button = btnBattle;
					}
					break;
				case (ConsoleType.HISTORY):
					if (!historyBlink) {
						historyBlink = true;
						button = btnHistory;
					}
					break;
				case (ConsoleType.MISSION):
					if (!missionBlink) {
						missionBlink = true;
						button = btnMission;
					}
					break;
			}
			if (button == null) {
				yield break;
			}
			while (consoleType != consoleTypeSelected) {
				ColorBlock cb = button.colors;
				if (cont % 2 == 0) {
					cb.normalColor = Color.gray;
				} else {
					cb.normalColor = Color.black;
				}
				button.colors = cb;
				cont++;
				yield return new WaitForSeconds(0.5F);
			}
			switch (consoleType) {
				case (ConsoleType.REPORT):
					reportBlink = false;
					break;
				case (ConsoleType.BATTLE):
					battleBlink = false;
					break;
				case (ConsoleType.HISTORY):
					historyBlink = false;
					break;
				case (ConsoleType.MISSION):
					missionBlink = false;
					break;
			}
			yield break;
		}
	}
}
