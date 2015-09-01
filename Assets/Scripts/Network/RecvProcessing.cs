using UnityEngine;
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;

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
		}
	}

	private void LoginProc(string data) {
		if (data.Equals ("f")) {
			UISet.ActiveUI (UISet.UIState.CAUTION);
			UISet.SetCaution("로그인에 실패했습니다. \n아이디나 비밀번호를 확인하세요");
		} else {
			int itemVersion;
			Int32.TryParse(data, out itemVersion);
			Debug.Log ("server: " + itemVersion + ", client: " + GlobalConfig.itemDataVersion);
			if(itemVersion != GlobalConfig.itemDataVersion) {
				UISet.ActiveUI (UISet.UIState.CAUTION);
				UISet.SetCaution("업데이트중입니다.");
			} else {
				UISet.ActiveUI (UISet.UIState.LOBBY_LOBBY);
				StructManager.user = new UserInfo(UISet.input_email.text);
			}
		} 
		UISet.SetUILock(false);
	}
	private void RoomCreateProc(string data) {
		if (data.Equals ("s")) {
			// 방 생성 성공
			Debug.Log ("success to create room");
		} else if (data.Equals ("f")) {
			UISet.ActiveUI (UISet.UIState.CAUTION);
			UISet.SetCaution("방 만들기에 실패했습니다. \n다시 시도해주세요.");
			UISet.SetUILock(false);
		}
	}
	private void FailToRoomEnter() {
		UISet.ActiveUI (UISet.UIState.CAUTION);
		UISet.SetCaution("입장에 실패했습니다. \n새로고침후 다시 시도해주세요.");
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
			isPublic = true;
		} else {
			isPublic = false;
		}
		List<Player> users = new List<Player> ();
		for (int i=4; i<tempStringArray.Length; i++) {
			if(tempStringArray[i].Equals("")) { break; }
			Player player = new Player(tempStringArray[i]);
			users.Add(player);
		}
		StructManager.myRoomInfo = new RoomInfo (roomNum, totalNum, maxNum);
		StructManager.myRoomInfo.owner = users [0];
		StructManager.myRoomInfo.RoomInfoUpdate(totalNum, maxNum, isPublic, users);

		UIManagement.roomUpdateFlag = true;
	}
	private void RoomChatProc(string data) {
		string[] tempStringArray = data.Split ('\r');
		string[] _tempStringArray = tempStringArray [1].Split ('\n');
		string sender = tempStringArray [0];
		string chat = _tempStringArray [1];
		StructManager.myRoomInfo.chatLog += sender + "\t : " + chat + "\n";
		
		UIManagement.chatUpdateFlag = true;
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
}
