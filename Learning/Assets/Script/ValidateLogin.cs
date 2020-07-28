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

public class ValidateLogin : MonoBehaviour {

	private Button btnLogin;
	public Button btnCadastrar;
	public InputField inputLogin;
	public InputField inputPassword;

	void Start() {
		btnLogin = GetComponent<Button>();
		btnLogin.onClick.AddListener(() => MyMethod());
		Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
	}

	public void MyMethod() {
		btnLogin.interactable = false;
		btnCadastrar.interactable = false;
		string userName = inputLogin.text;
		string password = inputPassword.text;
		GameObject alertBox = Alert.InstantiateAlert();
		Alert alert = alertBox.GetComponent<Alert>();
		if (userName.Length < 5 || password.Length < 6) {
			// Exibe que não pode ter essas quantidades
			alert.Create("Login e Senha precisam ser preenchidos, respectivamente, no minimo com 5 e 6 caracteres", new Button[] { btnLogin, btnCadastrar }, false);
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
				alert.Create("Usuário não encontrado.", new Button[] { btnLogin, btnCadastrar }, false);
			} else if (login.password == password) {
				alert.Create("Login Efetuado com sucesso", new Button[] { btnLogin, btnCadastrar }, true);
				UserLogged.GetInstance().userName = login.userName;

				BattleReportList battleReportList = SaveLoadController.GetInstance().Load(SaveTypes.BATTLEREPORT) as BattleReportList;
				if (battleReportList != null) {
					UserLogged.GetInstance().battlesFought = battleReportList.BattleReportEntityList.Count;
				}
				StartCoroutine(SceneRedirect());
			} else {
				alert.Create("Senha Incorreta!", new Button[] { btnLogin, btnCadastrar }, false);
			}
		} else {
			alert.Create("Usuário não encontrado.", new Button[] { btnLogin, btnCadastrar }, false);
		}
	}

	private IEnumerator SceneRedirect() {
		yield return new WaitForSeconds(2);
		SceneManager.LoadScene("MenuPrincipal");
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
