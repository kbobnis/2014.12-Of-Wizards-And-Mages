using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Game : MonoBehaviour {

	public static Game Me;
	public GameObject PanelMinigame;
	public Camera Camera;

	public List<SpellCard> Spells = new List<SpellCard>();
	
	private OrbState MageState;

	void Awake () {
		Me = this;

		Dictionary<AnimationType, Sprite> animations = new Dictionary<AnimationType, Sprite>(){ {AnimationType.Card, SpriteManager.FireballIcon}, {AnimationType.OnBoard, SpriteManager.FireballAnimation}, {AnimationType.Explode, SpriteManager.FireballExplode}};
		Dictionary<EffectType, int> effects = new Dictionary<EffectType, int>(){ {EffectType.Speed, 180}, {EffectType.Damage, 10}, {EffectType.Health, 1 }};
		Spells.Add(new SpellCard("Fireball", 5, animations, effects));

		animations = new Dictionary<AnimationType,Sprite>(){ {AnimationType.Card, SpriteManager.MudIcon}, {AnimationType.OnBoard, SpriteManager.MudEffect}, {AnimationType.Explode, SpriteManager.MudExplode}  };
		effects = new Dictionary<EffectType,int>(){ {EffectType.Speed, 80}, {EffectType.Damage, 3}, {EffectType.Health, 5}};
		Spells.Add(new SpellCard("Mud block", 3, animations, effects));

		animations = new Dictionary<AnimationType, Sprite>() { { AnimationType.Card, SpriteManager.IceIcon}, { AnimationType.OnBoard, SpriteManager.IceAnimation}, { AnimationType.Explode, SpriteManager.IceExplode } };
		effects = new Dictionary<EffectType, int>() { { EffectType.Speed, 220 }, { EffectType.Damage, 1 }, { EffectType.Health, 20 } };
		Spells.Add(new SpellCard("Ice lance", 3, animations, effects));

		Deck deck = new Deck(new List<SpellCard>() { Spells[2], Spells[1], Spells[0], Spells[2], Spells[1], Spells[0], Spells[1], Spells[1], Spells[2] });
		
		animations = new Dictionary<AnimationType, Sprite>() { { AnimationType.OnBoard, SpriteManager.CardBack}, { AnimationType.Dead, SpriteManager.CardBackDead} };
		effects = new Dictionary<EffectType, int>() {  { EffectType.Health, 20 } };
		SpellCard spell = new SpellCard("Deck", 10, animations, effects);
		OrbState DeckLeft = new OrbState(deck, spell, null, null);

		animations = new Dictionary<AnimationType, Sprite>() { { AnimationType.OnBoard, SpriteManager.Orb }, { AnimationType.Dead, SpriteManager.OrbDead } }; effects = new Dictionary<EffectType, int>() { { EffectType.Damage, 1 }, { EffectType.Health, 20 } };
		effects = new Dictionary<EffectType, int>() { { EffectType.Health, 8 } };
		spell = new SpellCard("Orb", 10, animations, effects);
		OrbState LeftOrbState = new OrbState(null, spell, DeckLeft, null);

		animations = new Dictionary<AnimationType, Sprite>() { { AnimationType.OnBoard, SpriteManager.CardBack }, { AnimationType.Dead, SpriteManager.CardBackDead } };
		effects = new Dictionary<EffectType, int>() { { EffectType.Health, 8 } };
		spell = new SpellCard("Deck", 10, animations, effects);
		OrbState DeckRight = new OrbState(deck, spell, null, null);

		animations = new Dictionary<AnimationType, Sprite>() { { AnimationType.OnBoard, SpriteManager.Orb }, { AnimationType.Dead, SpriteManager.OrbDead } };
		effects = new Dictionary<EffectType, int>() { { EffectType.Health, 8 } };
		spell = new SpellCard("Deck", 10, animations, effects);
		OrbState RightOrbState = new OrbState(null, spell, null, DeckRight);

		animations = new Dictionary<AnimationType, Sprite>() { { AnimationType.Card, SpriteManager.LasiaAlive }, { AnimationType.OnBoard, SpriteManager.LasiaAlive }, { AnimationType.Explode, SpriteManager.LasiaDead } };
		effects = new Dictionary<EffectType, int>() { { EffectType.Health, 30 } };
		SpellCard lasia = new SpellCard("Lasia", 10, animations, effects);
		MageState = new OrbState(null, lasia, LeftOrbState, RightOrbState);

		animations = new Dictionary<AnimationType, Sprite>() { { AnimationType.Card, SpriteManager.ZlyDementor }, { AnimationType.OnBoard, SpriteManager.ZlyDementor }, { AnimationType.Explode, SpriteManager.ZlyDementorDead } };
		Dictionary<EffectType, int>  dEffects = new Dictionary<EffectType, int>() {  { EffectType.Health, 30 } };
		SpellCard dementor = new SpellCard("Dementor", 10, animations, dEffects);
		OrbState LeftOrbStateD = new OrbState(deck, spell, DeckLeft, null);
		OrbState RightOrbStateD = new OrbState(deck, spell, DeckLeft, null);
		OrbState oponentState = new OrbState(null, dementor, LeftOrbStateD, RightOrbStateD);

		PanelMinigame.GetComponent<PanelMinigame>().Prepare(MageState, oponentState);
	}
}
