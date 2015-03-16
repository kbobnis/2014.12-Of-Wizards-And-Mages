using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PanelMinigame : MonoBehaviour, CastListener {

	public GameObject BulletPrefab, PanelMageBottom, PanelMageTop;

	public BoxCollider TopCollider, BottomCollider, LeftCollider, RightCollider;

	public List<GameObject> Bullets = new List<GameObject>();
	public GameObject MageTop, MageBottom;

	void Awake() {
		BulletPrefab.SetActive(false);
	}

	internal void Prepare(Mage mageBottom, Mage mageTop) {

		PanelMageBottom.GetComponent<PanelMage>().Prepare(mageBottom, this);
		if (PanelMageTop != null) {
			PanelMageTop.GetComponent<PanelMage>().Prepare(mageTop, this);
		}
		LeftCollider.center = new Vector3(360 * AspectRatioKeeper.ActualScale / 2, 0);
		LeftCollider.size = new Vector3(0, 600 * AspectRatioKeeper.ActualScale, 1);

		RightCollider.center = new Vector3(-360 * AspectRatioKeeper.ActualScale / 2, 0);
		RightCollider.size = new Vector3(0, 600 * AspectRatioKeeper.ActualScale);

		TopCollider.center = new Vector3(0, -AspectRatioKeeper.ActualScale / 2 * 600);
		TopCollider.size = new Vector3(360 * AspectRatioKeeper.ActualScale, 0);
		BottomCollider.center = new Vector3(0, AspectRatioKeeper.ActualScale / 2 * 600);
		BottomCollider.size = new Vector3(360 * AspectRatioKeeper.ActualScale, 0);
	}

	public void CastIt(Mage caster, Spell spell, Vector2 from, Vector2 direction) {
		Debug.Log("Casting spell " + spell.Name + ", by: " + caster.Name + ", he has " + caster.ActualMana + " mana , direction: " + direction);

		caster._ActualMana -= spell.Cost;
		BulletPrefab.SetActive(true);
		GameObject bulletTmp = Instantiate(BulletPrefab) as GameObject;
		bulletTmp.transform.parent = transform;
		bulletTmp.AddComponent<Bullet>().Prepare(caster, spell, from, direction);
		Bullets.Add(bulletTmp);
		BulletPrefab.SetActive(false);
	}
}


