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
public enum PlayerJob { UNSET, VAMPIRE, HUNTER };
public struct Player {
	public string id;
	public PlayerJob job;
	public PlayerState state;
	public bool isAI;
	public Player(string id) {
		this.id = id;
		job = PlayerJob.UNSET;
		state = PlayerState.UNSET;
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
public struct UserInfo {
	public string id;
	public Dictionary<int, AbilityInfo> ability;
	public Card cards;
	public UserInfo (string _id) {
		id = _id;
		ability = null;
		cards = new Card ();
	}
}
public struct AbilityInfo {
	string abilityName;
	PlayerJob jobClass;
	float effect;
	string description;
}
public struct Card {
	Dictionary<int, InfoCard> infoCardSet;
	Dictionary<int, BattleCard> battleCardSet;
	public void SetCards (Dictionary<int, InfoCard> _infoCardSet) {
		infoCardSet = _infoCardSet;
	}
	public void SetCards (Dictionary<int, BattleCard> _battleCardSet) {
		battleCardSet = _battleCardSet;
	}
	public void SetCards (Dictionary<int, InfoCard> _infoCardSet, Dictionary<int, BattleCard> _battleCardSet) {
		infoCardSet = _infoCardSet;
		battleCardSet = _battleCardSet;
	}
}
public struct InfoCard {
	string cardName;
	int grade;
	float pickRate;
	float cuccessRate;
	float upgrade;
	string description;
	string note;
}
public struct BattleCard {
	string cardName;
	int grade;
	float effect;
	float pickRate;
	float cuccessRate;
	float upgrade;
	string description;
	string note;
}