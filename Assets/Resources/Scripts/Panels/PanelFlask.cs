using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PanelFlask : MonoBehaviour {

	internal void UpdateValue(int actual, int max) {
		gameObject.GetComponentInChildren<Text>().text = actual + "/" + max;
	}
}
