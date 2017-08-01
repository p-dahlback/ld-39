using UnityEngine;
using System.Collections;

public class Engine : MonoBehaviour
{
	public AudioSource engineSound;

	public float standardEnginePitch = 1.0f;
	public float fastEnginePitch = 2.0f;
	public float slowEnginePitch = 0.5f;
	public float targetPitch = 1.0f;
	public float pitchChangeFactor = 2.0f;

	// Use this for initialization
	void Start ()
	{
		targetPitch = standardEnginePitch;
		engineSound.pitch = targetPitch;
	}
	
	// Update is called once per frame
	void Update ()
	{
		var difference = targetPitch - engineSound.pitch;
		if (Mathf.Abs(difference) > 0.1) {
			engineSound.pitch = engineSound.pitch + difference * pitchChangeFactor * Time.deltaTime;
		} else {
			engineSound.pitch = targetPitch;
		}

	}
}

