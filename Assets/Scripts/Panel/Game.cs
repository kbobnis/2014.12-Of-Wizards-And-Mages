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

		//animations
		//Spells.Add(new SpellCard("Cart", 2, new Dictionary<AnimationType, Sprite[]>(){}, new Dictionary<EffectType, value>(){ ET.speed => 180, ET.damage => 1, ET.health => 1}));
		//Spells.Add(new SpellCard("Shield", 2, new Dictionary<AnimationType, Sprite[]>(){}, new Dictionary<EffectType, value>(){ ET.drawShape => 10, ET.health => 10}));

		Deck deck = new Deck(new List<SpellCard>() { Spells[1], Spells[0], Spells[1], Spells[0], Spells[1], Spells[1] });
		animations = new Dictionary<AnimationType, Sprite>() { { AnimationType.OnBoard, SpriteManager.CardBack}, { AnimationType.Dead, SpriteManager.CardBackDead} };
		OrbState DeckLeft = new OrbState(deck, 10, animations, null, null);
		animations = new Dictionary<AnimationType,Sprite>(){ { AnimationType.OnBoard, SpriteManager.Orb}, {AnimationType.Dead, SpriteManager.OrbDead}};
		OrbState LeftOrbState = new OrbState(null, 8, animations, DeckLeft, null);

		animations = new Dictionary<AnimationType, Sprite>() { { AnimationType.OnBoard, SpriteManager.CardBack }, { AnimationType.Dead, SpriteManager.CardBackDead } };
		OrbState DeckRight = new OrbState(deck, 10, animations, null, null);
		animations = new Dictionary<AnimationType, Sprite>() { { AnimationType.OnBoard, SpriteManager.Orb }, { AnimationType.Dead, SpriteManager.OrbDead } };
		OrbState RightOrbState = new OrbState(null, 8, animations, null, DeckRight);

		animations = new Dictionary<AnimationType, Sprite> { { AnimationType.OnBoard, SpriteManager.LasiaAlive }, { AnimationType.Dead, SpriteManager.LasiaDead } };
		MageState = new OrbState(null, 30, animations, LeftOrbState, RightOrbState);

		PanelMinigame.GetComponent<PanelMinigame>().Prepare(MageState);

	}

}
