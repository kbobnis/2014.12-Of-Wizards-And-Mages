using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ImageChanger : MonoBehaviour {

	private Sprite Sprite;
	private float TimeOfChange;
	private Sprite OldSprite;
	private float StartChange;

	// Update is called once per frame
	void Update () {
		if (Sprite != null && OldSprite == null) {
			OldSprite = GetComponent<SpriteRenderer>().sprite;
			StartChange = Time.time;
			GetComponent<SpriteRenderer>().sprite = Sprite;
		}

		if (Time.time > StartChange + TimeOfChange) {
			GetComponent<SpriteRenderer>().sprite = OldSprite;
			Destroy(this);
		}
	}

	internal void Prepare(Sprite sprite, float timeOfChange) {
		Sprite = sprite;
		TimeOfChange = timeOfChange;
	}
}
