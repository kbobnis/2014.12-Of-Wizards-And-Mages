using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PanelMinigame : MonoBehaviour, CastListener {

	public GameObject BulletPrefab, PanelMageBottom, PanelMageTop;

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
		BoxCollider[] bcs = GetComponents<BoxCollider>();
		bcs[0].center = new Vector3(360 * AspectRatioKeeper.ActualScale / 2, 0);
		bcs[1].center = new Vector3(-360 * AspectRatioKeeper.ActualScale / 2, 0);
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


