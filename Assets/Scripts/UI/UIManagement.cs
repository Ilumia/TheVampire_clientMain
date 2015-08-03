using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIManagement : MonoBehaviour {
	public static List<UISet.UIState> UIEventList;
	public static List<string> UICautionList;

	void Awake () {
		UIEventList = new List<UISet.UIState> ();
		UICautionList = new List<string> ();

		// Initialize UI Set
		UISet.Main = GameObject.Find("Main");
		// Caution
		UISet.Caution = GameObject.Find("Caution");
		UISet.txt_caution = GameObject.Find("txt_caution").GetComponent<Text>();
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
		
		UISet.Lobby = GameObject.Find("Lobby");
		// Lobby
		UISet.Set_Lobby = GameObject.Find("Set_Lobby");
		UISet.btn_createroom = GameObject.Find("btn_createroom").GetComponent<Button>();
		UISet.btn_friends = GameObject.Find("btn_friends").GetComponent<Button>();
		UISet.btn_exit = GameObject.Find("btn_exit").GetComponent<Button>();
		UISet.btn_room1 = GameObject.Find("btn_room1").GetComponent<Button>();
		UISet.btn_room2 = GameObject.Find("btn_room2").GetComponent<Button>();
		UISet.btn_room3 = GameObject.Find("btn_room3").GetComponent<Button>();
		UISet.btn_room4 = GameObject.Find("btn_room4").GetComponent<Button>();
		UISet.btn_room5 = GameObject.Find("btn_room5").GetComponent<Button>();
		UISet.btn_listup = GameObject.Find("btn_listup").GetComponent<Button>();
		UISet.btn_listdown = GameObject.Find("btn_listdown").GetComponent<Button>();
		// CreateRoom
		UISet.Set_CreateRoom = GameObject.Find ("Set_CreateRoom");
		UISet.slider_number = GameObject.Find ("slider_number").GetComponent<Slider> ();
		UISet.toggle_public = GameObject.Find ("toggle_public").GetComponent<Toggle> ();
		UISet.btn_createconfirm = GameObject.Find ("btn_createconfirm").GetComponent<Button> ();
		UISet.btn_createcancel = GameObject.Find ("btn_createcancel").GetComponent<Button> ();
		UISet.txt_numbercount = GameObject.Find ("txt_numbercount").GetComponent<Text> ();
		
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
		UISet.btn_createroom.onClick.AddListener (UISet.Ebtn_createroom);
		UISet.btn_friends.onClick.AddListener (UISet.Ebtn_friends);
		UISet.btn_exit.onClick.AddListener (UISet.Ebtn_exit);
		UISet.btn_room1.onClick.AddListener (UISet.Ebtn_room);
		UISet.btn_room2.onClick.AddListener (UISet.Ebtn_room);
		UISet.btn_room3.onClick.AddListener (UISet.Ebtn_room);
		UISet.btn_room4.onClick.AddListener (UISet.Ebtn_room);
		UISet.btn_room5.onClick.AddListener (UISet.Ebtn_room);
		UISet.btn_listup.onClick.AddListener (UISet.Ebtn_listup);
		UISet.btn_listdown.onClick.AddListener (UISet.Ebtn_listdown);
		// CreateRoom
		UISet.slider_number.onValueChanged.AddListener (UISet.Eslider_number);
		UISet.toggle_public.onValueChanged.AddListener (UISet.Etoggle_public);
		UISet.btn_createconfirm.onClick.AddListener (UISet.Ebtn_createconfirm);
		UISet.btn_createcancel.onClick.AddListener (UISet.Ebtn_createcancel);
		
		UISet.ActiveUI (UISet.UIState.MAIN_SELECT);

	}
	
	// Update is called once per frame
	void Update () {
		UIEventProcessing ();
		UICautionProcessing ();
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
}
