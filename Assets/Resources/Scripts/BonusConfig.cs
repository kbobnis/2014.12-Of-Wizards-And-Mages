using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Bonus {
	
	public Vial Vial;
	public Sprite SpriteOnMap;
	public Sprite SpriteOnMapShadow;
	private bool Instant;

	public Bonus(Vial vial, Sprite spriteOnMap, Sprite spriteOnMapShadow, bool instant) {
		Vial = vial;
		SpriteOnMap = spriteOnMap;
		SpriteOnMapShadow = spriteOnMapShadow;
		Instant = instant;
	}
}
