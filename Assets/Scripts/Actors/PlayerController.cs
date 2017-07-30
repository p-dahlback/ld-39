using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : ActorController {

	public Player player;
	public Rigidbody body;

	public float horizontalSpeed = 2f;
	public float verticalSpeed = 2f;
	public float bounceSpeed = 2f;
	public float bounceHeightLimitIncrease = 0.2f;

	public float maxHorizontalSpeed = 2f;
	public float maxVerticalSpeed = 5f;

	private float heightLimitFactor = 1.0f;
	private float bounceHeightAllowance = 0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!player.IsDead) {
			Move();
			Act();
		}
		var level = GameController.Instance.currentLevel;
		ReduceHeightByEnergy();
		ForceWithinBounds(level.width, level.height);
	}

	protected override void Move() {
		var horizontalThrust = Input.GetAxis("Horizontal");
		var verticalThrust = Input.GetAxis("Vertical");

		var horizontalForce = horizontalSpeed * horizontalThrust * Time.deltaTime;
		var verticalForce = verticalSpeed * verticalThrust * Time.deltaTime;

		body.AddForce(horizontalForce, verticalForce, 0);
		ClampToMaxSpeed();
	}

	protected override void Act() {
		var button = "Jump";
		if (Input.GetButtonDown(button)) {
			player.targetSpeed = player.minSpeed;
		} else if (Input.GetButtonUp(button)) {
			if (player.targetSpeed == player.minSpeed) {
				player.targetSpeed = player.baseSpeed;
			}
		}
	}

	public override void Bounce() {
		var speed = player.IsDead ? bounceSpeed / 2f : bounceSpeed;
		body.AddForce(0, speed, 0);
		ClampToMaxSpeed();

		if (heightLimitFactor < bounceHeightLimitIncrease) {
			bounceHeightAllowance = bounceHeightLimitIncrease;
		}
	}

	private void ClampToMaxSpeed() {
		var velocity = body.velocity;
		if (Mathf.Abs(velocity.x) >= maxHorizontalSpeed) {
			velocity.x = maxHorizontalSpeed * Mathf.Sign(velocity.x);
		}
		if (Mathf.Abs(velocity.y) >= maxVerticalSpeed) {
			velocity.y = maxVerticalSpeed * Mathf.Sign(velocity.y);
		}
		body.velocity = velocity;
	}

	private void ReduceHeightByEnergy() {
		heightLimitFactor = (player.energy + 1) / player.maxEnergy;
		if (bounceHeightAllowance > 0) {
			heightLimitFactor += bounceHeightAllowance;
			bounceHeightAllowance -= player.energyExpenditureRate / player.maxEnergy * Time.deltaTime;
		}

		if (heightLimitFactor > 1.0f) {
			heightLimitFactor = 1.0f;
		}
	}

	private void ForceWithinBounds(float width, float height) {
		var halfWidth = width / 2f;
		var halfPlayerWidth = player.width / 2f;
		var halfPlayerHeight = player.height / 2f;
		var heightWithLimit = height * heightLimitFactor;
		var position = transform.position;
		if (position.x - halfPlayerWidth < -halfWidth) {
			position.x = -halfWidth + halfPlayerWidth;
		} else if (position.x + halfPlayerWidth > halfWidth) {
			position.x = halfWidth - halfPlayerWidth;
		}
		if (position.y - halfPlayerHeight < 0) {
			position.y = halfPlayerHeight;
		} else if (position.y + halfPlayerHeight > heightWithLimit) {
			position.y = heightWithLimit - halfPlayerHeight;
		}
		transform.position = position;
	}
}
