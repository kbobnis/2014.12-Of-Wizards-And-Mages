using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class ButtonSpell : MonoBehaviour {

	public GameObject ImageSpellReady;
	public Spell Spell;
	private Mage Caster;
	public GameObject WhereToCastFrom;
	
	Vector2 StartingMousePos;
	float Distance;
	Vector2 Direction;
	bool WillCast;

	public CastListener CastListener;
	private bool Casted;

	public void Prepare(Mage caster, Spell spell, CastListener castListener) {
		CastListener = castListener;
		Caster = caster;
		Spell = spell;
	}

	void Awake() {

		PointerUp();

		EventTrigger et = gameObject.AddComponent<EventTrigger>();
		et.triggers = new System.Collections.Generic.List<EventTrigger.Entry>();

		EventTrigger.TriggerEvent te = new EventTrigger.TriggerEvent();
		te.AddListener((eventData) => PointerDown());
		EventTrigger.Entry entry = new EventTrigger.Entry() { callback = te, eventID = EventTriggerType.PointerDown };
		et.triggers.Add(entry);

		EventTrigger.TriggerEvent te2 = new EventTrigger.TriggerEvent();
		te2.AddListener((eventData) => PointerMove());
		EventTrigger.Entry entry2 = new EventTrigger.Entry() { callback = te2, eventID = EventTriggerType.Drag };
		et.triggers.Add(entry2);

		EventTrigger.TriggerEvent te3 = new EventTrigger.TriggerEvent();
		te3.AddListener((eventData) => PointerUp());
		EventTrigger.Entry entry3 = new EventTrigger.Entry() { callback = te3, eventID = EventTriggerType.PointerUp };
		et.triggers.Add(entry3);
	}

	void Update() {
		if (Caster != null) {
			ImageSpellReady.SetActive(Caster.ActualMana >= Spell.Cost);
		}
	}

	void PointerUp() {

		try {
			if (WillCast && !Casted) {
				Cast(Direction);
			}
			
		} catch (Exception e) {
			Debug.Log("Exception: " + e);
		}
	}

	public void Cast(Vector3 direction) {
		if (Caster.CanAfford(Spell)) {
			Vector2 pos = WhereToCastFrom.GetComponent<Transform>().position;
			float meRadius = WhereToCastFrom.GetComponent<RectTransform>().GetHeight() / 2;
			float spellRadius = GetComponent<RectTransform>().GetHeight() / 2;
			float x = pos.x + direction.x * (meRadius + spellRadius);
			float y = pos.y + direction.y * (meRadius + spellRadius);
			CastListener.CastIt(Caster, Spell, new Vector2(x, y), direction);
			Casted = true;
		}
	}
	
	void PointerDown() {
		Casted = false ;
		WillCast = false;
		StartingMousePos = Input.mousePosition;
		PointerMove();
	}

	void PointerMove(){
		if(!Casted) {
			int dX = (int)Input.mousePosition.x;
			int dY = (int)Input.mousePosition.y;
			Distance = (Mathf.Abs(dX - StartingMousePos.x) + Mathf.Abs(dY - StartingMousePos.y)) / (float)Screen.width;
			Direction = new Vector2((dX - GetComponent<Transform>().position.x), (dY - GetComponent<Transform>().position.y));
			Direction.Normalize();

			WillCast = Distance > 0.1f;
			if (WillCast) {
				PointerUp();
			}
		}
	}
}
