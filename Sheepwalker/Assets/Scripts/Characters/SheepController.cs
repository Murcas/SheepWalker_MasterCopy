using UnityEngine;
using System.Collections;

public class SheepController : MonoBehaviour 
{
	public float maxSpeed =3;
	public float MovementSpeed = 10f;
	public float jumpPower = 50f;
	public float slopeFriction;

	public float squatAmount; 
	private bool doubleJumped;
	private bool facingRight;

	[SerializeField]
	private Transform[] groundPoints;

	[SerializeField]
	private float groundRadius;

	[SerializeField]
	private LayerMask whatIsGround;

	private bool isGrounded;

	public bool isNudging = false;
	private bool isSquatting = false;
	public Rigidbody2D rb2d;
	private Animator anim;

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




		if (isGrounded) 

			doubleJumped = false;

			if (Input.GetKeyDown (KeyCode.UpArrow) && isGrounded) 

				Jump ();
			

			if (Input.GetKeyDown (KeyCode.UpArrow) && !isGrounded && !doubleJumped) {
				
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

		float horizontal = Input.GetAxis ("Horizontal");

		HandleMovement (horizontal);

		Flip (horizontal);

		HandleLayers ();

		isGrounded = IsGrounded ();

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


	void NormalizeSlope () { 
		// Attempt vertical normalization 
		if (isGrounded) { 
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


 	private void HandleMovement(float horizontal) 

	{

		if (rb2d.velocity.y < 0) 
		{
			anim.SetBool("NinjaLanding", true);
		}
		rb2d.velocity = new Vector2 (horizontal * MovementSpeed, rb2d.velocity.y);
		anim.SetFloat ("speed", Mathf.Abs (horizontal));
	}

	private void Flip (float horizontal)
	
	{
		if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight) 
		
		{
			facingRight = !facingRight;
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
			
			
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

	private bool IsGrounded()

	{
		if (rb2d.velocity.y <= 0) 
		{
			foreach (Transform point in groundPoints)
			{
				Collider2D[] colliders = Physics2D.OverlapCircleAll (point.position, groundRadius, whatIsGround);

				for(int i = 0; i < colliders.Length; i++) 

				{
					if(colliders[i].gameObject != gameObject) 
					{
						anim.ResetTrigger("NinjaJump");
						anim.SetBool("NinjaLanding", false);
						return true;
					}
				}

			}
		}
		return false;
	}

}