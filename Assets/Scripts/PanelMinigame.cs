using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PanelMinigame : MonoBehaviour {

	public GameObject PanelMageAndOrbsTop, PanelMageAndOrbsBottom, PanelSpells;

	internal void Prepare(OrbState yourState) {

		PanelMageAndOrbsBottom.GetComponent<PanelMageAndOrbs>().Prepare(yourState);
		//PanelMageAndOrbsTop.GetComponent<PanelMageAndOrbs>().Prepare(opponentsState, opponentsDeck);

		//GetComponent<BgImage>().Sprite = minigameBackground.Sprite;
	}

}
