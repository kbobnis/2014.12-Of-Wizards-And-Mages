using UnityEngine;
using System.Collections;

public class PanelMage : MonoBehaviour {

	public GameObject ButtonSpellLeft, ButtonSpellRight, PanelFlaskLife, PanelFlaskMana, ImageAvatar, PanelShield, ButtonRightBonus, ButtonLeftBonus;
	private Player _Player;

	public Player Player {
		set { _Player = value; }
		get { return _Player; }
	}

	void Update() {
        
        Vector3 pos = transform.position;
        Vector2 pos2 = transform.position;


        Vector3 posWorld = transform.TransformPoint( transform.position );
        Vector2 pos2World = transform.TransformPoint(transform.position);

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

		if (ButtonLeftBonus != null) {
			ButtonLeftBonus.GetComponent<PanelVial>().Prepare(_Player.Mage, _Player.Mage.LeftVials);
		}
		if (ButtonRightBonus != null) {
			ButtonRightBonus.GetComponent<PanelVial>().Prepare(_Player.Mage, _Player.Mage.RightVials);
		}
	}

	internal void TakeDamage(int p) {
        Debug.Log(Player.Mage.board.currentSlice + " currentTimeInBoard");
		Player.Mage._ActualHealth -= p;

	}
}
