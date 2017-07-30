using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
	public Spawn[] spawns;
	public Transform spawnContainer;
	public int spawnLimit = -1;

	private Queue<Spawn> spawnQueue;
	private int spawnedObjectCount = 0;

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
		SpawnUntilDistance(distance);
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
		foreach (Spawn spawn in spawns) {
			spawn.spawnDistance += distance;
			spawnQueue.Enqueue(spawn);
		}
	}
}

