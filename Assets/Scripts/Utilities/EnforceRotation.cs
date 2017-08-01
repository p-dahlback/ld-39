using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnforceRotation : MonoBehaviour {

	public Quaternion rotation;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.rotation = rotation;
	}
}
