using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Utils;
using Board;

public class GameManager : MonoBehaviour {
	public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
	private BoardManager boardScript;                       //Store a reference to our BoardManager which will set up the level.
	[HideInInspector]
	public TurnManager turnManager;
	private Grid grid;
	private WinningConditions winningConditionsScript;
	public GameObject unitBackground;
	private ScriptLoader scriptLoader = null;

	private GameManager() { }

	//Awake is always called before any Start functions
	void Awake() {
		//Check if instance already exists
		if (instance == null)
			//if not, set instance to this
			instance = this;

		//If instance already exists and it's not this:
		else if (instance != this)
			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
			Destroy(gameObject);

		//Sets this to not be destroyed when reloading scene
		//DontDestroyOnLoad(gameObject);

		//Get a component reference to the attached BoardManager script
		grid = GetComponent<Grid>();
		boardScript = GetComponent<BoardManager>();
		turnManager = GetComponent<TurnManager>();

		winningConditionsScript = turnManager.winningConditions;
		//Call the InitGame function to initialize the first level 
		InitGame();
		ConsoleReport.ClearLogs();
	}

	//Initializes the game for each level.
	void InitGame() {
		grid.BoardSetup();
		if (winningConditionsScript.winningConditions.Count == 0) {
			winningConditionsScript.addCondition(Conditions.KILL_EVERYONE);
			//winningConditionsScript.addCondition(Conditions.ALL_TEAM_ALIVE);
		}
	}
	void Start() {
		winningConditionsScript.endGame = false;
		StartCoroutine(executeFirstAction());
		StartCoroutine(checkEndBattle());
	}

	IEnumerator executeFirstAction() {
		Character.ProgrammerMove programmer = Character.ProgrammerMove.getInstance();
		if (programmer != null) {
			programmer.gameObject.SetActive(false);
		}
		boardScript.squares = grid.squares;
		if (boardScript.squares != null && boardScript.squares.Count > 0) {
			Debug.Log("Criação do Campo");
			yield return new WaitForSeconds(0.5F);
			//Call the SetupScene function of the BoardManager script, pass it current level number.
			// Ao passar "level" diferente, organizamos o campo para cada mapa. Fazendo um switch case dentro do SetupScene com as posições de cada inimigo e player no campo.
			boardScript.SetupScene();
			Debug.Log("Criação dos Personagens");
			saveBattleReport(null);
			yield return new WaitForSeconds(0.5F);
			try {
				//turnManager.EnemyTurn();
				Debug.Log("Turno dos Inimigos");
			} catch (Exception) {
				Debug.Log("Sem Characters no mapa");
			}
		} else {
			Debug.Log("Mapa sem Squares ou mapa não encontrado");
		}
		yield break;
	}

	IEnumerator checkEndBattle() {
		yield return new WaitUntil(() => winningConditionsScript.endGame);
		SaveLoadController.GetInstance().SavePlayer();
		yield break;
	}

