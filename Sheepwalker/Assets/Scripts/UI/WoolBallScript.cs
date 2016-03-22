using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WoolBallScript : MonoBehaviour 
{
	public int woolCount;
	public Text woolCountText;

	GameObject woolBall;
	SheepController sheepController;
	SheepStamina sheepStamina;

	void Awake () 
	{
		woolBall = GameObject.FindGameObjectWithTag ("WoolBall");
		sheepController = FindObjectOfType<SheepController> ();
		sheepStamina = FindObjectOfType<SheepStamina> ();
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Sheep") 
		{
			woolCount = woolCount +1;
			woolCountText.text = ("Wool: " + woolCount);
			woolBall.gameObject.SetActive(false);
		}
	}

	void Update () 
	{
	
	}
}
