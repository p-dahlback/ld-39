using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipShake : MonoBehaviour {

	public float duration = 0.5f;
	public float changeTime = 0.05f;
	public float speed = 5f;
	public float rotationChange = 10f;

	private float targetRotation;
	private float time;
	private float changeTimer;
	private float rotation;

	void OnEnable() {
		time = 0f;
		targetRotation  = rotationChange;
		rotation = transform.localRotation.z;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		if (time >= duration) {
			if (Mathf.Abs(rotation) <= 0.1f) {
				enabled = false;
				transform.rotation = Quaternion.identity;
				return;
			}
			targetRotation = 0f;
		}

		changeTimer += Time.deltaTime;
		if (changeTimer >= changeTime) {
			targetRotation = -targetRotation;
			changeTimer %= changeTime;
		}
			
		var angle = Mathf.LerpAngle (rotation, targetRotation, speed * Time.deltaTime);
		transform.rotation = Quaternion.Euler(0, 0, angle);
		rotation = angle;
	}
}
