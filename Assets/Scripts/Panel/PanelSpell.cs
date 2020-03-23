using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class PanelSpell : MonoBehaviour {

	public GameObject PanelHealth, PanelAttack;
	public SpellCard SpellCard;

	private float StartedExploding;
	private float ReturnToTrigerTime;

	private Dictionary<GameObject, bool> ActualColliding = new Dictionary<GameObject, bool>();
	
	private int LoseHealth;

	protected int Health {
		get { return PanelHealth.GetComponent<PanelHealth>().Health;  }
		set { PanelHealth.GetComponent<PanelHealth>().Health = value; }
	}

	public void Prepare(SpellCard c, Vector3 direction) {
		gameObject.name = c.Name;
		SpellCard = c;
		GetComponent<SpriteRenderer>().sprite = c.Animations[AnimationType.OnBoard];

		Rigidbody2D r = gameObject.AddComponent<Rigidbody2D>();
		r.gravityScale = 0;

		PolygonCollider2D pc = gameObject.AddComponent<PolygonCollider2D>();
		pc.isTrigger = true;
		PhysicsMaterial2D pm = new PhysicsMaterial2D();
		pm.bounciness = 1;
		pm.friction = 1;
		pc.sharedMaterial = pm;

		if (c.Effects.ContainsKey(EffectType.Speed)) {
			GetComponent<Rigidbody2D>().AddForce(direction.normalized * c.Effects[EffectType.Speed] * 100);
		}

		if (c.Effects.ContainsKey(EffectType.Health)) {
			Health = c.Effects[EffectType.Health];
		} else {
			PanelHealth.SetActive(false);
		}

		if (c.Effects.ContainsKey(EffectType.Damage) ) {
			if (PanelAttack == null) {
				throw new Exception("GameO: " + gameObject.name + " has no panel attack assigned");
			}
			PanelAttack.GetComponent<PanelHealth>().Health = c.Effects[EffectType.Damage];
		} else {
			PanelAttack.SetActive(false);
		}
	}

	void Update() {
		MyUpdate();
	}

	protected void MyUpdate() {
		if (Health <= 0 && StartedExploding == 0) {
			Explode();
		}

		if (StartedExploding > 0 && Time.time > StartedExploding + 0.5f) {
			Destroy(gameObject);
		}

		if (LoseHealth != 0) {
			Health -= LoseHealth;
			LoseHealth = 0;
		}
		if (ReturnToTrigerTime < Time.time && GetComponent<PolygonCollider2D>() != null) {
			GetComponent<PolygonCollider2D>().isTrigger = true;
		}
		
	}

	private void Explode() {
		GetComponent<SpriteRenderer>().sprite = SpellCard.Animations[AnimationType.Explode];
		StartedExploding = Time.time;
		Destroy(GetComponent<PolygonCollider2D>());
		gameObject.AddComponent<PolygonCollider2D>();
	}

	private void CollideWith(GameObject other) {
		//you can not define immediate actions here. because the other object may not collide if this will be immiediately dead. we will update lose health in the update method
		//when you have zero health, you don't work
		if (!ActualColliding.ContainsKey(other.gameObject) && other.GetComponent<PanelSpell>().Health > 0 && other.GetComponent<PanelSpell>().PanelAttack != null)  {
			LoseHealth += other.GetComponent<PanelSpell>().PanelAttack.GetComponent<PanelHealth>().Health;
			Debug.Log("will lose " + LoseHealth + " health: " + gameObject.name + ", from: " + other.name);
			ActualColliding.Add(other.gameObject, true);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log(gameObject.name + " interacts with: " + other.gameObject.name + ", " + Time.time);
		if (other.gameObject.GetComponent<PanelSpell>() != null) {
			CollideWith(other.gameObject);
		}
		//let it bounce off the border;
		if (other.gameObject.GetComponent<PanelBorder>() != null) {
			GetComponent<PolygonCollider2D>().isTrigger = false;
			ReturnToTrigerTime = Time.time + 0.01f ;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		ActualColliding.Remove(other.gameObject);
	}

	void OnMouseDown() {
		Debug.Log("Mouse is down");
	}

}
