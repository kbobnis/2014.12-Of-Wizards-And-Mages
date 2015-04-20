using UnityEngine;
using System.Collections;

public class PanelMage : MonoBehaviour {

	public GameObject ButtonSpellLeft, ButtonSpellRight, PanelFlaskLife, PanelFlaskMana, ImageAvatar, PanelShield, VialsList;
	private Player _Player;

	public Player Player {
		set { _Player = value; }
		get { return _Player; }
	}

	void Update() {
		if (  Player != null) {
			PanelFlaskLife.GetComponent<PanelFlask>().UpdateValue((int)Player.Mage.ActualHealth, Player.Mage.MaxHealth);
			PanelFlaskMana.GetComponent<PanelFlask>().UpdateValue((int)Player.Mage.ActualMana, Player.Mage.MaxMana);
			Player.Mage.Update(Time.deltaTime);
		}
	}
	internal void Prepare(Player caster, CastListener castListener) {
		Player = caster;
		ImageAvatar.GetComponent<SphereCollider>().radius = ImageAvatar.GetComponent<RectTransform>().GetSize().x/2;
		ButtonSpellLeft.GetComponent<ButtonSpell>().Prepare(caster.Mage, caster.Mage.LeftHand, castListener);
		ButtonSpellRight.GetComponent<ButtonSpell>().Prepare(caster.Mage, caster.Mage.RightHand, castListener);
		if (PanelShield != null) {
			PanelShield sc = PanelShield.GetComponent<PanelShield>();
			sc.Prepare(_Player.Mage.Shield, _Player.Mage);
		}
        if (VialsList != null) {
            VialsList.GetComponent<PanelVial>().Prepare(caster.Mage, caster.Mage.ActiveBonuses);
        }
	}

	internal void TakeDamage(int p) {
		Player.Mage._ActualHealth -= p;

	}
}
