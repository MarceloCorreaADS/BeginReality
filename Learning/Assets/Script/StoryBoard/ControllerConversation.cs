using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Utils;

public class ControllerConversation : MonoBehaviour {
	public Button button;
	public SpeakDesign speakDesign;
	public string conversationName;
	private Speak speak;
	private Personagem left;
	private Personagem right;
	private GameObject instBlockConversation;
	private GameObject instConversation;
	private GameObject blockConversationSelected;

	void Start() {
		if (button != null)
			button.onClick.AddListener(() => Show());
	}
	public void Show() {
		List<Tela> telas = new List<Tela>();
		telas = SaveLoadController.GetInstance().Load(SaveTypes.TELA) as List<Tela>;
		if (telas == null) {
			telas = new List<Tela>();
		}
		Tela telaAtual = telas.Find(t => t.tela == conversationName);
		if (telaAtual == null) {
			telaAtual = new Tela(conversationName, false);
			telas.Add(telaAtual);
			SaveLoadController.GetInstance().Save(SaveTypes.TELA, telas);
		}
		if (gameObject.scene.name == "Battle" && ApplicationModel.BattleType == BattleType.BOSS_BATTLE) {
			Accomplishment accomplishment = SaveLoadController.GetInstance().Load(SaveTypes.ACCOMPLISHMENT) as Accomplishment;
			if (accomplishment != null && accomplishment.checkBossDead(ApplicationModel.CurrentWorldMap)) {
				telas.Remove(telaAtual);
				telaAtual.lido = true;
				telas.Add(telaAtual);
				SaveLoadController.GetInstance().Save(SaveTypes.TELA, telas);
			}
		}
		if (telaAtual.lido == false) {
			List<Speak> speaks = speakDesign.speaks;
			if (speaks.Count > 0) {
				left = speakDesign.left;
				right = speakDesign.right;
				speak = speakDesign.speaks[0];
				speakDesign.speaks.Remove(speakDesign.speaks[0]);
				instBlockConversation = instanciaConversationSprite(0, 0, 0, 0, Resources.Load<GameObject>("SpritesSpeaks/Prefabs/BlockConversation"), GameObject.Find("Canvas").transform);
                if (gameObject.scene.name == "ManutencaoEquipe" || gameObject.scene.name == "CriarMetodo" || gameObject.scene.name == "RelatorioBatalha")
                {
                    instBlockConversation.GetComponent<Image>().color = Color.clear;
                }
                    instanciaConversationBubble(0, -120, 0, 0, Resources.Load<GameObject>("SpritesSpeaks/Prefabs/ConversationBubble"), instBlockConversation.transform);
			} else {
				if (gameObject.scene.name != "Battle") {
					telas.Remove(telaAtual);
					telaAtual.lido = true;
					telas.Add(telaAtual);
					SaveLoadController.GetInstance().Save(SaveTypes.TELA, telas);
				}else if (ApplicationModel.BattleType != BattleType.BOSS_BATTLE) {
					telas.Remove(telaAtual);
					telaAtual.lido = true;
					telas.Add(telaAtual);
					SaveLoadController.GetInstance().Save(SaveTypes.TELA, telas);
				}
                Destroy(GameObject.Find("Canvas/BlockConversation"));
			}
		}
	}
	GameObject instanciaConversationSprite(int posX, int posY, int width, int height, GameObject prefab, Transform pai) {
		GameObject objeto;

		objeto = Instancia(posX, posY, width, height, prefab, pai);
		if (speakDesign.background != null)
			objeto.transform.GetComponent<Image>().sprite = speakDesign.background;

		objeto.transform.GetChild(0).GetComponent<Image>().sprite = left.Sprite;
		objeto.transform.GetChild(0).GetComponent<Image>().rectTransform.sizeDelta = new Vector2(left.width, left.heigth);

		objeto.transform.GetChild(1).GetComponent<Image>().sprite = right.Sprite;
		objeto.transform.GetChild(1).GetComponent<Image>().rectTransform.sizeDelta = new Vector2(right.width, right.heigth);
		objeto.name = prefab.name;

		return objeto;
	}
	GameObject instanciaConversationBubble(int posX, int posY, int width, int height, GameObject prefab, Transform pai) {
		Sprite face;
		if (speak.tag == "left")
			face = speakDesign.left.Face;
		else
			face = speakDesign.right.Face;

		GameObject objeto;
		objeto = Instancia(posX, posY, width, height, prefab, pai);
		objeto.transform.GetChild(1).GetComponent<Image>().sprite = face;
		instConversation = objeto.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
		instConversation.GetComponent<Text>().text = setConversation();

		GameObject buttonNext = instConversation.transform.GetChild(0).gameObject;

		buttonNext.AddComponent<ControllerConversation>();
		buttonNext.GetComponent<ControllerConversation>().button = buttonNext.GetComponent<Button>();
		buttonNext.GetComponent<ControllerConversation>().speakDesign = speakDesign;
		buttonNext.GetComponent<ControllerConversation>().conversationName = conversationName;
		buttonNext.transform.GetChild(0).GetComponent<Text>().text = "Continuar...";

		objeto.name = prefab.name;
		StartCoroutine(Gambi());
		return objeto;
	}
	string setConversation() {
		string conversation = "";

		conversation += "[" + speak.nome + "]: \n";
		conversation += speak.speaks + "\n";
        if (gameObject.scene.name == "Battle")
            new Task(printLog("[" + speak.nome + "]: " + speak.speaks), true);
		return conversation;
	}

    IEnumerator printLog(string log) {
        yield return null;
        ConsoleReport.LogHistory(log);
        yield break;
    }
    GameObject Instancia(int posX, int posY, int width, int height, GameObject prefab, Transform pai) {
		GameObject objeto;
		Vector3 infoPosition = transform.position;
		objeto = Instantiate(prefab, infoPosition, Quaternion.identity) as GameObject;
		objeto.transform.SetParent(pai);
		objeto.transform.localPosition = new Vector3(posX, posY, 1);
		objeto.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
		objeto.transform.localScale = new Vector3(1, 1, 1);

		return objeto;
	}
	private IEnumerator Gambi() {
		yield return null;
		GameObject descricao = instConversation;
		GameObject content = descricao.transform.parent.gameObject;
		content.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(descricao.GetComponent<RectTransform>().sizeDelta.x, 35 + descricao.GetComponent<RectTransform>().sizeDelta.y);
		if (GameObject.Find("Canvas/BlockConversation") != instBlockConversation) {
			Destroy(GameObject.Find("Canvas/BlockConversation"));
		}
		yield break;
	}
}