using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class MageControl : MonoBehaviour {

	public GameObject ArrowUp, ArrowDown, ArrowLeft, ArrowRight;
	public List<MageControlListener> MageControlListeners = new List<MageControlListener>();

	private Vector3 StartDrag;
	private bool IsDown;
	private Vector3 Delta;
	private Side ActualSide;


	void Awake() {
		EventTrigger et = gameObject.AddComponent<EventTrigger>();
		et.delegates = new System.Collections.Generic.List<EventTrigger.Entry>();

		EventTrigger.Entry entry = new EventTrigger.Entry();
		entry.eventID = EventTriggerType.Drag;
		entry.callback = new EventTrigger.TriggerEvent();
		entry.callback.AddListener(new UnityEngine.Events.UnityAction<BaseEventData>(Drag));
		et.delegates.Add(entry);

		EventTrigger.Entry release = new EventTrigger.Entry();
		release.eventID = EventTriggerType.PointerUp;
		release.callback = new EventTrigger.TriggerEvent();
		release.callback.AddListener(new UnityEngine.Events.UnityAction<BaseEventData>(Release));
		et.delegates.Add(release);

		EventTrigger.Entry down = new EventTrigger.Entry();
		down.eventID = EventTriggerType.PointerDown;
		down.callback = new EventTrigger.TriggerEvent();
		down.callback.AddListener(new UnityEngine.Events.UnityAction<BaseEventData>(Down));
		et.delegates.Add(down);

		SetArrowsActive(false);
	}

	private void SetArrowsActive(bool set) {
		if (ArrowUp != null) {
			ArrowUp.SetActive(set);
			ArrowDown.SetActive(set);
			ArrowLeft.SetActive(set);
			ArrowRight.SetActive(set);
		}
	}


	private void HighlightArrow(Side s) {
		if (ArrowUp != null){
			ArrowUp.GetComponent<Text>().color = Color.white;
			ArrowDown.GetComponent<Text>().color = Color.white;
			ArrowLeft.GetComponent<Text>().color = Color.white;
			ArrowRight.GetComponent<Text>().color = Color.white;

			if (s != Side.None) {
				switch (s) {
					case Side.Left: ArrowLeft.GetComponent<Text>().color = Color.black; ArrowLeft.SetActive(true); break;
					case Side.Right: ArrowRight.GetComponent<Text>().color = Color.black; ArrowRight.SetActive(true); break;
					case Side.Up: ArrowUp.GetComponent<Text>().color = Color.black; ArrowUp.SetActive(true); break;
					case Side.Down: ArrowDown.GetComponent<Text>().color = Color.black; ArrowDown.SetActive(true); break;
				}
			}
		}

	}

	private void Down(BaseEventData bed) {
		StartDrag = Input.mousePosition;
		IsDown = true;
		SetArrowsActive(true);
		HighlightArrow(Side.None);
	}

	private void Drag(BaseEventData bed) {
		Vector3 now = Input.mousePosition;
		Delta = now - StartDrag;
		GetComponent<RectTransform>().transform.position = now;

		float distance = Mathf.Abs(Delta.x) > Mathf.Abs(Delta.y) ? Mathf.Abs(Delta.x) : Mathf.Abs(Delta.y);
		bool isFarAway = distance > Screen.width / 20;
		ActualSide = Side.None;
		if (Mathf.Abs(Delta.x) > Mathf.Abs(Delta.y)) {
			ActualSide = Delta.x > 0 ? Side.Right : Side.Left;
		} else {
			ActualSide = Delta.y > 0 ? Side.Up : Side.Down;
		}
		HighlightArrow(isFarAway? ActualSide : Side.None) ;
	}


	public void Release(BaseEventData bed) {
		GetComponent<RectTransform>().localPosition = new Vector3();
		IsDown = false;
		Delta.Set(0, 0, 0);
		SetArrowsActive(false);
		switch (ActualSide) {
			case Side.Left: 
			case Side.Right: {
				foreach(MageControlListener mcl in MageControlListeners){
					mcl.SwapSpell(ActualSide);
				}
				break;
			}
			case Side.Down: {
				foreach (MageControlListener mcl in MageControlListeners) {
					mcl.Drop();
				}
				break;
			}
		}
	}
}
