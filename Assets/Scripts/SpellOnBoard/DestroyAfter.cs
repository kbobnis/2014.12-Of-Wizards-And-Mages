using UnityEngine;
using System.Collections;

public class DestroyAfter : MonoBehaviour {
	
	private int Delay;
	private float StartCount;

	// Update is called once per frame
	void Update () {
		Debug.Log("time.time: " + Time.time);
		if (Time.time > Delay + StartCount) {
			Destroy(gameObject);
		}
	}

	internal void Prepare(int delay) {
		Delay = delay;
		StartCount = Time.time;

	}
}
