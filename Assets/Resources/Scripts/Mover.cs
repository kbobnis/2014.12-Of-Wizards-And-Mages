using UnityEngine;
using System.Collections;
using System;

public class Mover : MonoBehaviour {
    
	public bool RightMove = false;



	void Update () {

/*
        RectTransform tr = gameObject.GetComponent<RectTransform>();
        SphereCollider col = gameObject.GetComponent<SphereCollider>();
		Vector3 oldPos = gameObject.GetComponent<RectTransform>().position;

		float w = GetComponent<RectTransform> ().GetWidth ();
    
		float xPos = oldPos.x + (float)150 * (RightMove?1:-1) * Time.deltaTime;
        //float xPos = oldPos.x + (float)3 * (RightMove ? 1 : -1) ;
		float x = Game.GetXPositionFromGlobal (xPos + w/2 * (RightMove?1:-1));
		if (x > 1){
			RightMove = false;
		}
		if (x  < -0) {
			RightMove = true;
		}

		gameObject.GetComponent<RectTransform> ().position = new Vector3 (xPos, oldPos.y, oldPos.z);
 */
	}

}
