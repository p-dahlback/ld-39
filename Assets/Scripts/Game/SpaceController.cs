using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpaceController : MonoBehaviour {

	public Rigidbody playerBody;
	public DialogBox dialogBox;
	public Transform finalMessage;

	public float flightForce = 10f;
	public Vector3 torque;

	private bool acceptEnd = false;

	// Use this for initialization
	void Start () {
		playerBody.AddForce(Vector3.right * flightForce);
		playerBody.AddTorque(torque);
		StartCoroutine("FinalGab");
	}
	
	// Update is called once per frame
	void Update () {
		if (!acceptEnd) {
			return;
		}

		if (Input.GetKeyDown(KeyCode.Q)) {
			if (Application.platform != RuntimePlatform.WebGLPlayer && !Application.isMobilePlatform && !Application.isEditor) {
				Application.Quit();
			}
		}

		if (Input.GetKeyDown(KeyCode.R)) {
			SceneManager.LoadSceneAsync(0);
		}
	}

	IEnumerator FinalGab() {
		yield return new WaitForSeconds(2.0f);
		dialogBox.SetDialog("just wait, doctor llama!", false, 2.0f);
		yield return new WaitForSeconds(6.0f);
		dialogBox.SetDialog("is there a bus around here?", true, 2.0f);
		yield return new WaitForSeconds(4.0f);
		finalMessage.gameObject.SetActive(true);
		acceptEnd = true;
	}
}
