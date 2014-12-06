using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class PanelMageAndOrbs : MonoBehaviour, MageControlListener {

	public GameObject LeftOrb, RightOrb, MageOrb;

	private MageState MageState;
	private Deck Deck;

	internal void Prepare(MageState mageState, Deck yourDeck) {
		MageState = mageState;
		Deck = yourDeck;

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
		c.ThrowMe(MageOrb.transform.position, direction);
		MageOrb.GetComponent<PanelOrb>().Card = null;
		LoadCards();
	}

	public void WantCast() {
		Card c = MageOrb.GetComponent<PanelOrb>().Card;
		Sprite s = c.Effect;

		MageOrb.GetComponent<PanelOrb>().ImageSpell.GetComponent<Image>().sprite = s;
		MageOrb.GetComponent<PanelOrb>().ImageSpell.GetComponent<RectTransform>().anchorMin = new Vector2(-s.bounds.size.x / 2, -s.bounds.size.y / 2);
	}

	public void DontWantCast() {
		Card c = MageOrb.GetComponent<PanelOrb>().Card;
		Sprite s = c.Icon;

		MageOrb.GetComponent<PanelOrb>().ImageSpell.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
		MageOrb.GetComponent<PanelOrb>().ImageSpell.GetComponent<Image>().sprite = s;
	}

	internal void LoadCards() {
		LoadCardsInner();
	}

	private void LoadCardsInner() {
		LoadCard(MageOrb.GetComponent<PanelOrb>(), new List<PanelOrb>() { RightOrb.GetComponent<PanelOrb>(), LeftOrb.GetComponent<PanelOrb>() }, Deck);
		LoadCard(RightOrb.GetComponent<PanelOrb>(), null, Deck);
		LoadCard(LeftOrb.GetComponent<PanelOrb>(), null, Deck);
	}

	private void LoadCard(PanelOrb panelOrb, List<PanelOrb> beforeOrbs, Deck deck) {
		if ((panelOrb.Card == null)) {
			
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

			panelOrb.Card = c;
		}
	}



}
