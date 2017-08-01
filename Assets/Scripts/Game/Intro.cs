using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour {

	public DialogBox dialogBox;
	public StartScreenController startScreen;
	public ShipShake shipShake;
	public AudioSource explosion;

	void OnEnable() {
		StartCoroutine("RunCutscene");
	}

	void OnDisable() {
		StopCoroutine("RunCutscene");
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StopCutscene() {
		StopCoroutine("RunCutscene");
	}

	IEnumerator RunCutscene() {
		dialogBox.SetDialog("um, systems check...", false, 2.0f);
		yield return new WaitForSeconds(3.0f);
		dialogBox.SetDialog("all hunky dory!", false, 2.0f);
		yield return new WaitForSeconds(3.0f);
		dialogBox.SetDialog("taking off! the galaxy needs us!", false, 2.0f);
		startScreen.AssumePosition();
		yield return new WaitForSeconds(3.0f);
		dialogBox.SetDialog("doctor llama will regret messing with staraway!", true, 4.0f);
		yield return new WaitForSeconds(3.0f);
		explosion.Play();
		shipShake.enabled = true;
		yield return new WaitForSeconds(2.0f);
		dialogBox.SetDialog("um?", false, 2.0f);
		yield return new WaitForSeconds(3.0f);
		dialogBox.SetDialog("why is that gauge dropping??", true, 4.0f);
		yield return new WaitForSeconds(5.0f);
		startScreen.State = StartScreenState.End;
	}
}
