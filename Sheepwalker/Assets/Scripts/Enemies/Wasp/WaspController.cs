using UnityEngine;
using System.Collections;


public class WaspController : MonoBehaviour {
	
	public float fireRate;
	private float nextFire;
	public GameObject sting;
	public float speed = 20;
	Transform sheep;
	public Transform stingSpawn;
	
	public float movementSpeed, maxRight, maxLeft;
	public Rigidbody2D rb2d;
	public bool facingRight;
	bool shotsFired = false; 

	
	public Transform sightSheepStart, sightSheepEnd, sightSWStart, sightSWEnd;
	public LayerMask whatIsGround;
	private bool spottedSheep, spottedSleepwalker;
	private bool swSpotted =  false;

	public StingMover stingMover;

	
	
	public Animator anim;
	
	// Use this for initialization
	void Start () {
		
		anim = gameObject.GetComponent<Animator>();
		rb2d = gameObject.GetComponent<Rigidbody2D> ();
		sheep = GameObject.FindGameObjectWithTag("Sheep").transform;
		stingMover = FindObjectOfType<StingMover> ();
		
	}
	
	// Update is called once per frame
	void Update () {

		
		if (spottedSleepwalker == true) {
			swSpotted = true;
		}
		
		if (spottedSheep == false) {
			

			
		 	if (rb2d.transform.position.x >= maxRight) {
				MoveLeft ();
				transform.localScale = new Vector3 (-2f, 2f, 1f);
				
			} else if (rb2d.transform.position.x <= maxLeft) {
				MoveRight ();
				transform.localScale = new Vector3 (2f, 2f, 1f);
				
			}


			
		}

		if (spottedSheep == true && shotsFired == true) {

			RestartMotion();

		}
		
		
		


			Raycasting ();
			Behaviours ();

		

		
		
		
	}
	
	public void MoveRight()
	{
		rb2d.AddForce(new Vector2(movementSpeed, 0));
		facingRight = true;
		
		
	}
	
	
	
	public void MoveLeft()
	{
		rb2d.AddForce(new Vector2(-movementSpeed, 0));
		facingRight = false;


		
		
	}
	
	void Raycasting() 
		
	{
		//Debug.DrawLine(sightSheepStart.position, sightSheepEnd.position, Color.red);
		Debug.DrawLine(sightSWStart.position, sightSWEnd.position, Color.blue);
		//spottedSheep = Physics2D.Linecast(sightSheepStart.position, sightSheepEnd.position, 1 << LayerMask.NameToLayer("SheepWaspCollider")); 
		spottedSleepwalker = Physics2D.Linecast(sightSWStart.position, sightSWEnd.position, 1 << LayerMask.NameToLayer("Sleepwalker")); 
		
		
		
	}
	
	void Behaviours()
	{
		
		if(spottedSheep == true) {
				anim.SetBool ("isFlying", false);
				anim.SetBool ("isStinging", true);
				rb2d.isKinematic = true;
				rb2d.velocity = Vector3.zero;

				
			}
			
			
		if (spottedSheep == false) {
			anim.SetBool ("isStinging", false);
			anim.SetBool ("isFlying", true);
		}

		if(swSpotted == true) {
			anim.SetBool ("isFlying", false);

			anim.SetBool ("isShooting", true);


			rb2d.isKinematic = true;
			rb2d.velocity = Vector3.zero;
			
			
		}
		
		
//		if (spottedSleepwalker == false) {
//			anim.SetBool ("isStinging", false);
//			anim.SetBool ("isFlying", true);
//		}

		
	}

	//Called from animator controller
	public void FireSting () {

		if(Time.time > nextFire){
			nextFire = Time.time + fireRate;
			Instantiate (sting, stingSpawn.transform.position, stingSpawn.transform.rotation);
			//Rigidbody2D instantiatedProjectile = Instantiate(shot,transform.position,transform.rotation)as Rigidbody2D;
			//instantiatedProjectile.velocity = (sheep.position - transform.position).normalized * speed;
		}

		//stingMover.sheepAttack ();


	}

	public void RestartMotion () {

		swSpotted = false;
		rb2d.isKinematic = false;
		anim.SetBool ("isFlying", true);
		anim.SetBool ("isShooting", false);



		if (rb2d.transform.position.x < maxRight || rb2d.transform.position.x > maxLeft) {


			transform.localScale = new Vector3 (-2f, 2f, 1f);
			rb2d.AddForce(new Vector2(-movementSpeed, 0));




		}

	}
}