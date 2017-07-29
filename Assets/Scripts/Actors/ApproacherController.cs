using UnityEngine;
using System.Collections;

public class ApproacherController : ActorController
{
	public float yPositionAtHorizon = 0;
	public float height;

	private float zSpawnPosition;
	private float spawnDistance;

	// Use this for initialization
	void Start ()
	{
		zSpawnPosition = transform.position.z;
		spawnDistance = GameController.Instance.CurrentDistance + zSpawnPosition;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (DestroyIfPassed()) {
			return;
		}
		Move();
	}

	protected override void Move() {
		var level = GameController.Instance.currentLevel;
		ApproachCamera();
		ApproachHorizonForLevel(level);
	}

	protected override void Act() {
		// Do nothing
	}

	public override void Bounce() {
		// Do nothing
	}

	private bool DestroyIfPassed() {
		if (transform.position.z < -2) {
			Destroy(gameObject);
			return true;
		}
		return false;
	}

	private void ApproachCamera() {
		var currentDistance = GameController.Instance.CurrentDistance;
		var difference = spawnDistance - currentDistance;
		var position = transform.position;
		position.z = difference;

		transform.position = position;
	}

	private void ApproachHorizonForLevel(Level level) {
		var position = transform.position;
		if (position.z > level.horizonPosition) {
			position.y = GetYPositionAtDistance(position.z, level.horizonPosition);
		} else {
			position.y = yPositionAtHorizon;
		}
		transform.position = position;
	}

	private float GetYPositionAtDistance(float distance, float horizonPosition) {
		var fullDistanceFromHorizon = zSpawnPosition - horizonPosition;
		var currentDistanceFromHorizon = distance - horizonPosition;
		var shareOfDistanceTraversed = 1.0 - currentDistanceFromHorizon / fullDistanceFromHorizon;
		return -height + Mathf.Sin((float) shareOfDistanceTraversed * Mathf.PI / 2) * (yPositionAtHorizon + height);
	}
}

