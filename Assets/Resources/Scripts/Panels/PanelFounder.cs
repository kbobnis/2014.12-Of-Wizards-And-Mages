using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PanelFounder : MonoBehaviour {

	public GameObject FounderMain, FounderShadow;

	public Bonus Bonus;

	internal void Prepare(Bonus bonus) {
		Bonus = bonus;
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

		int width = (int)FounderMain.GetComponent<RectTransform>().GetWidth();

		FounderMain.GetComponent<SphereCollider>().radius = width/2;
	}

	internal void TakeBy(Mage Caster) {
		Caster.AddVial(Bonus.Vial);
		Destroy(this.gameObject);
	}
}
