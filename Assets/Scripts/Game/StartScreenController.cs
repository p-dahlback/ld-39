using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartScreenController : MonoBehaviour, IRotationListener, IPositionListener
{
	public DialogBox dialogBox;
	public Intro introManager;
	public Transform startScreen;

	public PositionToTarget playerPositioning;
	public RotateToTarget cameraRotation;
	public AutoRotate autoRotate;

	private bool playerInPosition = false;
	private bool cameraInPosition = false;

	private StartScreenState _state = StartScreenState.Screen;
	public StartScreenState State {
		get { return _state; }
		set { 
			var old = _state;
			_state = value;
			if (old != value) {
				OnStateChange(old, value);
			}
		}
	}

	// Use this for initialization
	void Start ()
	{
		GameController.Instance.GameState = GameState.Start;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetButtonDown("Cancel")) {
			if (State == StartScreenState.Intro) {
				dialogBox.CloseDialog();
				introManager.StopCutscene();
				StartCoroutine("PrepareForGame");
			}
		}
		if (Input.anyKey) {
			if (State == StartScreenState.Screen) {
				State = StartScreenState.Intro;
				startScreen.gameObject.SetActive(false);
			}
		}
	}

	void OnStateChange(StartScreenState oldState, StartScreenState newState) {
		if (newState == StartScreenState.Intro) {
			introManager.gameObject.SetActive(true);
		} else if (newState == StartScreenState.End) {
			StartGameIfOk();
		}
	}

	public void AssumePosition() {
		if (!cameraInPosition) {
			autoRotate.enabled = false;
			cameraRotation.enabled = true;
			cameraRotation.listener = this;
		}
		if (!playerInPosition) {
			playerPositioning.enabled = true;
			playerPositioning.listener = this;
		}
	}

	public void RotationChangeFinished(Quaternion newRotation) {
		cameraInPosition = true;
		StartGameIfOk();
	}

	public void PositionChangeFinished(Vector3 newPosition) {
		playerInPosition = true;
		StartGameIfOk();
	}

	IEnumerator PrepareForGame() {
		yield return new WaitForSeconds(1.0f);
		State = StartScreenState.End;
	}

	private void StartGameIfOk() {
		if (State == StartScreenState.End) {
			AssumePosition();
		}

		if (cameraInPosition && playerInPosition && State == StartScreenState.End) {
			SceneManager.LoadScene(1);
		}
	}
}

