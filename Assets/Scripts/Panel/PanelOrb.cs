using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PanelOrb : PanelSpell {

	public GameObject ImageBack, ImageSpell, LeftOrb, RightOrb;

	public bool HideActualCard;

	private SpellCard _ActualCard;
	private Deck Deck;

	internal SpellCard ActualCard{
		get {
			return _ActualCard; 
		}
		set {
			_ActualCard = value;
			ImageSpell.GetComponent<SpriteRenderer>().sprite = _ActualCard==null ? null :_ActualCard.Animations[AnimationType.Card];
			ImageSpell.GetComponent<SpriteRenderer>().enabled = _ActualCard != null && !HideActualCard;
		}
	}

	void Update() {
		base.MyUpdate();
		ImageSpell.SetActive(Health > 0);
		ImageBack.SetActive(Health > 0);
	}
			

	internal void Prepare(Deck deck, SpellCard c, OrbState leftOrb, OrbState rightOrb) {
		base.Prepare(c, new Vector3());

		Deck = deck;
		PanelHealth.GetComponent<PanelHealth>().Health = c.Effects[EffectType.Health];
		if (LeftOrb != null){
			if (leftOrb != null) {
				LeftOrb.GetComponent<PanelOrb>().Prepare(leftOrb.Deck, leftOrb.FromCard, leftOrb.LeftOrbState, leftOrb.RightOrbState);
			} else {
				LeftOrb.SetActive(false);
			}
		} 
		if (RightOrb != null){
			if (rightOrb != null) {
				RightOrb.GetComponent<PanelOrb>().Prepare(rightOrb.Deck, rightOrb.FromCard, rightOrb.LeftOrbState, rightOrb.RightOrbState);
			} else {
				RightOrb.SetActive(false);
			}
		}
		TakeNextCard();
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
		if (ActualCard != null) {
			GameObject spell = (GameObject)Instantiate(Game.Me.PanelMinigame.GetComponent<PanelMinigame>().PanelSpells.GetComponent<PanelSpells>().SpellPrefab);
			spell.SetActive(true);
			spell.transform.parent = Game.Me.PanelMinigame.GetComponent<PanelMinigame>().PanelSpells.transform;
			spell.transform.position = ImageSpell.transform.position;

			spell.GetComponent<PanelSpell>().Prepare(ActualCard, direction);

			ActualCard = null;
			TakeNextCard();
		}
	}

	public void WantCast() {
		if (ActualCard != null) {
			ImageSpell.GetComponent<SpriteRenderer>().sprite = ActualCard.Animations[AnimationType.OnBoard];
		}
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
