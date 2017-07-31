using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipLean : MonoBehaviour {

	public Axis axis;
	public float targetRotation;
	public float rotationChangeFactor = 0.6f;
	public float speedModifier = 1.0f;

	private float rotation = 0f;

	// Use this for initialization
	void Start () {
		targetRotation = 0f;
		rotation = GetShipRotation();
	}
	
	// Update is called once per frame
	void Update () {
		ApproachTargetRotation();
	}

	private void ApproachTargetRotation() {
		var difference = targetRotation - rotation;
		if (Mathf.Abs(difference) > 0.1) {
			var rotationValue = rotation + difference * rotationChangeFactor * speedModifier * Time.deltaTime;
			SetShipRotation(rotationValue);
		} else {
			SetShipRotation(targetRotation);
		}
	}

	private float GetShipRotation() {
		switch (axis) {
		case Axis.X:
			return transform.localRotation.x;
		case Axis.Y:
			return transform.localRotation.y;
		case Axis.Z:
			return transform.localRotation.z;
		}
		return transform.localRotation.x;
	}

	private void SetShipRotation(float rotation) {
		this.rotation = rotation;
		switch (axis) {
		case Axis.X:
			transform.localRotation = Quaternion.Euler(rotation, transform.rotation.y, transform.rotation.z);
			break;
		case Axis.Y:
			transform.localRotation = Quaternion.Euler(transform.rotation.x, rotation, transform.rotation.z);
			break;
		case Axis.Z:
			transform.localRotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, rotation);
			break;
		}
	}
}
