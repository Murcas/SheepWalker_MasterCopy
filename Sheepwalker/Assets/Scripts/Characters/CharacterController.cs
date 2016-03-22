using UnityEngine;
using System.Collections;

public class CharacterController: MonoBehaviour {

	public float movementSpeed;
	public bool facingRight = true;
	public bool isGrounded;
	public LayerMask whatIsGround;
	public float slopeFriction;

	public Rigidbody2D rb2d;
	public Animator anim;
	
	public void NormalizeSlope () { 
		// Attempt vertical normalization 
		if (isGrounded) { 
			RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 3f, whatIsGround); 
			
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


	public void Flip() 

	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;

	}

	public bool IsGrounded()
		
	{
		if (rb2d.velocity.y <= 0) 
		{

			RaycastHit2D hit = Physics2D.Raycast(this.transform.position, -Vector2.up, 2f, whatIsGround);

				if(hit.collider.tag == "Ground") 
					{
						
						return true;
					}
				
		}
		return false;
	}

}


