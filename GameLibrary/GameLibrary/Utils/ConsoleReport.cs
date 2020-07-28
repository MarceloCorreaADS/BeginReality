using UnityEngine;
using UnityEngine.UI;

namespace Utils {

    public enum ConsoleType { REPORT, BATTLE, HISTORY, MISSION }

    public static class ConsoleReport {

        public static string consoleReport = "";
        public static string consoleBattle = "";
        public static string consoleHistory = "";
        public static string consoleMission = "";
        static ConsoleType activeConsole;
        static Text text;
        static ScrollRect scrollRect;
        static ConsoleButtons consoleButtons;

        public static void Settings(Text Text, ScrollRect ScrollRect, ConsoleButtons ConsoleButtons) {
            text = Text;
            scrollRect = ScrollRect;
            text.text = consoleReport;
            activeConsole = ConsoleType.REPORT;
            consoleButtons = ConsoleButtons;
        }

        public static void Log(string log, ConsoleType consoleType) {
            log += "\n";
            switch (consoleType) {
                case (ConsoleType.REPORT):
                    consoleReport += log;
                    break;
                case (ConsoleType.BATTLE):
                    consoleBattle += log;
                    break;
                case (ConsoleType.HISTORY):
                    consoleHistory += log;
                    break;
                case (ConsoleType.MISSION):
                    consoleMission += log;
                    break;
            }
            UpdateConsole();
        }

        public static void LogReport(string log) {
            log += "\n";
            consoleReport += log;
            if (ConsoleType.REPORT == activeConsole) {
                UpdateConsole();
            } else {
                blinkButton(ConsoleType.REPORT);
            }
        }

        public static void LogBattle(string log) {
            log += "\n";
            consoleBattle += log;
            if (ConsoleType.BATTLE == activeConsole) {
                UpdateConsole();
            } else {
                blinkButton(ConsoleType.BATTLE);
            }
        }

        public static void LogHistory(string log) {
            log += "\n";
            consoleHistory += log;
            if (ConsoleType.HISTORY == activeConsole) {
                UpdateConsole();
            } else {
                blinkButton(ConsoleType.HISTORY);
            }
        }

        public static void LogMission(string log) {
            log += "\n";
            consoleMission += log;
            if (ConsoleType.MISSION == activeConsole) {
                UpdateConsole();
            } else {
                blinkButton(ConsoleType.MISSION);
            }
        }

        static void UpdateConsole() {
            switch (activeConsole) {
                case (ConsoleType.REPORT):
                    text.text = consoleReport;
                    break;
                case (ConsoleType.BATTLE):
                    text.text = consoleBattle;
                    break;
                case (ConsoleType.HISTORY):
                    text.text = consoleHistory;
                    break;
                case (ConsoleType.MISSION):
                    text.text = consoleMission;
                    break;
            }
            Canvas.ForceUpdateCanvases();
            scrollRect.verticalNormalizedPosition = 0f;
        }

        public static void SetConsole(ConsoleType consoleType) {
            activeConsole = consoleType;
            UpdateConsole();
        }

        public static void blinkButton(ConsoleType consoleType) {
            consoleButtons.blinkButton(consoleType);
        }

		public static void ClearLogs() {
			consoleBattle = "";
			consoleHistory = "";
			consoleMission = "";
			consoleReport = "";
		}
    }
}
