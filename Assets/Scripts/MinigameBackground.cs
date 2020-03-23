using UnityEngine;
using System.Collections;

public class MinigameBackground  {
	
	private Sprite _Sprite;

	public Sprite Sprite {
		get { return _Sprite; }
	}

	public MinigameBackground(Sprite sprite) {
		_Sprite = sprite;
	}

}
