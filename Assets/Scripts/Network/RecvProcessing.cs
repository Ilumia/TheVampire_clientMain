using UnityEngine;
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public partial class Communication {
	private void ReceiveProcessing(byte type, byte[] data) {
		try{
			string _data = "";
			if (data.Length > 0) {
				_data = Encoding.Unicode.GetString (data);
			}
			switch ((char)type) {
			case 'a':
				LoginProc(_data);
				break;
			case 'b':
				//미사용
				break;
			case 'c':
				//RoomCreateProc(_data);
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
				GameStartProc(_data);
				break;
			case 'k':
				//RandomNumberProc(_data);
				break;
			case 'l':
				GameInfoProc(_data);
				break;
			case 'm':
				GameTurnInfoProc(_data);
				break;
			case 'n':
				RoomInOutProc(_data);
				break;
			case 'o':
				TimerUpdateProc(_data);
				break;
			case 'p':
				GameOverProc(_data);
				break;

			case 'u':
				GameInfoTurnInfoProc(_data);
				break;
			case 'w':
				ItemListProc(_data);
				break;
			default:
				//SendMessageToServer('U', "");
				break;
			}
		} catch(Exception e) {
			Debug.Log (e.Message);
			Debug.Log (e.StackTrace);
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
		try {
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
			Dictionary<string, Player> users = new Dictionary<string, Player> ();
			for (int i=4; i<tempStringArray.Length; i++) {
				if(tempStringArray[i].Equals("")) { break; }
				Player player = new Player(tempStringArray[i]);
				users.Add(tempStringArray[i], player);
			}
			if (StructManager.myRoomInfo == null) {
				StructManager.myRoomInfo = new RoomInfo (roomNum, totalNum, maxNum);
				StructManager.myRoomInfo.owner = tempStringArray[4];
			}
			StructManager.myRoomInfo.RoomInfoUpdate(totalNum, maxNum, isPublic, users);

			System.Threading.Thread.Sleep (100);
			UIManagement.chatUpdateFlag = true;
			UIManagement.roomUpdateFlag = true;
		} catch(Exception e) {
			Debug.Log(e.Message);
			Debug.Log(e.StackTrace);
		}
	}
	private void RoomChatProc(string data) {
		string[] tempStringArray = data.Split ('\r');
		string[] _tempStringArray = tempStringArray [1].Split ('\n');
		string sender = tempStringArray [0];
		string chat = _tempStringArray [1];
		UISet.SetChat (sender + "\t : " + chat);
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
	private void GameStartProc(string data) {
		if (data.Equals ("v")) {
			StructManager.user.isVampire = true;
			UISet.SetChat("<SYSTEM> 게임이 시작되었습니다.\n<SYSTEM> 당신은 뱀파이어입니다.");
		} else if (data.Equals ("h")){
			StructManager.user.isVampire = false;
			UISet.SetChat("<SYSTEM> 게임이 시작되었습니다.\n<SYSTEM> 당신은 헌터입니다.");
		}
		StructManager.isOver = false;
		StructManager.myRoomInfo.roomState = 1;
		UISet.ActiveUI (UISet.UIState.ROOM_STARTED);
		UICard.gettedCard = 3;
		UIManagement.status = "제 " + 1 + " 턴" + "\nHP: " + 
			StructManager.myRoomInfo.users[StructManager.user.id].hp + " / 100";
		UIManagement.hpUpdateFlag = true;
		UIManagement.profileUpdateFlag = true;
	}
	private void GameInfoProc(string data) {
		try{
			UISet.SetChat ("<SYSTEM> " + data);
		} catch(Exception e) {
			Debug.Log(e.Message);
			Debug.Log(e.StackTrace);
		}
	}
	private void GameTurnInfoProc(string data) {
		try {
			string status = "";
			string[] tmp = data.Split (' ');
			int newRoomState = Int32.Parse (tmp [0]);
			StructManager.myRoomInfo.roomState = newRoomState;
			for (int i = 1; i < 8; i+=2) {
				int hp = Int32.Parse (tmp[i+1]);
				StructManager.myRoomInfo.users[tmp[i]].hp = hp;
				if(tmp[i].Equals(StructManager.user.id)) {
					status = "제 " + newRoomState + " 턴" + "\nHP: " + hp + " / 100";
					UIManagement.status = status;
				}
			}
			UISet.SetChat ("<SYSTEM> " + newRoomState + " 턴 입니다.");
			UIManagement.hpUpdateFlag = true;
			UIManagement.timerNotice = "15";
			UICard.gettedCard++;
			UIManagement.cardNotice = UICard.gettedCard.ToString ();
		} catch(Exception e) {
			Debug.Log(e.Message);
			Debug.Log(e.StackTrace);
		}
	}
	private void RoomInOutProc(string data) {
		string[] tmp = data.Split (' ');
		string _message = "<SYSTEM> " + tmp [1] + "님이 ";
		if (tmp [0].Equals ("i")) {
			_message += "입장하셨습니다.";
		} else if (tmp [0].Equals ("o")) {
			_message += "퇴장하셨습니다.";
		}
		UISet.SetChat (_message);
	}
	private void TimerUpdateProc(string data) {
		float tmp = float.Parse (data);
		if (StructManager.myRoomInfo.roomState == -1) {
			if (tmp <= 3.0f) {
				UISet.SetChat ("<SYSTEM> " + tmp + "초 후 게임이 시작됩니다.");
			}
		} else if (StructManager.myRoomInfo.roomState > 0) {
			UIManagement.timerNotice = tmp.ToString();
		}
	}
	private void GameOverProc(string data) {
		if (data.Equals ("v")) {
			UISet.SetChat ("<SYSTEM> 뱀파이어 팀이 승리했습니다.");
		} else if (data.Equals ("h")) {
			UISet.SetChat ("<SYSTEM> 헌터 팀이 승리했습니다.");
		} else if (data.Equals ("d")) {
			UISet.SetChat ("<SYSTEM> 무승부입니다.");
		}
		UISet.SetActiveBigCard (false, null);
		UISet.ActiveUI (UISet.UIState.ROOM_READIED_PUBLIC);
	}
	private void GameInfoTurnInfoProc(string data) {
		string[] separator = new string[] { "\n\r" };
		string[] _data = data.Split (separator, StringSplitOptions.None);
		Debug.Log ("!!!!!!!!!!!!!!!!!!!!!" + data);
		if (!_data [0].Equals ("")) {
			GameInfoProc (_data [0]);
		}
		GameTurnInfoProc (_data [1]);
	}
	private void ItemListProc(string data) {
		File.WriteAllText (ItemSetInterpreter.path, data);
		ItemSetInterpreter.ReadSet ();
		UISet.SetUILock(false);
	}
}
