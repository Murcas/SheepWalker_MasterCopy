using UnityEngine;
using System.Collections;

public class CanepeController : MonoBehaviour {

	public Sleepwalker sleepwalker;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnCollisionEnter2D (Collision2D col) {

		if (col.gameObject.tag == "Sleepwalker") {
			sleepwalker.IsIdle();
		}

	}
}
