using UnityEngine;
using System.Collections;

public class PanelMinigame : MonoBehaviour {

	public GameObject PanelMageAndOrbsTop, PanelMageAndOrbsBottom;

	internal void Prepare(MageState yourState, Deck yourDeck, MageState opponentsState, Deck opponentsDeck, MinigameBackground minigameBackground) {

		PanelMageAndOrbsBottom.GetComponent<PanelMageAndOrbs>().Prepare(yourState, yourDeck);
		PanelMageAndOrbsTop.GetComponent<PanelMageAndOrbs>().Prepare(opponentsState, opponentsDeck);

		GetComponent<BgImage>().Sprite = minigameBackground.Sprite;
	}
}
