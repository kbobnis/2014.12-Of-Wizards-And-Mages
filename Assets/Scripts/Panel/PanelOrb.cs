using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PanelOrb : MonoBehaviour {

	public GameObject ImageBack, ImageSpell, TextHealth;

	private Card _Card;

	internal Card Card{
		get {
			
			return _Card; 
		}
		set {
			_Card = value;
			ImageSpell.GetComponent<SpriteRenderer>().sprite = _Card==null ? null :_Card.Icon;
			ImageSpell.GetComponent<SpriteRenderer>().enabled = _Card != null;
		}
	}

			

	internal void Prepare(Card card) {
		Card = card;
	}

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log("on trigger enter: " + gameObject.name + ", other: " + other.gameObject.name);
		other.GetComponent<CardComponent>().Card.TriggerAction(other.gameObject, gameObject);
	}
	void OnTriggerStay2D(Collider2D other) {
		Debug.Log("on trigger stay: " + gameObject.name + ", other: " + other.gameObject.name);
	}
	void OnTriggerExit2D(Collider2D other) {
		Debug.Log("on trigger exit: " + gameObject.name + ", other: " + other.gameObject.name);
	}

}
