using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

	public float horizonPosition = 5;
	public float width = 2;
	public float height = 2;

	public float Speed {
		get;
		set;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		var distance = GameController.Instance.CurrentDistance;
		distance += Speed * Time.deltaTime;
		GameController.Instance.CurrentDistance = distance;
	}
}
