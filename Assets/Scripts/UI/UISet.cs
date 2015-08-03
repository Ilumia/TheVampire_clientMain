using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UISet : MonoBehaviour {
	private static UISet uiSet;
	public static UISet GetSet() {
		if(uiSet == null) {
			uiSet = new UISet();
		}
		return uiSet;
	}

	public static Communication comm;

	public enum UIState { CAUTION, MAIN_SELECT, MAIN_LOGIN, LOBBY_CREATEROOM, LOBBY_LOBBY};
	public static UIState uiState;

	public static GameObject Main;
	public static GameObject Caution;		// Caution
	public static Text txt_caution;
	public static GameObject Set_SelectMode;	// SelectMode
	public static Button btn_solo;
	public static Button btn_multi;
	public static GameObject Set_Login;			// Login
	public static InputField input_email;
	public static InputField input_password;
	public static Toggle toggle_remember;
	public static Toggle toggle_autologin;
	public static Button btn_signup;
	public static Button btn_cancel;
	public static Button btn_login;

	public static GameObject Lobby;
	public static GameObject Set_Lobby;			// Lobby
	public static Button btn_createroom;
	public static Button btn_friends;
	public static Button btn_exit;
	public static Button btn_room1;
	public static Button btn_room2;
	public static Button btn_room3;
	public static Button btn_room4;
	public static Button btn_room5;
	public static Button btn_listup;
	public static Button btn_listdown;
	public static GameObject Set_CreateRoom;	// CreateRoom
	public static Slider slider_number;
	public static Toggle toggle_public;
	public static Button btn_createconfirm;
	public static Button btn_createcancel;
	public static Text txt_numbercount;

	// SelectMode
	public static void Ebtn_solo() {
		Communication.DisconnectSocket ();
	}
	public static void Ebtn_multi() {
		comm = Communication.GetCommunication();
		ActiveUI (UIState.MAIN_LOGIN);
	}
	// Login
	public static void Einput_email<String>(String str) {
		
	}
	public static void Einput_password<String>(String str) {
		
	}
	public static void Etoggle_remember(bool check) {
		
	}
	public static void Etoggle_autologin(bool check) {
		
	}
	public static void Ebtn_signup() {
		
	}
	public static void Ebtn_cancel() {
		input_email.text = "";
		input_password.text = "";
		ActiveUI (UIState.MAIN_SELECT);
	}
	public static void Ebtn_login() {
		string email = input_email.text;
		string password = input_password.text;
		comm.SendMessageToServer ('A', email + " " + password);
		//ActiveUI (UIState.LOBBY_LOBBY);
	}
	// Lobby
	public static void Ebtn_createroom() {
		ActiveUI (UIState.LOBBY_CREATEROOM);
	}
	public static void Ebtn_friends() {
		
	}
	public static void Ebtn_exit() {
		ActiveUI (UIState.MAIN_LOGIN);
	}
	public static void Ebtn_room() {

	}
	public static void Ebtn_listup() {
		
	}
	public static void Ebtn_listdown() {
		
	}
	// CreateRoom
	public static void Eslider_number(float param) {
		txt_numbercount.text = Mathf.Round (param).ToString ();
	}
	public static void Etoggle_public(bool check) {
		
	}
	public static void Ebtn_createconfirm() {
		
	}
	public static void Ebtn_createcancel() {
		ActiveUI (UIState.LOBBY_LOBBY);
	}

	
	public static void InactiveUI() {
		
		Main.SetActive (false);
		Set_SelectMode.SetActive (false);
		Set_Login.SetActive (false);
		
		Lobby.SetActive (false);
		Set_Lobby.SetActive (false);
		Set_CreateRoom.SetActive (false);
		
		Caution.SetActive (false);
	}
	public static void ActiveUI (UIState state) {
		UIManagement.UIEventList.Add (state);
	}
	public static void ActiveUI_(UIState state) {
		InactiveUI ();
		switch (state) {
		case UIState.CAUTION:
			//ActiveUI (uiState);
			if(uiState == UIState.MAIN_SELECT) {
				Main.SetActive(true);
				Set_SelectMode.SetActive(true);
			} else if(uiState == UIState.MAIN_LOGIN) {
				Main.SetActive(true);
				Set_Login.SetActive(true);
			} else if(uiState == UIState.LOBBY_LOBBY) {
				Lobby.SetActive(true);
				Set_Lobby.SetActive(true);
			} else if(uiState == UIState.LOBBY_CREATEROOM) {
				Lobby.SetActive(true);
				Set_Lobby.SetActive(true);
				Set_CreateRoom.SetActive(true);
			}
			Caution.SetActive(true);
			break;
		case UIState.MAIN_SELECT:
			Main.SetActive(true);
			Set_SelectMode.SetActive(true);
			uiState = UIState.MAIN_SELECT;
			break;
		case UIState.MAIN_LOGIN:
			Main.SetActive(true);
			Set_Login.SetActive(true);
			uiState = UIState.MAIN_LOGIN;
			break;
		case UIState.LOBBY_LOBBY:
			Lobby.SetActive(true);
			Set_Lobby.SetActive(true);
			uiState = UIState.LOBBY_LOBBY;
			break;
		case UIState.LOBBY_CREATEROOM:
			Lobby.SetActive(true);
			Set_Lobby.SetActive(true);
			Set_CreateRoom.SetActive(true);
			break;
		}
	}
	public static void SetCaution (string text) {
		UIManagement.UICautionList.Add (text);
	}
	public static void SetCaution_ (string text) {
		txt_caution.text = text;
	}
}
