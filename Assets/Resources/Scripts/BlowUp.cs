using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

class BlowUp : MonoBehaviour{

	internal void Prepare(Spell Spell) {
		Destroy(gameObject.GetComponent<Bullet>());
		GetComponent<Image>().color = Color.black;
		StartCoroutine(DieInSeconds());
	}

	IEnumerator DieInSeconds() {
		yield return new WaitForSeconds(0.25f);
		Destroy(gameObject);

	}
}
