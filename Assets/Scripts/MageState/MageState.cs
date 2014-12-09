using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MageState  {

	private List<Deck> _Decks = new List<Deck>();
	private OrbState _Mage, _LeftOrb, _RightOrb;
	private List<Deck> decks;

	public OrbState Mage {
		get { return _Mage; }
	}
	public OrbState LeftOrb {
		get { return _LeftOrb; }
	}
	public OrbState RightOrb {
		get { return _RightOrb; }
	}

	public List<Deck> Decks {
		get { return _Decks; }
	}

	public MageState(List<Deck> decks, OrbState mage, OrbState leftOrb, OrbState rightOrb) {
		_Decks = decks;
		_Mage = mage;
		_LeftOrb = leftOrb;
		_RightOrb = rightOrb;
	}

}

public class OrbState {

	private int _Health;

	public OrbState(int health) {
		_Health = health;
	}

	public int Health {
		get { return _Health; }
	}
}
