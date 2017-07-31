using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour {

	public float speedIncrease = 2f;
	public float energyIncrease = 6f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.layer == (int) Layers.Player) {
			var player = collider.GetComponent<Player>();
			var playerController = collider.GetComponent<PlayerController>();
			BoostPlayer(player, playerController);
			if (!player.IsDead) {
				AddEnergyToPlayer(player);
			}
		}
	}

	protected virtual void AddEnergyToPlayer(Player player) {
		player.energy += Mathf.Min(player.energy + energyIncrease, player.maxEnergy);
		if (player.energy > player.maxEnergy) {
			player.energy = player.maxEnergy;
		}
	}

	protected virtual void BoostPlayer(Player player, PlayerController playerController) {
		player.targetSpeed = Mathf.Max(player.currentSpeed, player.baseSpeed) + speedIncrease;
		player.boostSound.Play();
		playerController.Bounce(transform);
	}
}
