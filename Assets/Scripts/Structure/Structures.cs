using System.Collections.Generic;

public class RoomInfo {
	public int roomNumber;
	public int totalNumber;
	public int maximumNumber;
	public bool isPublic;
	public Dictionary<string, Player> users;
	public string owner;
	public int roomState;   // -1: 시작 전, 0: 게임종료, 1이상의 양수: 진행회차
	public string chatLog;

	public RoomInfo() {

	}
	public RoomInfo(int _roomNumber, int _totalNumber, int _maximumNumber) {
		roomNumber = _roomNumber;
		totalNumber = _totalNumber;
		maximumNumber = _maximumNumber;
		isPublic = true;
		users = new Dictionary<string, Player>();
		owner = "";
		roomState = -1;
		chatLog = "";
	}
	public void RoomUpdate(int _totalNumber) {
		this.totalNumber = _totalNumber;
	}
	public void RoomInfoUpdate(int _totalNumber, int _maximumNumber, bool _isPublic, Dictionary<string, Player> _users) {
		totalNumber = _totalNumber;
		maximumNumber = _maximumNumber;
		isPublic = _isPublic;
		users = _users;
	}

}

public class Player {
	public string id;
	public int hp;
	public bool isAI;
	public Player() {
		hp = 10;
		isAI = false;
	}
	public Player(string id) {
		this.id = id;
		hp = 10;
		isAI = false;
	}
}
public class UserInfo {
	public string id;
	public Item item;
	public bool isVampire;
	public UserInfo (string _id) {
		id = _id;
		item = new Item ();
	}
}
public class Item {
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
public enum PlayerJob { UNSET, VAMPIRE, HUNTER };
public class Ability {
	public string abilityName;
	public PlayerJob jobClass;
	public float effect;
	public float effectFactor;
	public string description;
}
public class InfoCard {
	public string cardName;
	public string grade;
	public float pickRate;
	public float cuccessRate;
	public float upgrade;
	public string description;
	public string note;
}
public class BattleCard {
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