using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	private static GameController _instance;
	public static GameController Instance {
		get { return _instance; }
	}

	public Level currentLevel;
	public Player player;
	public Transform spawnContainer;
	public DialogBox dialogBox;

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

	public void PlayerDied() {
		var random = Random.Range(0, 4);
		switch (random) {
		case 0:
			dialogBox.SetDialog("not like thiiiis!", true, 2.0f);
			break;
		case 1:
			dialogBox.SetDialog("who even built all of this!", true, 2.0f);
			break;
		case 2:
			dialogBox.SetDialog("i'm registering a complaaaiint!", true, 2.0f);
			break;
		case 3:
			dialogBox.SetDialog("come on baby, hold together!", true, 2.0f);
			break;
		}
	}

	public void PlayerTookDamage() {
		var random = Random.Range(0, 6);
		switch(random) {
		case 0:
			dialogBox.SetDialog("aaahh!", true, 2.0f);
			break;
		case 1:
			dialogBox.SetDialog("nooo!", true, 2.0f);
			break;
		case 2:
			dialogBox.SetDialog("that's not fair!", true, 2.0f);
			break;
		case 3:
			dialogBox.SetDialog("why did this happen!", true, 2.0f);
			break;
		case 4:
			dialogBox.SetDialog("oww!", true, 2.0f);
			break;
		case 5:
			dialogBox.SetDialog("i bid my dongue!", true, 2.0f);
			break;
		}
	}

	public void SpeedIncrease(int number) {
		switch (number) {
		case 1:
			dialogBox.SetDialog("energy levels are dropping even faster!", true, 2.0f);
			break;
		case 2:
			dialogBox.SetDialog("noo it's just getting worse!", true, 2.0f);
			break;
		case 3:
			dialogBox.SetDialog("save meee!", true, 2.0f);
			break;
		}
	}

	private void GameStateDidChange(GameState oldState, GameState newState) {
		if (oldState == GameState.Start) {
			CurrentDistance = 0;
			DestroySpawns();
		}

		if (newState == GameState.GameOver) {
			StartCoroutine("GameOverGab");
		}

		if (newState == GameState.Finished) {
			StartCoroutine("FinishGab");
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

	IEnumerator GameOverGab() {
		yield return new WaitForSeconds(2.0f);
		switch(Random.Range(0, 3)) {
		case 0:
			dialogBox.SetDialog("but what about the galaxy...", true, 2.0f);
			break;
		case 1:
			dialogBox.SetDialog("um, i meant to do that", false, 2.0f);
			break;
		case 2:
			dialogBox.SetDialog("stupid windows 98!", true, 2.0f);
			break;
		}
		yield return new WaitForSeconds(4.0f);
		SceneManager.LoadSceneAsync(1);
	}

	IEnumerator FinishGab() {
		switch(Random.Range(0, 3)) {
		case 0:
			dialogBox.SetDialog("whooaaa!!", true, 2.0f);
			break;
		case 1:
			dialogBox.SetDialog("liftooooff!!", true, 2.0f);
			break;
		case 2:
			dialogBox.SetDialog("here i cooome!!", true, 2.0f);
			break;
		}
		yield return new WaitForSeconds(4.0f);
		SceneManager.LoadSceneAsync(2);
	}
}
