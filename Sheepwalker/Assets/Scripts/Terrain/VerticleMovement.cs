using UnityEngine;
using System.Collections;

public class VerticleMovement : MonoBehaviour 
{

	public float movementSpeed;
	public float HighestPoint;
	public float LowestPoint;
	public Rigidbody2D rb2d;


	void Start () 
	{
		rb2d = gameObject.GetComponent<Rigidbody2D> ();
	}


	void Update () 
	{
		if (rb2d.transform.position.y >= HighestPoint) 
		{
			MoveDown ();
		}

		if (rb2d.transform.position.y <= LowestPoint) 
		{
			MoveUp ();
		}
	
	}
	

	public void MoveDown()
	{
		rb2d.AddForce(new Vector2(0, -movementSpeed));
	}

	public void MoveUp() 
	{
		rb2d.AddForce(new Vector2(0, movementSpeed));
	}


}
