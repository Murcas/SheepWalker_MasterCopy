using UnityEngine;
using System.Collections;

public class SheepController : MonoBehaviour 
{

	public float maxSpeed =3;
	public float speed = 50f;
	public float jumpPower = 150f;
	public float slopeFriction;
	public float groundCheckRadius;
	public float squatAmount; 
	private bool doubleJumped;

	public bool isNudging = false;
	private bool Grounded;
//	private bool isJumping = false;
	private bool isSquatting = false;

	public Rigidbody2D rb2d;
	private Animator anim;
	public Transform groundCheck;
	public LayerMask whatIsGround;
	public Sleepwalker sleepwalker;




	void Start () 
	{
		rb2d = gameObject.GetComponent<Rigidbody2D>();
		anim = gameObject.GetComponent<Animator>();
		sleepwalker = FindObjectOfType<Sleepwalker> ();
	}

		void Update () 
	{


		//Sets animation to the default Idle state when no keys are being pressed
		if (Input.GetAxis ("Horizontal") == 0.0f) 
		{
			anim.SetBool ("isWalking", false);

		}
		
		// lets your characters face left if - and face right if +
		if (Input.GetAxis ("Horizontal") < -0.1f) 
		{
			anim.SetBool ("isWalking", true);
			transform.localScale = new Vector3 (-1.5f, 1.5f, 1f);
			

		}
		
		if (Input.GetAxis ("Horizontal") > 0.1f) 
		{
			anim.SetBool ("isWalking", true);
			transform.localScale = new Vector3 (1.5f, 1.5f, 1f);
		}

		if (Grounded) 

			doubleJumped = false;

			if (Input.GetKeyDown (KeyCode.UpArrow) && Grounded) 
				Jump ();
			

			if (Input.GetKeyDown (KeyCode.UpArrow) && !Grounded && !doubleJumped) {
				
				Jump ();
				doubleJumped = true;
			}

		

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

		//uses for physics movement
	void FixedUpdate()
	{
		//To check if the sheep is on the ground
		//Grounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, whatIsGround);

		float h = Input.GetAxis("Horizontal");

			//moving the player
			rb2d.AddForce((Vector2.right * speed) * h);

		Grounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, whatIsGround);
		
		
     // limiting the speed of the player
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

	// @NOTE Must be called from FixedUpdate() to work properly 
	void NormalizeSlope () { 
		// Attempt vertical normalization 
		if (Grounded) { 
			RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 1f, whatIsGround); 
			
			if (hit.collider != null && Mathf.Abs(hit.normal.x) > 0.1f) { 
				Rigidbody2D body = GetComponent<Rigidbody2D>(); 
				// Apply the opposite force against the slope force  
				// You will need to provide your own slopeFriction to stabalize movement 
				body.velocity = new Vector2(body.velocity.x - (hit.normal.x * slopeFriction), body.velocity.y); 
				
				
				//Move Player up or down to compensate for the slope below them 
				Vector3 pos = transform.position; 
				pos.y += -hit.normal.x * Mathf.Abs(body.velocity.x) * Time.deltaTime * (body.velocity.x - hit.normal.x > 0 ? 1 : -1); 
				transform.position = pos; 
			} 
		} 
	}

		public void Jump()
		
		{
			rb2d.AddForce (Vector2.up * jumpPower);
	}


//	void OnCollisionEnter2D(Collider2D col) 
//	{
//
//
//		if (col.gameObject.tag == "SleepWalker" && isSquatting == true) 
//		{
//
//			sleepwalker.IsIdle();
//
//		}
//
//
//	}



}