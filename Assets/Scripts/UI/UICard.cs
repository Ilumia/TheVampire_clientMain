using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UICard {
	public enum CardSize { SMALL, BIG }
	enum CardType { ABILITY, INFO, BATTLE }

	static RectTransform cardSetTransform = GameObject.Find ("CardSet").GetComponent<RectTransform>();
	static public int selectedCardNum = -1;
	static public int selectedCard = -1;
	static public int gettedCard = 3;
	static public List<UICard> cards = new List<UICard> ();

	static Vector2 defaultCardPos_small = new Vector2 (-4966.9f, 0);
	static float cardSpace = 139.9f;
	static Vector2 cardSize_small = new Vector2(136.2f, 174);
	//static Vector2 cardSize_big = new Vector2 (10, 10);
	static Vector2 nameSize_small = new Vector2 (130.4f, 51);
	static Vector2 namePos_small = new Vector2 (0, -58.6f);
	//static Vector2 nameSize_big = new Vector2 (10, 10);
	//static Vector2 namePos_big = new Vector2 (0, 0);
	static Vector2 imageSize_small = new Vector2 (130.4f, 115);
	static Vector2 imagePos_small = new Vector2 (0, 26.8f);
	//static Vector2 imageSize_big = new Vector2 (10, 10);
	//static Vector2 imagePos_big = new Vector2 (0, 0);

	GameObject card;
	RectTransform cardTransform;
	public int number;
	public int id;
	CardSize size = CardSize.SMALL;
	CardType type;
	Text name;
	Image image;
	Button button;

	public UICard() {  }
	public UICard(int cardID) {
		SetCard (cardID);
	}
	public UICard(CardSize _size, int cardID) {
		size = _size;
		SetCard (cardID);
	}
	void OnClick() {
		if (size == CardSize.SMALL) {

		} else {

		}
		if (id == 30 || id == 34 || id == 37 || id == 60 || id == 61 || id == 69) {
			if (selectedCard == id) {
				selectedCard = -1;
				selectedCardNum = -1;
				return;
			}
			selectedCard = id;
			selectedCardNum = 0;
		} else {
			Communication.Instance().SendMessageToServer('M', id.ToString());
			CardDestroy();
		}
	}
	public void CardDestroy () {
		List<GameObject> children = new List<GameObject>();
		foreach (Transform child in card.transform) children.Add(child.gameObject);
		foreach (GameObject child in children) {
			Object.Destroy(child);
		}
		Object.Destroy (card);
	}
	public void SetSize (CardSize _size) {
		size = _size;
	}
	public UICard SetOrder (int order) {
		Vector2 newPos = defaultCardPos_small;
		newPos.x += (float)order * cardSpace;
		cardTransform.anchoredPosition = newPos;
		return this;
	}
	void SetPosition (float width, float height) {
		SetPosition (new Vector2 (width, height));
	}
	void SetPosition (Vector2 pos) {
		cardTransform.anchoredPosition = pos;
	}
	public void SetCard(int cardID) {
		InitCard ();
		id = cardID;
		string cardName = "";
		string cardImage = "";
		if (cardID < 30) {
			Ability newCard = StructManager.itemSet.abilitySet [cardID];
			cardName = newCard.abilityName;
			type = CardType.ABILITY;
		} else if (cardID < 60) {
			InfoCard newCard = StructManager.itemSet.infoCardSet [cardID];
			cardName = newCard.cardName;
			type = CardType.INFO;
		}else {
			BattleCard newCard = StructManager.itemSet.battleCardSet [cardID];
			cardName = newCard.cardName;
			type = CardType.BATTLE;
		}
		cardImage = cardID.ToString ();
		if (cardID < 10) {
			cardImage = "0" + cardImage;
		}
		name.text = cardName;
		image.sprite = Resources.Load<Sprite>("CardSet/" + cardImage);
		button.onClick.AddListener (OnClick);
	}
	void InitCard() {
		card = new GameObject();
		card.name = "card";
		card.transform.SetParent (GameObject.Find ("Canvas").transform);
		//card.transform.parent = GameObject.Find ("Canvas").transform;
		card.transform.localScale = Vector3.one;
		card.AddComponent<CanvasRenderer>();
		cardTransform = card.AddComponent<RectTransform> ();
		cardTransform.sizeDelta = cardSize_small;
		cardTransform.anchoredPosition = new Vector2 (-100, -200);
		card.AddComponent<Image> ().sprite = Resources.Load<Sprite> ("UI/cardBoarder");
		button = card.AddComponent<Button>();
		button.targetGraphic = card.GetComponent<Image> ();
		ColorBlock colorBlock = new ColorBlock ();
		colorBlock.colorMultiplier = 1;
		colorBlock.normalColor = Color.white;
		colorBlock.highlightedColor = Color.white;
		colorBlock.pressedColor = Color.black;
		colorBlock.fadeDuration = 0.1f;
		button.colors = colorBlock;
		
		GameObject nameObject = new GameObject ();
		nameObject.name = "Text";
		nameObject.transform.SetParent(card.transform);
		//nameObject.transform.parent = card.transform;
		nameObject.transform.localScale = Vector3.one;
		RectTransform nameTransform = nameObject.AddComponent<RectTransform> ();
		nameTransform.sizeDelta = nameSize_small;
		nameTransform.anchoredPosition = namePos_small;
		name = nameObject.AddComponent<Text> ();
		name.text = "test";
		name.color = Color.white;
		name.alignment = TextAnchor.MiddleCenter;
		name.fontSize = 22;
		name.font = Resources.Load<Font> ("Font/NanumBarunGothic");
		
		GameObject imageObject = new GameObject ();
		imageObject.name = "Image";
		imageObject.transform.SetParent(card.transform);
		//imageObject.transform.parent = card.transform;
		imageObject.transform.localScale = Vector3.one;
		RectTransform imageTransform = imageObject.AddComponent<RectTransform> ();
		imageTransform.sizeDelta = imageSize_small;
		imageTransform.anchoredPosition = imagePos_small;
		image = imageObject.AddComponent<Image> ();
		image.sprite = Resources.Load<Sprite> ("CardSet/00");
		if (size == CardSize.SMALL) {
			card.transform.SetParent(cardSetTransform);
			//card.transform.parent = cardSet.transform;
			card.transform.localScale = Vector3.one;
			cardTransform.sizeDelta = cardSize_small;
			nameTransform.sizeDelta = nameSize_small;
			nameTransform.anchoredPosition = namePos_small;
			imageTransform.sizeDelta = imageSize_small;
			imageTransform.anchoredPosition = imagePos_small;
		} else {

		}
	}
}
