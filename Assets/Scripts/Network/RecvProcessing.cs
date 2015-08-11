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
		}
	}

	private void LoginProc(string data) {
		if (data.Equals ("s")) {
			UISet.ActiveUI (UISet.UIState.LOBBY_LOBBY);
			StructManager.user = new User(UISet.input_email.text);
		} else if (data.Equals ("f")) {
			UISet.ActiveUI (UISet.UIState.CAUTION);
			UISet.SetCaution("로그인에 실패했습니다. \n아이디나 비밀번호를 확인하세요");
		}
	}
	private void RoomCreateProc(string data) {
		if (data.Equals ("s")) {
			// 방 생성 성공
			Debug.Log ("success to create room");
			SendMessageToServer('B', "");
		} else if (data.Equals ("f")) {
			UISet.ActiveUI (UISet.UIState.CAUTION);
			UISet.SetCaution("방 만들기에 실패했습니다. \n다시 시도해주세요.");
		}
	}
	private void FailToRoomEnter() {
		UISet.ActiveUI (UISet.UIState.CAUTION);
		UISet.SetCaution("입장에 실패했습니다. \n새로고침후 다시 시도해주세요.");
	}
	private void RoomUpdateProc(string data) {

	}
}
