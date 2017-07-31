using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : ActorController {

	public Player player;
	public Rigidbody body;

	public ShipLean sideLean;
	public ShipLean frontLean;

	public float sideRotationDeadZone = 0.5f;
	public float sideRotation = 30f;
	public float frontRotation = 10f;

	public float horizontalSpeed = 2f;
	public float verticalSpeed = 2f;
	public float bounceSpeed = 2f;
	public float bounceHeightLimitIncrease = 0.2f;

	public float maxHorizontalSpeed = 2f;
	public float maxVerticalSpeed = 5f;

	public bool allowVerticalMovement = false;

	private float heightLimitFactor = 1.0f;
	private float bounceHeightAllowance = 0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!player.IsDead && GameController.Instance.GameState == GameState.InGame) {
			Move();
			Act();
			ReduceHeightByEnergy();
		} else if (player.IsDead) {
			heightLimitFactor = 1.0f;
		}
		var level = GameController.Instance.currentLevel;
		ForceWithinBounds(level.width, level.height);
	}

	protected override void Move() {
		var horizontalThrust = Input.GetAxis("Horizontal");
		var verticalThrust = allowVerticalMovement ? Input.GetAxis("Vertical") : 0.0f;

		var horizontalForce = horizontalSpeed * horizontalThrust * Time.deltaTime;
		var verticalForce = verticalSpeed * verticalThrust * Time.deltaTime;

		body.AddForce(horizontalForce, verticalForce, 0);
		ClampToMaxSpeed();

		if (horizontalThrust < -sideRotationDeadZone) {
			sideLean.targetRotation = sideRotation;
		} else if (horizontalThrust > sideRotationDeadZone) {
			sideLean.targetRotation = -sideRotation;
		} else {
			sideLean.targetRotation = 0f;
		}

		if (body.velocity.y > 0) {
			frontLean.targetRotation = -frontRotation;
		} else if (heightLimitFactor < 1.0) {
			frontLean.targetRotation = frontRotation;
		} else {
			frontLean.targetRotation = 0f;
		}
	}

	protected override void Act() {
		var slowButton = "Fire2";
		var fastButton = "Fire1";
		// The ship doesn't speed up automatically when we let go, so we need to handle this case
		if (Input.GetButtonDown(slowButton)) {
			player.targetSpeed = player.minSpeed;
		} else if (Input.GetButtonUp(slowButton)) {
			if (player.targetSpeed == player.minSpeed) {
				player.targetSpeed = player.baseSpeed;
			}
		}

		// The ship WILL however slow down by itself once we let go
		if (Input.GetButton(fastButton)) {
			if (player.targetSpeed < player.fastSpeed) {
				player.targetSpeed = player.fastSpeed;
			}
		}

		if (Debug.isDebugBuild && Input.GetKeyDown(KeyCode.Alpha1)) {
			player.RemoveEffect(EntityEffect.Invincibility);
			player.Damage(1000);
		}

		if (Debug.isDebugBuild && Input.GetKeyDown(KeyCode.Alpha2)) {
			if (player.HasEffect(EntityEffect.Invincibility)) {
				player.RemoveEffect(EntityEffect.Invincibility);
			} else {
				player.AddEffect(EntityEffect.Invincibility);
			}
		}
	}

	public override void Bounce(Transform origin) {
		var speed = player.IsDead ? bounceSpeed / 3f : bounceSpeed;
		body.AddForce(0, speed, 0);
		ClampToMaxSpeed();

		if (origin.gameObject.layer != (int) Layers.Booster) {
			if (heightLimitFactor < bounceHeightLimitIncrease) {
				bounceHeightAllowance = bounceHeightLimitIncrease;
			}
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
		var velocity = body.velocity;
		if (position.x - halfPlayerWidth < -halfWidth) {
			position.x = -halfWidth + halfPlayerWidth;
		} else if (position.x + halfPlayerWidth > halfWidth) {
			position.x = halfWidth - halfPlayerWidth;
		}
		if (position.y - halfPlayerHeight < 0) {
			position.y = halfPlayerHeight;
			velocity.y = 0;
		} else if (position.y + halfPlayerHeight > heightWithLimit) {
			position.y = heightWithLimit - halfPlayerHeight;
			velocity.y = 0;
		}
		transform.position = position;
		body.velocity = velocity;
	}
}
