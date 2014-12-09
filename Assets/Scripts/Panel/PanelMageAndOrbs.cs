using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public delegate void UpdateHealth(int value);

public class PanelMageAndOrbs : MonoBehaviour, MageControlListener {

	public GameObject LeftOrb, RightOrb, MageOrb;

	private MageState MageState;
	private Deck Deck;

	internal void Prepare(MageState mageState, Deck yourDeck) {
		MageState = mageState;
		Deck = yourDeck;

		MageOrb.AddComponent<Health>().Prepare(mageState.Mage.Health, (int value) => { Debug.Log("update health"); MageOrb.GetComponent<PanelOrb>().TextHealth.GetComponent<TextMesh>().text = "" + value; });
		MageOrb.GetComponent<PanelOrb>().Prepare(TakeNextCard(null, Deck));
		Game.Me.PanelMinigame.GetComponent<PanelMinigame>().AddElementOnBoard(MageOrb);

		RightOrb.AddComponent<Health>().Prepare(mageState.RightOrb.Health, (int value) => { RightOrb.GetComponent<PanelOrb>().TextHealth.GetComponent<TextMesh>().text = "" + value; });
		RightOrb.GetComponent<PanelOrb>().Prepare(TakeNextCard(null, Deck));

		LeftOrb.AddComponent<Health>().Prepare(mageState.LeftOrb.Health, (int value) => { LeftOrb.GetComponent<PanelOrb>().TextHealth.GetComponent<TextMesh>().text = "" + value; });
		LeftOrb.GetComponent<PanelOrb>().Prepare(TakeNextCard(null, Deck));

		LoadCards();
	}

	public void SwapSpell(Side s) {
		switch (s) {
			case Side.Left: {
				Card c = LeftOrb.GetComponent<PanelOrb>().Card;
				if (c != null) {
					LeftOrb.GetComponent<PanelOrb>().Card = MageOrb.GetComponent<PanelOrb>().Card;
					MageOrb.GetComponent<PanelOrb>().Card = c;
				}
				break;
			}
			case Side.Right: {
				Card c = RightOrb.GetComponent<PanelOrb>().Card;
				if (c != null) {
					RightOrb.GetComponent<PanelOrb>().Card = MageOrb.GetComponent<PanelOrb>().Card;
					MageOrb.GetComponent<PanelOrb>().Card = c;
				}
				break;
			}
		}
	}

	public void Drop() {
		MageOrb.GetComponent<PanelOrb>().Card = null;
		LoadCards();
	}

	public void Cast(Vector3 direction) {
		Card c = MageOrb.GetComponent<PanelOrb>().Card;
		GameObject spell = c.ThrowMe(MageOrb.transform.position, direction);
		Game.Me.PanelMinigame.GetComponent<PanelMinigame>().AddElementOnBoard(spell);
		MageOrb.GetComponent<PanelOrb>().Card = null;
		LoadCards();
	}

	public void WantCast() {
		//Card c = MageOrb.GetComponent<PanelOrb>().Card;
		//Sprite s = c.Effect;
		//MageOrb.GetComponent<PanelOrb>().ImageSpell.GetComponent<Image>().sprite = s;
	}

	public void DontWantCast() {
		Card c = MageOrb.GetComponent<PanelOrb>().Card;
		Sprite s = c.Icon;
		MageOrb.GetComponent<PanelOrb>().ImageSpell.GetComponent<SpriteRenderer>().sprite = s;
	}

	internal void LoadCards() {
		LoadCardsInner();
	}

	private void LoadCardsInner() {
		if (MageOrb.GetComponent<PanelOrb>().Card == null) {
			MageOrb.GetComponent<PanelOrb>().Card = TakeNextCard(new List<PanelOrb>() { RightOrb.GetComponent<PanelOrb>(), LeftOrb.GetComponent<PanelOrb>() }, Deck);
		}
		if (RightOrb.GetComponent<PanelOrb>().Card == null) {
			RightOrb.GetComponent<PanelOrb>().Card = TakeNextCard(null, Deck);
		}
		if (LeftOrb.GetComponent<PanelOrb>().Card == null) {
			LeftOrb.GetComponent<PanelOrb>().Card = TakeNextCard(null, Deck);
		}
	}

	private Card TakeNextCard(List<PanelOrb> beforeOrbs, Deck deck) {
		Card c = null;
		//first try one of before orbs
		if (beforeOrbs != null) {
			foreach (PanelOrb po in beforeOrbs.ToArray()) {
				if (po.Card == null) {
					beforeOrbs.Remove(po);
				}
			}
			if (beforeOrbs.Count > 0) {
				int index = Random.Range(0, beforeOrbs.Count);
				c = beforeOrbs[index].Card;
				beforeOrbs[index].Card = null;
			}
		}
			
		//if still not found, try deck
		if (c == null) {
			c = Deck.TopCard();
		}
		return c;
	}



}
