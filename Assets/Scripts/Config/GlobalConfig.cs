using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GlobalConfig : MonoBehaviour {
	public static float musicSound;
	public static float effectSound;
	public static bool isEmailRemember;
	public static bool isAutoLogin;
	public static string email;
	public static string password;

	private bool allowQuitting = false;

	void Awake () {
		FileSystem.ReadConfig ();
	}

	void Update() {

	}

	void OnApplicationQuit()
	{
		Communication.Instance().Disconnect ();
	}
}
