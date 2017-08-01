using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeHearts : MonoBehaviour {

	public Image[] hearts;
	public float flashTime = 1.0f;
	public int flashFrequency = 2;

	private float lastHealthValue;
	private int destroyedHealth = 0;
	private int flashingHealth = 0;

	private float time = 0f;

	// Use this for initialization
	void Start () {
		lastHealthValue = GameController.Instance.player.Health;
	}
	
	// Update is called once per frame
	void Update () {
		var player = GameController.Instance.player;
		if (player.IsDead) {
			for (int i = destroyedHealth; i < hearts.Length; i++) {
				hearts[i].enabled = false;
			}
			return;	
		}

		if (player.Health != lastHealthValue) {
			if (flashingHealth > 0) {
				time = flashTime;
				FlashDisappearingHearts();
			}

			flashingHealth = (int) (lastHealthValue - player.Health);
			lastHealthValue = player.Health;
			time = 0f;
			Debug.Log("Flashing Health: " + flashingHealth);
		}

		FlashDisappearingHearts();
	}

	private void FlashDisappearingHearts() {
		if (flashingHealth <= 0) {
			return;
		}

		var timeForSingleFlash = 1.0f / flashFrequency;
		time += Time.deltaTime;

		bool visible;
		if (time >= flashTime) {
			visible = false;
		} else  {
			visible = time % timeForSingleFlash >= timeForSingleFlash / 2f;
		}
		for (int i = 0; i < flashingHealth; i++) {
			hearts[i + destroyedHealth].enabled = visible;
		}

		if (time >= flashTime) {
			destroyedHealth += flashingHealth;
			flashingHealth = 0;
			Debug.Log("Destroyed Health: " + destroyedHealth);
		}
	}
}
