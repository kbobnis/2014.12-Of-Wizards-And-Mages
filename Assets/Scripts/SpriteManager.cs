using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public delegate void LoadSprite(Sprite s);

public class SpriteManager : MonoBehaviour{

	public static Sprite LasiaAlive, LasiaDead;
	public static Sprite ZlyDementor, ZlyDementorDead;
	public static Sprite Orb, OrbDead;
	public static Sprite CardBack, CardBackDead;

	public static Sprite FireballIcon, FireballAnimation, FireballExplode;
	public static Sprite ZombieIcon, ZombieAnimation, ZombieAttack;
	public static Sprite IceIcon, IceAnimation, IceExplode;
	public static Sprite MudIcon, MudEffect, MudExplode;


	static SpriteManager(){
		FireballIcon = Resources.Load<Sprite>("Images/fireballIcon");
		FireballAnimation = Resources.Load<Sprite>("Images/fireball");
		FireballExplode = Resources.Load<Sprite>("Images/fireballExplode");

		ZombieIcon = Resources.Load<Sprite>("Images/zombie");
		ZombieAnimation = Resources.Load<Sprite>("Images/zombieAnimation");
		ZombieAttack = Resources.Load<Sprite>("Images/zombieAttack");

		IceIcon = Resources.Load<Sprite>("Images/iceIcon");
		IceAnimation = Resources.Load<Sprite>("Images/iceAnimation");
		IceExplode = Resources.Load<Sprite>("Images/iceExplode");

		LasiaAlive = Resources.Load<Sprite>("Images/lasia");
		LasiaDead = Resources.Load<Sprite>("Images/lasiaDead");

		ZlyDementor = Resources.Load<Sprite>("Images/zlyDementor");
		ZlyDementorDead = Resources.Load<Sprite>("Images/zlyDementorDead");

		Orb = Resources.Load<Sprite>("Images/orb");
		OrbDead = Resources.Load<Sprite>("Images/orbDead");

		CardBack = Resources.Load<Sprite>("Images/cardBack");
		CardBackDead = Resources.Load<Sprite>("Images/cardBackDead");

		MudIcon = Resources.Load<Sprite>("Images/mudIcon");
		MudEffect = Resources.Load<Sprite>("Images/mudEffect");
		MudExplode = Resources.Load<Sprite>("Images/mudExplode");
	}

}

public enum AnimationType {
	Card, OnBoard, Explode, Dead

}
