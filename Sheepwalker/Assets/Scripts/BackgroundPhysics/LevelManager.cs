using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public GameObject currentCheckpoint;
	private Sleepwalker sleepwalker;

	// Use this for initialization
	void Start () {

		sleepwalker = FindObjectOfType<Sleepwalker> ();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void RespawnPlayer() {

		Debug.Log ("Player Respawned");
		sleepwalker.transform.position = currentCheckpoint.transform.position;
	}
}
