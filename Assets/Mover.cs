using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

	public bool RightMove = false;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 oldPos = gameObject.GetComponent<RectTransform>().position;

		float w = GetComponent<RectTransform> ().GetWidth ();

		float xPos = oldPos.x + (float)3 * (RightMove?1:-1);
		float x = Game.GetXPositionFromGlobal (xPos + w/2 * (RightMove?1:-1));
		if (x > 1){
			RightMove = false;
		}
		if (x  < -0) {
			RightMove = true;
		}

		//Debug.Log ("my x position is " + xPos);

		gameObject.GetComponent<RectTransform> ().position = new Vector3 (xPos, oldPos.y, oldPos.z);
	}
}
