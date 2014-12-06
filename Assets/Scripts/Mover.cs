using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

	private Vector3 Direction;
	private int Speed;

	void Update () {
		if (Speed != 0) {
			transform.position += Direction.normalized * Speed * Time.deltaTime * AspectRatioKeeper.ActualScale;
		}
	}

	internal void Prepare(Vector3 direction, int speed) {
		Direction = direction;
		Speed = speed;
	}
}
