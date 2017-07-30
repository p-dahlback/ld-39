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

	public float maxEnergy = 10f;
	public float energy;
	public float energyExpenditureRate = 1f;
	public bool expendEnergy = true;

	void Start() {
		targetSpeed = baseSpeed;
		currentSpeed = baseSpeed;
		energy = maxEnergy;
	}

	void Update() {
		if (GameController.Instance.GameState != GameState.InGame) {
			return;
		}

		ExpendEnergy();
		ApproachTargetSpeed();
		GameController.Instance.currentLevel.Speed = currentSpeed;
	}

	private void ExpendEnergy() {
		if (!expendEnergy) {
			return;
		}

		if (energy > maxEnergy) {
			energy = maxEnergy;
		}
		energy -= energyExpenditureRate * Time.deltaTime;

		if (energy < 0) {
			energy = 0;
		}
	}

	private void ApproachTargetSpeed() {
		targetSpeed = Mathf.Clamp(targetSpeed, minSpeed, maxSpeed);

		var difference = targetSpeed - currentSpeed;
		if (Mathf.Abs(difference) > 0.1) {
			currentSpeed = currentSpeed + difference * speedChangeFactor * Time.deltaTime;
		} else {
			currentSpeed = targetSpeed;
			if (targetSpeed > baseSpeed) {
				targetSpeed = baseSpeed;
			}
		}

	}
}
