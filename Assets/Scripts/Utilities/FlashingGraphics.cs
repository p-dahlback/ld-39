using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashingGraphics : MonoBehaviour {

	public MaskableGraphic graphics;
	public float flashFrequency = 4f;

	private float time = 0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		var flashTime = 1.0f / flashFrequency;
		time += Time.deltaTime;
		time %= flashTime;

		graphics.enabled = time <= flashTime / 2f;
	}
}
