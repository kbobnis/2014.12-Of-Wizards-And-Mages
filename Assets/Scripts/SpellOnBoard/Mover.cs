using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

	private Vector3 Direction;
	private int Speed;

	internal void Prepare(Vector3 direction, int speed) {
		Direction = direction;
		Speed = speed;
		GetComponent<Rigidbody2D>().AddForce(direction.normalized * Speed * 100);
	}
}
