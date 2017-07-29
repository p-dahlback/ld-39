﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	private static GameController _instance;
	public static GameController Instance {
		get { return _instance; }
	}

	public Level currentLevel;

	void Awake () {
		if (_instance != null) {
			Destroy(_instance.gameObject);
		}
		_instance = this;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
