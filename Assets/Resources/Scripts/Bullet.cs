﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.Resources.Scripts.AI;

public class Bullet : MonoBehaviour {

	private Mage Caster;
	Vector2 Direction;
	Spell Spell;
	bool Bounced = false;

	void Update() {
        Vector3 v3 = GetComponent<Collider>().bounds.size;
		Vector2 oldPos = transform.position;
		oldPos.x += Direction.x * Time.deltaTime * 5;
		oldPos.y += Direction.y * Time.deltaTime * 5;
		transform.position = oldPos;
	}

	internal void Prepare(Mage caster, Spell spell, Vector2 from, Vector2 direction, Board board) {
		Caster = caster;
		Spell = spell;
		Direction = direction * spell.FlyingParams[FlyingParam.Speed] * AspectRatioKeeper.ActualScale;
		GetComponent<Image>().sprite = Resources.Load<Sprite>("GUI/" + spell.Name);
		
		float w = Screen.width * AspectRatioKeeper.ActualScale * spell.FlyingParams[FlyingParam.Size] / 100f;
		float h = Screen.height * AspectRatioKeeper.ActualScale * spell.FlyingParams[FlyingParam.Size] / 100f;

		GetComponent<RectTransform>().offsetMin = new Vector2(-w / 2, -h / 2);
		GetComponent<RectTransform>().offsetMax = new Vector2(w / 2, h / 2);
		transform.position = from;

		GetComponent<SphereCollider>().radius = w/2;

        if (!caster.virtualplayer)
        {
            Vector3[] corners = new Vector3[4];
            GetComponent<RectTransform>().GetWorldCorners(corners);
            board.addBullet(from, corners, Direction);
        }
        
        

	}

	void OnTriggerEnter(Collider other) {

		PanelMage pm = other.gameObject.transform.parent.GetComponent<PanelMage>();
		if (pm != null) {
			pm.TakeDamage(Spell.AfterHitParams[AfterHitParam.Damage]);
			BlowUp();
			
		}
		PanelMinigame pmini = other.gameObject.GetComponent<PanelMinigame>();
		if (pmini != null) {

			if (pmini.LeftCollider == other || pmini.RightCollider == other) {
				Direction.x = -Direction.x;
				if (Bounced) {
					BlowUp();
				}
				Bounced = true;
			}

			if (pmini.TopCollider == other || pmini.BottomCollider == other) {
				BlowUp();
			}
		}
		Bullet b = other.GetComponent<Bullet>();
		if (b != null) {
			BlowUp();
			b.BlowUp();
		}

		PanelFounder pf = other.transform.parent.GetComponent<PanelFounder>();
		if (pf != null) {
			BlowUp();
			Debug.Log("bonus found");
			pf.TakeBy(Caster);
		}
	}

	public void BlowUp() {
		gameObject.AddComponent<BlowUp>().Prepare(Spell);
	}
}
