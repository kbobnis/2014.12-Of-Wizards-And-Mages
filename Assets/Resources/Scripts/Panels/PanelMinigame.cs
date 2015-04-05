using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PanelMinigame : MonoBehaviour, CastListener {

	public GameObject PanelMenu;
	public GameObject BulletPrefab, PanelMageBottom, PanelMageTop;

	public BoxCollider TopCollider, BottomCollider, LeftCollider, RightCollider;

	public List<GameObject> Bullets = new List<GameObject>();
	public GameObject MageTop, MageBottom;

	void Awake() {
		BulletPrefab.SetActive(false);
	}

	void Update() {

		Player winner = MageTop.GetComponent<PanelMage>().Player;
		Player loser = MageBottom.GetComponent<PanelMage>().Player;
		bool endGame = false;


		if (MageBottom.GetComponent<PanelMage>().Player != null && MageBottom.GetComponent<PanelMage>().Player.Mage.IsDead()) {
			endGame = true;
		}
		if (MageTop.GetComponent<PanelMage>().Player != null && MageTop.GetComponent<PanelMage>().Player.Mage.IsDead()) {
			endGame = true;
			Player tmp = winner;
			winner = loser;
			loser = tmp;
		}

		if (endGame) {
			PanelMenu.SetActive(true);
			PanelMenu.GetComponent<PanelMenu>().SetWinnerAndLoser(winner, loser);
			gameObject.SetActive(false);
		}
	}

	internal void Prepare(Player humanPlayer, Player enemyPlayer) {

		PanelMageBottom.GetComponent<PanelMage>().Prepare(humanPlayer, this);
		PanelMageTop.GetComponent<PanelMage>().Prepare(enemyPlayer, this);
		
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
		caster._ActualMana -= spell.Cost;
		BulletPrefab.SetActive(true);
		GameObject bulletTmp = Instantiate(BulletPrefab) as GameObject;
		bulletTmp.transform.parent = transform;
		bulletTmp.AddComponent<Bullet>().Prepare(caster, spell, from, direction);
		Bullets.Add(bulletTmp);
		BulletPrefab.SetActive(false);
	}
}


