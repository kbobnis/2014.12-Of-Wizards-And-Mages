using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PanelVial : MonoBehaviour
{

    public GameObject ImageVialReady, ImageVialSpent;
    private Mage Caster;
    private List<Vial> Vials;
    private bool WillUse, Used;

    internal void Prepare(Mage caster, List<Vial> vials) {
        Vials = vials;
        Caster = caster;
    }

    // Use this for initialization
    void Awake() {

        PointerUp();

        EventTrigger et = gameObject.AddComponent<EventTrigger>();
        et.delegates = new System.Collections.Generic.List<EventTrigger.Entry>();

        EventTrigger.TriggerEvent te = new EventTrigger.TriggerEvent();
        te.AddListener((eventData) => PointerDown());
        EventTrigger.Entry entry = new EventTrigger.Entry() { callback = te, eventID = EventTriggerType.PointerDown };
        et.delegates.Add(entry);

        EventTrigger.TriggerEvent te2 = new EventTrigger.TriggerEvent();
        te2.AddListener((eventData) => PointerMove());
        EventTrigger.Entry entry2 = new EventTrigger.Entry() { callback = te2, eventID = EventTriggerType.Drag };
        et.delegates.Add(entry2);

        EventTrigger.TriggerEvent te3 = new EventTrigger.TriggerEvent();
        te3.AddListener((eventData) => PointerUp());
        EventTrigger.Entry entry3 = new EventTrigger.Entry() { callback = te3, eventID = EventTriggerType.PointerUp };
        et.delegates.Add(entry3);
    }

    // Update is called once per frame
    void Update() {
        if (Caster != null) {
            ImageVialReady.SetActive(Caster.ActiveBonuses.Count > 0);
        }
    }

    void PointerDown() {
        Used = false;
        WillUse = true;
    //    StartingMousePos = Input.mousePosition;
        ImageVialReady.SetActive(true);
        ImageVialReady.GetComponent<Image>().color = Color.black;
        Vials = Caster.ActiveBonuses;
   //     PointerMove();

    }

    void PointerMove() {

    }

    void PointerUp() {

        try {
            if (Vials.Count == 0) {
                ImageVialReady.SetActive(false);
            }

            if (WillUse && !Used && Vials.Count > 0)
            {
                Caster.UseVial(Vials[0]);
                Vials.RemoveAt(0);
                Used = true;
                WillUse = false;
            }

        }
        catch (Exception e)
        {
            Debug.Log("Exception: " + e);
        }
    }
}