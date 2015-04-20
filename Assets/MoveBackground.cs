using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MoveBackground : MonoBehaviour {

	private float startingPos = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = transform.position;
		if (startingPos == 0) {
			startingPos = pos.y;
		}
		Debug.Log("pos.y is: " + pos.y);
		if ( pos.y - startingPos > 100) {
			pos.y = startingPos;
		}
		transform.position = new Vector3(pos.x, pos.y + 0.5f, 0);

	}
}
