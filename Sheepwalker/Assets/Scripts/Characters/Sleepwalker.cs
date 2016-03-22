using UnityEngine;
using System.Collections;

public class Sleepwalker : CharacterController
{
	//variables

	public float nudgePower = 500f;
	private bool isIdle = false;
	private bool isSplat = false;
	[SerializeField]
	private bool wasNudged = false;
	public float nudgeUp;


	public SheepController sheepController;

	void Start() 
	{

		rb2d = gameObject.GetComponent<Rigidbody2D> ();
		sheepController = FindObjectOfType<SheepController> ();
		anim = GetComponent<Animator> ();
		nudgeUp = nudgePower;
	
	}

	void Update () {

		if (!isGrounded) {
			anim.SetLayerWeight (1, 1);
			anim.SetBool("isNudged", true);
		} 
		
		else 
			
		{
			anim.SetLayerWeight (1, 0);
		}

	}


	void FixedUpdate()
	
	{

	
		//Ground Check
		isGrounded = IsGrounded ();


		if (isSplat == false) { 
		
			if (isIdle == false) {

				if (wasNudged == false) {

					if (facingRight == true) {
		
						MoveLeftToRight ();

					}
		
					if (facingRight == false) {

						MoveRightToLeft ();

					}

				}

			}

		}

		if (isGrounded){
			if (wasNudged == true){
				anim.SetBool("isWalking",false);
				anim.SetBool("isNudged",true);
				
				
				rb2d.AddForce (Vector2.up * nudgeUp);
			}
			
			
		}
		
		if(isGrounded)
			
		{
			wasNudged = false;
		}



		NormalizeSlope ();
		//Flip (horizontal);
		
	}
	

	public void MoveLeftToRight()
	
	{
		
		rb2d.AddForce(new Vector2(movementSpeed,0));


	}
	
	public void MoveRightToLeft()
	
	{
		
		rb2d.AddForce(new Vector2(-movementSpeed,0));	

		
	}

	

	public void OnTriggerEnter2D (Collider2D other)
		
	{
		if (other.tag == "Edge") {

			Flip ();		
			
		}
	}

	public void HasBeenNudged ()
	{
		wasNudged = true;




	}

	public void IsIdle () 
	
	{
	
		isIdle = true;
		anim.SetBool ("isIdle", true);

	
	}
	
	public void IsSplat () 
	
	{
	
	anim.SetBool ("isSplat", true);
	isSplat = true;
	
	}




}