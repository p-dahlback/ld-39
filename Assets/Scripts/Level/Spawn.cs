using UnityEngine;
using System.Collections;

[System.Serializable]
public class Spawn
{
	public Transform[] spawnPrefabs;
	public float spawnDistance;

	public Transform GetPrefabToSpawn() {
		var index = Random.Range(0, spawnPrefabs.Length - 1);
		return spawnPrefabs[index];
	}
}

