using UnityEngine;
using System.Collections;

public class Card  {
	
	private int Cost;
	private Sprite _Icon, Effect;

	public Sprite Icon {
		get { return _Icon; }
	}

	public Card(int cost, Sprite icon, Sprite effect) {
		Cost = cost;
		_Icon = icon;
		Effect = effect;
	}

	public override string ToString() {
		return "Card, cost: " + Cost;
	}

}
