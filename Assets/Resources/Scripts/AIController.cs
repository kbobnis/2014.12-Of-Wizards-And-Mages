using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class AiController : PlayerController, GameTickListener {

	Mage Mage;
	Ai Ai;
	PanelMage MyPanel, EnemysPanel;
	float LastShot;

	public override void PrepareFight(Mage mage, Ai ai, PanelMage myPanel, PanelMage enemy) {
		Mage = mage;
		Ai = ai;
		MyPanel = myPanel;
		EnemysPanel = enemy;
	}

	public void GameUpdate() {
		if (LastShot > Ai.FiringSpeed) {
			LastShot -= Ai.FiringSpeed;

			Vector3 enemyPos = EnemysPanel.GetComponent<PanelMage>().ImageAvatar.transform.position;
			Vector3 myPos = MyPanel.GetComponent<PanelMage>().ImageAvatar.transform.position;

			Vector3 direction = (enemyPos - myPos).normalized;

			Debug.Log("Shot, direction : " + direction);

			MyPanel.GetComponent<PanelMage>().ButtonSpellLeft.GetComponent<ButtonSpell>().Cast(direction);

		}
		LastShot += Time.deltaTime;
	}



}
