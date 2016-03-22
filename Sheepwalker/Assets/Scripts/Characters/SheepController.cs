using UnityEngine;
using System.Collections;

public class SheepController : CharacterController
{
	public float jumpPower;
	public float maxSpeed = 5;
	public float squatAmount; 
	private bool doubleJumped;
	public bool isNudging = false;
	private bool isSquatting = false;
	public Sleepwalker sleepwalker;
	public bool isNinja;

	
	void Start () 
	{
		rb2d = gameObject.GetComponent<Rigidbody2D>();
		anim = gameObject.GetComponent<Animator>();
		sleepwalker = FindObjectOfType<Sleepwalker> ();
		facingRight = true;
		isNinja = false;
	}

		void Update () 
	{

		if (isNinja == true && !isGrounded) 
			
		{
			anim.SetTrigger("NinjaJump");
		}

		if (isGrounded) {

			anim.ResetTrigger ("NinjaJump");
			anim.SetBool ("NinjaLanding", false);			
			doubleJumped = false;

		}
		
		if (Input.GetKeyDown (KeyCode.UpArrow) && isGrounded) {
			Jump ();
		}
		
		if (Input.GetKeyDown (KeyCode.UpArrow) && !isGrounded && !doubleJumped) {
			
			Jump ();
			doubleJumped = true;
		}

		
		Behaviour ();

	}
	

	//uses for physics movement
	void FixedUpdate()
	{

		float horizontal = Input.GetAxis ("Horizontal");

		HandleMovement (horizontal);

		ChangeDirection (horizontal);

		HandleLayers ();

		isGrounded = IsGrounded ();

		 //limiting the speed of the player
		if(rb2d.velocity.x > maxSpeed)
		{
			rb2d.velocity = new Vector2(maxSpeed,rb2d.velocity.y);
		}
		
		if(rb2d.velocity.x < -maxSpeed)
		{
			rb2d.velocity = new Vector2(-maxSpeed,rb2d.velocity.y);
		}

 		
		NormalizeSlope ();
	}

	public void Jump()
		
	{

		rb2d.AddForce (Vector2.up * jumpPower);

	}


 	private void HandleMovement(float horizontal)

	{

		if (rb2d.velocity.y <= 0) 
		{
			anim.SetBool("NinjaLanding", true);
		}

		if (isGrounded) {

			//transform.position += Vector3.right * movementSpeed * Time.deltaTime;
			rb2d.velocity = new Vector2 (horizontal * movementSpeed, rb2d.velocity.y);
		}

		anim.SetFloat ("speed", Mathf.Abs (horizontal));

	}

	private void ChangeDirection (float horizontal)
	
	{
		if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight) 
		
		{

			Flip ();
			
			
		}
	}

	private void HandleLayers() {

		if (isNinja == true) {
			anim.SetLayerWeight (1, 1);

		} else 
		
		{

			anim.SetLayerWeight (1,0);
		}

		if (isNinja == true && !isGrounded) {
			anim.SetLayerWeight (2, 1);
		} 

		else 
		
		{
			anim.SetLayerWeight (2, 0);
		}

	}

	private void Behaviour()

	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			anim.SetBool ("isNudging", true);
			isNudging = true;
		}
		
		if (Input.GetKeyUp(KeyCode.Space))
		{
			anim.SetBool ("isNudging", false);
			isNudging = false;
			
		}
		
		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			anim.SetBool ("isSquatting", true);
			isSquatting = true;
			
		}
		
		if (Input.GetKeyUp(KeyCode.DownArrow))
		{
			anim.SetBool ("isSquatting", false);
			isSquatting = false;
			
		}

	}

}