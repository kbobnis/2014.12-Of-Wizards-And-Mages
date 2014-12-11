using UnityEngine;
using System.Collections;

public class PanelSpell : MonoBehaviour {

	public SpellCard SpellCard;

	public void Prepare(SpellCard c, Vector3 direction) {
		
		SpellCard = c;

		gameObject.AddComponent<SpriteRenderer>().sprite = c.Animations[AnimationType.OnBoard];

		Rigidbody2D r = gameObject.AddComponent<Rigidbody2D>();
		r.gravityScale = 0;

		gameObject.AddComponent<PolygonCollider2D>();
		gameObject.AddComponent<Mover>().Prepare(direction, c.Effects[EffectType.Speed]);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
