using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Xml;

public class GlobalConfig : MonoBehaviour {
	public static float musicSound;
	public static float effectSound;
	public static bool isEmailRemember;
	public static bool isAutoLogin;
	public static string email;
	public static string password;
	public static int cardSetVersion;

	void Awake () {
		FileSystem.ReadConfig ();
		CardSetInterpreter.ReadSet ();
	}

	void Update() {

	}

	void OnApplicationQuit()
	{
		Communication.Instance().Disconnect ();
	}
}
