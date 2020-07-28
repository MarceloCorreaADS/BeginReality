
namespace Utils {
	public class UserLogged {
		private static UserLogged instance;
		public string userName = "Usuarios";
		public int battlesFought = 0;

		private UserLogged() { }

		public static UserLogged GetInstance() {
			if (instance == null)
				instance = new UserLogged();
			return instance;
		}
	}
}
