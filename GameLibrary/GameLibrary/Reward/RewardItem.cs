using UnityEngine;
using UnityEngine.UI;

namespace Reward {
	public class RewardItem : MonoBehaviour {
		public Text txtName;
		public Text txtLevel;
		public Text txtXp;
		public Text txtByteCoin;

		public void setReward(string name, string level, string xp, string byteCoin) {
			txtName.text = name;
			txtLevel.text = level;
			txtXp.text = xp;
			txtByteCoin.text = byteCoin;
		}
	}
}
