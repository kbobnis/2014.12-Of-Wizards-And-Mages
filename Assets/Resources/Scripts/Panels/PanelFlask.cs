using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PanelFlask : MonoBehaviour {

	public GameObject LifeImage1, LifeImage2;

	private Vector3 StartingPos;

	void Awake() {

		if (LifeImage1 != null) {
			Vector3 lifeImage2Pos = LifeImage1.GetComponent<RectTransform>().position;
			lifeImage2Pos.y -= LifeImage1.GetComponent<RectTransform>().GetHeight();
			LifeImage2.GetComponent<RectTransform>().position = lifeImage2Pos;

			StartingPos = LifeImage1.GetComponent<RectTransform>().position;
		}
	}

	void Update() {

		if (LifeImage1 != null) {
			Vector3 pos1 = LifeImage1.GetComponent<RectTransform>().position;
			pos1.y += 0.5f;
			LifeImage1.GetComponent<RectTransform>().position = pos1;

			Vector3 pos2 = LifeImage2.GetComponent<RectTransform>().position;
			pos2.y += 0.5f;
			LifeImage2.GetComponent<RectTransform>().position = pos2;

			if (pos2.y >= StartingPos.y) {
				LifeImage1.GetComponent<RectTransform>().position = StartingPos;
				Vector3 tmp = StartingPos;
				tmp.y -= LifeImage1.GetComponent<RectTransform>().GetHeight();
				LifeImage2.GetComponent<RectTransform>().position = tmp;
			}
		}
	}

	internal void UpdateValue(int actual, int max) {
	}
}
