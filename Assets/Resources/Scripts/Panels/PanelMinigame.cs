﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Resources.Scripts.AI;

public class PanelMinigame : MonoBehaviour, CastListener {

	public GameObject PanelMenu;
	public GameObject BulletPrefab, PanelMageBottom, PanelMageTop, PanelFounders;

	public BoxCollider TopCollider, BottomCollider, LeftCollider, RightCollider;

	public List<GameObject> Bullets = new List<GameObject>();
	public GameObject MageTop, MageBottom;
	public List<GameTickListener> GameTickListeners = new List<GameTickListener>();

	private MinigameParameters MinigameParameters;
    private Board board;

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

		foreach (GameTickListener gtl in GameTickListeners) {
			gtl.GameUpdate();
		}
		MinigameParameters.Update();
	}

	internal void Prepare(Player humanPlayer, Player enemyPlayer, MinigameParameters minigameParameters) {
		GameTickListeners.Clear();
		MinigameParameters = minigameParameters;

		PanelMageBottom.GetComponent<PanelMage>().Prepare(humanPlayer, this);
		PanelMageTop.GetComponent<PanelMage>().Prepare(enemyPlayer, this);
		
		LeftCollider.center = new Vector3(Game.Me.W / 2, 0);
		LeftCollider.size = new Vector3(0, Game.Me.H, 1);
	
		RightCollider.center = new Vector3(- Game.Me.W/ 2, 0);
		RightCollider.size = new Vector3(0, Game.Me.H);

		TopCollider.center = new Vector3(0, -Game.Me.H / 2);
		TopCollider.size = new Vector3(Game.Me.W, 0);
		BottomCollider.center = new Vector3(0, Game.Me.H/2);
		BottomCollider.size = new Vector3(Game.Me.W, 0);


        board = new Board(GameObject.FindWithTag("MagImage").GetComponent<Collider>(), GetComponent<RectTransform>(), null, null);
        humanPlayer.Mage.board = board;
        enemyPlayer.Mage.board = board;
		AiController aic = new AiController(board);
		aic.PrepareFight(enemyPlayer.Mage, enemyPlayer.Ai, PanelMageTop.GetComponent<PanelMage>(), PanelMageBottom.GetComponent<PanelMage>());
		GameTickListeners.Add(aic);
	}

	public void CastIt(Mage caster, Spell spell, Vector2 from, Vector2 direction) {
		caster._ActualMana -= spell.Cost;
		BulletPrefab.SetActive(true);
		GameObject bulletTmp = Instantiate(BulletPrefab) as GameObject;
		bulletTmp.transform.parent = transform;
		bulletTmp.AddComponent<Bullet>().Prepare(caster, spell, from, direction, board);
		Bullets.Add(bulletTmp);
		BulletPrefab.SetActive(false);
	}


	internal void SpawnBonus(Bonus bonus) {
		PanelFounders.GetComponent<PanelFounders>().Prepare(bonus);
	}
}


