using UnityEngine;
using System.Collections;

public class SplashDetector : MonoBehaviour
{
	public Transform shipSplashPrefab;
	public Transform fallSplashPrefab;
	public Transform splashContainer;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void FallSplash(Collider collider) {
		var splash = Instantiate(fallSplashPrefab, splashContainer);
		var position = splash.position;
		position.x = collider.transform.position.x;
		position.z = collider.transform.position.z - 0.5f;
		splash.position = position;
	}

	void OnTriggerEnter(Collider collider) {
		Transform splash;

		var player = collider.GetComponent<Player>();
		if (player != null) {
			if (player.IsDead) {
				return;
			}
			splash = Instantiate(shipSplashPrefab, splashContainer);
			var shipSplash = splash.GetComponent<ShipSplash>();
			shipSplash.contact = collider.transform;
			Debug.Log("Ship splash!");
		} else {
			splash = Instantiate(fallSplashPrefab, splashContainer);
			Debug.Log("Other splash!");
		}
		var position = splash.position;
		position.x = collider.transform.position.x;
		position.z = collider.transform.position.z - 0.5f;
		splash.position = position;
	}
}

