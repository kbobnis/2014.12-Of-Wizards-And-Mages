using UnityEngine;
using System.Collections;

public class Mage  {
	public string Name;
	private MageClass MageClass;

	public Spell LeftHand, RightHand;
	public int _ActualHealth;
	public int _ActualMana;

	private float PercentManaRegenerated, PercentHealthRegenerated;

	public int ActualMana {
		get { return _ActualMana; }
		set { _ActualMana = value; }
	}
	public int ActualHealth {
		get { return _ActualHealth; }
		set { _ActualHealth = value; }
	}

	public int MaxHealth {
		get { return MageClass.StartingLife; }
	}

	public int MaxMana {
		get { return MageClass.StartingMana; }
	}

	public Mage(string name, MageClass mageClass) {
		Name = name;
		MageClass = mageClass;
		_ActualMana = mageClass.StartingMana;
		_ActualHealth = mageClass.StartingLife;
	}

	internal bool CanAfford(Spell Spell) {
		return Spell.Cost <= ActualMana;
	}


	internal void RegenerateMe(float p) {
		RegenerateMana(p);
		RegenerateHealth(p);
	}

	private void RegenerateHealth(float p) {
		if (ActualHealth< MaxHealth) {
			PercentHealthRegenerated += p / (float)MageClass.LifeRegen ;
		} else {
			PercentHealthRegenerated= 0;
		}

		if (PercentHealthRegenerated > 1) {
			ActualHealth += 1;
			PercentHealthRegenerated= 0;
		}
	}

	private void RegenerateMana(float p) {
		if (ActualMana < MaxMana) {
			PercentManaRegenerated += p / MageClass.ManaRegen;
		} else {
			PercentManaRegenerated = 0;
		}

		if (PercentManaRegenerated > 1) {
			ActualMana += 1;
			PercentManaRegenerated = 0;
		}
	}
}


public class MageClass {
	public static readonly MageClass Creator = new MageClass(100, 100, 1, 3);
	public static readonly MageClass Thenacurviat = new MageClass(100, 100, 3, 0.5f);
	
	public readonly int StartingLife;
	public readonly int StartingMana;
	public readonly int LifeRegen;
	public readonly float ManaRegen;

	public MageClass(int life, int mana, int lifeRegen, float manaRegen) {
		StartingLife = life;
		StartingMana = mana;
		LifeRegen = lifeRegen;
		ManaRegen = manaRegen;
	}
	
}
