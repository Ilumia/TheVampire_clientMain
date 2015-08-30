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

public enum PlayerState { UNSET, TURN_ON, TURN_OFF, DEFEATED };
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
	public Item item;
	public UserInfo (string _id) {
		id = _id;
		item = new Item ();
	}
}
public struct Item {
	public Dictionary<int, Ability> abilitySet;
	public Dictionary<int, InfoCard> infoCardSet;
	public Dictionary<int, BattleCard> battleCardSet;
	public void SetItem (Dictionary<int, Ability> _abilitySet) {
		abilitySet = _abilitySet;
	}
	public void SetItem (Dictionary<int, InfoCard> _infoCardSet) {
		infoCardSet = _infoCardSet;
	}
	public void SetItem (Dictionary<int, BattleCard> _battleCardSet) {
		battleCardSet = _battleCardSet;
	}
	public void SetItem (Dictionary<int, Ability> _abilitySet, Dictionary<int, InfoCard> _infoCardSet, Dictionary<int, BattleCard> _battleCardSet) {
		abilitySet = _abilitySet;
		infoCardSet = _infoCardSet;
		battleCardSet = _battleCardSet;
	}
}
public struct Ability {
	public string abilityName;
	public PlayerJob jobClass;
	public float effect;
	public float effectFactor;
	public string description;
}
public struct InfoCard {
	public string cardName;
	public string grade;
	public float pickRate;
	public float cuccessRate;
	public float upgrade;
	public string description;
	public string note;
}
public struct BattleCard {
	public string cardName;
	public string grade;
	public float effect;
	public float effectFactor;
	public float pickRate;
	public float cuccessRate;
	public float upgrade;
	public string description;
	public string note;
}