using UnityEngine;
using System.Collections;

public class Mage : MonoBehaviour {
	public string Name;
	private MageClass MageClass;

	public Spell LeftHand, RightHand;
	public int _ActualHealth;
	public int _ActualMana;
	public int ActualMana {
		get { return _ActualMana; }
	}
	public int ActualHealth {
		get { return _ActualHealth; }
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

}


public class MageClass {
	public static readonly MageClass Creator = new MageClass(100, 100, 0, 1);
	public static readonly MageClass Thenacurviat = new MageClass(100, 100, 0, 1);
	
	public readonly int StartingLife;
	public readonly int StartingMana;
	public readonly int LifeRegen;
	public readonly int ManaRegen;

	public MageClass(int life, int mana, int lifeRegen, int manaRegen) {
		StartingLife = life;
		StartingMana = mana;
		LifeRegen = lifeRegen;
		ManaRegen = manaRegen;
	}
	
}
