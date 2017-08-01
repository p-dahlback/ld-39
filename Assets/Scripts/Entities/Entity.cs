using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Entity : MonoBehaviour
{
	public float maxHealth = 3f;
	public MonoBehaviour deathHandler;
	public MonoBehaviour damageHandler;

	private float currentHealth = 0f;
	private bool isDead = false;
	private Dictionary<EntityEffect, int> effectCounters = new Dictionary<EntityEffect, int>();

	public float Health {
		get { return currentHealth; }
	}

	public bool IsDead {
		get { return isDead; }
	}

	void Awake() {
		Restore();
	}

	public void Restore() {
		currentHealth = maxHealth;
		isDead = false;
	}

	public void Damage(int damage) {
		if (isDead || HasEffect(EntityEffect.Invincibility)) {
			return;
		}

		Debug.Log("Damaged!");
		currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
		if (currentHealth <= 0) {
			Debug.Log("Dead!");
			isDead = true;
			if (deathHandler != null) {
				deathHandler.enabled = true;
			}
			OnDeath();
		} else {
			if (damageHandler != null) {
				damageHandler.enabled = true;
			}
			OnDamage();
		}
	}

	public void AddEffect(EntityEffect effect) {
		if (effectCounters.ContainsKey(effect)) {
			effectCounters[effect]++;
		} else {
			effectCounters[effect] = 1;
		}
	}

	public void RemoveEffect(EntityEffect effect) {
		if (effectCounters.ContainsKey(effect) && effectCounters[effect] > 0) {
			effectCounters[effect] = 0;
		}
	}

	public bool HasEffect(EntityEffect effect) {
		return effectCounters.ContainsKey(effect) && effectCounters[effect] > 0;
	}

	protected virtual void OnDamage() {
		
	}

	protected virtual void OnDeath() {
		
	}
}

