﻿using UnityEngine;
using System.Collections;

public class NudgeTesterScript : MonoBehaviour 
{

//	public SheepController sheepController;


	void Start () 
	{
	
	}
	

	void Update () 
	{
	
	}

	void OnTriggerStay2D(Collider2D col) 
	{
		if (col.gameObject.tag == "Sleepwalker") 
		{

			if(Input.GetKeyDown(KeyCode.Space)) 
			{

				col.gameObject.GetComponent<Sleepwalker>().HasBeenNudged ();
			
			}

			if(Input.GetKeyDown(KeyCode.DownArrow)){
				
				col.gameObject.GetComponent<Sleepwalker>().IsIdle ();
			}


		}

	}

}
