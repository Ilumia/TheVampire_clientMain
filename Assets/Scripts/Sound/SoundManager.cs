using UnityEngine;
using System;
using System.Collections;

public class SoundManager {
	static AudioSource bgm_1;
	static AudioSource[] effect_buttonclick;
	static AudioSource[] effect_cardselect;

	public static void PlayerBGM_1(bool check) {
		if(bgm_1 == null) {
			bgm_1 = GameObject.Find("bgm_1").GetComponent<AudioSource>();
		}
		if (check && !bgm_1.isPlaying) {
			bgm_1.Play ();
		} else if (!check) {
			bgm_1.Stop();
		}
	}
	public static void PlayEffectButtonClick() {
		if(effect_buttonclick == null) {
			effect_buttonclick = new AudioSource[5];
			for(int i=0; i<5; i++) {
				effect_buttonclick[i] = GameObject.Find("effect_buttonclick (" + i + ")").GetComponent<AudioSource>();
			}
		}
		for(int i=0; i<5; i++) {
			if(!effect_buttonclick[i].isPlaying) {
				effect_buttonclick[i].Play();
				return;
			}
		}
	}
	public static void PlayEffectCardSelect() {
		if(effect_cardselect == null) {
			effect_cardselect = new AudioSource[5];
			for(int i=0; i<5; i++) {
				effect_cardselect[i] = GameObject.Find("effect_cardselect (" + i + ")").GetComponent<AudioSource>();
			}
		}
		for(int i=0; i<5; i++) {
			if(!effect_cardselect[i].isPlaying) {
				effect_cardselect[i].Play();
				return;
			}
		}
	}
}
