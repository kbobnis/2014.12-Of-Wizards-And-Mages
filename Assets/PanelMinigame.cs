using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PanelMinigame : MonoBehaviour {

	public GameObject PanelMageAndOrbsTop, PanelMageAndOrbsBottom;

	private List<GameObject> ElementsOnBoard = new List<GameObject>();

	internal void Prepare(MageState yourState, Deck yourDeck, MageState opponentsState, Deck opponentsDeck, MinigameBackground minigameBackground) {

		PanelMageAndOrbsBottom.GetComponent<PanelMageAndOrbs>().Prepare(yourState, yourDeck);
		PanelMageAndOrbsTop.GetComponent<PanelMageAndOrbs>().Prepare(opponentsState, opponentsDeck);

		//GetComponent<BgImage>().Sprite = minigameBackground.Sprite;
	}

	internal void AddElementOnBoard(GameObject spell) {
		ElementsOnBoard.Add(spell);
		spell.transform.parent = transform;
	}
}
