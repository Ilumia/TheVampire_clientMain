using UnityEngine;
using System.Text;
using System.Collections;

public partial class Communication {
	private void ReceiveProcessing(byte type, byte[] data) {
		string _data = Encoding.Unicode.GetString (data);
		switch ((char)type) {
		case 'a':
			LoginProc(_data);
			break;
		}
	}

	private void LoginProc(string data) {
		if (data.Equals ("s")) {
			UISet.ActiveUI (UISet.UIState.LOBBY_LOBBY);
		} else if (data.Equals ("f")) {
			UISet.ActiveUI (UISet.UIState.CAUTION);
			UISet.SetCaution("로그인에 실패했습니다. \n아이디나 비밀번호를 확인하세요");
		}
	}
}
