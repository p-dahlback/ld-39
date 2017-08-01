using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : Booster {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	protected override void BoostPlayer(Player player, PlayerController playerController) {
		GameController.Instance.currentLevel.height = 20;
		GameController.Instance.GameState = GameState.Finished;
		playerController.body.AddForce(Vector3.up * 2000);
		player.currentSpeed = 0f;
		player.targetSpeed = 0f;
		player.baseSpeed = 0f;
		GameController.Instance.currentLevel.Speed = 0f;
	}
}
