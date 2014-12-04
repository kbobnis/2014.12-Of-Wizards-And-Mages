using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PanelMageAndOrbs : MonoBehaviour, MageControlListener {

	public GameObject MageControl;
	public GameObject PanelMage;
	public List<GameObject> LeftOrbs = new List<GameObject>();
	public List<GameObject> RightOrbs = new List<GameObject>();

	private List<Card> Deck;

	void Awake() {
		MageControl.GetComponent<MageControl>().MageControlListeners.Add(this);
	}

	public void SwapSpell(Side s) {
		switch (s) {
			case Side.Left: {
				Card c = LeftOrbs[0].GetComponent<PanelOrb>().Card;
				if (c != null) {
					LeftOrbs[0].GetComponent<PanelOrb>().Card = PanelMage.GetComponent<PanelOrb>().Card;
					PanelMage.GetComponent<PanelOrb>().Card = c;
				}
				break;
			}
			case Side.Right: {
				Card c = RightOrbs[0].GetComponent<PanelOrb>().Card;
				if (c != null) {
					RightOrbs[0].GetComponent<PanelOrb>().Card = PanelMage.GetComponent<PanelOrb>().Card;
					PanelMage.GetComponent<PanelOrb>().Card = c;
				}
				break;
			}
		}
	}

	public void Drop() {
		PanelMage.GetComponent<PanelOrb>().Card = null;
		LoadCards();
	}

	public void Cast(Vector3 direction) {

	}

	internal void SetDeck(List<Card> list) {
		Deck = list;
	}

	internal void LoadCards() {
		LoadCardsInner();
	}

	private void LoadCardsInner() {
		LoadCard(PanelMage.GetComponent<PanelOrb>(), new List<PanelOrb>() { RightOrbs[0].GetComponent<PanelOrb>(), LeftOrbs[0].GetComponent<PanelOrb>() }, Deck);
		LoadCard(RightOrbs[0].GetComponent<PanelOrb>(), null, Deck);
		LoadCard(LeftOrbs[0].GetComponent<PanelOrb>(), null, Deck);
	}

	private void LoadCard(PanelOrb panelOrb, List<PanelOrb> beforeOrbs, List<Card> Deck) {
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
					Debug.Log("index: " + index);
					c = beforeOrbs[index].Card;
					beforeOrbs[index].Card = null;
				}
			}
			
			//if still not found, try deck
			if (c == null) {
				c = Deck.Count > 0 ? Deck[0] : null;
				if (Deck.Count > 0) {
					Deck.RemoveAt(0);
				}
			}

			panelOrb.Card = c;
		}
	}
}
