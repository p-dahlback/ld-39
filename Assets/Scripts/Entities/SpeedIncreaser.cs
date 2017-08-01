using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedIncreaser : MonoBehaviour {

	public float speedIncrease = 2f;
	public float baseEnergyConsumptionIncrease = 0.2f;
	public float fastEnergyConsumptionIncrease = 0.2f;
	public float slowEnergyConsumptionIncrease = 0.2f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider collider) {
		var player = collider.GetComponent<Player>();
		player.baseSpeed += speedIncrease;
		player.fastSpeed += speedIncrease;
		player.minSpeed += speedIncrease;
		player.targetSpeed += speedIncrease;

		player.energyExpenditureRate += baseEnergyConsumptionIncrease;
		player.energyFastExpenditureRate += fastEnergyConsumptionIncrease;
		player.energySlowExpenditureRate += slowEnergyConsumptionIncrease;
	}
}
