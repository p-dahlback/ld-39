﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : ActorController {

	public Rigidbody body;
	public Transform dyingObject;
	public Transform deathReplacement;
	public AudioSource deathNoise;

	public float duration;
	public float spinSpeed = 4;
	public float bounceMultiplier = 10;
	public float replacementSpawnTimeOffset = 0.0f;
	public float deathNoiseTime = 0.2f;

	private float time = 0;
	private float deathNoiseTimer = 0;
	private bool generatedReplacement = false;

	void OnEnable () {
		time = 0;
		RandomSpin(1.0f);
		body.useGravity = true;

		if (dyingObject != null) {
			var dyingInstance = Instantiate(dyingObject, transform);
			dyingInstance.localPosition = Vector3.zero;
		}
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		deathNoiseTimer += Time.deltaTime;

		if (deathNoiseTimer >= deathNoiseTime) {
			deathNoiseTimer %= deathNoiseTime;
			deathNoise.Stop();
			deathNoise.Play();
		}

		if (!generatedReplacement && deathReplacement != null && time >= duration - replacementSpawnTimeOffset) {
			generatedReplacement = true;
			var position = transform.position;
			var deathInstance = Instantiate(deathReplacement);
			deathInstance.position = position;
		}
		if (time >= duration) {
			GameController.Instance.GameState = GameState.GameOver;
			Destroy(gameObject);
		}
	}

	protected override void Move() {
		
	}

	protected override void Act() {
		
	}

	public override void Bounce(Transform origin) {
		RandomSpin(bounceMultiplier);
	}

	private void RandomSpin(float modifier) {
		float x = Random.Range(-spinSpeed, spinSpeed) * modifier;
		float y = Random.Range(-spinSpeed, spinSpeed) * modifier;
		float z = Random.Range(-spinSpeed, spinSpeed) * modifier;

		body.freezeRotation = false;
		body.AddTorque(x, y, z);
	}
}
