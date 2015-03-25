using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class PanelShield : MonoBehaviour {

	GameObject ActualShield;

	void Awake() {
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

	private void PointerDown() {
		ActualShield = new GameObject();
		ActualShield.AddComponent<Shield>().PointerDown();
		ActualShield.transform.parent = transform.parent;
	}

	private void PointerMove() {
		ActualShield.GetComponent<Shield>().PointerMoved();
	}

	private void PointerUp() {
	}

}

class Shield : MonoBehaviour {

	public void PointerDown() {
		name = "Shield";
		AddShieldElement();
	}

	public void AddShieldElement() {
		GameObject go = new GameObject();
		go.AddComponent<ShieldElement>().Prepare();
		go.transform.parent = transform;
	}

	internal void PointerMoved() {
		AddShieldElement();
	}

	void Update() {
		if (transform.childCount == 0) {
			Destroy(gameObject);
		}
	}

}

class ShieldElement : MonoBehaviour {

	public static Sprite Fireball = Resources.Load<Sprite>("GUI/fireball");

	public void Prepare() {
		gameObject.AddComponent<Image>().sprite = Fireball;
		gameObject.GetComponent<RectTransform>().position = Input.mousePosition;
		SphereCollider sc = gameObject.AddComponent<SphereCollider>();
		sc.isTrigger = true;
		sc.radius = gameObject.GetComponent<RectTransform>().GetWidth() / 2;
		gameObject.GetComponent<Transform>().localScale = new Vector3(0.1f, 0.1f, 0.1f);
		Rigidbody r = gameObject.AddComponent<Rigidbody>();
		r.isKinematic = true;
		gameObject.name = "Shield part";
		gameObject.transform.parent = gameObject.transform;
		StartCoroutine(DieInSeconds());
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.GetComponent<Bullet>() != null) {
			other.gameObject.GetComponent<Bullet>().BlowUp();
		}
	}

	private IEnumerator DieInSeconds() {
		for (int i = 0; i < 10; i++) {
			yield return new WaitForSeconds(0.1f);
			Color c = GetComponent<Image>().color;
			GetComponent<Image>().color = new Color(c.r, c.g, c.b, c.a - 0.1f);
		}
		Destroy(gameObject);
	}

}
