using UnityEngine;
using System.Collections;

public class PositionToTarget : MonoBehaviour
{
	public Vector3 targetPosition;
	public float positionChangeSpeed = 2.0f;
	public IPositionListener listener;

	private float speedMultiplier = 2.0f;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		var distance1 = Vector3.Distance(transform.position, targetPosition);
		var speed = distance1 <= 0.1f ? positionChangeSpeed * speedMultiplier : positionChangeSpeed;
		transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
		if (Vector3.Distance(transform.position, targetPosition) < 0.01f) {
			transform.position = targetPosition;
			if (listener != null) {
				listener.PositionChangeFinished(targetPosition);
			}	
			enabled = false;
		}
	}
}

