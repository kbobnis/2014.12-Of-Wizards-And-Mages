using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public delegate void ThrowMeInner(GameObject spellGO, Vector3 direction);
public delegate void TriggerAction(GameObject spellGO, GameObject theHit);

public class Card {

	private string _Name;
	private int Cost;
	private Sprite _Icon;
	private ThrowMeInner ThrowMeInner;
	private string p1;
	private int p2;
	private Sprite sprite;
	private TriggerAction _TriggerAction;

	public Sprite Icon {
		get { return _Icon; }
	}

	public string Name {
		get { return _Name; }
	}

	public TriggerAction TriggerAction {
		get { return _TriggerAction; }
	}

	public Card(string name, int cost, Sprite icon, ThrowMeInner throwMeInner, TriggerAction triggerAction) {
		_Name = name;
		Cost = cost;
		_Icon = icon;
		ThrowMeInner = throwMeInner;
		_TriggerAction = triggerAction;
	}

	public override string ToString() {
		return "Card, cost: " + Cost;
	}


	public GameObject ThrowMe(Vector3 from, Vector3 direction) {
		GameObject spellGO = new GameObject();
		spellGO.transform.position = from;
		spellGO.transform.localScale = new Vector3(1, 1, 1);
		spellGO.name = "Spell: " + Name;
		Rigidbody2D r = spellGO.AddComponent<Rigidbody2D>();
		r.gravityScale = 0;
		spellGO.AddComponent<CardComponent>().Card = this;
	
		ThrowMeInner(spellGO, direction);
		spellGO.AddComponent<PolygonCollider2D>();
		return spellGO;
	}


}
