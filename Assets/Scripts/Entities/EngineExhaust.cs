using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineExhaust : MonoBehaviour {

	public ParticleSystem fireParticles;
	public ParticleSystem smokeParticles;

	public float targetScale = 1.0f;
	public float scaleChangeFactor = 0.6f;

	private float time;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (GameController.Instance.player.energy == 0) {
			fireParticles.Stop();
			smokeParticles.gameObject.SetActive(true);
			smokeParticles.Play();
		} else {
			if (fireParticles.isStopped) {
				fireParticles.Play();
			}
			if (smokeParticles.isPlaying) {
				smokeParticles.Stop();
			}
		}

		ApproachTargetScale();	
	}

	private void ApproachTargetScale() {
		var currentScale = fireParticles.transform.localScale.x;
		var difference = targetScale - currentScale;
		float scale;
		if (Mathf.Abs(difference) > 0.1) {
			scale = currentScale + difference * scaleChangeFactor * Time.deltaTime;
		} else {
			scale = targetScale;
		}
		var scaleVector = transform.localScale;
		scaleVector.Set(scale, scale, scale);
		fireParticles.transform.localScale = scaleVector;
		smokeParticles.transform.localScale = scaleVector;
	}
}
