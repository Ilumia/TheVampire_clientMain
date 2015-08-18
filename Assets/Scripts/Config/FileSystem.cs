using System;
using System.IO;
using UnityEngine;

public class FileSystem {
	private static string path = Application.persistentDataPath + "/.config";
	public static void ReadConfig() {
		bool errorCheck = false;
		if (!File.Exists (path)) {
			CreateConfig();
		}
		Debug.Log (path);

		try {
			string _config = File.ReadAllText(path);
			string[] config = _config.Split('$');
			float.TryParse(config[0], out GlobalConfig.musicSound);
			float.TryParse(config[1], out GlobalConfig.effectSound);
			if(config[2].Equals("0")) {
				GlobalConfig.isEmailRemember = false;
				GlobalConfig.isAutoLogin = false;
			} else {
				GlobalConfig.isEmailRemember = true;
				GlobalConfig.email = config[3];
				if(config[4].Equals("0")) {
					GlobalConfig.isAutoLogin = false;
				} else {
					GlobalConfig.isAutoLogin = true;
					GlobalConfig.password = config[5];
				}
			}
		} catch(IOException e) {
			Debug.Log (e.StackTrace);
		} catch(Exception e) {
			Debug.Log (e.StackTrace);
		}
		Debug.Log (GlobalConfig.musicSound + ", " + GlobalConfig.effectSound + ", " + 
		           GlobalConfig.isEmailRemember + "," + GlobalConfig.isAutoLogin);
		if (GlobalConfig.email != null) {
			Debug.Log (GlobalConfig.email);
		}
		if (GlobalConfig.password != null) {
			Debug.Log (GlobalConfig.password);
		}
	}
	public static void CreateConfig() {
		string config = "1.0$1.0$0$0$";
		File.WriteAllText (path, config);
		ReadConfig ();
	}
	public static void WriteEmailConfig(string email) {
		string _config = File.ReadAllText(path);
		string[] config = _config.Split('$');
		string newConfig = "";
		newConfig += config [0] + "$";
		newConfig += config [1] + "$";
		if (email != null) {
			newConfig += "1" + "$";
			newConfig += email + "$";
		} else {
			newConfig += "0" + "$";
		}
		newConfig += "0" + "$";

		File.WriteAllText (path, newConfig);
		ReadConfig ();
	}
	public static void WriteEmailConfig() {
		WriteEmailConfig (null);
	}
	public static void WritePasswordConfig(string email, string password) {
		string _config = File.ReadAllText(path);
		string[] config = _config.Split('$');
		string newConfig = "";
		newConfig += config [0] + "$";
		newConfig += config [1] + "$";
		if (email != null) {
			newConfig += "1" + "$";
			newConfig += email + "$";
			newConfig += "1" + "$";
			newConfig += password + "$";
		} else {
			newConfig += "0" + "$";
			newConfig += "0" + "$";
		}

		File.WriteAllText (path, newConfig);
		ReadConfig ();
	}
	public static void WritePasswordConfig() {
		WritePasswordConfig (null, null);
	}
	public static void WriteMusicSoundConfig(float value) {
		string _config = File.ReadAllText(path);
		string[] config = _config.Split('$');
		string newConfig = "";
		for (int i=0; i<config.Length; i++) {
			if(i == 0) {
				newConfig += value.ToString();
			} else {
				newConfig += config[i];
			}
		}
		
		File.WriteAllText (path, newConfig);
		ReadConfig ();
	}
	public static void WriteEffectSoundConfig(float value) {
		string _config = File.ReadAllText(path);
		string[] config = _config.Split('$');
		string newConfig = "";
		for (int i=0; i<config.Length; i++) {
			if(i == 1) {
				newConfig += value.ToString();
			} else {
				newConfig += config[i];
			}
		}
		
		File.WriteAllText (path, newConfig);
		ReadConfig ();
	}
}
