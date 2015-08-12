using System.Collections.Generic;

public struct RoomInfo {
	public int roomNumber;
	public int totalNumber;
	public int maximumNumber;
	public bool isPublic;
	public List<Player> users;
	public Player owner;
	public int roomState;   // -1: 시작 전, 0: 게임종료, 1이상의 양수: 진행회차
	public string chatLog;

	public RoomInfo(int _roomNumber, int _totalNumber, int _maximumNumber) {
		roomNumber = _roomNumber;
		totalNumber = _totalNumber;
		maximumNumber = _maximumNumber;
		isPublic = true;
		users = null;
		owner = new Player ();
		roomState = -1;
		chatLog = "";
	}
	public void RoomInfoUpdate(int _totalNumber, int _maximumNumber, bool _isPublic, List<Player> _users) {
		totalNumber = _totalNumber;
		maximumNumber = _maximumNumber;
		isPublic = _isPublic;
		users = _users;
	}

}

public enum PlayerState { UNSET, TURN_ON, TURN_OFF, DROPPED };
public enum PlayerJob { UNSET, VAMPIRE };
public struct Player {
	public string id;
	public PlayerJob job;
	public PlayerState state;
	public int item1;
	public int item2;
	public int item3;
	public bool isAI;
	public Player(string id) {
		this.id = id;
		job = PlayerJob.UNSET;
		state = PlayerState.UNSET;
		item1 = 0;
		item2 = 0;
		item3 = 0;
		isAI = false;
	}
	public void InitPlayer(PlayerJob job, bool isAI) {
		this.job = job;
		this.isAI = isAI;
		/*  초기아이템 및 상태정의
            switch (job)
            {
                case PlayerJob.:
                    break;
                case PlayerJob.:
                    break;
                case PlayerJob.:
                    break;
            }
            */
	}
}
public struct User {
	public string id;
	public User (string _id) {
		id = _id;
	}
}