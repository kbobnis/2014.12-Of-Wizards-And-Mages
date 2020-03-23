using UnityEngine;
using System.Collections;

public class PanelFounders : MonoBehaviour {

	public GameObject FounderPrefab;

	// Use this for initialization
	void Start () {
		FounderPrefab.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	internal void Prepare(Bonus bonus) {
		GameObject bonusPrefab = Instantiate(FounderPrefab) as GameObject;
		bonusPrefab.SetActive(true);
		bonusPrefab.transform.parent = transform;
		//bonusPrefab.GetComponent<PanelFounder>().Prepare(bonus);
	}
}
