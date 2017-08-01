using UnityEngine;
using System.Collections;

public class FallSplash : MonoBehaviour
{
	public Animator animator;
	public float duration = 0.25f;

	private float time;

	// Use this for initialization
	void Start ()
	{
		animator.SetBool("FallSplash", true);
	}
	
	// Update is called once per frame
	void Update ()
	{
		time += Time.deltaTime;
		if (time >= duration) {
			Destroy(gameObject);
		}
	}
}

