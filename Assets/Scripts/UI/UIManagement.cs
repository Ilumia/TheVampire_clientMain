using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class UIManagement : MonoBehaviour {
	public static EventSystem eventSystem;
	public static List<UISet.UIState> UIEventList;
	public static List<string> UICautionList;
	public static List<string> UIRoomList;

	void Awake () {
		eventSystem = EventSystem.current;
		UIEventList = new List<UISet.UIState> ();
		UICautionList = new List<string> ();
		UIRoomList = new List<string> ();

		// Initialize UI Set
		// *Main
		UISet.Main = GameObject.Find("Main");
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
		UISet.btn_randomenter = GameObject.Find("btn_randomenter").GetComponent<Button> ();
		UISet.btn_createroom = GameObject.Find("btn_createroom").GetComponent<Button>();
		UISet.btn_friends = GameObject.Find("btn_friends").GetComponent<Button>();
		UISet.btn_exit = GameObject.Find("btn_exit").GetComponent<Button>();
		// CreateRoom
		UISet.Set_CreateRoom = GameObject.Find ("Set_CreateRoom");
		UISet.slider_number = GameObject.Find ("slider_number").GetComponent<Slider> ();
		UISet.toggle_public = GameObject.Find ("toggle_public").GetComponent<Toggle> ();
		UISet.btn_createconfirm = GameObject.Find ("btn_createconfirm").GetComponent<Button> ();
		UISet.btn_createcancel = GameObject.Find ("btn_createcancel").GetComponent<Button> ();
		UISet.txt_numbercount = GameObject.Find ("txt_numbercount").GetComponent<Text> ();
		// *Room
		UISet.Room = GameObject.Find ("Room");
		// ReadiedRoom
		UISet.Set_ReadiedRoom = GameObject.Find ("Set_ReadiedRoom");
		UISet.Players = new Button[14];
		for (int i=0; i<UISet.Players.Length; i++) {
			UISet.Players[i] = GameObject.Find("Player (" + i + ")").GetComponent<Button>();
		}
		UISet.img_profile = GameObject.Find ("img_profile").GetComponent<Image> ();
		UISet.txt_profile = GameObject.Find ("txt_profile").GetComponent<Text> ();
		UISet.txt_roominfo = GameObject.Find ("txt_roominfo").GetComponent<Text> ();
		UISet.txt_chatlog = GameObject.Find ("txt_chatlog").GetComponent<Text> ();
		UISet.input_chat = GameObject.Find ("input_chat").GetComponent<InputField> ();
		UISet.scroll_chat = GameObject.Find ("scroll_chat").GetComponent<Scrollbar> ();
		UISet.btn_chatenter = GameObject.Find ("btn_chatenter").GetComponent<Button> ();
		// Caution
		UISet.Caution = GameObject.Find("Caution");
		UISet.txt_caution = GameObject.Find("txt_caution").GetComponent<Text>();
		
		// Initialize click event
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
		UISet.btn_randomenter.onClick.AddListener (UISet.Ebtn_randomenter);
		UISet.btn_createroom.onClick.AddListener (UISet.Ebtn_createroom);
		UISet.btn_friends.onClick.AddListener (UISet.Ebtn_friends);
		UISet.btn_exit.onClick.AddListener (UISet.Ebtn_exit);
		// CreateRoom
		UISet.slider_number.onValueChanged.AddListener (UISet.Eslider_number);
		UISet.toggle_public.onValueChanged.AddListener (UISet.Etoggle_public);
		UISet.btn_createconfirm.onClick.AddListener (UISet.Ebtn_createconfirm);
		UISet.btn_createcancel.onClick.AddListener (UISet.Ebtn_createcancel);
		foreach (Button player in UISet.Players) {
			player.onClick.AddListener(UISet.EPlayers);
		}
		UISet.scroll_chat.onValueChanged.AddListener (UISet.Escroll_chat);
		UISet.btn_chatenter.onClick.AddListener (UISet.Ebtn_chatenter);
		
		UISet.ActiveUI (UISet.UIState.MAIN_SELECT);

	}
	
	// Update is called once per frame
	void Update () {
		UIEventProcessing ();
		UICautionProcessing ();
		UITabProcessing ();
		UIEnterProcessing ();
	}
	void UIEventProcessing() {
		if (UIEventList.Count == 0) {
			return;
		}
		UISet.ActiveUI_ (UIEventList [0]);
		UIEventList.RemoveAt (0);
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
		if (UISet.uiState == UISet.UIState.MAIN_LOGIN) {
			if(eventSystem.currentSelectedGameObject.name.Equals(UISet.input_email.name)) {
				if(Input.GetKeyDown(KeyCode.Tab)) {
					UISet.input_password.Select();
				}
			}
		}
	}
	void UIEnterProcessing() {
		if (UISet.uiState == UISet.UIState.MAIN_LOGIN) {
			// Enter in login
			if (eventSystem.currentSelectedGameObject.name.Equals (UISet.input_password.name)) {
				if (Input.GetKeyDown (KeyCode.Return)) {
					UISet.Ebtn_login ();
				}
			}
		} else if (UISet.uiState == UISet.UIState.ROOM_READIED) {
			// Enter in chat
			if (eventSystem.currentSelectedGameObject.name.Equals (UISet.input_chat.name)) {
				if (Input.GetKeyDown (KeyCode.Return)) {
					UISet.Ebtn_chatenter ();
				}
			}
		}
	}
}
