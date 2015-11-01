using UnityEngine;
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public partial class Communication {
	private void ReceiveProcessing(byte type, byte[] data) {
		string _data = Encoding.Unicode.GetString (data);
		switch ((char)type) {
		case 'a':
			LoginProc(_data);
			break;
		case 'b':
			//미사용
			break;
		case 'c':
			RoomCreateProc(_data);
			break;
		case 'd':
			FailToRoomEnter();
			break;
		case 'e':
			RoomUpdateProc(_data);
			break;
		case 'f':
			RoomChatProc(_data);
			break;
		case 'g':
			SignUpProc(_data);
			break;
		case 'h':
			//show friends
			break;
		case 'i':
			//add friedn
			break;
		case 'j':
			GameStartProc();
			break;
		case 'w':
			ItemListProc(_data);
			break;
		case 'k':
			//RandomNumberProc(_data);
			break;
		case 'l':
			//GameInfoProc(_data);
			break;
		case 'm':
			//GameTurnInfoProc(_data);
			break;
		case 'n':
			RoomInOutProc(_data);
			break;
		case 'o':
			TimerUpdateProc(_data);
			break;
		}
	}

	private void LoginProc(string data) {
		if (data.Equals ("f")) {
			UISet.ActiveUI (UISet.UIState.CAUTION);
			UISet.SetCaution("로그인에 실패했습니다. \n아이디나 비밀번호를 확인하세요");
			UISet.SetUILock(false);
		} else {
			int itemVersion;
			Int32.TryParse(data, out itemVersion);
			Debug.Log ("server: " + itemVersion + ", client: " + GlobalConfig.itemDataVersion);
			if(itemVersion != GlobalConfig.itemDataVersion) {
				UISet.ActiveUI (UISet.UIState.CAUTION);
				UISet.SetCaution("업데이트를 시작합니다.\n잠시 기다려주세요");
				SendMessageToServer('V', "");
			} else {
				UISet.ActiveUI (UISet.UIState.LOBBY_LOBBY);
				StructManager.user = new UserInfo(UISet.input_email.text);
				UISet.SetUILock(false);
			}
		}
	}
	private void RoomCreateProc(string data) {
		String[] _data = data.Split (' ');
		if (_data[0].Equals ("s")) {
			// 방 생성 성공
			Debug.Log ("success to create room");
			StructManager.myRoomInfo = null;
			if(_data[1].Equals("t")) {
				UISet.ActiveUI(UISet.UIState.ROOM_READIED_PUBLIC);
			} else {
				UISet.ActiveUI(UISet.UIState.ROOM_READIED_PRIVATE);
			}
		} else if (_data[0].Equals ("f")) {
			UISet.ActiveUI (UISet.UIState.CAUTION);
			UISet.SetCaution("방 만들기에 실패했습니다. \n다시 시도해주세요.");
			UISet.SetUILock(false);
		}
	}
	private void FailToRoomEnter() {
		UISet.ActiveUI (UISet.UIState.CAUTION);
		UISet.SetCaution("입장할 수 있는 방이 없습니다.");
		UISet.SetUILock(false);
	}
	private void RoomUpdateProc(string data) {
		string[] tempStringArray = data.Split (' ');
		int roomNum;
		int totalNum;
		int maxNum;
		bool isPublic;
		Int32.TryParse (tempStringArray [0], out roomNum);
		Int32.TryParse (tempStringArray [1], out totalNum);
		Int32.TryParse (tempStringArray [2], out maxNum);
		if (tempStringArray [3].Equals ("t")) {
			UISet.ActiveUI (UISet.UIState.ROOM_READIED_PUBLIC);
			isPublic = true;
		} else {
			UISet.ActiveUI (UISet.UIState.ROOM_READIED_PRIVATE);
			isPublic = false;
		}
		List<Player> users = new List<Player> ();
		for (int i=4; i<tempStringArray.Length; i++) {
			if(tempStringArray[i].Equals("")) { break; }
			Player player = new Player(tempStringArray[i]);
			users.Add(player);
		}
		if (StructManager.myRoomInfo == null) {
			StructManager.myRoomInfo = new RoomInfo (roomNum, totalNum, maxNum);
			StructManager.myRoomInfo.owner = users [0];
		}
		StructManager.myRoomInfo.RoomInfoUpdate(totalNum, maxNum, isPublic, users);

		UIManagement.chatUpdateFlag = true;
		UIManagement.roomUpdateFlag = true;
	}
	private void RoomChatProc(string data) {
		string[] tempStringArray = data.Split ('\r');
		string[] _tempStringArray = tempStringArray [1].Split ('\n');
		string sender = tempStringArray [0];
		string chat = _tempStringArray [1];
		UISet.SetChat (sender + "\t : " + chat);
		//StructManager.myRoomInfo.chatLog += sender + "\t : " + chat + "\n";
		//UIManagement.chatUpdateFlag = true;
	}
	private void SignUpProc(string data) {
		if (data.Equals ("s")) {
			UISet.ActiveUI (UISet.UIState.CAUTION);
			UISet.SetCaution("회원가입에 성공했습니다.");
		} else if (data.Equals ("f")) {
			UISet.ActiveUI (UISet.UIState.CAUTION);
			UISet.SetCaution("이미 가입된 이메일입니다.");
		}
		UISet.SetUILock(false);
	}
	private void GameStartProc() {
		StructManager.myRoomInfo.roomState = 1;
		UISet.ActiveUI (UISet.UIState.ROOM_STARTED);
	}
	private void RoomInOutProc(string data) {
		string[] tmp = data.Split (' ');
		string _message = tmp [1] + "님이 ";
		if (tmp [0].Equals ("i")) {
			_message += "입장하셨습니다.";
			if(StructManager.myRoomInfo.users.Count == 4) {
				UISet.SetChat ("10초 후 자동으로 게임이 시작됩니다.");
			}
		} else if (tmp [0].Equals ("o")) {
			_message += "퇴장하셨습니다.";
		}
		UISet.SetChat (_message);
	}
	private void TimerUpdateProc(string data) {
		float tmp = float.Parse (data);
		if (tmp <= 3.0f) {
			UISet.SetChat (tmp + "초 후 게임이 시작됩니다.");
		}
	}
	private void ItemListProc(string data) {
		File.WriteAllText (ItemSetInterpreter.path, data);
		ItemSetInterpreter.ReadSet ();
		UISet.SetUILock(false);
	}
}
