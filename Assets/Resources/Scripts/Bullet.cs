using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Bullet : MonoBehaviour {

	Vector2 Direction;

	void Update() { 
		Vector2 oldPos = transform.position;
		oldPos.x += Direction.x * Time.deltaTime * 10;
		oldPos.y += Direction.y * Time.deltaTime * 10;
		transform.position = oldPos;
	}

	internal void Prepare(Mage caster, Spell spell, Vector2 from, Vector2 direction) {
		transform.position = from;
		Direction = direction * spell.FlyingParams[FlyingParam.Speed];
		GetComponent<Image>().sprite = Resources.Load<Sprite>("GUI/" + spell.Name);

		//size
		transform.localScale *= (float)spell.FlyingParams[FlyingParam.Size] / 5f;

	}
}
