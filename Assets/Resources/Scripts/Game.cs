using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {

	public static Game Me;

	public GameObject PanelMinigame;

	public List<Spell> Spells = new List<Spell>();


	void Awake() {
		Me = this;
		StartCoroutine(StartingGame());
	}

	IEnumerator StartingGame() {
		yield return new WaitForSeconds(0.5f);
		//nazwa, koszt, wielkość pocisku, szybkość lotu, wie
		Spells.Add(new Spell("Fireball", 10, new Dictionary<FlyingParam, int>(){  {FlyingParam.Speed, 40}, {FlyingParam.Damage, 10} }, new Dictionary<AfterHitParam, int>(){ { AfterHitParam.Damage, 10}, {AfterHitParam.Time, 2}} ));
		Spells.Add(new Spell("Ice", 20, new Dictionary<FlyingParam, int>(){ {FlyingParam.Speed, 60}, {FlyingParam.Damage, 10} }, new Dictionary<AfterHitParam, int>(){ { AfterHitParam.Damage, 20}, {AfterHitParam.Time, 2}, {AfterHitParam.SlowDown, 1}} ));

		Mage ivaAllesi = new Mage("Iva Alessi", MageClass.Thenacurviat);
		ivaAllesi.LeftHand = Spells[0];
		ivaAllesi.RightHand = Spells[1];

		Mage kelThuzad = new Mage("Kel Thuzad", MageClass.Creator);
		kelThuzad.LeftHand = Spells[1];
		kelThuzad.RightHand = Spells[0];

		PanelMinigame.GetComponent<PanelMinigame>().Prepare(ivaAllesi, kelThuzad);
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
