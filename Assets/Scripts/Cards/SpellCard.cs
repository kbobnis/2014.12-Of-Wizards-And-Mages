using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

class SpellCard : Card {

	private int Speed;
	private int Damage;

	public SpellCard(int cost, Sprite icon, Sprite effect, int speed, int dmg) : base(cost, icon, effect) {
		Speed = speed;
		Damage = dmg;
	}

	public override void ThrowMe(Vector3 from, Vector3 direction) {
		GameObject spell = new GameObject();
		Image i = spell.AddComponent<Image>();
		i.sprite = Effect;
		spell.transform.position = from;
		spell.AddComponent<Mover>().Prepare(direction, Speed);
		Game.Me.AddCardOnBoard(spell);
	}

}
