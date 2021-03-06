﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Text;
using System.Collections;
using System.Collections.Generic;

public class UIManagement : MonoBehaviour {
	public static EventSystem eventSystem;
	public static List<UISet.UIState> UIEventList;
	public static List<bool> UILockList;
	public static float uiLockTimer;
	public static List<string> UICautionList;
	public static List<string> UIRoomList;
	public static bool chatUpdateFlag;
	public static bool roomUpdateFlag;
	public static bool roomResetFlag;
	public static string status;
	public static bool hpUpdateFlag;
	public static bool profileUpdateFlag;
	public static string timerNotice;
	public static string cardNotice;
	private int frameCounter;
	public static bool cardUpdateFlag;
	
	//for debugging
	public static string debug;

	void Start () {
		UICard.cards.Add (new UICard (CardGenerator.GetCard (CardType.BATTLE)).SetOrder (0));
		UICard.cards [0].CardDestroy ();
		UICard.cards.Add (new UICard (CardGenerator.GetCard (CardType.BATTLE)).SetOrder (0));
		UICard.cards.Add (new UICard (CardGenerator.GetCard (CardType.BATTLE)).SetOrder (1));
		//UICard.cards.Add (new UICard (CardGenerator.GetCard (CardType.BATTLE)).SetOrder (2));
		//UICard.cards.Add (new UICard (CardGenerator.GetCard (CardType.BATTLE)).SetOrder (3));
		//UISet.SetActiveBigCard (true, UICard.cards [0]);
	}

	void Awake () {
		eventSystem = EventSystem.current;
		UIEventList = new List<UISet.UIState> ();
		UILockList = new List<bool> ();
		uiLockTimer = 0.0f;
		UICautionList = new List<string> ();
		UIRoomList = new List<string> ();
		roomUpdateFlag = false;
		roomResetFlag = false;
		chatUpdateFlag = false;
		status = "";
		hpUpdateFlag = false;
		timerNotice = "";
		cardNotice = "";
		frameCounter = 0;
		cardUpdateFlag = false;
		
		//for debugging
		debug = "";

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
		UISet.playerHP = new Dictionary<string, Image> ();
		UISet.Players = new Button[4];
		for (int i=0; i<UISet.Players.Length; i++) {
			UISet.Players[i] = GameObject.Find("Player (" + i + ")").GetComponent<Button>();
		}
		UISet.img_profile = GameObject.Find ("img_profile").GetComponent<Image> ();
		UISet.txt_chatlog = GameObject.Find ("txt_chatlog").GetComponent<Text> ();
		UISet.scroll_chat = GameObject.Find ("scroll_chat").GetComponent<Scrollbar> ();
		UISet.input_chat = GameObject.Find ("input_chat").GetComponent<InputField> ();
		UISet.btn_chatenter = GameObject.Find ("btn_chatenter").GetComponent<Button> ();
		// ReadiedRoom
		UISet.Set_ReadiedRoom = GameObject.Find ("Set_ReadiedRoom");
		UISet.txt_profile = GameObject.Find ("txt_profile").GetComponent<Text> ();
		UISet.txt_roominfo = GameObject.Find ("txt_roominfo").GetComponent<Text> ();
		UISet.btn_roominvite = GameObject.Find ("btn_roominvite").GetComponent<Button>();
		UISet.btn_roomexit = GameObject.Find ("btn_roomexit").GetComponent<Button>();
		// StartedRoom
		UISet.Set_StartedRoom = GameObject.Find ("Set_StartedRoom");
		UISet.txt_status = GameObject.Find ("txt_status").GetComponent<Text> ();
		UISet.txt_timernotice = GameObject.Find ("txt_timernotice").GetComponent<Text> ();
		UISet.txt_cardnotice = GameObject.Find ("txt_cardnotice").GetComponent<Text> ();
		UISet.scroll_cardset = GameObject.Find ("scroll_cardset").GetComponent<Scrollbar> ();
		UISet.btn_getinfocard = GameObject.Find ("btn_getinfocard").GetComponent<Button> ();
		UISet.btn_getbattlecard = GameObject.Find ("btn_getbattlecard").GetComponent<Button> ();
		// BigCardPanel in StartedRoom
		UISet.Set_BigCardPanel = GameObject.Find ("Set_BigCardPanel");
		UISet.txt_bigcardname = GameObject.Find ("txt_bigcardname").GetComponent<Text> ();
		UISet.img_bigcardimage = GameObject.Find ("img_bigcardimage").GetComponent<Image> ();
		UISet.txt_bigcarddescription = GameObject.Find ("txt_bigcarddescription").GetComponent<Text> ();
		UISet.btn_carduse = GameObject.Find ("btn_carduse").GetComponent<Button> ();
		UISet.btn_cardcancel = GameObject.Find ("btn_cardcancel").GetComponent<Button> ();
		// *Caution
		UISet.Caution = GameObject.Find("Caution");
		UISet.txt_caution = GameObject.Find("txt_caution").GetComponent<Text>();
		// *Loading
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
		// Room
		foreach (Button player in UISet.Players) {
			string _playerID = player.GetComponentInChildren<Text>().text;
			player.onClick.RemoveAllListeners();
			player.onClick.AddListener(delegate{UISet.EPlayers(_playerID);});
		}
		UISet.btn_chatenter.onClick.AddListener (UISet.Ebtn_chatenter);
		UISet.btn_roominvite.onClick.AddListener (UISet.Ebtn_roominvite);
		UISet.btn_roomexit.onClick.AddListener (UISet.Ebtn_roomexit);
		UISet.btn_getinfocard.onClick.AddListener (UISet.Ebtn_getinfocard);
		UISet.btn_getbattlecard.onClick.AddListener (UISet.Ebtn_getbattlecard);
		UISet.btn_carduse.onClick.AddListener (UISet.Ebtn_carduse);
		UISet.btn_cardcancel.onClick.AddListener (UISet.Ebtn_cardcancel);

		UISet.ActiveUI (UISet.UIState.MAIN);
		UISet.autoLoginFlag = true;
	}

