using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Bullet : MonoBehaviour {

	Vector2 Direction;
	Spell Spell;

	void Update() { 
		Vector2 oldPos = transform.position;
		oldPos.x += Direction.x * Time.deltaTime * 5;
		oldPos.y += Direction.y * Time.deltaTime * 5;
		transform.position = oldPos;

	}

	internal void Prepare(Mage caster, Spell spell, Vector2 from, Vector2 direction) {
		Spell = spell;
		Direction = direction * spell.FlyingParams[FlyingParam.Speed] * AspectRatioKeeper.ActualScale;
		GetComponent<Image>().sprite = Resources.Load<Sprite>("GUI/" + spell.Name);
		
		float w = GetComponent<Image>().sprite.rect.width * AspectRatioKeeper.ActualScale;
		float h = GetComponent<Image>().sprite.rect.height * AspectRatioKeeper.ActualScale;
		
		GetComponent<RectTransform>().offsetMin = new Vector2(-w / 2, -h / 2);
		GetComponent<RectTransform>().offsetMax = new Vector2(w / 2, h / 2);
		transform.position = from;

		GetComponent<SphereCollider>().radius = w/2;
	}

	void OnTriggerEnter(Collider other) {

		PanelMage pm = other.gameObject.transform.parent.GetComponent<PanelMage>();
		if (pm != null) {
			pm.TakeDamage(Spell.AfterHitParams[AfterHitParam.Damage]);
			BlowUp();
			
		}
		PanelMinigame pmini = other.gameObject.GetComponent<PanelMinigame>();
		if (pmini != null) {

			if (pmini.LeftCollider == other || pmini.RightCollider == other) {
				Direction.x = -Direction.x;
			}

			if (pmini.TopCollider == other || pmini.BottomCollider == other) {
				BlowUp();
			}
		}
		Bullet b = other.GetComponent<Bullet>();
		if (b != null) {
			BlowUp();
			b.BlowUp();
		}
	}

	public void BlowUp() {
		gameObject.AddComponent<BlowUp>().Prepare(Spell);
	}
}
