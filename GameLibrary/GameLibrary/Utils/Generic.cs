namespace Utils
{
	public class Generic
	{
		public TurnManager getTurnManager(){
			return GameManagerUtil.Instance.GameManager.GetComponent<TurnManager>();
		}
    }
}

