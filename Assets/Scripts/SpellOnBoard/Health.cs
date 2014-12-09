using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	private int _Value;
	private UpdateHealth UpdateHealth;

	private int Value {
		set {
			_Value = value;
			if (UpdateHealth != null) {
				UpdateHealth(value);
			}
		}
		get { return _Value; }
	}


	public void Prepare(int value, UpdateHealth updateHealth) {
		UpdateHealth = updateHealth;
		Value = value;
	}


	internal void ReceiveDamage(int p) {
		Value -= p;

	}
}
