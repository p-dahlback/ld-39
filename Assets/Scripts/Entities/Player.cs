using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity {

	public EngineExhaust[] exhausts;
	public Engine engine;
	public AudioSource fuelSound;
	public AudioSource damageSound;
	public AudioSource boostSound;

	public float minExhaustScale = 0.5f;
	public float maxExhaustScale = 2.0f;

	public float height = 0.2f;
	public float width = 0.5f;

	public float baseSpeed = 4;
	public float maxSpeed = 12;
	public float minSpeed = 2;
	public float fastSpeed = 8;
	public float speedModifier = 1.0f;

	public float targetSpeed;
	public float speedChangeFactor = 0.6f;
	public float currentSpeed;

	public float maxEnergy = 10f;
	public float energy;
	public float energyExpenditureRate = 1f;
	public float energySlowExpenditureRate = 1f;
	public float energyFastExpenditureRate = 1f;
	public bool expendEnergy = true;

	void Start() {
		targetSpeed = baseSpeed;
		currentSpeed = baseSpeed;
		energy = maxEnergy;
	}

	void Update() {
		if (GameController.Instance.GameState != GameState.InGame && !IsDead) {
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
		float expenditureRate;
		if (targetSpeed == minSpeed) {
			expenditureRate = energySlowExpenditureRate;
		} else if (targetSpeed == fastSpeed) {
			expenditureRate = energyFastExpenditureRate;
		} else {
			expenditureRate = energyExpenditureRate;
		}
		var energyBefore = energy;
		energy -= expenditureRate * Time.deltaTime;

		if (energy <= 0) {
			energy = 0;
			if (energyBefore > 0) {
				fuelSound.Play();
			}
		}
	}

	private void ApproachTargetSpeed() {
		targetSpeed = Mathf.Clamp(targetSpeed, minSpeed, maxSpeed);
		RegulateExhaustSize();

		var difference = targetSpeed * speedModifier - currentSpeed;
		if (Mathf.Abs(difference) > 0.1) {
			currentSpeed = currentSpeed + difference * speedChangeFactor * Time.deltaTime;
		} else {
			currentSpeed = targetSpeed * speedModifier;
			if (targetSpeed > baseSpeed) {
				targetSpeed = baseSpeed;
			}
		}
	}

	private void RegulateExhaustSize() {
		var scale = 1.0f;
		if (targetSpeed > baseSpeed) {
			scale = maxExhaustScale;
			engine.targetPitch = energy > 0 ? engine.fastEnginePitch : engine.standardEnginePitch;
		} else if (targetSpeed < baseSpeed) {
			scale = minExhaustScale;
			engine.targetPitch = energy > 0 ? engine.slowEnginePitch : engine.slowEnginePitch * 0.8f;
		} else {
			engine.targetPitch = energy > 0 ? engine.standardEnginePitch : engine.slowEnginePitch;
		}

		foreach (var engine in exhausts) {
			engine.targetScale = scale;
		}
	}

	protected override void OnDamage() {
		GameController.Instance.PlayerTookDamage();
		damageSound.Play();
	}

	protected override void OnDeath() {
		foreach (var engine in exhausts) {
			engine.gameObject.SetActive(false);
		}
		GameController.Instance.PlayerDied();
	}
}