	void LateUpdate() {
		try {
			UIRoomResetProcessing();
			UIEventProcessing ();
			UILockProcessing ();
			UILockTimer ();
			UICautionProcessing ();
			UITabProcessing ();
			UIEnterProcessing ();
			UIChatUpdateProcessing ();
			UIRoomUpdateProcessing ();
			UIStatusProcessing();
			UILoadingProcessing ();
			UICardSetProcessing();
			
			//for debugging
			UIDebuggingProcessing ();
		} catch (UnityException e) {
			debug = e.StackTrace;
		}
	}
	// Update is called once per frame
	void Update () {
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
		uiLockTimer = 10.0f;
		UISet.SetUILock_ (UILockList [0]);
		UILockList.RemoveAt (0);
	}
	void UILockTimer() {
		if (UISet.uiLock) {
			if (uiLockTimer > 0) {
				uiLockTimer -= Time.deltaTime;
			} else {
				UISet.SetCaution("서버에 연결할 수 없습니다");
				UISet.SetUILock (false);
			}
		}
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
			           UISet.uiState == UISet.UIState.ROOM_READIED_PRIVATE ||
			           UISet.uiState == UISet.UIState.ROOM_STARTED) {
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
		if (UISet.scroll_chat.value != 0) {
			UISet.txt_chatlog.text = StructManager.myRoomInfo.chatLog;
			UISet.scroll_chat.value = 0;
		} else {
			UISet.txt_chatlog.text = StructManager.myRoomInfo.chatLog;
			UISet.scroll_chat.value = 0;
		}
	}
	void UIRoomUpdateProcessing() {
		if (!roomUpdateFlag) { return; }
		if (StructManager.myRoomInfo == null) { return; }
		roomUpdateFlag = false;

		UISet.SetUILock(false);
		UISet.ActiveUI (UISet.uiState);

		string profile = "아이디: ";
		profile += StructManager.user.id + "\n";
		profile += "전적: 0전 0승 0패";
		UISet.txt_profile.text = profile;

		string roomInfo = "방 번호: ";
		roomInfo += StructManager.myRoomInfo.roomNumber.ToString () + "\n";
		roomInfo += "방 개설자: ";
		roomInfo += StructManager.myRoomInfo.owner + "\n";
		if (StructManager.myRoomInfo.isPublic) {
			roomInfo += "공개방\n";
		} else {
			roomInfo += "비밀방\n";
		}
		roomInfo += "인원: ";
		roomInfo += StructManager.myRoomInfo.totalNumber.ToString () + " / ";
		roomInfo += StructManager.myRoomInfo.maximumNumber.ToString () + "\n";
		UISet.txt_roominfo.text = roomInfo;

		if(StructManager.myRoomInfo.users.Count == 4) {
			UISet.SetChat ("10초 후 자동으로 게임이 시작됩니다.");
		}

		UISet.playerHP = new Dictionary<string, Image> ();
		int x = 0;
		foreach (Player player in StructManager.myRoomInfo.users.Values) {
			UISet.Players[x].GetComponentInChildren<Text>().text = player.id;
			Image[] tmpImages = UISet.Players[x].GetComponentsInChildren<Image>();
			foreach(Image img in tmpImages) {
				if(img.name.Contains("hp")){
					UISet.playerHP.Add(player.id, img);
				}
				if(img.name.Contains("highlight") && player.id.Equals(StructManager.user.id)) {
					img.color = new Color(0.2109375f, 1.0f, 0.25f, 0.30078125f);
				}
			}
			x++;
		}
		for (int i=0; i<4; i++) {
			if(i >= StructManager.myRoomInfo.users.Count) {
				UISet.Players[x].GetComponentInChildren<Text>().text = "";
			}
		}
		foreach (Button player in UISet.Players) {
			string _playerID = player.GetComponentInChildren<Text>().text;
			player.onClick.RemoveAllListeners();
			player.onClick.AddListener(delegate{UISet.EPlayers(_playerID);});
		}
	}
	void UIRoomResetProcessing() {
		if (!roomResetFlag) { return; }
		roomResetFlag = false;
		StructManager.myRoomInfo = null;
		UICard.gettedCard = 3;
		UISet.txt_profile.text = "";
		UISet.txt_roominfo.text = "";
		UISet.txt_chatlog.text = "";
		UISet.txt_status.text = "";
		UISet.txt_timernotice.text = "15";
		UISet.txt_cardnotice.text = "3";
		foreach (Button player in UISet.Players) {
			player.GetComponentInChildren<Text>().text = "";
		}
		foreach (Image img in UISet.playerHP.Values) {
			img.sprite = Resources.Load<Sprite>("UI/alpha");
		}
		for(int i=UICard.cards.Count - 1; i>=0; i--) {
			UICard.cards[i].CardDestroy();
		}
	}
	void UIStatusProcessing() {
		if (!status.Equals ("")) {
			UISet.txt_status.text = status;
			status = "";
		}
		if (hpUpdateFlag) {
			if(StructManager.myRoomInfo != null) { 
				hpUpdateFlag = false;
				foreach(Player _player in StructManager.myRoomInfo.users.Values) {
					Texture2D texture = new Texture2D(100, 1);
					for(int i=0; i<100; i++) {
						if(i < _player.hp) {
							texture.SetPixel(i, 0, new Color(188.8f, 0, 0));
						} else {
							texture.SetPixel(i, 0, Color.black);
						}
					}
					texture.Apply();
					Rect rect = new Rect (0, 0, 100, 1);
					Sprite sprite = Sprite.Create (texture, rect, new Vector2 (0.5f, 0.5f));
					UISet.playerHP[_player.id].sprite = sprite;
				}
			}
		}
		if (profileUpdateFlag) {
			if(StructManager.user.isVampire) {
				UISet.img_profile.sprite = Resources.Load<Sprite>("UI/icon_vampire");
			} else {
				UISet.img_profile.sprite = Resources.Load<Sprite>("UI/icon_hunter");
			}
		}
		if (!timerNotice.Equals ("")) {
			UISet.txt_timernotice.text = timerNotice;
			timerNotice = "";
		}
		if (!cardNotice.Equals ("")) {
			UISet.txt_cardnotice.text = cardNotice;
			timerNotice = "";
		}
		if (StructManager.myRoomInfo != null) {
			if(StructManager.myRoomInfo.users.ContainsKey(StructManager.user.id)) {
				if (StructManager.myRoomInfo.users [StructManager.user.id].hp <= 0) {
					StructManager.myRoomInfo.users [StructManager.user.id].hp = 0;
					string _tmp = UISet.txt_status.text.Split('\n')[0];
					UISet.txt_status.text = _tmp + "\nHP: 0 / 100";
					UISet.SetActiveBigCard (false, null);
					if (!StructManager.isOver) {
						UISet.SetChat ("<SYSTEM> 당신은 패배했습니다. 게임이 끝날 때 까지 아무 행동도 할 수 없습니다.");
						StructManager.isOver = true;
					}
					return;
				}
			}
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
	void UICardSetProcessing() {
		float count = UICard.cards.Count;
		if (count > 3) {
			count -= 3;
		} else {
			count = 0; 
		}
		float value = count / 68.00643316654753f;
		if (UISet.scroll_cardset.value > value) {
			UISet.scroll_cardset.value = value;
		}
		if (cardUpdateFlag) {
			for(int i = 0; i < UICard.cards.Count; i++) {
				UICard.cards[i].SetOrder(i);
				UICard.cards[i].index = i;
			}
		}
	}
	
	//for debugging
	void UIDebuggingProcessing() {
		if (!debug.Equals ("")) {
			//GameObject.Find("debug").GetComponent<Text>().text = debug;
			debug = "";
		}
	}
}
