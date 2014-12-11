using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class TouchMageController : MonoBehaviour {

	public GameObject ArrowUp, ArrowDown, ArrowLeft, ArrowRight, PanelOrbToControl;

	private Vector3 StartDrag;
	private bool IsDown;
	private Vector3 Delta;
	private Side ActualSide;
	private bool IsFarAway;


	void Awake() {

		SetArrowsActive(false);
	}

	void OnMouseDown() {
		Down();
	}

	void OnMouseUp() {
		Release();
	}

	void OnMouseDrag() {
		Drag();
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
			ArrowUp.GetComponent<SpriteRenderer>().color = Color.white;
			ArrowDown.GetComponent<SpriteRenderer>().color = Color.white;
			ArrowLeft.GetComponent<SpriteRenderer>().color = Color.white;
			ArrowRight.GetComponent<SpriteRenderer>().color = Color.white;

			if (s != Side.None) {
				switch (s) {
					case Side.Left: ArrowLeft.GetComponent<SpriteRenderer>().color = Color.black; ArrowLeft.SetActive(true); break;
					case Side.Right: ArrowRight.GetComponent<SpriteRenderer>().color = Color.black; ArrowRight.SetActive(true); break;
					case Side.Up: ArrowUp.GetComponent<SpriteRenderer>().color = Color.black; ArrowUp.SetActive(true); break;
					case Side.Down: ArrowDown.GetComponent<SpriteRenderer>().color = Color.black; ArrowDown.SetActive(true); break;
				}
			}
		}

	}

	private void Down(){
		StartDrag = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Debug.Log("Mouse position: " + Input.mousePosition + " " + StartDrag);
		IsDown = true;
		SetArrowsActive(true);
		HighlightArrow(Side.None);
	}

	private void Drag(){
		Vector3 now = Camera.main.ScreenToWorldPoint( Input.mousePosition );
		Delta = now - StartDrag;
		transform.position = now;

		float distance = Mathf.Abs(Delta.x) > Mathf.Abs(Delta.y) ? Mathf.Abs(Delta.x) : Mathf.Abs(Delta.y);
		IsFarAway = distance > 0.3f;
		ActualSide = Side.None;
		if (Mathf.Abs(Delta.x) > Mathf.Abs(Delta.y)) {
			ActualSide = Delta.x > 0 ? Side.Right : Side.Left;
		} else {
			ActualSide = Delta.y > 0 ? Side.Up : Side.Down;
		}
		HighlightArrow(IsFarAway ? ActualSide : Side.None);
		//if casting spell
		if (IsFarAway && ActualSide == Side.Up) {
			PanelOrbToControl.GetComponent<PanelOrb>().WantCast();
		} else {
			PanelOrbToControl.GetComponent<PanelOrb>().DontWantCast();
		}
	}


	public void Release() {
		transform.localPosition = new Vector3();
		GetComponent<CircleCollider2D>().enabled = false; //to not collide with the spell
		IsDown = false;
		
		SetArrowsActive(false);
		if (IsFarAway){
			switch (ActualSide) {
				case Side.Left: 
				case Side.Right: {
					PanelOrbToControl.GetComponent<PanelOrb>().SwapSpell(ActualSide);
					break;
				}
				case Side.Down: {
					PanelOrbToControl.GetComponent<PanelOrb>().Drop();
					break;
				}
				case Side.Up: {
					PanelOrbToControl.GetComponent<PanelOrb>().Cast(Delta);
					break;
				}
			}
		}
		Delta.Set(0, 0, 0);
		GetComponent<CircleCollider2D>().enabled = true;

	}
}
