using UnityEngine;
using System.Collections;

public class CheckpointScript : MonoBehaviour 
{
	public Sleepwalker sleepwalker;
	public SheepController sheepController;
	public GameObject checkPoint;


	void Start () 
	{
		sheepController = FindObjectOfType<SheepController> ();
		sleepwalker = FindObjectOfType<Sleepwalker> ();
	}
	
	
	void FixedUpdate () 
	{
//		if(sleepwalker.rb2d.transform.position.y < -5)
//		{
//			sleepwalker.rb2d.transform.position = checkPoint.transform.position;
//		}
//
//		if (sheepController.rb2d.transform.position.y < -5)
//		{
//			sheepController.rb2d.transform.position = checkPoint.transform.position;
//	
//		}
	
	}
}
