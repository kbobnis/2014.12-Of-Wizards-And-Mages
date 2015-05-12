using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {

	public static Game Me;
	public GameObject PanelMinigame, PanelMenu;
	public List<Spell> Spells = new List<Spell>();
    public List<Vial> Vials = new List<Vial>();
	public Player Player, Enemy;

	public int W, H;
	public static readonly int GameW = 1080;
	public static readonly int GameH = 1920;
	

	void Awake() {
		Me = this;
		PanelMinigame.SetActive(false);
		PanelMenu.SetActive(true);
	}

	void Update() {
		W = (int)(GameW * AspectRatioKeeper.ActualScale);
		H = (int)(GameH * AspectRatioKeeper.ActualScale);
	}

	public static float GetXPositionFromGlobal(float screenPos){
		return (screenPos - (Screen.width / 2 - Game.Me.W/2)) / (Game.Me.W);
	}

	IEnumerator StartingGame() {
		yield return new WaitForSeconds(0.1f);

		Shield shield = new Shield(5f, 3f, 2f, 0.15f);

		Spells.Add(new Spell("Fireball", 5, new Dictionary<FlyingParam, int>(){  {FlyingParam.Speed, 80}, {FlyingParam.Damage, 10}, {FlyingParam.Size, 12} }, new Dictionary<AfterHitParam, int>(){ { AfterHitParam.Damage, 10}, {AfterHitParam.Time, 2}} ));
		Spells.Add(new Spell("Ice", 10, new Dictionary<FlyingParam, int>(){ {FlyingParam.Speed, 120}, {FlyingParam.Damage, 10}, {FlyingParam.Size, 10} }, new Dictionary<AfterHitParam, int>(){ { AfterHitParam.Damage, 20}, {AfterHitParam.Time, 2}, {AfterHitParam.SlowDown, 1}} ));

		Vial vial = new Vial("Minor Mana Potion", new Dictionary<VialParam, int>() { { VialParam.ManaAdd, 100 }, { VialParam.HealthAdd, 100 } }, SpriteManager.ManaPotion);
        Vials.Add(vial);
        Vials.Add(new Vial("Minor Health Potion", new Dictionary<VialParam, int>() { { VialParam.HealthAdd, 100 } }, SpriteManager.HealthPotion));

		List<Vial> rightVials = new List<Vial>();
		rightVials.Add(new Vial("Double bonus", new Dictionary<VialParam, int>() { { VialParam.ManaAdd, 100 }, { VialParam.HealthAdd, 100 } }, SpriteManager.HealthPotion));
        rightVials.Add(new Vial("Faster mana recovery", new Dictionary<VialParam, int>() { { VialParam.HealthAdd, 100 } }, SpriteManager.ManaPotion));

		Mage ivaAllesi = new Mage("Iva Alessi", MageClass.Thenacurviat);
		ivaAllesi.LeftHand = Spells[0];
		ivaAllesi.RightHand = Spells[1];
		ivaAllesi.Shield = shield;
        ivaAllesi.LeftVials = Vials;
		ivaAllesi.RightVials = rightVials;

		Mage kelThuzad = new Mage("Kel Thuzad", MageClass.Creator);
		kelThuzad.LeftHand = Spells[1];
		kelThuzad.RightHand = Spells[0];
        kelThuzad.Shield = shield;
        kelThuzad.LeftVials = Vials;

		Player = new Player(ivaAllesi, new List<Spell>(){ Spells[0], Spells[1]});
		Enemy = new Player(kelThuzad, new List<Spell>() { Spells[0], Spells[1]});
		Enemy.SetAi(new Ai(0.9f, 5.9f, 0.9f));

		PanelMenu.SetActive(false);
		PanelMinigame.SetActive(true);

		Bonus b = new Bonus(vial, SpriteManager.HealthBonusOnMap, SpriteManager.HealthBonusOnMapShadow, true);

		MinigameParameters minigameParameters = new MinigameParameters(new BonusParameters(0, 3, 1, 1, new List<BonusConfig>(){new BonusConfig(b, 0.8f), new BonusConfig(null, 0.1f)}));

		PanelMinigame.GetComponent<PanelMinigame>().Prepare(Player, Enemy, minigameParameters);
	}

	public void StartGame() {
		StartCoroutine(StartingGame());
	}

}

public enum FlyingParam {
	Speed,
	Damage,
	Size
}

public enum AfterHitParam {
	Damage,
	Time,
	SlowDown,
}
