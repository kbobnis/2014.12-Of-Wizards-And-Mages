using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class PanelShield : MonoBehaviour {

	GameObject ActualShield;
	Shield Shield;
	Mage Mage;

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
		ActualShield.AddComponent<ShieldComponent>().Prepare(Shield, Mage);
		ActualShield.transform.parent = transform.parent;
	}

	private void PointerMove() {
	}

	private void PointerUp() {
		ActualShield.GetComponent<ShieldComponent>().PointerUp();
	}

	internal void Prepare(Shield shield, Mage mage) {

		Shield = shield;
		Mage = mage;
		float h = shield.ShieldHeight * AspectRatioKeeper.ActualScale * Screen.height;
		GetComponent<RectTransform>().offsetMin = new Vector2(-180 * AspectRatioKeeper.ActualScale, -h/2);
		GetComponent<RectTransform>().offsetMax = new Vector2(180 * AspectRatioKeeper.ActualScale, h/2);

	}
}

class ShieldComponent : MonoBehaviour {

	Shield Shield;
	Mage Mage;
	bool Sustain;

	public void Prepare(Shield shield, Mage mage) {
		Sustain = true;
		Shield = shield;
		mage.ActualMana -= shield.SetupCost;
		Mage = mage;
		name = "Shield";
		AddShieldElement();
	}

	public void AddShieldElement() {
		GameObject go = new GameObject();
		go.AddComponent<ShieldElement>().Prepare(Shield);
		go.transform.SetParent(transform);
	}

	void Update(){
		if (Sustain) {
			Mage.ActualMana -= Shield.SustainCost * Time.deltaTime;
			AddShieldElement();
		}

		if (transform.childCount == 0) {
			Destroy(gameObject);
		}
	}

	internal void PointerUp() {
		Sustain = false;
	}
}

class ShieldElement : MonoBehaviour {

	public static Sprite Fireball = Resources.Load<Sprite>("GUI/fireball");

	public void Prepare(Shield shield) {
		gameObject.AddComponent<Image>().sprite = Fireball;
		gameObject.GetComponent<RectTransform>().position = Input.mousePosition;
		SphereCollider sc = gameObject.AddComponent<SphereCollider>();
		sc.isTrigger = true;
		sc.radius = gameObject.GetComponent<RectTransform>().GetWidth() / 2;
		gameObject.GetComponent<Transform>().localScale = new Vector3(0.1f, 0.1f, 0.1f);
		Rigidbody r = gameObject.AddComponent<Rigidbody>();
		r.isKinematic = true;
		gameObject.name = "Shield part";
		gameObject.transform.SetParent(gameObject.transform);
		StartCoroutine(DieInSeconds(shield.SustainTime));
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.GetComponent<Bullet>() != null) {
			other.gameObject.GetComponent<Bullet>().BlowUp();
		}
	}

	private IEnumerator DieInSeconds(float sustainTime) {

		for (int i = 0; i < 10; i++) {
			yield return new WaitForSeconds(sustainTime / 10f);
			Color c = GetComponent<Image>().color;
			GetComponent<Image>().color = new Color(c.r, c.g, c.b, c.a - 0.1f);
		}
		Destroy(gameObject);
	}

}
