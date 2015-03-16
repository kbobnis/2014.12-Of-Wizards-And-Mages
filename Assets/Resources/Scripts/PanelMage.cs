using UnityEngine;
using System.Collections;

public class PanelMage : MonoBehaviour {

	public GameObject ButtonSpellLeft, ButtonSpellRight, PanelFlaskLife, PanelFlaskMana, ImageAvatar;
	private Mage _Mage;


	public Mage Mage {
		set { _Mage = value; Debug.Log("updating mage value in " + gameObject.name +" "+ value.Name); }
		get { return _Mage; }
	}

	void Update() {
		//Debug.Log("mage in: " + gameObject.name + " is : " + (Mage != null ? "not null" : "null"));
		if (  Mage != null) {
			Debug.Log("updating mage: " + Mage.ActualHealth);
			PanelFlaskLife.GetComponent<PanelFlask>().UpdateValue(Mage.ActualHealth, Mage.MaxHealth);
			PanelFlaskMana.GetComponent<PanelFlask>().UpdateValue(Mage.ActualMana, Mage.MaxMana);

			Mage.RegenerateMe(Time.deltaTime);
		}
	}

	internal void Prepare(Mage caster, CastListener castListener) {
		Mage = caster;

		ImageAvatar.GetComponent<SphereCollider>().radius = ImageAvatar.GetComponent<RectTransform>().GetSize().x/2;

		ButtonSpellLeft.GetComponent<ButtonSpell>().Prepare(caster, caster.LeftHand, castListener);
		ButtonSpellRight.GetComponent<ButtonSpell>().Prepare(caster, caster.RightHand, castListener);
	}

	internal void TakeDamage(int p) {
		Mage._ActualHealth -= p;

	}
}
