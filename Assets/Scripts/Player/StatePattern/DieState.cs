﻿using UnityEngine;
using System.Collections;

public class DieState : PlayerState {

	private Player player;
//	private Transform model;
	private Animator anim;
	private TrackMove trackMove;

	void Awake ()
	{
		player = GetComponent<Player> ();
//		model = transform.FindChild ("Model");
		anim = player.GetAnimator ();
		trackMove = GameObject.Find ("TrackController").GetComponent<TrackMove> ();
	}

	void OnEnable ()
	{
		anim.SetBool ("isDie", true);

		trackMove.enabled = false;

		StopAllCoroutines ();

		StartCoroutine ("RestartGame");

		InputController.instance.enabled = false;
	}

	void OnDisable ()
	{
		anim.SetBool ("isDie", false);

		InputController.instance.enabled = true;
	}

	IEnumerator RestartGame ()
	{
		yield return new WaitForSeconds (2f);

		InputController.instance.ClearInputs ();

		player.transform.position = Vector3.zero;

		player.ChangeState (States.Running);
	}
}
