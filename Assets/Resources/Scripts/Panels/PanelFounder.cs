using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PanelFounder : MonoBehaviour {

	public GameObject FounderMain, FounderShadow;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	internal void Prepare(Bonus bonus) {
		FounderMain.GetComponent<Image>().sprite = bonus.SpriteOnMap;
		FounderShadow.GetComponent<Image>().sprite = bonus.SpriteOnMapShadow;

		GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
		GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);

		float maxX = transform.parent.transform.GetComponent<RectTransform>().GetWidth();
		float maxY = transform.parent.transform.GetComponent<RectTransform>().GetHeight();
		float givenX = Random.Range(0, maxX - GetComponent<RectTransform>().GetWidth());
		float givenY = Random.Range(0, maxY - GetComponent<RectTransform>().GetHeight() );

		GetComponent<RectTransform>().offsetMin = new Vector2(givenX, -givenY);
		GetComponent<RectTransform>().offsetMax = new Vector2(givenX, -givenY);

		//Debug.Log("Given x: " + givenX + ", given y: " + givenY + ", Max x : " + maxX + ", max y: " + maxY);
		//GetComponent<RectTransform>().position = new Vector3(givenX, givenY);
	}
}
