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
	//SheepStamina sheepStamina;
	bool headBandActive;
	
	void Awake () 
	{
		ramboHeadband = GameObject.FindGameObjectWithTag ("RamboHeadband");
		//ramzooka = GameObject.FindGameObjectWithTag ("Ramzooka");
		sheepController = FindObjectOfType<SheepController> ();
		//sheepStamina = FindObjectOfType<SheepStamina> ();
		headbandSprite = ramboHeadband.GetComponent<SpriteRenderer>();
		headbandCollider = ramboHeadband.GetComponent<CircleCollider2D>();
	}

	void RamboBazooka()
	{
		if(headBandActive == true)
		{
			//ramzooka.SetActive(true);
		}
		else
		{
			//ramzooka.SetActive(false);
		}
	}
	
	void FixedUpdate () 
	{
		if(headBandActive == true)
		{
			//sheepController.jumpPower = 700f;
			//ramboSlider.value -= Time.deltaTime;
			//ramboText.text = (" " + ramboSlider.value);
		}

//		if(ramboSlider.value == 0)
//		{
//			headBandActive = false;
//			sheepController.jumpPower = 400f;
//			sheepController.maxSpeed = 5;
//		}

		RamboBazooka ();
	
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
			sheepController.MovementSpeed = sheepController.MovementSpeed * 50;
			sheepController.jumpPower = sheepController.jumpPower * 1.5f;

		}
		
	}

}
