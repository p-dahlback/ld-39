using UnityEngine;
using System.Collections;

public abstract class ActorController : MonoBehaviour
{
	protected abstract void Move();
	protected abstract void Act();

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	}
}

