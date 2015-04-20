using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PanelVial : MonoBehaviour
{

    private Mage Caster;
	private List<Vial> VialList;
    private bool WillUse, Used;

    internal void Prepare(Mage caster, List<Vial> vialList) {
        Caster = caster;
		VialList = vialList;
		
    }

	void Update() {
		GetComponent<Image>().enabled = VialList.Count > 0;
		if (VialList.Count > 0) {
			GetComponent<Image>().sprite = VialList[0].Sprite;
		}
	}

	public void Use() {

		if (VialList.Count > 0) {
			Caster.UseVial(VialList[0]);
			Debug.Log("using vial: " + VialList[0].Name);
			VialList.RemoveAt(0);
		} else {
			Debug.Log("There are no vials");
		}
	}

}