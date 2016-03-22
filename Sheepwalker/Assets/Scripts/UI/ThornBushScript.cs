using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class ThornBushScript : MonoBehaviour
{
	public int attackDamage = 1;
	public bool isSleepwalkerRecovering = false;
	public bool isSheepRecovering = false;

	public GameObject theSleepwalker;
	SleepwalkerHealth sleepWalkerHealth;

	GameObject theSheep;
	SheepStamina sheepStamina;


	void Awake ()
	{
		theSleepwalker = GameObject.FindGameObjectWithTag ("Sleepwalker");
		sleepWalkerHealth = theSleepwalker.GetComponent <SleepwalkerHealth> ();

		theSheep = GameObject.FindGameObjectWithTag ("Sheep");
		sheepStamina = theSheep.GetComponent <SheepStamina> ();
	}


	void OnCollisionStay2D (Collision2D other)
	{
		if(other.gameObject.tag == "Sleepwalker")	
		{
			if(!isSleepwalkerRecovering) 
			{
				AttackSleepwalker ();
				isSleepwalkerRecovering = true;
				Invoke ("turnSleepwalkerRecoverOff", 2);
			}

		}

		if(other.gameObject.tag == "Sheep")	
		{
			if(!isSheepRecovering) 
			{
				AttackSheep ();
				isSheepRecovering = true;
				Invoke ("turnSheepRecoverOff", 3);
			}

		}
	}


	void AttackSleepwalker ()
	{
		if(sleepWalkerHealth.currentHealth > 0)
		{
			sleepWalkerHealth.TakeDamage (attackDamage);
		}
	}

	void AttackSheep ()
	{
		if(sheepStamina.currentStamina > 0)
		{
			sheepStamina.TakeStaminaDamage (attackDamage);
		}
	}

	public void turnSheepRecoverOff()
	{
		isSheepRecovering = false;
	}

	public void turnSleepwalkerRecoverOff() 
	{
		isSleepwalkerRecovering = false;
	}
}