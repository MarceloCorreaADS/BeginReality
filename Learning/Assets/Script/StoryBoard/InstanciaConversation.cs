using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Utils;

public class InstanciaConversation : MonoBehaviour {
	public ConversationName conversationName = ConversationName.None;
	private Conversation conversation;
	SpeakDesign speakDesign;
	// Use this for initialization
	void Start() {

		if (gameObject.scene.name == "Battle") {
			conversationName = mapName(ApplicationModel.CurrentWorldMap, (ApplicationModel.BattleType == BattleType.BOSS_BATTLE ? true : false));
		}
		if (conversationName != ConversationName.None) {
			conversation = new Conversation(conversationName);
			speakDesign = conversation.speakDesign;

			if (speakDesign.speaks.Count > 0) {
				gameObject.AddComponent<ControllerConversation>();
				ControllerConversation controller = GetComponent<ControllerConversation>();
				controller.speakDesign = speakDesign;
				controller.conversationName = conversationName.ToString();
				controller.Show();
			}
		}
	}
	public ConversationName mapName(int map, bool isBossMap) {
		map = map - 1;

		List<ConversationName> mapsName = new List<ConversationName>();
		mapsName.Add(ConversationName.Floresta);
		mapsName.Add(ConversationName.Praia);
		mapsName.Add(ConversationName.Ruínas);
		mapsName.Add(ConversationName.Montanha);
		mapsName.Add(ConversationName.Vulcão);
		mapsName.Add(ConversationName.FlorestaChefe);
		mapsName.Add(ConversationName.PraiaChefe);
		mapsName.Add(ConversationName.RuínasChefe);
		mapsName.Add(ConversationName.MontanhaChefe);
		mapsName.Add(ConversationName.VulcãoChefe);

		if (isBossMap)
			map = map + 5;

		return mapsName[map];
	}
}
