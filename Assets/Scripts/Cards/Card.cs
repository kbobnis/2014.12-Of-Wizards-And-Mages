using UnityEngine;
using System.Collections;

public abstract class Card  {
	
	private int Cost;
	private Sprite _Icon, _Effect;

	public Sprite Icon {
		get { return _Icon; }
	}

	public Sprite Effect {
		get { return _Effect;  }
	}

	public Card(int cost, Sprite icon, Sprite effect) {
		Cost = cost;
		_Icon = icon;
		_Effect = effect;
	}

	public override string ToString() {
		return "Card, cost: " + Cost;
	}


	public abstract void ThrowMe(Vector3 from, Vector3 direction);
}
