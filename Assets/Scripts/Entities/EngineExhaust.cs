using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineExhaust : MonoBehaviour {

	public float targetScale = 1.0f;
	public float scaleChangeFactor = 0.6f;

	private float time;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		ApproachTargetScale();	
	}

	private void ApproachTargetScale() {
		var currentScale = transform.localScale.x;
		var difference = targetScale - currentScale;
		float scale;
		if (Mathf.Abs(difference) > 0.1) {
			scale = currentScale + difference * scaleChangeFactor * Time.deltaTime;
		} else {
			scale = targetScale;
		}
		var scaleVector = transform.localScale;
		scaleVector.Set(scale, scale, scale);
		transform.localScale = scaleVector;
	}
}
