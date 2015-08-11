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

	public enum UIState { MAIN_SELECT, MAIN_LOGIN, LOBBY_CREATEROOM, 
		LOBBY_LOBBY, ROOM_READIED, CAUTION};
	public static UIState uiState;

	public static GameObject Main;				// Main
	public static GameObject Set_SelectMode;	// SelectMode Set
	public static Button btn_solo;
	public static Button btn_multi;
	public static GameObject Set_Login;			// Login Set
	public static InputField input_email;
	public static InputField input_password;
	public static Toggle toggle_remember;
	public static Toggle toggle_autologin;
	public static Button btn_signup;
	public static Button btn_cancel;
	public static Button btn_login;
	public static GameObject Lobby;				// Lobby
	public static GameObject Set_Lobby;			// Lobby Set
	public static Button btn_randomenter;
	public static Button btn_createroom;
	public static Button btn_friends;
	public static Button btn_exit;
	public static GameObject Set_CreateRoom;	// CreateRoom Set
	public static Slider slider_number;
	public static Toggle toggle_public;
	public static Button btn_createconfirm;
	public static Button btn_createcancel;
	public static Text txt_numbercount;
	public static GameObject Room;				// Room
	public static GameObject Set_ReadiedRoom;	// ReadiedRoom Set
	public static Button[] Players;					// player buttons
	public static Image img_profile;
	public static Text txt_profile;
	public static Text txt_roominfo;
	public static Text txt_chatlog;
	public static InputField input_chat;
	public static Scrollbar scroll_chat;
	public static Button btn_chatenter;
	public static GameObject Caution;			// Caution
	public static Text txt_caution;



	// SelectMode
	public static void Ebtn_solo() {

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
		string email = input_email.text;
		string password = input_password.text;
		comm.SendMessageToServer ('I', email + " " + password);
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
	}
	// Lobby
	public static void Ebtn_randomenter() {
		comm.SendMessageToServer ('F', "");
		// UI통제 필요
	}
	public static void Ebtn_createroom() {
		ActiveUI (UIState.LOBBY_CREATEROOM);
	}
	public static void Ebtn_friends() {
		
	}
	public static void Ebtn_exit() {
		ActiveUI (UIState.MAIN_LOGIN);
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
		string maxNum = slider_number.value.ToString ();
		string isPublc;
		if(toggle_public) { isPublc = "t"; }
		else { isPublc = "f"; }
		comm.SendMessageToServer ('C', maxNum + " " + isPublc);
	}
	public static void Ebtn_createcancel() {
		ActiveUI (UIState.LOBBY_LOBBY);
	}
	// ReadiedRoom
	public static void EPlayers() {
		// player 확인 필요
	}
	public static void Escroll_chat(float value) {

	}
	public static void Ebtn_chatenter() {

	}
	
	public static void InactiveUI() {
		
		Main.SetActive (false);
		Set_SelectMode.SetActive (false);
		Set_Login.SetActive (false);
		
		Lobby.SetActive (false);
		Set_Lobby.SetActive (false);
		Set_CreateRoom.SetActive (false);

		Room.SetActive (false);
		
		Caution.SetActive (false);
	}
	public static void ActiveUI (UIState state) {
		UIManagement.UIEventList.Add (state);
	}
	public static void ActiveUI_(UIState state) {
		InactiveUI ();
		switch (state) {
		case UIState.MAIN_SELECT:
			Main.SetActive(true);
			Set_SelectMode.SetActive(true);
			uiState = UIState.MAIN_SELECT;
			break;
		case UIState.MAIN_LOGIN:
			Main.SetActive(true);
			Set_Login.SetActive(true);
			input_email.Select();
			uiState = UIState.MAIN_LOGIN;
			StructManager.roomListIndex = 0;
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
		case UIState.ROOM_READIED:
			Room.SetActive(true);
			Set_ReadiedRoom.SetActive(true);
			input_chat.Select();
			uiState = UIState.ROOM_READIED;
			break;
		case UIState.CAUTION:
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
			} else if(uiState == UIState.ROOM_READIED) {
				Room.SetActive(true);
				Set_ReadiedRoom.SetActive(true);
			}
			Caution.SetActive(true);
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
