using UnityEngine;
using System.Collections;

public class ShipSplash : MonoBehaviour
{
	public Transform contact;
	public float minimumDistanceToContact = 0.5f;
	public float targetYPosition = 0f;
	public float yPositionChangeRate = 2f;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (contact == null || !contact.gameObject.activeInHierarchy) {
			Debug.Log("Contact gone; destroying self");
			DestroySelf();
			return;
		}

		var contactPosition = contact.position;
		if (contactPosition.y > minimumDistanceToContact) {
			Debug.Log("Contact lifted; destroying self");
			DestroySelf();
			return;
		}

		var position = contactPosition;
		position.y = transform.position.y;
		transform.position = position;

		ApproachTargetYPosition();
	}

	private void ApproachTargetYPosition() {
		var difference = targetYPosition - transform.position.y;
		float newPos;
		if (Mathf.Abs(difference) > 0.01) {
			newPos = transform.position.y + difference * Time.deltaTime;
		} else {
			newPos = targetYPosition;
		}
		var position = transform.position;
		position.y = newPos;
		transform.position = position;
	}

	private void DestroySelf() {
		Destroy(gameObject);
	}
}

