using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public delegate void UpdateHealth(int value);

public class PanelMageAndOrbs : MonoBehaviour {

	public GameObject MageOrb;

	internal void Prepare(OrbState mageState) {

		MageOrb.GetComponent<PanelOrb>().Prepare(mageState.Deck, mageState.Animations, mageState.Health, mageState.LeftOrbState, mageState.RightOrbState);
	}
}
