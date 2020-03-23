using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OrbState  {

	public SpellCard FromCard;
	private Deck _Deck;
	private OrbState _LeftOrbState, _RightOrbState;
	public Dictionary<AnimationType, Sprite> Animations;

	public OrbState LeftOrbState {
		get { return _LeftOrbState; }
	}
	public OrbState RightOrbState {
		get { return _RightOrbState; }
	}

	public Deck Deck {
		get { return _Deck; }
	}
	public OrbState(Deck deck, SpellCard fromCard, OrbState leftOrbState, OrbState rightOrbState) {
		_Deck = deck;
		FromCard = fromCard;
		_LeftOrbState = leftOrbState;
		_RightOrbState = rightOrbState;
	}

}

