using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity {

	public float height = 0.2f;
	public float width = 0.5f;

	public float baseSpeed = 4;
	public float maxSpeed = 12;
	public float minSpeed = 2;

	public float targetSpeed;
	public float speedChangeFactor = 0.6f;
	public float currentSpeed;

	void Start() {
		targetSpeed = baseSpeed;
		currentSpeed = baseSpeed;
	}

	void Update() {
		var difference = targetSpeed - currentSpeed;
		if (Mathf.Abs(difference) > 0.1) {
			currentSpeed = currentSpeed + difference * speedChangeFactor * Time.deltaTime;
		} else {
			currentSpeed = targetSpeed;
			if (targetSpeed > baseSpeed) {
				targetSpeed = baseSpeed;
			}
		}

		GameController.Instance.currentLevel.Speed = currentSpeed;
	}
}
