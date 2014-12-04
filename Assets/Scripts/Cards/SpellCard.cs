using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class SpellCard : Card {

	private float Speed;
	private int Damage;

	public SpellCard(int cost, Sprite icon, Sprite effect, float speed, int dmg) : base(cost, icon, effect) {
		Speed = speed;
		Damage = dmg;
	}

}
