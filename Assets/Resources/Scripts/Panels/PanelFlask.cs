using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PanelFlask : MonoBehaviour {

	public GameObject Image1, Image2, CutMask;

	private Vector3 StartingPos;
	private float CutMaskStartingY, CutMaskHeight;

	void OnEnable() {

		if (Image1 != null) {
			Vector3 lifeImage2Pos = Image1.GetComponent<RectTransform>().position;
			lifeImage2Pos.y -= Image1.GetComponent<RectTransform>().GetHeight();
			Image2.GetComponent<RectTransform>().position = lifeImage2Pos;

			StartingPos = Image1.GetComponent<RectTransform>().position;

			CutMaskStartingY = CutMask.transform.position.y;
			CutMaskHeight = CutMask.GetComponent<RectTransform>().GetHeight();
		}
	}

	void Update() {

		if (Image1 != null) {
			Vector3 pos1 = Image1.GetComponent<RectTransform>().position;
			pos1.y += 0.5f;
			Image1.GetComponent<RectTransform>().position = pos1;

			Vector3 pos2 = Image2.GetComponent<RectTransform>().position;
			pos2.y += 0.5f;
			Image2.GetComponent<RectTransform>().position = pos2;

			if (pos2.y >= StartingPos.y) {
				Image1.GetComponent<RectTransform>().position = StartingPos;
				Vector3 tmp = StartingPos;
				tmp.y -= Image1.GetComponent<RectTransform>().GetHeight();
				Image2.GetComponent<RectTransform>().position = tmp;
			}
		}
	}
	
	internal void UpdateValue(int actual, int max) {

		if (CutMask != null) {
			float percent = actual / (float)max;

			float CutMaskActualY = CutMaskStartingY -  CutMaskHeight * (1 - percent);
			Vector3 pos = CutMask.GetComponent<RectTransform>().position;
			pos.y = CutMaskActualY;
			CutMask.GetComponent<RectTransform>().position = pos;
		}
	}
}
