using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class ButtonSpell : MonoBehaviour {

	public GameObject ImageSpellCasting, ImageSpellReady;
	public Spell Spell;
	private Mage Caster;
	public GameObject WhereToCastFrom;
	
	Vector2 StartingMousePos;
	float Distance;
	Vector2 Direction;
	bool WillCast;

	CastListener CastListener;
	private bool Casted;

	public void Prepare(Mage caster, Spell spell, CastListener castListener) {
		CastListener = castListener;
		Caster = caster;
		Spell = spell;
	}

	void Awake() {
		PointerUp();

		EventTrigger et = gameObject.AddComponent<EventTrigger>();
		et.delegates = new System.Collections.Generic.List<EventTrigger.Entry>();

		EventTrigger.TriggerEvent te = new EventTrigger.TriggerEvent();
		te.AddListener((eventData) => PointerDown());
		EventTrigger.Entry entry = new EventTrigger.Entry() { callback = te, eventID = EventTriggerType.PointerDown };
		et.delegates.Add(entry);

		EventTrigger.TriggerEvent te2 = new EventTrigger.TriggerEvent();
		te2.AddListener((eventData) => PointerMove());
		EventTrigger.Entry entry2 = new EventTrigger.Entry() { callback = te2, eventID = EventTriggerType.Drag };
		et.delegates.Add(entry2);

		EventTrigger.TriggerEvent te3 = new EventTrigger.TriggerEvent();
		te3.AddListener((eventData) => PointerUp());
		EventTrigger.Entry entry3 = new EventTrigger.Entry() { callback = te3, eventID = EventTriggerType.PointerUp };
		et.delegates.Add(entry3);
	}

	void Update() {
		if (Caster != null) {
			ImageSpellReady.SetActive(Caster.ActualMana >= Spell.Cost);
		}
	}

	void PointerUp() {

		try {
			ImageSpellCasting.SetActive(false);
			if (WillCast && !Casted) {
				if (Caster.CanAfford(Spell)) {
					Vector2 pos = WhereToCastFrom.GetComponent<Transform>().position;
					CastListener.CastIt(Caster, Spell, new Vector2(pos.x, pos.y- WhereToCastFrom.GetComponent<RectTransform>().GetHeight()/2-GetComponent<RectTransform>().GetHeight()/2 -1 ), Direction);
					Casted = true;
				}
			}
			
		} catch (Exception e) {
			Debug.Log("Exception: " + e);
		}
	}
	
	void PointerDown() {
		Casted = false ;
		WillCast = false;
		StartingMousePos = Input.mousePosition;
		ImageSpellCasting.SetActive(true);
		ImageSpellCasting.GetComponent<Image>().color = Color.black;
		PointerMove();
	}

	void PointerMove(){
		if(!Casted) {
			int dX = (int)Input.mousePosition.x;
			int dY = (int)Input.mousePosition.y;
			Distance = (Mathf.Abs(dX - StartingMousePos.x) + Mathf.Abs(dY - StartingMousePos.y)) / (float)Screen.width;
			Direction = new Vector2((dX - GetComponent<Transform>().position.x), (dY - GetComponent<Transform>().position.y));
			Direction.Normalize();

			ImageSpellCasting.GetComponent<Image>().color = Distance > 0.1f ? Color.green : Color.black;
			ImageSpellCasting.transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

			WillCast = Distance > 0.1f;
			if (WillCast) {
				PointerUp();
			}
		}
	}
}
