using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : ActorController {

	public Rigidbody body;
	public Transform dyingObject;
	public Transform deathReplacement;
	public float duration;
	public float spinSpeed = 4;
	public float bounceMultiplier = 10;

	private float time = 0;

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

		if (time >= duration) {
			var position = transform.position;
			Destroy(gameObject);
			if (deathReplacement != null) {
				var deathInstance = Instantiate(deathReplacement);
				deathInstance.position = position;
			}
		}
	}

	protected override void Move() {
		
	}

	protected override void Act() {
		
	}

	public override void Bounce() {
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
