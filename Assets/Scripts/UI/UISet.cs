using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UISet : MonoBehaviour {
	public static bool uiLock;
	public static Communication comm;

	public enum UIState { MAIN, MAIN_SELECT, MAIN_LOGIN, 
		LOBBY_LOBBY, ROOM_READIED_PUBLIC, ROOM_READIED_PRIVATE, ROOM_STARTED, CAUTION};
	public static UIState uiState;

	public static bool autoLoginFlag;

	public static GameObject Main;				// Main
	public static GameObject Set_Main;			// Main Set
	public static Button btn_startgame;
	public static Button btn_setting;
	public static Button btn_staff;
	public static Button btn_exit;
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
	public static Button btn_roomenter;
	public static Button btn_createroom;
	public static Button btn_createprivateroom;
	public static Button btn_friends;
	public static Button btn_lobbyexit;
	public static GameObject Room;				// Room
	public static GameObject Set_Room;			// Room Set
	public static Button[] Players;
	public static Image img_profile;
	public static Text txt_chatlog;
	public static Scrollbar scroll_chat;
	public static InputField input_chat;
	public static Button btn_chatenter;
	public static GameObject Set_ReadiedRoom;	// ReadiedRoom Set
	public static Text txt_profile;
	public static Text txt_roominfo;
	public static Button btn_roomexit;
	public static Button btn_roominvite;
	public static GameObject Set_StartedRoom;	// StartedRoom Set
	public static Text txt_status;
	public static Text txt_timernotice;
	public static Text txt_cardnotice;
	public static Scrollbar scroll_cardset;
	public static GameObject Caution;			// Caution
	public static Text txt_caution;
	public static GameObject Loading;			// Loading
	public static Image img_loadcircle;

	// Main
	public static void Ebtn_startgame() {
		if (uiLock) { return; }
		ActiveUI (UIState.MAIN_SELECT);
	}
	public static void Ebtn_setting() {
		if (uiLock) { return; }

	}
	public static void Ebtn_staff() {
		if (uiLock) { return; }

	}
	public static void Ebtn_exit() {
		Application.Quit ();
	}
	// SelectMode
	public static void Ebtn_solo() {
		if (uiLock) { return; }
	}
	public static void Ebtn_multi() {
		if (uiLock) { return; }
		ActiveUI (UIState.MAIN_LOGIN);
	}
	// Login
	public static void Einput_email<String>(String str) {
		if (uiLock) { return; }
		
	}
	public static void Einput_password<String>(String str) {
		if (uiLock) { return; }
		
	}
	public static void Etoggle_remember(bool check) {
		if (uiLock) { return; }
		
	}
	public static void Etoggle_autologin(bool check) {
		if (uiLock) { return; }

	}
	public static void Ebtn_signup() {
		if (uiLock) { return; }
		comm = Communication.Instance();
		string email = input_email.text;
		string password = input_password.text;
		comm.SendMessageToServer ('I', email + " " + password);
		UISet.SetUILock(true);
	}
	public static void Ebtn_cancel() {
		if (uiLock) { return; }
		input_email.text = "";
		input_password.text = "";
		ActiveUI (UIState.MAIN);
	}
	public static void Ebtn_login() {
		if (uiLock) { return; }
		comm = Communication.Instance();
		string email = input_email.text;
		string password = input_password.text;

		if (toggle_remember.isOn && toggle_autologin.isOn) {
			FileSystem.WritePasswordConfig (email, password);
		} else if (!toggle_remember.isOn && !toggle_autologin.isOn) {
			FileSystem.WritePasswordConfig ();
		} else if (toggle_remember.isOn) {
			FileSystem.WriteEmailConfig (email);
		} else if (!toggle_remember.isOn) {
			FileSystem.WriteEmailConfig ();
		} 

		comm.SendMessageToServer ('A', email + " " + password);
		UISet.SetUILock(true);
	}
	// Lobby
	public static void Ebtn_roomenter() {
		if (uiLock) { return; }
		comm.SendMessageToServer ('F', "");
		UISet.SetUILock(true);
	}
	public static void Ebtn_createroom() {
		if (uiLock) { return; }
		comm.SendMessageToServer ('C', "t");
		UISet.SetUILock(true);
	}
	public static void Ebtn_createprivateroom() {
		if (uiLock) { return; }
		comm.SendMessageToServer ('C', "f");
		UISet.SetUILock(true);
	}
	public static void Ebtn_friends() {
		if (uiLock) { return; }
		 
	}
	public static void Ebtn_lobbyexit() {
		if (uiLock) { return; }
		ActiveUI (UIState.MAIN_LOGIN);
	}
	// ReadiedRoom
	public static void EPlayers(string _player) {
		if (uiLock) { return; }
		if (UICard.selectedCard == -1) { Debug.Log ("notSelected!"); return; }

		bool isNeedTarget = (UICard.selectedCard == 30 || UICard.selectedCard == 34 || 
		                     UICard.selectedCard == 37 || UICard.selectedCard == 60 ||
		                     UICard.selectedCard == 61 || UICard.selectedCard == 69);

		if (isNeedTarget) {
			if (_player.Equals (StructManager.user.id)) {
				SetChat("<SYSTEM> 현재 선택된 카드(" + CardGenerator.GetCardNameFromNum(UICard.selectedCard) + 
				        ")는 자기자신에게 사용할 수 없습니다.");
				return;
			} else {
				string msg = UICard.selectedCard + " " + _player;
				comm.SendMessageToServer('N', msg);
				UICard.cards[UICard.selectedCardNum].CardDestroy();
				UICard.selectedCard = -1;
				UICard.selectedCardNum = -1;
			}
		}
	}
	public static void Ebtn_chatenter() {
		if (uiLock) { return; }
		if(input_chat.text == null || input_chat.text.Equals("")) { return; }
		comm.SendMessageToServer ('G', input_chat.text);
		input_chat.text = "";
	}
	public static void Ebtn_roominvite() {
		
	}
	public static void Ebtn_roomexit() {
		comm.SendMessageToServer ('H', StructManager.myRoomInfo.roomNumber.ToString());
		StructManager.myRoomInfo = null;
		UISet.ActiveUI (UISet.UIState.LOBBY_LOBBY);
	}

	public static void SetUILock(bool check) {
		UIManagement.UILockList.Add (check);
	}
	public static void SetUILock_(bool check) {
		uiLock = check;
		if (check) {
			Loading.SetActive (true);
		} else {
			Loading.SetActive (false);
		}
	}
	public static void InactiveUI() {
		Main.SetActive (false);
		Set_Main.SetActive (false);
		Set_SelectMode.SetActive (false);
		Set_Login.SetActive (false);
		Lobby.SetActive (false);
		Set_Lobby.SetActive (false);
		Room.SetActive (false);
		Set_StartedRoom.SetActive (false);
		Set_ReadiedRoom.SetActive (false);
		Caution.SetActive (false);
		Loading.SetActive (false);
	}
	public static void ActiveUI (UIState state) {
		UIManagement.UIEventList.Add (state);
	}
	public static void ActiveUI_(UIState state) {
		InactiveUI ();
		switch (state) {
		case UIState.MAIN:
			Main.SetActive(true);
			Set_Main.SetActive (true);
			uiState = UIState.MAIN;
			break;
		case UIState.MAIN_SELECT:
			Main.SetActive(true);
			Set_SelectMode.SetActive(true);
			uiState = UIState.MAIN_SELECT;
			break;
		case UIState.MAIN_LOGIN:
			Main.SetActive(true);
			Set_Login.SetActive(true);
			input_email.Select();
			toggle_remember.isOn = GlobalConfig.isEmailRemember;
			toggle_autologin.isOn = GlobalConfig.isAutoLogin;
			if(GlobalConfig.email != null) {
				input_email.text = GlobalConfig.email;
			}
			if(GlobalConfig.password != null) {
				input_password.text = GlobalConfig.password;
			}
			if(GlobalConfig.isAutoLogin) {
				if(autoLoginFlag) {
					Ebtn_login();
					autoLoginFlag = false;
				}
			}
			uiState = UIState.MAIN_LOGIN;
			break;
		case UIState.LOBBY_LOBBY:
			Lobby.SetActive(true);
			Set_Lobby.SetActive(true);
			txt_chatlog.text = "";
			uiState = UIState.LOBBY_LOBBY;
			break;
		case UIState.ROOM_READIED_PUBLIC:
			Room.SetActive(true);
			Set_ReadiedRoom.SetActive(true);
			btn_roominvite.gameObject.SetActive(false);
			uiState = UIState.ROOM_READIED_PUBLIC;
			break;
		case UIState.ROOM_READIED_PRIVATE:
			Room.SetActive(true);
			Set_ReadiedRoom.SetActive(true);
			btn_roominvite.gameObject.SetActive(true);
			uiState = UIState.ROOM_READIED_PRIVATE;
			break;
		case UIState.ROOM_STARTED:
			Room.SetActive(true);
			Set_StartedRoom.SetActive(true);
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
			} else if(uiState == UIState.ROOM_READIED_PRIVATE) {
				Room.SetActive(true);
				Set_ReadiedRoom.SetActive(true);
				btn_roominvite.gameObject.SetActive(false);
			} else if(uiState == UIState.ROOM_READIED_PUBLIC) {
				Room.SetActive(true);
				Set_ReadiedRoom.SetActive(true);
				btn_roominvite.gameObject.SetActive(true);
			} else if(uiState == UIState.ROOM_STARTED) {
				Room.SetActive(true);
				Set_StartedRoom.SetActive(true);
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
	public static void SetChat (string text) {
		StructManager.myRoomInfo.chatLog += text + "\n";
		UIManagement.chatUpdateFlag = true;
	}
	public static void SetDebug (string text) {
		UIManagement.debug = text;
	}
}
