using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ButtonDrag : MonoBehaviour {

	private Vector3 StartDrag;

	void Awake() {
		EventTrigger et = gameObject.AddComponent<EventTrigger>();
		EventTrigger.Entry entry = new EventTrigger.Entry();
		entry.eventID = EventTriggerType.Drag;
		entry.callback = new EventTrigger.TriggerEvent();
		entry.callback.AddListener(new UnityEngine.Events.UnityAction<BaseEventData>(Drag));
		
		et.delegates = new System.Collections.Generic.List<EventTrigger.Entry>();
		et.delegates.Add(entry);

		EventTrigger.Entry release = new EventTrigger.Entry();
		release.eventID = EventTriggerType.PointerUp;
		release.callback = new EventTrigger.TriggerEvent();
		release.callback.AddListener(new UnityEngine.Events.UnityAction<BaseEventData>(Release));

		et.delegates.Add(release);
	}

	private void Drag(BaseEventData bed) {
		Vector3 now = Input.mousePosition;
		GetComponent<RectTransform>().transform.position = now;
	}


	public void Release(BaseEventData bed) {
		GetComponent<RectTransform>().localPosition = new Vector3();
	}
}
