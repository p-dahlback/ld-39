using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour {

	public float speedIncrease = 2f;

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
			player.targetSpeed = Mathf.Max(player.currentSpeed, player.baseSpeed) + speedIncrease;
			playerController.Bounce();
		}
	}
}
