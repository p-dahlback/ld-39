using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashableParticles : MonoBehaviour {

	public ParticleSystem particleSystem;
	public SplashDetector splashDetector;

	private List<ParticleCollisionEvent> collisionEvents;

	// Use this for initialization
	void Start () {
		collisionEvents = new List<ParticleCollisionEvent>();
		splashDetector = GameController.Instance.currentLevel.GetComponentInChildren<SplashDetector>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnParticleCollision(GameObject other) {
		int numCollisionEvents = particleSystem.GetCollisionEvents(other, collisionEvents);
		foreach (var collision in collisionEvents) {
			splashDetector.FallSplash(collision.intersection);
		}
	}
}
