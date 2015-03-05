using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {

	public static Game Me;

	public GameObject PanelMinigame;

	public List<Spell> Spells = new List<Spell>();


	void Awake() {
		Me = this;

		//nazwa, koszt, wielkość pocisku, szybkość lotu, wie
		Spells.Add(new Spell("Fireball", 1, new Dictionary<FlyingParam, int>(){ {FlyingParam.Size, 2}, {FlyingParam.Speed, 40}, {FlyingParam.Damage, 3} }, new Dictionary<AfterHitParam, int>(){ { AfterHitParam.Damage, 1}, {AfterHitParam.Time, 2}} ));
		Spells.Add(new Spell("Ice", 2, new Dictionary<FlyingParam, int>(){ {FlyingParam.Size, 4}, {FlyingParam.Speed, 60}, {FlyingParam.Damage, 3} }, new Dictionary<AfterHitParam, int>(){ { AfterHitParam.Damage, 1}, {AfterHitParam.Time, 2}, {AfterHitParam.SlowDown, 1}} ));

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
	Size,
	Speed,
	Damage,
}

public enum AfterHitParam {
	Damage,
	Time,
	Size,
	SlowDown,
}
