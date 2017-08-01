using UnityEngine;
using System.Collections;

public class ShipSplash : MonoBehaviour
{
	public Transform contact;
	public AudioSource sound;
	public float minimumDistanceToContact = 0.5f;
	public float targetYPosition = 0f;
	public float yPositionChangeRate = 2f;
	public float soundTime = 0.25f;

	private Entity entity;

	private float time = 0f;

	// Use this for initialization
	void Start ()
	{
		entity = contact.GetComponent<Entity>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (contact == null || !contact.gameObject.activeInHierarchy) {
			DestroySelf();
			return;
		}
		if (entity != null && entity.IsDead) {
			DestroySelf();
			return;
		}

		var contactPosition = contact.position;
		if (contactPosition.y > minimumDistanceToContact) {
			DestroySelf();
			return;
		}

		time += Time.deltaTime;
		if (time >= soundTime && GameController.Instance.GameState != GameState.Start) {
			time %= soundTime;
			sound.Stop();
			sound.Play();
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

