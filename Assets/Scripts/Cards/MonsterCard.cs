using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class MonsterCard : Card {

	private int Speed, Damage, Health;

	public MonsterCard(int cost, Sprite icon, Sprite animation, int speed, int damage, int health) : base(cost, icon, animation) {
		Speed = speed;
		Damage = damage;
		Health = health;
	}
}
