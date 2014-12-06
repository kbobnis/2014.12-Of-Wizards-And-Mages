using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

class MonsterCard : Card {

	private int Speed, Damage, Health;

	public MonsterCard(int cost, Sprite icon, Sprite animation, int speed, int damage, int health) : base(cost, icon, animation) {
		Speed = speed;
		Damage = damage;
		Health = health;
	}

	public override void ThrowMe(Vector3 from, Vector3 direction)
	{
	 	GameObject spell = new GameObject();
		Image i = spell.AddComponent<Image>();
		i.sprite = Effect;
		spell.transform.position = from;
		spell.AddComponent<Mover>().Prepare(direction, Speed);
		Game.Me.AddCardOnBoard(spell);
	}
}
