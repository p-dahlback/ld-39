using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyMeter : MonoBehaviour {

	public float minimumWidth = 0f;
	public float maximumWidth;

	public Image image;
	public Color okColor;
	public Color dangerColor;
	public Color flashColor;

	public float flashWhenEnergyLeft = 0.25f;
	public float flashFrequency = 2f;

	private float time;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		var player = GameController.Instance.player;
		var energyRate = 0.0f;
		if (player != null) {
			energyRate = player.energy / player.maxEnergy;
		}

		UpdateWidthForEnergyRate(energyRate);
		UpdateColorForEnergyRate(energyRate);
	}

	private void UpdateWidthForEnergyRate(float energyRate) {
		var width = minimumWidth + energyRate * (maximumWidth - minimumWidth);
		var rectTransform = transform as RectTransform;
		rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
	}

	private void UpdateColorForEnergyRate(float energyRate) {
		var baseColor = Color.Lerp(dangerColor, okColor, energyRate);
		Color color;
		if (energyRate <= flashWhenEnergyLeft) {
			var flashTime = 1.0f / flashFrequency;
			time = time % flashTime;
			time += Time.deltaTime;

			var currentFlashTime = time / flashTime;
			var flash = (1 + Mathf.Cos(currentFlashTime * Mathf.PI * 2) / 2f);
			color = Color.Lerp(flashColor, baseColor, flash);
		} else {
			time = 0f;
			color = baseColor;
		}

		image.color = color;
	}
}
