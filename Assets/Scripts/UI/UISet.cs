using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UISet : MonoBehaviour {
	public static bool uiLock;
	public static Communication comm;

	public enum UIState { MAIN, MAIN_SELECT, MAIN_LOGIN, 
		LOBBY_LOBBY, ROOM_READIED_PUBLIC, ROOM_READIED_PRIVATE, ROOM_STARTED, CAUTION};
	public static UIState uiState;
	static UICard selectedCard;

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
	//public static Image[] PlayerHP;
	public static Dictionary<string, Image> playerHP;
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
	public static Button btn_getinfocard;
	public static Button btn_getbattlecard;
	public static GameObject Set_BigCardPanel;	// BigCard Set in StartedRoom Set
	public static Text txt_bigcarddescription;
	public static Image img_bigcardimage;
	public static Text txt_bigcardname;
	public static Button btn_carduse;
	public static Button btn_cardcancel;
	public static GameObject Caution;			// Caution
	public static Text txt_caution;
	public static GameObject Loading;			// Loading
	public static Image img_loadcircle;

	// Main
	public static void Ebtn_startgame() {
		if (uiLock) { return; }
		SoundManager.PlayEffectButtonClick ();
		ActiveUI (UIState.MAIN_SELECT);
	}
	public static void Ebtn_setting() {
		if (uiLock) { return; }
		SoundManager.PlayEffectButtonClick ();

	}
	public static void Ebtn_staff() {
		if (uiLock) { return; }
		SoundManager.PlayEffectButtonClick ();

	}
	public static void Ebtn_exit() {
		SoundManager.PlayEffectButtonClick ();
		Application.Quit ();
	}
	// SelectMode
	public static void Ebtn_solo() {
		if (uiLock) { return; }
		SoundManager.PlayEffectButtonClick ();
	}
	public static void Ebtn_multi() {
		if (uiLock) { return; }
		SoundManager.PlayEffectButtonClick ();
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
		SoundManager.PlayEffectButtonClick ();
		comm = Communication.Instance();
		string email = input_email.text;
		string password = input_password.text;
		comm.SendMessageToServer ('I', email + " " + password);
		UISet.SetUILock(true);
	}
	public static void Ebtn_cancel() {
		if (uiLock) { return; }
		SoundManager.PlayEffectButtonClick ();
		input_email.text = "";
		input_password.text = "";
		ActiveUI (UIState.MAIN);
	}
	public static void Ebtn_login() {
		if (uiLock) { return; }
		SoundManager.PlayEffectButtonClick ();
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
		SoundManager.PlayEffectButtonClick ();
		comm.SendMessageToServer ('F', "");
		UISet.SetUILock(true);
	}
	public static void Ebtn_createroom() {
		if (uiLock) { return; }
		SoundManager.PlayEffectButtonClick ();
		comm.SendMessageToServer ('C', "t");
		UISet.SetUILock(true);
	}
	public static void Ebtn_createprivateroom() {
		if (uiLock) { return; }
		SoundManager.PlayEffectButtonClick ();
		comm.SendMessageToServer ('C', "f");
		UISet.SetUILock(true);
	}
	public static void Ebtn_friends() {
		if (uiLock) { return; }
		SoundManager.PlayEffectButtonClick ();

	}
	public static void Ebtn_lobbyexit() {
		if (uiLock) { return; }
		SoundManager.PlayEffectButtonClick ();
		ActiveUI (UIState.MAIN_LOGIN);
	}
	// ReadiedRoom
	public static void EPlayers(string _player) {
		if (uiLock) { return; }
		if (selectedCard == null) { return; }
		if (StructManager.myRoomInfo.users [StructManager.user.id].hp <= 0) {
			return;
		}
		SoundManager.PlayEffectCardSelect ();
		
		if (_player.Equals (StructManager.user.id)) {
			SetChat("<SYSTEM> 현재 선택된 카드(" + selectedCard.nameString + 
			        ")는 자기자신에게 사용할 수 없습니다.");
			return;
		} else {
			string msg = selectedCard.id + " " + _player;
			comm.SendMessageToServer('M', msg);
			UICard.cards[selectedCard.index].CardDestroy();
			selectedCard.CardDestroy();
			selectedCard = null;
			SetActiveBigCard(false, null);
		}
	}
	public static void Ebtn_chatenter() {
		if (uiLock) { return; }
		if(input_chat.text == null || input_chat.text.Equals("")) { return; }
		SoundManager.PlayEffectButtonClick ();
		comm.SendMessageToServer ('G', input_chat.text);
		input_chat.text = "";
	}
	public static void Ebtn_roominvite() {
		SoundManager.PlayEffectButtonClick ();

	}
	public static void Ebtn_roomexit() {
		SoundManager.PlayEffectButtonClick ();
		comm.SendMessageToServer ('H', StructManager.myRoomInfo.roomNumber.ToString());
		UISet.ActiveUI (UISet.UIState.LOBBY_LOBBY);
		SetRoomDefault ();
	}
	// StartedRoom
	public static void Ebtn_getinfocard() {
		if (StructManager.myRoomInfo.users [StructManager.user.id].hp <= 0) {
			return;
		}
		SoundManager.PlayEffectCardSelect ();
		SetUILock (true);
		if (UICard.gettedCard > 0) {
			UICard.gettedCard--;
			UICard.cards.Add (new UICard (CardGenerator.GetCard (CardType.INFO)).SetOrder (0));
		}
		SetUILock (false);
		UIManagement.cardUpdateFlag = true;
		UIManagement.cardNotice = UICard.gettedCard.ToString ();
	}
	public static void Ebtn_getbattlecard() {
		if (StructManager.myRoomInfo.users [StructManager.user.id].hp <= 0) {
			return;
		}
		SoundManager.PlayEffectCardSelect ();
		SetUILock (true);
		if (UICard.gettedCard > 0) {
			UICard.gettedCard--;
			UICard.cards.Add (new UICard (CardGenerator.GetCard (CardType.BATTLE)).SetOrder (0));
		}
		SetUILock (false);
		UIManagement.cardUpdateFlag = true;
		UIManagement.cardNotice = UICard.gettedCard.ToString ();
	}
	public static void Ebtn_carduse() {
		if (StructManager.myRoomInfo.users [StructManager.user.id].hp <= 0) {
			SetActiveBigCard(false, null);
			return;
		}
		SoundManager.PlayEffectCardSelect ();
		int id = selectedCard.id;
		bool isTargeting = (id == 30 || id == 34 || id == 37 || id == 60 || id == 61 || id == 69);
		if (!isTargeting) {
			Communication.Instance().SendMessageToServer('M', selectedCard.id.ToString());
			selectedCard.CardDestroy();
			selectedCard = null;
		}
		if (id == 38 || id == 70) {
			UICard.gettedCard += 2;
			SetChat("<SYSTEM> [행동 재개]의 효과로 두 개의 카드를 더 뽑을 수 있습니다.");
			UIManagement.cardNotice = UICard.gettedCard + "";
		}
		SetActiveBigCard(false, null);
		UIManagement.cardUpdateFlag = true;
	}
	public static void Ebtn_cardcancel() {
		SoundManager.PlayEffectCardSelect ();
		SetActiveBigCard (false, null);
		selectedCard = null;
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
	public static void SetActiveBigCard(bool check, UICard uiCard) {
		if (StructManager.myRoomInfo.users [StructManager.user.id].hp <= 0) {
			return;
		}
		if (uiCard == null) {
			Set_BigCardPanel.SetActive (false);
			return;
		}
		if (check) {
			selectedCard = uiCard;
			Set_BigCardPanel.SetActive (true);
			txt_bigcarddescription.text = selectedCard.description;
			txt_bigcardname.text = selectedCard.nameString;
			img_bigcardimage.sprite = selectedCard.img;
		} else {
			Set_BigCardPanel.SetActive (false);
			return;
		}
	}
	public static void SetRoomDefault() {
		StructManager.myRoomInfo = null;
		for (int i=0; i<4; i++) {
			Players [i].GetComponentInChildren<Text> ().text = "";
		}
		UICard.gettedCard = 3;
		UISet.img_profile.sprite = Resources.Load<Sprite> ("UI/alpha");
		UISet.txt_profile.text = "";
		UISet.txt_roominfo.text = "";
		UISet.txt_chatlog.text = "";
		UISet.txt_status.text = "";
		UISet.txt_timernotice.text = "";
		UISet.txt_cardnotice.text = "3";
		for(int i=UICard.cards.Count - 1; i>=0; i--) {
			UICard.cards[i].CardDestroy();
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
		Set_ReadiedRoom.SetActive (false);
		Set_StartedRoom.SetActive (false);
		Set_BigCardPanel.SetActive (false);
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
			uiState = UIState.ROOM_STARTED;
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
		if (StructManager.myRoomInfo != null) {
			StructManager.myRoomInfo.chatLog += text + "\n";
		}
		UIManagement.chatUpdateFlag = true;
	}
	public static void SetDebug (string text) {
		UIManagement.debug = text;
	}
}
