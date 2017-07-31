using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
	public Spawn[] spawns;
	public Spawn goal;
	public Transform spawnContainer;
	public int spawnLimit = -1;
	public int resetsBeforeGoal = 3;

	private Queue<Spawn> spawnQueue;
	private int spawnedObjectCount = 0;
	private int resetCount = 0;

	private float distanceSinceReset = 0;

	// Use this for initialization
	void Start ()
	{
		Reset();
	}

	void Update() {
		if (spawnQueue.Count == 0 || spawnLimit >= 0 && spawnedObjectCount >= spawnLimit) {
			return;
		}

		var distance = GameController.Instance.CurrentDistance;
		if (GameController.Instance.GameState == GameState.InGame) {
			SpawnUntilDistance(distance);
		}
	}

	public void SpawnUntilDistance(float distance) {
		float distanceChecked = 0;
		do {
			var spawn = spawnQueue.Peek();
			if (spawn.spawnDistance <= distance) {
				spawnQueue.Dequeue();
				var prefab = spawn.GetPrefabToSpawn();
				var instance = Instantiate(prefab, spawnContainer);
				var approacher = instance.GetComponent<ApproacherController>();
				instance.position = new Vector3(instance.position.x, -approacher.height, transform.position.z);

				spawnedObjectCount++;
			}
			distanceChecked = spawn.spawnDistance;
		} while(distanceChecked < distance && spawnQueue.Count > 0);

		if (spawnQueue.Count == 0) {
			Reset();
		}
	}

	public void Reset() {
		var distance = GameController.Instance.CurrentDistance;
		spawnQueue = new Queue<Spawn>();
		GameController.Instance.SpeedIncrease(resetCount);
		resetCount += 1;
		Debug.Log("Reset spawner!" + resetCount);
		if (resetCount >= resetsBeforeGoal) {
			goal.spawnDistance = distance + 20f;
			spawnedObjectCount = 0;
			spawnLimit = 1;
			spawnQueue.Enqueue(goal);
		} else {
			var additionalDistance = distance > 0f ? 20f - distanceSinceReset : 0f;
			distanceSinceReset = distance;
			var spawnCount = 0;
			foreach (Spawn spawn in spawns) {
				spawn.spawnDistance += distance + additionalDistance;
				spawnQueue.Enqueue(spawn);
				spawnCount++;
			}
		}
	}
}

