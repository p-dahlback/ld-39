using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider collider) {
		var entity = collider.GetComponent<Entity>();
		if (entity != null) {
			Debug.Log("Hit Obstacle!");
			entity.Damage(1);
		}
	}
}
