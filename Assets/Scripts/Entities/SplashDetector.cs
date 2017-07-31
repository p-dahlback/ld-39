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

	void OnTriggerEnter(Collider collider) {
		Transform splash;

		if (collider.gameObject.layer == (int) Layers.Player) {
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
		position.z = collider.transform.position.z;
		splash.position = position;
	}
}

