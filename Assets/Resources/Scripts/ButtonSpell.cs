using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class ButtonSpell : MonoBehaviour {

	public GameObject ImageSpellCasting;
	public Spell Spell;
	private Mage Caster;
	
	Vector2 StartingMousePos;
	float Distance;
	Vector2 Direction;
	bool WillCast;

	CastListener CastListener;

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

	void PointerUp() {

		try {

			ImageSpellCasting.SetActive(false);
			if (WillCast) {

				if (Caster.CanAfford(Spell)) {
					CastListener.CastIt(Caster, Spell, StartingMousePos, Direction);
				} else {
				}
			}
		} catch (Exception e) {
			Debug.Log("Exception: " + e);

		}
	}
	
	void PointerDown() {
		WillCast = false;
		StartingMousePos = Input.mousePosition;
		ImageSpellCasting.SetActive(true);
		ImageSpellCasting.GetComponent<Image>().color = Color.black;
		PointerMove();
	}

	void PointerMove() {
		int dX = (int)Input.mousePosition.x;
		int dY = (int)Input.mousePosition.y;
		Distance = ( Mathf.Abs(dX - StartingMousePos.x) + Mathf.Abs(dY - StartingMousePos.y)) / (float)Screen.width;
		Direction = new Vector2((dX-StartingMousePos.x), (dY-StartingMousePos.y));
		Direction.Normalize();

		ImageSpellCasting.GetComponent<Image>().color = Distance > 0.05f?Color.green:Color.black;
		WillCast = Distance > 0.05f;
		ImageSpellCasting.transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
	}
}
