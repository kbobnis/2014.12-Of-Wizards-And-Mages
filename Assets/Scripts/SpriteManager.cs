using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public delegate void LoadSprite(Sprite s);

public class SpriteManager : MonoBehaviour{

	public static Sprite FireballIcon;
	public static Sprite FireballAnimation;

	public static Sprite ZombieIcon;
	public static Sprite ZombieAnimation;

	public static Sprite IceIcon;
	public static Sprite IceAnimation;

	public static Dictionary<BackgroundType, Sprite> Backgrounds = new Dictionary<BackgroundType, Sprite>();

	static SpriteManager(){
		FireballIcon = Resources.Load<Sprite>("Images/fireballIcon");
		FireballAnimation = Resources.Load<Sprite>("Images/fireball");

		ZombieIcon = Resources.Load<Sprite>("Images/zombie");
		ZombieAnimation = Resources.Load<Sprite>("Images/zombieAnimation");

		IceIcon = Resources.Load<Sprite>("Images/iceIcon");
		IceAnimation = Resources.Load<Sprite>("Images/iceAnimation");

		Backgrounds.Add(BackgroundType.ROAD, Resources.Load<Sprite>("Images/Backgrounds/road"));
	}

}

public enum BackgroundType {
	ROAD
}
