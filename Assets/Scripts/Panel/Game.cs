using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {

	public static Game Me;
	public GameObject PanelMinigame;

	public List<Card> Spells = new List<Card>();
	private List<GameObject> Monsters = new List<GameObject>();
	private MageState MageState;

	void Awake () {
		Me = this;

		Spells.Add(new SpellCard(5, SpriteManager.FireballIcon, SpriteManager.FireballAnimation, 80, 2));
		Spells.Add(new MonsterCard(6, SpriteManager.ZombieIcon, SpriteManager.ZombieAnimation, 20, 2, 3));
		Spells.Add(new SpellCard(4, SpriteManager.IceIcon, SpriteManager.IceAnimation, 80, 3));
		
		MageState = new MageState();
		MageState.Decks.Add( new Deck(new List<Card>() { Spells[1], Spells[0], Spells[2], Spells[0], Spells[1], Spells[2] }) );

		MageState opponentsState = new MageState();
		opponentsState.Decks.Add( new Deck(new List<Card>() { Spells[1], Spells[1], Spells[1], Spells[1] } ));

		MinigameBackground minigameBackground = new MinigameBackground(SpriteManager.Backgrounds[BackgroundType.ROAD]);

		PanelMinigame.GetComponent<PanelMinigame>().Prepare(MageState, MageState.Decks[0], opponentsState, opponentsState.Decks[0], minigameBackground);

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	internal void AddCardOnBoard(GameObject spell) {
		Monsters.Add(spell);
		spell.transform.parent = PanelMinigame.transform;
	}
}
