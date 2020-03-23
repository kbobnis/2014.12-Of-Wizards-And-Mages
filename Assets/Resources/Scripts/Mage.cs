using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Resources.Scripts.AI;

public class Mage  {
	public string Name;
	private MageClass MageClass;

	public Spell LeftHand, RightHand;
	public float _ActualHealth;
	public float _ActualMana;
	public Shield Shield;
    public List<Vial> LeftVials = new List<Vial>();
	public List<Vial> RightVials = new List<Vial>();
    public List<Vial> ActiveBonuses;// = new List<Vial>();
    public bool virtualplayer;

    public Board board;

	public float ActualMana {
		get { return _ActualMana; }
		set { 
			_ActualMana = value;
			if (_ActualMana > MaxMana) {
				_ActualMana = MaxMana;
			}
		}
	}
	public float ActualHealth {
		get { return _ActualHealth; }
		set { 
			_ActualHealth = value;
			if (_ActualHealth > MaxHealth) {
				_ActualHealth = MaxHealth;
			}
		}
	}

	public int MaxHealth {
		get { return MageClass.StartingLife; }
	}

	public int MaxMana {
		get { return MageClass.StartingMana; }
	}

	public Mage(string name, MageClass mageClass, bool virtualPlayer) {
		Name = name;
		MageClass = mageClass;
		_ActualMana = mageClass.StartingMana;
		_ActualHealth = mageClass.StartingLife;
        this.virtualplayer = virtualPlayer;
	}

	internal bool CanAfford(Spell Spell) {
		return Spell.Cost <= ActualMana;
	}

    internal bool CanAfford(Shield Shield)
    {
        return Shield.SetupCost <= ActualMana;
    }

    internal bool CanSustain(Shield Shield)
    {
        return Shield.SustainCost <= ActualMana;
    }


	internal void Update(float deltaTime) {

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


    internal void UseVial(Vial vial) {
        if (vial.VialParams.ContainsKey(VialParam.HealthAdd)) {
			ActualHealth += vial.VialParams[VialParam.HealthAdd];
        }

        if (vial.VialParams.ContainsKey(VialParam.ManaAdd)) {
			ActualMana += vial.VialParams[VialParam.ManaAdd];
        }
    }

    public void AddVial(Vial vial) {
        LeftVials.Add(vial);
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
