using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	private static GameController _instance;
	public static GameController Instance {
		get { return _instance; }
	}

	public Level currentLevel;
	public Player player;
	public Transform spawnContainer;

	private GameState state = GameState.InGame;

	public GameState GameState {
		get { return state; }
		set { 
			var oldValue = state;
			state = value;
			if (oldValue != value) {
				GameStateDidChange(oldValue, value);
			}
		}
	}

	public float CurrentDistance {
		get;
		set;
	}

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

	private void GameStateDidChange(GameState oldState, GameState newState) {
		if (oldState == GameState.Start) {
			CurrentDistance = 0;
			DestroySpawns();
		}
	}

	private void DestroySpawns() {
		var children = new List<Transform>();
		foreach (Transform child in spawnContainer) {
			children.Add(child);
		}
		foreach (var child in children) {
			Destroy(child.gameObject);
		}
	}
}
