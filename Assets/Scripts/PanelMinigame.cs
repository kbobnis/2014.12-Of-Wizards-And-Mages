using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PanelMinigame : MonoBehaviour {

	public GameObject PanelMageAndOrbsTop, PanelMageAndOrbsBottom, PanelSpells;

	internal void Prepare(OrbState yourState, OrbState opponent) {

		PanelMageAndOrbsBottom.GetComponent<PanelMageAndOrbs>().Prepare(yourState);
		PanelMageAndOrbsTop.GetComponent<PanelMageAndOrbs>().Prepare(opponent);

		//GetComponent<BgImage>().Sprite = minigameBackground.Sprite;
	}

}
