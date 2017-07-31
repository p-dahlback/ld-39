using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour {

	public SplashDetector splashDetector;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
	void OnTriggerEnter(Collider collider) {
		var entity = collider.GetComponent<Entity>();
		if (entity != null) {
			Debug.Log("Hit Ground!");
			entity.Damage(1);
		}

		var controllers = collider.GetComponents<ActorController>();
		if (controllers != null) {
			foreach (var controller in controllers) {
				if (controller.isActiveAndEnabled) {
					controller.Bounce();
				}
			}
		}
		splashDetector.FallSplash(collider.transform.position);
	}
}
