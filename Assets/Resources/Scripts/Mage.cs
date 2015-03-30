using UnityEngine;
using System.Collections;

public class Mage  {
	public string Name;
	private MageClass MageClass;

	public Spell LeftHand, RightHand;
	public float _ActualHealth;
	public float _ActualMana;
	public Shield Shield;

	public float ActualMana {
		get { return _ActualMana; }
		set { _ActualMana = value; }
	}
	public float ActualHealth {
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


	internal void RegenerateMe(float deltaTime) {
		RegenerateMana(deltaTime);
		RegenerateHealth(deltaTime);
	}

	private void RegenerateHealth(float deltaTime) {
		if (ActualHealth < MaxHealth) {
			ActualHealth += deltaTime * MageClass.LifeRegen ;
		} 
	}

	private void RegenerateMana(float deltaTime) {

		if (ActualMana < MaxMana) {
			ActualMana += deltaTime * MageClass.ManaRegen;
		} 
	}

	public bool IsDead() {
		return ActualHealth <= 0;
	}

}


public class MageClass {
	public static readonly MageClass Creator = new MageClass(100, 100, 3, 1);
	public static readonly MageClass Thenacurviat = new MageClass(100, 100, 3, 2);
	
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
