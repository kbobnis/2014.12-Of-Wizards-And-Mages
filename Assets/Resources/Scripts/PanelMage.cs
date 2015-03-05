using UnityEngine;
using System.Collections;

public class PanelMage : MonoBehaviour {

	public GameObject ButtonSpellLeft, ButtonSpellRight, PanelFlaskLife, PanelFlaskMana, ImageAvatar;
	Mage Mage;

	void Update () {
		PanelFlaskLife.GetComponent<PanelFlask>().UpdateValue(Mage.ActualHealth, Mage.MaxHealth);
		PanelFlaskMana.GetComponent<PanelFlask>().UpdateValue(Mage.ActualMana, Mage.MaxMana);
	}

	internal void Prepare(Mage caster, CastListener castListener) {
		Mage = caster;
		ButtonSpellLeft.GetComponent<ButtonSpell>().Prepare(caster, caster.LeftHand, castListener);
		ButtonSpellRight.GetComponent<ButtonSpell>().Prepare(caster, caster.RightHand, castListener);
	}
}
