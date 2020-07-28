using System;

[Serializable]
public class Login {

	public string userName;
	public string password;
	public int battlesFought;

	public Login(string login, string password) {
		this.userName = login;
		this.password = password;
		this.battlesFought = 0;
	}
}
