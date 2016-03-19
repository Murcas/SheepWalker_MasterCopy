using UnityEngine;
using System.Collections;

public class HorizontalMovement : MonoBehaviour 
{

	public float movementSpeed;
	public float maxRight;
	public float maxLeft;
	public Rigidbody2D rb2d;


	void Start () 
	{
		rb2d = gameObject.GetComponent<Rigidbody2D> ();
	}
	

	void Update () 
	{
		if (rb2d.transform.position.x >= maxRight)
		{
			MoveLeft ();
		}

		else if (rb2d.transform.position.x <= maxLeft)
		{
			MoveRight ();
		}
	}


	public void MoveRight()
	{
		rb2d.AddForce(new Vector2(movementSpeed, 0));
	}
	
	public void MoveLeft()
	{
		rb2d.AddForce(new Vector2(-movementSpeed, 0));
	}

}


