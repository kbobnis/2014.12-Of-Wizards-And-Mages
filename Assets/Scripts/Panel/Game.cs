using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Game : MonoBehaviour {

	public static Game Me;
	public GameObject PanelMinigame;
	public Camera Camera;

	public List<Card> Spells = new List<Card>();
	
	private MageState MageState;

	void Awake () {
		Me = this;

		Spells.Add(new Card("Fireball", 5, SpriteManager.FireballIcon,  /*after hitting board */(GameObject spellGO, Vector3 direction) => {
			spellGO.AddComponent<SpriteRenderer>().sprite = SpriteManager.FireballAnimation;
			spellGO.AddComponent<Mover>().Prepare(direction, 80);
		}, (GameObject spellGO, GameObject theHit) => {
				spellGO.AddComponent<DestroyAfter>().Prepare(1);
				spellGO.AddComponent<ImageChanger>().Prepare(SpriteManager.FireballExplode, 0.4f);
				theHit.GetComponent<Health>().ReceiveDamage(5);
		}));
		Spells.Add(new Card("Zombie", 6, SpriteManager.ZombieIcon, (GameObject spellGO, Vector3 direction) => {
			spellGO.AddComponent<SpriteRenderer>().sprite = SpriteManager.ZombieAnimation;
			spellGO.AddComponent<Mover>().Prepare(direction, 40);
			spellGO.AddComponent<Health>().ReceiveDamage(-5);
			}, (GameObject spellGO2, GameObject theHit) => {
				spellGO2.AddComponent<ImageChanger>().Prepare(SpriteManager.ZombieAttack, 0.2f);
				theHit.GetComponent<Health>().ReceiveDamage(2);
			}
		));
		Spells.Add(new Card("Ice bolt", 4, SpriteManager.IceIcon, (GameObject spellGO, Vector3 direction) => {
			spellGO.AddComponent<SpriteRenderer>().sprite = SpriteManager.IceAnimation;
			spellGO.AddComponent<Mover>().Prepare(direction, 100);
			spellGO.AddComponent<FreezingEffect>().Prepare(0.4f, 0.1f);
			}, (GameObject spellGO2, GameObject theHit) =>{
				theHit.GetComponent<Health>().ReceiveDamage(3);
				spellGO2.AddComponent<ImageChanger>().Prepare(SpriteManager.IceExplode, 1f);
				spellGO2.AddComponent<DestroyAfter>().Prepare(1);
		}));
		
		List<Deck> decks = new List<Deck>();
		decks.Add(new Deck(new List<Card>() { Spells[1], Spells[0], Spells[2], Spells[0], Spells[1], Spells[2] }));
		MageState = new MageState(decks, new OrbState(30), new OrbState(8), new OrbState(8));

		List<Deck> opponentsDecks = new List<Deck>();
		opponentsDecks.Add( new Deck(new List<Card>() { Spells[1], Spells[1], Spells[1], Spells[1] } ));
		MageState opponentsState = new MageState(opponentsDecks, new OrbState(30), new OrbState(8), new OrbState(8));

		MinigameBackground minigameBackground = new MinigameBackground(SpriteManager.Backgrounds[BackgroundType.ROAD]);

		PanelMinigame.GetComponent<PanelMinigame>().Prepare(MageState, MageState.Decks[0], opponentsState, opponentsState.Decks[0], minigameBackground);

	}

}
