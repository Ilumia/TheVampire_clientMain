using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class UIManagement : MonoBehaviour {
	public static EventSystem eventSystem;
	public static List<UISet.UIState> UIEventList;
	public static List<bool> UILockList;
	public static List<string> UICautionList;
	public static List<string> UIRoomList;
	public static bool chatUpdateFlag;
	public static bool roomUpdateFlag;
	private int frameCounter;

	void Awake () {
		eventSystem = EventSystem.current;
		UIEventList = new List<UISet.UIState> ();
		UILockList = new List<bool> ();
		UICautionList = new List<string> ();
		UIRoomList = new List<string> ();
		roomUpdateFlag = false;
		chatUpdateFlag = true;
		frameCounter = 0;

		// Initialize UI Set
		// *Main
		UISet.Main = GameObject.Find("Main");
		// Main
		UISet.Set_Main = GameObject.Find ("Set_Main");
		UISet.btn_startgame = GameObject.Find ("btn_startgame").GetComponent<Button>();
		UISet.btn_setting = GameObject.Find ("btn_setting").GetComponent<Button>();
		UISet.btn_staff = GameObject.Find ("btn_staff").GetComponent<Button>();
		UISet.btn_exit = GameObject.Find ("btn_exit").GetComponent<Button>();
		// SelectMode
		UISet.Set_SelectMode = GameObject.Find("Set_SelectMode");
		UISet.btn_solo = GameObject.Find("btn_solo").GetComponent<Button>();
		UISet.btn_multi = GameObject.Find("btn_multi").GetComponent<Button>();
		// Login
		UISet.Set_Login = GameObject.Find("Set_Login");
		UISet.input_email = GameObject.Find("input_email").GetComponent<InputField>();
		UISet.input_password = GameObject.Find("input_password").GetComponent<InputField>();
		UISet.toggle_remember = GameObject.Find("toggle_remember").GetComponent<Toggle>();
		UISet.toggle_autologin = GameObject.Find("toggle_autologin").GetComponent<Toggle>();
		UISet.btn_signup = GameObject.Find("btn_signup").GetComponent<Button>();
		UISet.btn_cancel = GameObject.Find("btn_cancel").GetComponent<Button>();
		UISet.btn_login = GameObject.Find("btn_login").GetComponent<Button>();
		// *Lobby
		UISet.Lobby = GameObject.Find("Lobby");
		// Lobby
		UISet.Set_Lobby = GameObject.Find("Set_Lobby");
		UISet.btn_roomenter = GameObject.Find("btn_roomenter").GetComponent<Button> ();
		UISet.btn_createroom = GameObject.Find("btn_createroom").GetComponent<Button>();
		UISet.btn_createprivateroom = GameObject.Find ("btn_createprivateroom").GetComponent<Button> ();
		UISet.btn_friends = GameObject.Find("btn_friends").GetComponent<Button>();
		UISet.btn_lobbyexit = GameObject.Find("btn_lobbyexit").GetComponent<Button>();
		// *Room
		UISet.Room = GameObject.Find ("Room");
		// ReadiedRoom
		UISet.Set_ReadiedRoom = GameObject.Find ("Set_ReadiedRoom");
		UISet.txt_profile = GameObject.Find ("txt_profile").GetComponent<Text> ();
		UISet.txt_roominfo = GameObject.Find ("txt_roominfo").GetComponent<Text> ();
		UISet.btn_roominvite = GameObject.Find ("btn_roominvite").GetComponent<Button>();
		UISet.btn_roomexit = GameObject.Find ("btn_roomexit").GetComponent<Button>();
		UISet.Set_StartedRoom = GameObject.Find ("Set_StartedRoom");
		UISet.Players = new Button[4];
		for (int i=0; i<UISet.Players.Length; i++) {
			UISet.Players[i] = GameObject.Find("Player (" + i + ")").GetComponent<Button>();
		}
		UISet.img_profile = GameObject.Find ("img_profile").GetComponent<Image> ();
		UISet.txt_chatlog = GameObject.Find ("txt_chatlog").GetComponent<Text> ();
		UISet.input_chat = GameObject.Find ("input_chat").GetComponent<InputField> ();
		UISet.btn_chatenter = GameObject.Find ("btn_chatenter").GetComponent<Button> ();
		// Caution
		UISet.Caution = GameObject.Find("Caution");
		UISet.txt_caution = GameObject.Find("txt_caution").GetComponent<Text>();
		// Loading
		UISet.Loading = GameObject.Find ("Loading");
		UISet.img_loadcircle = GameObject.Find ("img_loadcircle").GetComponent<Image> ();
		
		// Initialize click event
		// Main
		UISet.btn_startgame.onClick.AddListener (UISet.Ebtn_startgame);
		UISet.btn_setting.onClick.AddListener (UISet.Ebtn_setting);
		UISet.btn_staff.onClick.AddListener (UISet.Ebtn_staff);
		UISet.btn_exit.onClick.AddListener (UISet.Ebtn_exit);
		// SelectMode
		UISet.btn_solo.onClick.AddListener (UISet.Ebtn_solo);
		UISet.btn_multi.onClick.AddListener (UISet.Ebtn_multi);
		// Login
		UISet.input_email.onValueChange.AddListener (UISet.Einput_email);
		UISet.input_password.onValueChange.AddListener (UISet.Einput_password);
		UISet.toggle_remember.onValueChanged.AddListener (UISet.Etoggle_remember);
		UISet.toggle_autologin.onValueChanged.AddListener (UISet.Etoggle_autologin);
		UISet.btn_signup.onClick.AddListener (UISet.Ebtn_signup);
		UISet.btn_cancel.onClick.AddListener (UISet.Ebtn_cancel);
		UISet.btn_login.onClick.AddListener (UISet.Ebtn_login);
		// Lobby
		UISet.btn_roomenter.onClick.AddListener (UISet.Ebtn_roomenter);
		UISet.btn_createroom.onClick.AddListener (UISet.Ebtn_createroom);
		UISet.btn_createprivateroom.onClick.AddListener (UISet.Ebtn_createprivateroom);
		UISet.btn_friends.onClick.AddListener (UISet.Ebtn_friends);
		UISet.btn_lobbyexit.onClick.AddListener (UISet.Ebtn_lobbyexit);
		foreach (Button player in UISet.Players) {
			player.onClick.AddListener(UISet.EPlayers);
		}
		UISet.btn_chatenter.onClick.AddListener (UISet.Ebtn_chatenter);
		UISet.btn_roominvite.onClick.AddListener (UISet.Ebtn_roominvite);
		UISet.btn_roomexit.onClick.AddListener (UISet.Ebtn_roomexit);

		UISet.ActiveUI (UISet.UIState.MAIN);
		UISet.autoLoginFlag = true;

	}
	
	// Update is called once per frame
	void Update () {
		UIEventProcessing ();
		UILockProcessing ();
		UICautionProcessing ();
		UITabProcessing ();
		UIEnterProcessing ();
		UIChatUpdateProcessing ();
		UIRoomUpdateProcessing ();
		UILoadingProcessing ();
	}
	void UIEventProcessing() {
		if (UIEventList.Count == 0) {
			return;
		}
		UISet.ActiveUI_ (UIEventList [0]);
		UIEventList.RemoveAt (0);
	}
	void UILockProcessing() {
		if (UILockList.Count == 0) {
			return;
		}
		UISet.SetUILock_ (UILockList [0]);
		UILockList.RemoveAt (0);
	}
	void UICautionProcessing() {
		if (UISet.Caution.activeSelf) {
			if(Input.GetMouseButtonDown(0)) {
				UISet.ActiveUI(UISet.uiState);
			}
		}
		if (UICautionList.Count == 0) {
			return;
		}
		UISet.SetCaution_ (UICautionList [0]);
		UICautionList.RemoveAt (0);
	}
	void UITabProcessing() {
		// Tab in login
		if (Input.GetKeyDown (KeyCode.Tab)) {
			if (UISet.uiState == UISet.UIState.MAIN_LOGIN) {
				if(eventSystem.currentSelectedGameObject.name.Equals(UISet.input_email.name)) {
					UISet.input_password.Select();
				}
			}
		}
	}
	void UIEnterProcessing() {
		if (Input.GetKeyDown (KeyCode.Return)) {
			if (UISet.uiState == UISet.UIState.MAIN_LOGIN) {
				if (eventSystem.currentSelectedGameObject.name.Equals (UISet.input_password.name)) {
					UISet.Ebtn_login ();
				}
			} else if (UISet.uiState == UISet.UIState.ROOM_READIED_PUBLIC || 
			           UISet.uiState == UISet.UIState.ROOM_READIED_PRIVATE) {
				if (eventSystem.currentSelectedGameObject == null) { 
					UISet.input_chat.Select();
				} else if (eventSystem.currentSelectedGameObject.name.Equals (UISet.input_chat.name)) {
					UISet.Ebtn_chatenter ();
				} else {
					UISet.input_chat.Select();
				}
			}
		}
	}
	void UIChatUpdateProcessing() {
		if (!chatUpdateFlag) { return; }
		chatUpdateFlag = false;
		UISet.txt_chatlog.text = StructManager.myRoomInfo.chatLog;
	}
	void UIRoomUpdateProcessing() {
		if (!roomUpdateFlag) { return; }
		roomUpdateFlag = false;

		UISet.SetUILock(false);
		UISet.ActiveUI (UISet.uiState);

		string roomInfo = "방 번호: ";
		roomInfo += StructManager.myRoomInfo.roomNumber.ToString () + "\n\n";
		if (StructManager.myRoomInfo.isPublic) {
			roomInfo += "공개방\n";
		} else {
			roomInfo += "비밀방\n";
		}
		roomInfo += "인원: ";
		roomInfo += StructManager.myRoomInfo.totalNumber.ToString () + " / ";
		roomInfo += StructManager.myRoomInfo.maximumNumber.ToString () + "\n";
		roomInfo += "방 개설자: ";
		roomInfo += StructManager.myRoomInfo.owner.id + "\n";
		UISet.txt_roominfo.text = roomInfo;

		for(int i=0; i<StructManager.myRoomInfo.users.Count; i++) {
			UISet.Players[i].transform.GetChild(0).GetComponent<Text>().text = StructManager.myRoomInfo.users[i].id;
			Debug.Log (StructManager.myRoomInfo.users[i].id);
		}
	}
	void UILoadingProcessing() {
		if (frameCounter != 2) {
			frameCounter++;
			return;
		}
		frameCounter = 0;
		if (UISet.Loading.activeInHierarchy) {
			UISet.img_loadcircle.transform.Rotate(Vector3.back, 30.0f);
		}
	}
}
