using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {

	public static Game Me;
	public GameObject PanelMinigame, PanelMenu;
	public List<Spell> Spells = new List<Spell>();
    public List<Vial> Bonuses = new List<Vial>();
	public Player Player, Enemy;

	public List<GameTickListener> GameTickListeners = new List<GameTickListener>();

	void Awake() {
		Me = this;
		PanelMinigame.SetActive(false);
		PanelMenu.SetActive(true);
	}

	public static float GetXPositionFromGlobal(float screenPos){
		return (screenPos - (Screen.width / 2 - 360 * AspectRatioKeeper.ActualScale/2)) / (360 * AspectRatioKeeper.ActualScale);
	}

	IEnumerator StartingGame() {
		yield return new WaitForSeconds(0.1f);

		Shield shield = new Shield(5f, 3f, 2f, 0.15f);



		Spells.Add(new Spell("Fireball", 5, new Dictionary<FlyingParam, int>(){  {FlyingParam.Speed, 80}, {FlyingParam.Damage, 10} }, new Dictionary<AfterHitParam, int>(){ { AfterHitParam.Damage, 10}, {AfterHitParam.Time, 2}} ));
		Spells.Add(new Spell("Ice", 10, new Dictionary<FlyingParam, int>(){ {FlyingParam.Speed, 120}, {FlyingParam.Damage, 10} }, new Dictionary<AfterHitParam, int>(){ { AfterHitParam.Damage, 20}, {AfterHitParam.Time, 2}, {AfterHitParam.SlowDown, 1}} ));
        Bonuses.Add(new Vial("Minor Mana Potion", new Dictionary<VialParam, int>() { { VialParam.ManaRegen, 100 }, { VialParam.LifeRegen, 100 } }));
        Bonuses.Add(new Vial("Minor Health Potion", new Dictionary<VialParam, int>() { { VialParam.LifeRegen, 100 } }));

   // ManaRegen, LifeRegen, DmgBonus, Time;

		Mage ivaAllesi = new Mage("Iva Alessi", MageClass.Thenacurviat);
		ivaAllesi.LeftHand = Spells[0];
		ivaAllesi.RightHand = Spells[1];
		ivaAllesi.Shield = shield;
        ivaAllesi.ActiveBonuses = Bonuses;

		Mage kelThuzad = new Mage("Kel Thuzad", MageClass.Creator);
		kelThuzad.LeftHand = Spells[1];
		kelThuzad.RightHand = Spells[0];
        kelThuzad.Shield = shield;
        kelThuzad.ActiveBonuses = Bonuses;

		Player = new Player(ivaAllesi, new List<Spell>(){ Spells[0], Spells[1]});
		Enemy = new Player(kelThuzad, new List<Spell>() { Spells[0], Spells[1] });

		PanelMenu.SetActive(false);
		PanelMinigame.SetActive(true);

		Ai ai = new Ai(0.9f, 0.9f, 0.9f);

		AiController aic = new AiController();
		aic.PrepareFight(Enemy.Mage, ai, PanelMinigame.GetComponent<PanelMinigame>().PanelMageTop.GetComponent<PanelMage>(), PanelMinigame.GetComponent<PanelMinigame>().PanelMageBottom.GetComponent<PanelMage>());

		GameTickListeners.Add(aic);

		PanelMinigame.GetComponent<PanelMinigame>().Prepare(Player, Enemy);
	}

	public void StartGame() {
		StartCoroutine(StartingGame());
	}

	void Update() {
		foreach (GameTickListener gtl in GameTickListeners) {
			gtl.GameUpdate();
		}
	}
}

public enum FlyingParam {
	Speed,
	Damage,
}

public enum AfterHitParam {
	Damage,
	Time,
	SlowDown,
}
