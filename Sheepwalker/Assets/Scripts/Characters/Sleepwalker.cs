using UnityEngine;
using System.Collections;

public class Sleepwalker : MonoBehaviour 
{
	//variables
	public float movementSpeed;
	public float jumpPower;
	bool facingRight = true;
	private bool isIdle = false;
	private bool isSplat = false;
	private bool wasNudged = false;

	//objects
	Animator anim;
	public Rigidbody2D rb2d;
	//public Collider checkPoint;

	//scripts
	public SheepController sheepController;
	public Sleepwalker sleepwalker;


	//Ground Check
	public float groundCheckRadius;
	public float slopeFriction;
	private bool Grounded;
	public LayerMask whatIsGround;
	public Transform groundCheck;


	


	void Start() 
	{

		rb2d = gameObject.GetComponent<Rigidbody2D> ();
		sheepController = FindObjectOfType<SheepController> ();
		anim = GetComponent<Animator> ();
	
	}

	void Update () {

	}


	void FixedUpdate()
	
	{
		
		//Ground Check
		Grounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, whatIsGround);


		if (isSplat == false) { 
		
			if (isIdle == false) {

				if (wasNudged == false) {


					if (facingRight == true) {
		
						MoveLeftToRight ();
						IsWalking ();
					}
		
					if (facingRight == false) {

						MoveRightToLeft ();
						IsWalking ();
					}

				}

			}

		}

		if (Grounded){
			if (wasNudged == true){
				anim.SetBool("isWalking",false);
				anim.SetBool("isNudged",true);


				rb2d.AddForce (Vector2.up * jumpPower);
			}
		}
		
			
	


		if (wasNudged == true && Grounded) {
			wasNudged = false;
		}

		NormalizeSlope ();
		
	}
	

	public void MoveLeftToRight()
	
	{
		
		GetComponent<Rigidbody2D>().AddForce(new Vector2(movementSpeed,0));


	}
	
	public void MoveRightToLeft()
	
	{
		
		GetComponent<Rigidbody2D>().AddForce(new Vector2(-movementSpeed,0));	

		
	}


	void OnCollisionEnter2D(Collision2D Other)
	{
		
		if (Other.gameObject.tag == "Wall") {

			Debug.Log ("There is a wall in front of the object!");
			Flip ();

		}

//		if (Other.gameObject.tag == "Ground") {
//			anim.SetBool ("isWalking", true);
//		}

	}
	
	// @NOTE Must be called from FixedUpdate() to work properly 
	void NormalizeSlope () { 
		// Attempt vertical normalization 
		if (Grounded) { 
			RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 1f, whatIsGround); 
			
			if (hit.collider != null && Mathf.Abs(hit.normal.x) > 0.1f) 
			{ 
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
	
	
	public	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
		
	}

	public void HasBeenNudged ()
	{

		wasNudged = true;
		Debug.Log ("wasNudged = " + wasNudged);



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

	public void IsWalking()

	{
		if (wasNudged == false && Grounded) {
			anim.SetBool ("isWalking", true);
		}
	}


}