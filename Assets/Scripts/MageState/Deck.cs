using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Deck {
	private List<Card> Cards;

	public Deck(List<Card> list) {
		Cards = list;
	}

	public Card TopCard() {
		Card c = null;
		if (Cards.Count > 0) {
			c = Cards[0];
			Cards.Remove(c);
		}
		return c;
	}
}
