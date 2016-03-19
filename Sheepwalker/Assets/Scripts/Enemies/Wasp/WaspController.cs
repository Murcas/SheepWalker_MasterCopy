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
		
		Move ();
			
//		 	if (rb2d.transform.position.x >= maxRight) {
//				MoveLeft ();
//				transform.localScale = new Vector3 (-2f, 2f, 1f);
//				
//			} else if (rb2d.transform.position.x <= maxLeft) {
//				MoveRight ();
//				transform.localScale = new Vector3 (2f, 2f, 1f);
//				
//			}


			


		if (spottedSheep == true && shotsFired == true) {

			RestartMotion();

		}
		
		
		


			Raycasting ();
			Behaviours ();

		

		
		
		
	}
	
//	public void MoveRight()
//	{
//		rb2d.AddForce(new Vector2(movementSpeed, 0));
//		facingRight = true;
//		
//		
//	}
//	
//	
//	
//	public void MoveLeft()
//	{
//		rb2d.AddForce(new Vector2(-movementSpeed, 0));
//		facingRight = false;
//
//
//		
//		
//	}
	
	void Raycasting() 
		
	{
		//Debug.DrawLine(sightSheepStart.position, sightSheepEnd.position, Color.red);
		Debug.DrawLine(sightSWStart.position, sightSWEnd.position, Color.blue);
		//spottedSheep = Physics2D.Linecast(sightSheepStart.position, sightSheepEnd.position, 1 << LayerMask.NameToLayer("SheepWaspCollider")); 
		spottedSleepwalker = Physics2D.Linecast(sightSWStart.position, sightSWEnd.position, 1 << LayerMask.NameToLayer("Sleepwalker")); 
		
		
		
	}
	
	void Behaviours()
	{


		if(swSpotted == true) {
			anim.SetBool ("isFlying", false);
			anim.SetBool ("isShooting", true);

			rb2d.isKinematic = true;
			rb2d.velocity = Vector3.zero;
			
			
		}

		
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

	public void OnTriggerEnter2D(Collider2D other) 

	{
		if (other.tag == "Edge") {
			ChangeDirection ();
			//Debug.Log ("on trigger called");
		}
	}

	public void ChangeDirection ()
		
	{
		Debug.Log ("Change direction called");
		facingRight = !facingRight;
		transform.localScale = new Vector3 (transform.localScale.x *-1, 1, 1);
		
	}

	public void Move()
	{
		Debug.Log ("Moving");
		anim.SetFloat ("Speed", 1);
		transform.Translate (GetDirection () * (movementSpeed * Time.deltaTime));
	}

	public Vector2 GetDirection()

	{
			return facingRight ? Vector2.right : Vector2.left;
	}
}