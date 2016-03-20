using UnityEngine;
using System.Collections;

public class StingMover : MonoBehaviour {
	
//Test text for github test

		public float speed = 1;
//		public float RotationSpeed = 1;
		Transform sleepwalker;
	    private Transform myTransform;
		WaspController waspController;
		
		
void Start() {     
			sleepwalker = GameObject.FindGameObjectWithTag("Sleepwalker").transform;
		    myTransform = transform;
			waspController = FindObjectOfType<WaspController> ();
			
			

		}
		
	void FixedUpdate()
	 {

		transform.LookAt (sleepwalker.transform.position); 
		transform.position = Vector3.MoveTowards (transform.position, sleepwalker.transform.position, speed * Time.deltaTime);
		waspController.swSpotted = false;

	


}


	}


