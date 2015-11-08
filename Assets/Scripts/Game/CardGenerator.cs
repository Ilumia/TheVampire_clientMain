using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public enum CardType { INFO, BATTLE }

public class CardGenerator {
	static System.Random rand = new System.Random();

	static public int GetCard(CardType type) {
		float randomValue = GetRandom ();
		float tmpRate = 0;
		if (type == CardType.INFO) {
			foreach(KeyValuePair<int, InfoCard> card in StructManager.itemSet.infoCardSet) {
				tmpRate += card.Value.pickRate;
				if(randomValue - tmpRate < 0) {
					return card.Key;
				}
			}
		} else {
			foreach(KeyValuePair<int, BattleCard> card in StructManager.itemSet.battleCardSet) {
				tmpRate += card.Value.pickRate;
				if(randomValue - tmpRate < 0) {
					return card.Key;
				}
			}
		}
		return -1;
	}
	static float GetRandom()
	{
		return (float)rand.Next(0, 10000) / (float)10000;
	}
}
