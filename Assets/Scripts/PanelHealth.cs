using UnityEngine;
using System.Collections;

public class PanelHealth : MonoBehaviour {

	public GameObject TextHealth;

	private int _Health;

	public int Health {
		set { 
			_Health = value;
			TextHealth.GetComponent<TextMesh>().text = "" + _Health;
		}
		get {  return _Health; }
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool IsDead() {
		return _Health <= 0;
	}
}
