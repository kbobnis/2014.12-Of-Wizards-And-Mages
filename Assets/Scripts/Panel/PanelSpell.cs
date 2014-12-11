using UnityEngine;
using System.Collections;

public class PanelSpell : MonoBehaviour {

	public GameObject PanelHealth, PanelAttack;
	public SpellCard SpellCard;

	public void Prepare(SpellCard c, Vector3 direction) {
		
		SpellCard = c;

		gameObject.GetComponent<SpriteRenderer>().sprite = c.Animations[AnimationType.OnBoard];

		Rigidbody2D r = gameObject.AddComponent<Rigidbody2D>();
		r.gravityScale = 0;

		PolygonCollider2D pc = gameObject.AddComponent<PolygonCollider2D>();//.isTrigger = true;
		PhysicsMaterial2D pm = new PhysicsMaterial2D();
		pm.bounciness = 1;
		pm.friction = 1;
		pc.sharedMaterial = pm;
		
		gameObject.AddComponent<Mover>().Prepare(direction, c.Effects[EffectType.Speed]);

		if (c.Effects.ContainsKey(EffectType.Health)) {
			PanelHealth.GetComponent<PanelHealth>().Health = c.Effects[EffectType.Health];
		} else {
			PanelHealth.SetActive(false);
		}

		if (c.Effects.ContainsKey(EffectType.Damage)) {
			PanelAttack.GetComponent<PanelHealth>().Health = c.Effects[EffectType.Damage];
		} else {
			PanelAttack.SetActive(false);
		}

		
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
