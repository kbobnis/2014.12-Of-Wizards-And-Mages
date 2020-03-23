using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class Ai {

	/**
	 * Accuracy 0 - 1 where 1 i always hit the center of enemy
	 */
	public float Accuracy;
	public float FiringSpeed;
	public float MoveSpeed;

	public Ai(float accuracy, float firingSpeed, float moveSpeed) {
		Accuracy = accuracy;
		FiringSpeed = firingSpeed;
		MoveSpeed = moveSpeed;
	}

}
