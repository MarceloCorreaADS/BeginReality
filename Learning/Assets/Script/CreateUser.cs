using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
using Utils;

public class CreateUser : MonoBehaviour {

	private Button btnLogin;
	public Button btnRetornar; 
	public InputField inputLogin;
	public InputField inputPassword;
	public InputField inputConfirmPassword;

	void Start () {
		btnLogin = GetComponent<Button>();
		btnLogin.onClick.AddListener(() => MyMethod());
		Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
	}

	public void MyMethod() {
		btnLogin.interactable = false;
		btnRetornar.interactable = false;
		string userName = inputLogin.text;
		string password = inputPassword.text;
		string confirmPassword = inputConfirmPassword.text;
		GameObject alertBox = Instantiate(Resources.Load<GameObject>("AlertBox"));
		alertBox.transform.SetParent(GameObject.Find("Canvas").transform, false);
		alertBox.transform.position = new Vector3(0, 0);
		Alert alert = alertBox.GetComponent<Alert>();
		if (userName.Length < 5 || password.Length < 6) {
			// Exibe que não pode ter essas quantidades
			alert.Create("Login e Senha precisam ser preenchidos, respectivamente, no minimo com 5 e 6 caracteres", new Button[] {btnLogin, btnRetornar }, false);
			return;
		}
		if (!password.Equals(confirmPassword)) {
			alert.Create("Senha e Confirmar Senha não são iguais.", new Button[] { btnLogin, btnRetornar }, false);
			inputPassword.text = "";
			inputConfirmPassword.text = "";
			return;
		}
		userName = MD5Hash(userName);
		password = MD5Hash(password);

		CreatePath();
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		FileStream fileStream;
		string nomePath = "Data/users.dat";
		Login login = null;
		if (File.Exists(nomePath)) {
			fileStream = File.Open(nomePath, FileMode.Open, FileAccess.Read, FileShare.Delete);
			List<Login> logins = new List<Login>();
			try {
				logins = binaryFormatter.Deserialize(fileStream) as List<Login>;
				fileStream.Close();
			} catch (Exception) {
				fileStream.Close();
			}
			login = logins.Find(l => l.userName == userName);
			if (login == null) {
				login = new Login(userName, password);
				FileStream newFileStream = File.Open(nomePath, FileMode.Create, FileAccess.Write, FileShare.Read);
				AddUsuarios(logins, login, newFileStream, binaryFormatter, alert);
			} else {
				alert.Create("Esse usuário já existe!", new Button[] { btnLogin, btnRetornar }, false);
			}
		} else {
			CreateUsuarios(nomePath, userName, password, alert);
		}
	}

	private void CreateUsuarios(string nomePath, string userName, string password, Alert alert) {
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		FileStream fileStream = File.Create(nomePath);
		Login login = new Login(userName, password);
		AddUsuarios(new List<Login>(), login, fileStream, binaryFormatter, alert);
	}

	private void AddUsuarios(List<Login> logins, Login login, FileStream fileStream, BinaryFormatter binaryFormatter, Alert alert) {
		logins.Add(login);
		for (int i = 0; i < logins.Count; i++) {
			Login temp = logins[i];
			int randomIndex = Random.Range(i, logins.Count);
			logins[i] = logins[randomIndex];
			logins[randomIndex] = temp;
		}
		binaryFormatter.Serialize(fileStream, logins);
		fileStream.Close();
		alert.Create("Usuario e senha adicionados com sucesso!", new Button[] { btnLogin, btnRetornar }, true);
		StartCoroutine(SceneRedirect());
	}

	private IEnumerator SceneRedirect() {
		yield return new WaitForSeconds(2);
		SceneManager.LoadScene("Login");
		yield break;
	}

	public string MD5Hash(string text) {
		MD5 md5 = new MD5CryptoServiceProvider();

		//compute hash from the bytes of text
		md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

		//get hash result after compute it
		byte[] result = md5.Hash;

		StringBuilder strBuilder = new StringBuilder();
		for (int i = 0; i < result.Length; i++) {
			//change it into 2 hexadecimal digits
			//for each byte
			strBuilder.Append(result[i].ToString("x2"));
		}

		return strBuilder.ToString();
	}
	private static void CreatePath() {
		if (!Directory.Exists("Data")) {
			Directory.CreateDirectory("Data");
		}
	}
}
