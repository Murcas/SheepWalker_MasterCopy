using UnityEngine;
using System.Collections;

public class StingMover : MonoBehaviour {


		public float speed = 1;
//		public float RotationSpeed = 1;
		Transform sleepwalker;
	    private Transform myTransform;
		
		
void Start() {     
			sleepwalker = GameObject.FindGameObjectWithTag("Sleepwalker").transform;
		    myTransform = transform;
			//myTransform.LookAt (sheep);
			

		}
		
	void FixedUpdate()
	 {

		transform.LookAt (sleepwalker.transform.position); 
		transform.position = Vector3.MoveTowards (transform.position, sleepwalker.transform.position, speed * Time.deltaTime);
	
		//sheepAttack();

}

//	public void sheepAttack() {
//
//
////		myTransform.rotation = Quaternion.Slerp(myTransform.rotation,
////		Quaternion.LookRotation(sheep.position - myTransform.position), RotationSpeed*Time.deltaTime);
//		

//		//move towards the player
//		myTransform.position += myTransform.forward * speed * Time.deltaTime;
//	}
	}


