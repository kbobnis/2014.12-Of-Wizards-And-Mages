using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Deck {
	private List<SpellCard> Cards;

	public Deck(List<SpellCard> list) {
		Cards = list;
	}

	public SpellCard TakeAndRemoveTopCard() {
		SpellCard c = null;
		if (Cards.Count > 0) {
			c = Cards[0];
			Cards.Remove(c);
		}
		return c;
	}
}
