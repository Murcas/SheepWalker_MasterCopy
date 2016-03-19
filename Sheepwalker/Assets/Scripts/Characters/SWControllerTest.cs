using UnityEngine;
using System.Collections;

public class SWControllerTest : MonoBehaviour {
	
	float movementSpeed = 5f;
	bool facingRight = true;
//	Animator anim;
	private Rigidbody2D rb2d;
	public float movementHeight;
	public float jumpPower = 150f;

	public Transform groundCheck;
	public float groundCheckRadius;
	private bool Grounded;
	public LayerMask whatIsGround;
	public float slopeFriction;

	public SheepController sheepController;
	private bool TrueFalse;
	
	void Start() {
		
//		anim = GetComponent<Animator>();
		sheepController = FindObjectOfType<SheepController> ();
		rb2d = gameObject.GetComponent<Rigidbody2D>();
		
		
	}
	
	void FixedUpdate(){

		//Ground Check
		Grounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, whatIsGround);
		
		if(facingRight == true){
			MoveLeftToRight();
		}
		
		if (facingRight == false) {
			MoveRightToLeft();
		}
		
		NormalizeSlope ();
		

	}
	
	public void MoveLeftToRight(){
		
		GetComponent<Rigidbody2D>().AddForce(new Vector2(movementSpeed,0));
		
	}
	
	public void MoveRightToLeft(){
		
		GetComponent<Rigidbody2D>().AddForce(new Vector2(-movementSpeed,0));	
		
	}
	
	void OnCollisionEnter2D(Collision2D Other){
		
		
		
		if (Other.gameObject.tag == "Left Facing Wall")
		{
			//Debug.Log ("There is a left facing wall in front of the object!");
			Flip();
			
			
			
		}

		if(Other.gameObject.tag == "Right Facing Wall")
		{
			
			//Debug.Log ("There is a right facing wall in front of the object!");
			Flip();
			
			
		}

//		if (Other.gameObject.tag == "Player" && sheepController.IsBeingNudged(TrueFalse) == true) {
//			
//			Debug.Log ("The code is correct");
//			
//		}
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
	
	
	void Flip(){
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
		
	}

	public void HasBeenNudged (){

		Debug.Log ("the sheep is tagging the sleepwalker after you press space");

		//GetComponent<Rigidbody2D> ().velocity = new Vector2 (movementSpeed, movementHeight);
		rb2d.AddForce (Vector2.up * jumpPower);

	
	}
}