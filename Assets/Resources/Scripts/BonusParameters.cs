using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class BonusParameters {
	private int MinNumberOfDraws;
	private int MaxNumberOfDraws;
	private int MinSecondsOfSpawn;
	private int MaxSecondsOfSpawn;
	private List<BonusConfig> BonusConfigs;

	private float NextSpawnTime;


	public BonusParameters(int minNumberOfDraws, int maxNumberOfDraws, int minSecondsOfSpawn, int maxSecondsOfSpawn, List<BonusConfig> bonusConfigs) {

		MinNumberOfDraws = minNumberOfDraws;
		MaxNumberOfDraws = maxNumberOfDraws;
		MinSecondsOfSpawn = minSecondsOfSpawn;
		MaxSecondsOfSpawn = maxSecondsOfSpawn;
		BonusConfigs = bonusConfigs;
		NextSpawnTime = UnityEngine.Random.Range(MinSecondsOfSpawn, MaxSecondsOfSpawn) + Time.time;
	}

	public void Update() {
		if (NextSpawnTime < Time.time) {
			DrawSpawns();
			NextSpawnTime += UnityEngine.Random.Range(MinSecondsOfSpawn, MaxSecondsOfSpawn) ;
		}
	}

	private void DrawSpawns() {
		int howManyToDraw = UnityEngine.Random.Range(MinNumberOfDraws, MaxNumberOfDraws+1);

		for (int i = 0; i < howManyToDraw; i++) {
			SpawnOneBonus();
		}
	}
	private void SpawnOneBonus() {
		float sumOfWeight = 0;
		foreach (BonusConfig bc in BonusConfigs) {
			sumOfWeight += bc.Weight;
		}

		float oneDraw = UnityEngine.Random.Range(0, sumOfWeight);

		foreach (BonusConfig bc in BonusConfigs) {
			if (bc.Weight > oneDraw) {
				if (bc.Bonus != null) {
					Game.Me.PanelMinigame.GetComponent<PanelMinigame>().SpawnBonus(bc.Bonus);
				}
				break;
			} else {
				oneDraw -= bc.Weight;
			}
		}
	}
}
