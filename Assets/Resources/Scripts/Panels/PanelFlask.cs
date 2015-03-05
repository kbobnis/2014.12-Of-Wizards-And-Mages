using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PanelFlask : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	internal void UpdateValue(int actual, int max) {
		gameObject.GetComponentInChildren<Text>().text = actual + "/" + max;
	}
}