	public void setLose(ScenesNames sceneName) {
		winningConditionsScript.setLose();
		StartCoroutine(redirect(sceneName));
	}
	IEnumerator redirect(ScenesNames sceneName) {
		yield return new WaitForSeconds(3f);
		UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName.ToString());
		yield break;
	}

	public bool checkConditions(String commands) {
		List<WinningCondition> winningConditions = winningConditionsScript.winningConditions;
		foreach (WinningCondition winningCondition in winningConditions) {
			switch (winningCondition.Condition) {
				case (Conditions.IF):
					winningCondition.completeQuantity += textContain(commands, "if");
					winningCondition.checkQuantity();
					break;
				case (Conditions.FOR):
					winningCondition.completeQuantity += textContain(commands, "for");
					winningCondition.checkQuantity();
					break;
				case (Conditions.WHILE):
					winningCondition.completeQuantity += textContain(commands, "while");
					winningCondition.checkQuantity();
					break;
			}
		}
		instance.turnManager.winningConditions.winningConditions = winningConditions;
		return true;
	}

	private int textContain(String text, String condition) {
		return System.Text.RegularExpressions.Regex.Matches(text.ToLower(), condition.ToLower()).Count;
	}

	public void Eval(string sCSCode) {
		if (scriptLoader == null) {
			scriptLoader = ScriptLoader.Instance;
		}
		if (checkStrings(sCSCode)) {
			return;
		}
		if (!checkEndTurn(sCSCode)) {
			ConsoleReport.LogReport("Você precisa terminar o seu turno.\nAdicione turnEnd(); no final do seu código");
			return;
		}
		Simulator simulator = Simulator.getInstance();
		simulator.QtySimulacoes = 1;
		checkConditions(sCSCode);
		//CompilerParameters cp = new CompilerParameters();

		//cp.ReferencedAssemblies.Add("system.dll");
		//cp.ReferencedAssemblies.Add("system.xml.dll");
		//cp.ReferencedAssemblies.Add("system.data.dll");
		//cp.ReferencedAssemblies.Add("system.windows.forms.dll");
		//cp.ReferencedAssemblies.Add("system.drawing.dll");
		//cp.ReferencedAssemblies.Add(@"dll\UnityEngine.dll");
		//cp.ReferencedAssemblies.Add(@"Assets\Plugins\GameLibrary.dll");
		//cp.ReferencedAssemblies.Add(@"dll\UnityEngine.UI.dll");

		//cp.CompilerOptions = "/t:library";
		//cp.GenerateInMemory = true;
		string code = sCSCode;
		StringBuilder sb = new StringBuilder("");
		sb.Append("using Utils;\n");
		sb.Append("using Movements;\n");
		sb.Append("using Character;\n");
		StringBuilder str = new StringBuilder();
		List<Method> methods = SaveLoadController.GetInstance().Load(SaveTypes.METHOD) as List<Method>;
		if (methods != null) {
			foreach (Method m in methods) {
				str.Append(m.MethodFinal() + "\n");
			}
		}

		sb.Append("" +
				"public class CSCodeEvaler{ \n" +
					"private TurnManager turnManager;\n" +
					str.ToString() +
					"public void EvalCode(){\n" +
						"try {\n" +
						   sCSCode + " \n" +
						 "} catch(System.Exception e) {" +
							"ConsoleReport.LogReport(e.Message);" +
						"}" +
					 "} \n" +
					 "private Player getSoldier(){\n" +
						   "setTurnManager();\n" +
						   "return turnManager.getPlayer(\"Nortun\");\n" +
					"} \n" +
					 "private Player getMage(){\n" +
						   "setTurnManager();\n" +
						   "return turnManager.getPlayer(\"Kapirsky\");\n" +
					"} \n" +
					 "private Player getArcher(){\n" +
						   "setTurnManager();\n" +
						   "return turnManager.getPlayer(\"Pandmé\");\n" +
					"} \n" +
				   "private void turnEnd(){\n" +
						   "setTurnManager();\n" +
						   "new Task (turnManager.NextTurn(), true);\n" +
				   "} \n" +
				   "private void setTurnManager(){\n" +
						"if (turnManager == null) {this.turnManager = new Generic().getTurnManager();}" +
				   "} \n" +
				"} \n");
		scriptLoader.Loader.LoadAndWatchScriptsBundle(sb);

		saveBattleReport(code);
	}

	private bool checkEndTurn(string text) {
		if (textContain(text, "turnEnd()") > 0) {
			return true;
		}
		return false;
	}
	private bool checkStrings(string text) {
		int cont = 0;
		if (textContain(text, "//") > 0) {
			ConsoleReport.LogReport("Comentários no código não são permitidos.");
			cont++;
		}
		if (textContain(text, "using ") > 0) {
			ConsoleReport.LogReport("Importar bibliotecas não é permitido.");
			cont++;
		}
		if (cont > 0)
			return true;
		return false;
	}

	private void saveBattleReport(string code) {
		SaveLoadController saveLoadController = SaveLoadController.GetInstance();
		BattleReportList battleReportList = saveLoadController.Load(SaveTypes.BATTLEREPORT) as BattleReportList;
		if (battleReportList == null) {
			battleReportList = new BattleReportList();
		}
		int battleNumber = UserLogged.GetInstance().battlesFought;
		BattleReportEntity battleReportEntity = battleReportList.getByBattleNumber(battleNumber);
		bool isAdd = false;
		if (battleReportEntity == null) {
			isAdd = true;
			battleReportEntity = new BattleReportEntity();
			battleReportEntity.BattleNumber = battleNumber;
			battleReportEntity.DateBattleStart = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
			battleReportEntity.QtyTurns = 0;
			battleReportEntity.Map = ApplicationModel.CurrentWorldMap;
			battleReportEntity.BossBattle = ApplicationModel.BattleType.Equals(BattleType.BOSS_BATTLE);
			battleReportEntity.Inimigos = new List<string>();
			List<GameObject> enemys = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
			foreach (GameObject enemy in enemys) {
				Character.Player player = enemy.GetComponent<Character.Player>();
				battleReportEntity.Inimigos.Add(player.atributos.Classe.NomeGameObject + " - Level: " + player.atributos.Level);
			}
		}
		if (code != null) {
			battleReportEntity.PlayerActions.Add(code);
			battleReportEntity.QtyTurns++;
		}
		if (isAdd) {
			battleReportList.BattleReportEntityList.Add(battleReportEntity);
		} else {
			battleReportList.addPosition(battleNumber, battleReportEntity);
		}
		saveLoadController.Save(SaveTypes.BATTLEREPORT, battleReportList);
	}
}