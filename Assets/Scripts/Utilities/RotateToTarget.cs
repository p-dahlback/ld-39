using UnityEngine;
using System.Collections;

public class RotateToTarget : MonoBehaviour
{
	public Quaternion targetRotation;
	public float rotationChangeSpeed = 2.0f;
	public IRotationListener listener;

	private float rotation;

	void OnEnable() {
		rotation = transform.rotation.y;
	}

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		var angle = Mathf.LerpAngle (rotation, targetRotation.y, rotationChangeSpeed * Time.deltaTime);
		transform.rotation = Quaternion.Euler(0, angle, 0);
		var difference = Mathf.DeltaAngle (angle, targetRotation.y);
		if (difference <= 0.5f) {
			transform.rotation = targetRotation;
			if (listener != null) {
				listener.RotationChangeFinished (transform.rotation);
			}
			enabled = false;
		}
		rotation = angle;
	}
}

