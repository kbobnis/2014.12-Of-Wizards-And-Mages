using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {

	public static Game Me;
	public GameObject PanelMageAndOrbs, PanelMinigame;

	public List<Card> Spells = new List<Card>();
	private List<GameObject> Monsters = new List<GameObject>();

	void Awake () {
		Me = this;

		Spells.Add(new SpellCard(5, SpriteManager.FireballIcon, SpriteManager.FireballAnimation, 80, 2));
		Spells.Add(new MonsterCard(6, SpriteManager.ZombieIcon, SpriteManager.ZombieAnimation, 20, 2, 3));
		Spells.Add(new SpellCard(4, SpriteManager.IceIcon, SpriteManager.IceAnimation, 80, 3));

		PanelMageAndOrbs.GetComponent<PanelMageAndOrbs>().SetDeck(new List<Card>() { Spells[1], Spells[0], Spells[2], Spells[0], Spells[1], Spells[2] });
		PanelMageAndOrbs.GetComponent<PanelMageAndOrbs>().LoadCards();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	internal void AddCardOnBoard(GameObject spell) {
		Monsters.Add(spell);
		spell.transform.parent = PanelMinigame.transform;
	}
}
