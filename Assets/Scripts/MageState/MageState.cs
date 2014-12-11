using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OrbState  {

	private Deck _Deck;
	private int _Health;
	private OrbState _LeftOrbState, _RightOrbState;
	public Dictionary<AnimationType, Sprite> Animations;

	public OrbState LeftOrbState {
		get { return _LeftOrbState; }
	}
	public OrbState RightOrbState {
		get { return _RightOrbState; }
	}

	public int Health {
		get { return _Health; }
	}

	public Deck Deck {
		get { return _Deck; }
	}
	public OrbState(Deck deck, int health, Dictionary<AnimationType, Sprite> animations, OrbState leftOrbState, OrbState rightOrbState) {
		_Deck = deck;
		Animations = animations;
		_Health = health;
		_LeftOrbState = leftOrbState;
		_RightOrbState = rightOrbState;
	}

}

