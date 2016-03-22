using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RamboScript : MonoBehaviour 
{
	//public Slider ramboSlider;
	//public Text ramboText;
	//public GameObject ramzooka;

	SpriteRenderer headbandSprite;
	CircleCollider2D headbandCollider;
	GameObject ramboHeadband;
	SheepController sheepController;
	Sleepwalker sleepwalker;
	//SheepStamina sheepStamina;
	bool headBandActive;
	
	void Awake () 
	{
		ramboHeadband = GameObject.FindGameObjectWithTag ("RamboHeadband");

		sheepController = FindObjectOfType<SheepController> ();

		headbandSprite = ramboHeadband.GetComponent<SpriteRenderer>();
		headbandCollider = ramboHeadband.GetComponent<CircleCollider2D>();
	}


	
	void FixedUpdate () 
	{
	
	
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Sheep") 
		{
			Debug.Log ("the sheep is tagging the bandana");
			headBandActive = true;
			headbandSprite.enabled = false;
			headbandCollider.enabled = false;
			sheepController.isNinja = true;
			sheepController.movementSpeed = sheepController.movementSpeed * 50;
			sheepController.jumpPower = sheepController.jumpPower * 1.5f;
			sheepController.maxSpeed = sheepController.maxSpeed * 2;
			sleepwalker.nudgeUp = sleepwalker.nudgePower * 2;

		}
		
	}

}
