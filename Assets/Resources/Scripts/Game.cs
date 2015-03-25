using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {

	public static Game Me;
	public GameObject PanelMinigame, PanelMenu;
	public List<Spell> Spells = new List<Spell>();
	public Player Player, Enemy;

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
		//nazwa, koszt, wielkość pocisku, szybkość lotu, wie
		Spells.Add(new Spell("Fireball", 5, new Dictionary<FlyingParam, int>(){  {FlyingParam.Speed, 80}, {FlyingParam.Damage, 10} }, new Dictionary<AfterHitParam, int>(){ { AfterHitParam.Damage, 10}, {AfterHitParam.Time, 2}} ));
		Spells.Add(new Spell("Ice", 10, new Dictionary<FlyingParam, int>(){ {FlyingParam.Speed, 120}, {FlyingParam.Damage, 10} }, new Dictionary<AfterHitParam, int>(){ { AfterHitParam.Damage, 20}, {AfterHitParam.Time, 2}, {AfterHitParam.SlowDown, 1}} ));

		Mage ivaAllesi = new Mage("Iva Alessi", MageClass.Thenacurviat);
		ivaAllesi.LeftHand = Spells[0];
		ivaAllesi.RightHand = Spells[1];

		Mage kelThuzad = new Mage("Kel Thuzad", MageClass.Creator);
		kelThuzad.LeftHand = Spells[1];
		kelThuzad.RightHand = Spells[0];

		Player = new Player(ivaAllesi, new List<Spell>(){ Spells[0], Spells[1]});
		Enemy = new Player(kelThuzad, new List<Spell>() { Spells[0], Spells[1] });

		PanelMenu.SetActive(false);
		PanelMinigame.SetActive(true);
		PanelMinigame.GetComponent<PanelMinigame>().Prepare(Player, Enemy);
	}

	public void StartGame() {
		StartCoroutine(StartingGame());
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
