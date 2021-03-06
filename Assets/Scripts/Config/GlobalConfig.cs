﻿using UnityEngine;
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
	public static int itemDataVersion;

	void Awake () {
		FileSystem.ReadConfig ();
		ItemSetInterpreter.ReadSet ();
		Screen.SetResolution (1280, 720, false);
	}

	void Update() {

	}

	void OnApplicationQuit()
	{
		Communication.Instance().Disconnect ();
	}

	void ExitProcessing() {
		
	}
}
