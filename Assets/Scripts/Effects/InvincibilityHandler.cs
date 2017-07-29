using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityHandler : MonoBehaviour {

	public Entity entity;
	public Renderer renderer;
	public Material transparentMaterial;
	public float duration = 2.0f;
	public float flashFrequency = 2f;

	private Material material;
	private float time = 0f;

	void OnEnable() {
		time = 0f;
		entity.AddEffect(EntityEffect.Invincibility);
		material = renderer.material;
		renderer.material = transparentMaterial;
	}

	void OnDisable() {
		entity.RemoveEffect(EntityEffect.Invincibility);
		renderer.material = material;
		material = null;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;

		if (time >= duration) {
			enabled = false;
		}

		var flashTime = 1.0f / flashFrequency;
		var flashProgress = time % flashTime;
		var alpha = (1 + Mathf.Cos(flashProgress / flashTime * Mathf.PI * 2)) / 2f;
		var color = renderer.material.color;
		color.a = alpha;
		renderer.material.color = color;
	}
}
