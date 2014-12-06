using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PanelOrb : MonoBehaviour {

	public GameObject ImageBack, ImageSpell;
	private Card _Card;

	internal Card Card{
		get {
			
			return _Card; 
		}
		set {
			_Card = value;
			ImageSpell.GetComponent<Image>().sprite = _Card==null ? null :_Card.Icon;
			ImageSpell.GetComponent<Image>().enabled = _Card != null;
		}
	}

}
