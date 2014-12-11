using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PanelOrb : MonoBehaviour {

	public GameObject ImageBack, ImageSpell, PanelHealth, LeftOrb, RightOrb;

	private SpellCard _ActualCard;
	private Deck Deck;
	public Dictionary<AnimationType, Sprite> Animations;

	internal SpellCard ActualCard{
		get {
			return _ActualCard; 
		}
		set {
			_ActualCard = value;
			ImageSpell.GetComponent<SpriteRenderer>().sprite = _ActualCard==null ? null :_ActualCard.Animations[AnimationType.Card];
			ImageSpell.GetComponent<SpriteRenderer>().enabled = _ActualCard != null;
		}
	}

	void Update() {
		if (Animations != null) {
			bool isDead = PanelHealth.GetComponent<PanelHealth>().IsDead();
			GetComponent<SpriteRenderer>().sprite = Animations[isDead ? AnimationType.Dead : AnimationType.OnBoard];
			ImageSpell.SetActive(!isDead);
			ImageBack.SetActive(!isDead);
		}
	}
			

	internal void Prepare(Deck deck, Dictionary<AnimationType, Sprite> animations, int health, OrbState leftOrb, OrbState rightOrb) {
		Animations = animations;
		Deck = deck;
		PanelHealth.GetComponent<PanelHealth>().Health = health;
		if (LeftOrb != null){
			if (leftOrb != null) {
				LeftOrb.GetComponent<PanelOrb>().Prepare(leftOrb.Deck, leftOrb.Animations, leftOrb.Health, leftOrb.LeftOrbState, leftOrb.RightOrbState);
			} else {
				LeftOrb.SetActive(false);
			}
		} 
		if (RightOrb != null){
			if (rightOrb != null) {
				RightOrb.GetComponent<PanelOrb>().Prepare(rightOrb.Deck, rightOrb.Animations, rightOrb.Health, rightOrb.LeftOrbState, rightOrb.RightOrbState);
			} else {
				RightOrb.SetActive(false);
			}
		}
		TakeNextCard();
	}

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log("on trigger enter: " + gameObject.name + ", other: " + other.gameObject.name);
		//other.GetComponent<PanelOrb>().Card.TriggerAction(other.gameObject, gameObject);
	}
	void OnTriggerStay2D(Collider2D other) {
		Debug.Log("on trigger stay: " + gameObject.name + ", other: " + other.gameObject.name);
	}
	void OnTriggerExit2D(Collider2D other) {
		Debug.Log("on trigger exit: " + gameObject.name + ", other: " + other.gameObject.name);
	}

	public void SwapSpell(Side s) {
		Debug.Log("SwapSpell, " + s);
		switch (s) {
			case Side.Left: {
				Debug.Log("Swapping");
				SpellCard c = LeftOrb.GetComponent<PanelOrb>().ActualCard;
				if (c != null && ActualCard != null) {
					Debug.Log("swapping with left");
					LeftOrb.GetComponent<PanelOrb>().ActualCard = ActualCard;
					ActualCard = c;
				}
				break;
			}
			case Side.Right: {
					SpellCard c = RightOrb.GetComponent<PanelOrb>().ActualCard;
					if (c != null && ActualCard != null) {
						RightOrb.GetComponent<PanelOrb>().ActualCard = ActualCard;
						ActualCard = c;
					}
					break;
				}
		}
	}

	public void Drop() {
		ActualCard = null;
		TakeNextCard();
	}

	public void Cast(Vector3 direction) {
		Debug.Log("casting spell");
		GameObject spell = (GameObject)Instantiate(Game.Me.PanelMinigame.GetComponent<PanelMinigame>().PanelSpells.GetComponent<PanelSpells>().SpellPrefab);
		spell.SetActive(true);
		spell.transform.position = transform.position;
		spell.GetComponent<PanelSpell>().Prepare(ActualCard, direction);
		
		ActualCard = null;
		TakeNextCard();
	}

	public void WantCast() {
		ImageSpell.GetComponent<SpriteRenderer>().sprite = ActualCard.Animations[AnimationType.OnBoard];
	}

	public void DontWantCast() {
		if (ActualCard != null) {
			ImageSpell.GetComponent<SpriteRenderer>().sprite = ActualCard.Animations[AnimationType.Card];
		}
	}

	private void TakeNextCard() {

		if (ActualCard == null) {

			Side s = Mathf.Sign(Random.value - 0.5f) == -1 ? Side.Left : Side.Right;
			TakeNextCardFrom(s);
			if (ActualCard == null) {
				TakeNextCardFrom(s.Opposite());
			}
			if (ActualCard == null && Deck != null) {
				ActualCard = Deck.TakeAndRemoveTopCard();
			}
		}
		Debug.Log("taking next card with: " + gameObject.name + ", next card is : " + (ActualCard!=null?ActualCard.Name:"none"));
	}

	private void TakeNextCardFrom(Side s) {

		if (ActualCard == null && s == Side.Left && LeftOrb != null && LeftOrb.GetComponent<PanelOrb>().ActualCard != null) {
			ActualCard = LeftOrb.GetComponent<PanelOrb>().ActualCard;
			LeftOrb.GetComponent<PanelOrb>().ActualCard = null;
			LeftOrb.GetComponent<PanelOrb>().TakeNextCard();
		}
		if (ActualCard == null && s == Side.Right && RightOrb != null && RightOrb.GetComponent<PanelOrb>().ActualCard != null) {
			ActualCard = RightOrb.GetComponent<PanelOrb>().ActualCard;
			RightOrb.GetComponent<PanelOrb>().ActualCard = null;
			RightOrb.GetComponent<PanelOrb>().TakeNextCard();
		}
	}


	internal void ReceiveDamage(int p) {
		PanelHealth.GetComponent<PanelHealth>().Health -= p;
	}

	internal void SetHealth(int p) {
		PanelHealth.GetComponent<PanelHealth>().Health = p;
	}
}
