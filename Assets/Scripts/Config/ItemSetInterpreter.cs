using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class ItemSetInterpreter {
	public static string path = Application.persistentDataPath + "/.itemset";

	public static void ReadSet() {
		if (!File.Exists (path)) {
			Debug.Log ("need File!");
			return;
		}
		try {
			string _cardSet = File.ReadAllText(path);
			string[] cardSet = _cardSet.Split('\n');
			Dictionary<int, Ability> abilitySet = new Dictionary<int, Ability>();
			Dictionary<int, InfoCard> infoCardSet = new Dictionary<int, InfoCard>();
			Dictionary<int, BattleCard> battleCardSet = new Dictionary<int, BattleCard>();

			string[] tmpNum = cardSet[0].Split(' ');
			Int32.TryParse(tmpNum[0], out GlobalConfig.itemDataVersion);
			int abilityNum; int informationNum; int battleNum;
			Int32.TryParse(tmpNum[1], out abilityNum);
			Int32.TryParse(tmpNum[2], out informationNum);
			Int32.TryParse(tmpNum[3], out battleNum);
			int i = 1;
			for(; i < abilityNum + 2; i++) {
				string[] tmp = cardSet[i].Split('\t');
				Ability _ability = new Ability();
				int number;
				Int32.TryParse(tmp[0], out number);
				if(tmp[1].Equals("뱀파이어")) {
					_ability.jobClass = PlayerJob.VAMPIRE;
				} else if(tmp[1].Equals("헌터")) {
					_ability.jobClass = PlayerJob.HUNTER;
				}
				_ability.abilityName = tmp[2];
				float.TryParse(tmp[3], out _ability.effect);
				float.TryParse(tmp[4], out _ability.effectFactor);
				_ability.description = tmp[5];
				abilitySet.Add(number, _ability);
			}
			for(; i < informationNum + abilityNum + 2; i++) {
				string[] tmp = cardSet[i].Split('\t');
				InfoCard _info = new InfoCard();
				int number;
				Int32.TryParse(tmp[0], out number);
				_info.cardName = tmp[1];
				_info.grade = tmp[2];
				float.TryParse(tmp[3], out _info.pickRate);
				float.TryParse(tmp[4], out _info.cuccessRate);
				_info.description = tmp[5];
				infoCardSet.Add(number, _info);
			}
			for(; i < cardSet.Length; i++) {
				string[] tmp = cardSet[i].Split('\t');
				BattleCard _battle = new BattleCard();
				int number;
				Int32.TryParse(tmp[0], out number);
				_battle.cardName = tmp[1];
				_battle.grade = tmp[2];
				float.TryParse(tmp[3], out _battle.effect);
				float.TryParse(tmp[4], out _battle.effectFactor);
				float.TryParse(tmp[5], out _battle.pickRate);
				float.TryParse(tmp[6], out _battle.cuccessRate);
				_battle.description = tmp[7];
				battleCardSet.Add(number, _battle);
			}
			StructManager.itemSet = new Item();
			StructManager.itemSet.SetItem(abilitySet, infoCardSet, battleCardSet);

		} catch(IOException e) {
			Debug.Log (e.StackTrace);
		} catch(Exception e) {
			Debug.Log (e.Message);
			Debug.Log (e.StackTrace);
		}
	}
}
