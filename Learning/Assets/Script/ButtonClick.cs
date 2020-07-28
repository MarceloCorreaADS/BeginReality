using UnityEngine;
using UnityEngine.UI;
using Utils;

public class ButtonClick : MonoBehaviour {

    public InputField input;
    public Button executar;
    [HideInInspector]
    private GameManager gameManager;
	
    // Use this for initialization
    void Start() {
		gameManager = GameManager.instance;
		executar.onClick.AddListener(() => MyMethod());
    }

    public void MyMethod() {
		string text = input.text;
        if (text != null && text.Length > 0) {
            gameManager.Eval(text);
		}
		executar.interactable = false;
	}

    // Update is called once per frame
    void Update() {
        if (gameManager != null && gameManager.turnManager != null && !gameManager.turnManager.turnActive && (gameManager.turnManager.teamTurn == null || gameManager.turnManager.teamTurn.Equals(""))) {
            executar.interactable = true;
        } else if (gameManager != null && gameManager.turnManager != null) {
			executar.interactable = false;
		}
    }
}