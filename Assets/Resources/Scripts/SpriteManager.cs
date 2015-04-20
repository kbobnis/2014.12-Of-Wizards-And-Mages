using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class SpriteManager {
	
	public static Sprite HealthPotion;
	public static Sprite ManaPotion;


	static SpriteManager() {
		Debug.Log("loading misktury");
		HealthPotion = Resources.Load<Sprite>("GUI/mikstura_1");
		ManaPotion = Resources.Load<Sprite>("GUI/mikstura_2");
	}
	
}
