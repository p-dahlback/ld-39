﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogBox : MonoBehaviour {

	public Animator portraitAnimator;
	public Image portrait;
	public Text textBox;

	public AudioSource openSound;
	public AudioSource closeSound;
	public AudioSource talkSound;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
	public void CloseDialog() {
		StopCoroutine("DisplayTextAfterCommsOpened");
		StartCoroutine("CloseComms");
	}

	public void SetDialog(string dialog, bool damaged, float uptime) {
		textBox.text = dialog;
		textBox.gameObject.SetActive(false);
		portrait.gameObject.SetActive(true);
		portraitAnimator.SetBool("Start", true);
		portraitAnimator.SetBool("Damaged", damaged);
		portraitAnimator.SetBool("Close", false);
		StopCoroutine("CloseComms");
		StartCoroutine("DisplayTextAfterCommsOpened", uptime);
	}

	IEnumerator DisplayTextAfterCommsOpened(float uptime) {
		openSound.Play();
		var damaged = portraitAnimator.GetBool("Damaged");
		portraitAnimator.SetBool("Start", false);
		yield return new WaitForSeconds(0.3f);
		textBox.gameObject.SetActive(true);
		talkSound.pitch = damaged ? 1.1f : 1f;
		talkSound.Play();
		yield return new WaitForSeconds(uptime);
		portraitAnimator.SetBool("Close", true);
		closeSound.Play();
		yield return new WaitForSeconds(0.3f);
		portrait.gameObject.SetActive(false);
		textBox.gameObject.SetActive(false);
	}

	IEnumerator CloseComms() {
		if (portraitAnimator.isActiveAndEnabled) {
			portraitAnimator.SetBool("Start", false);
			portraitAnimator.SetBool("Close", true);
			closeSound.Play();
			yield return new WaitForSeconds(0.3f);
		}
		portrait.gameObject.SetActive(false);
		textBox.gameObject.SetActive(false);
	}
}
