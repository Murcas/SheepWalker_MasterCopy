using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SleepwalkerHealth : MonoBehaviour
{
	public int startingHealth = 3; 
	public int sleepWalkerLives = 3;
	public int currentHealth;

	public Slider healthSlider;                                 
	public Image damageImage;
	public Text lifeCount;

	public float flashSpeed = 5f;                               
	public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     

	public ParticleSystem ZzzPoints;
	public ThornBushScript thornBush;
	public CheckpointScript myCheckPoint;

	GameObject mySleepwalkerObject;                                                                          
	bool damaged;
	bool isDead = false;


	void Awake ()
	{
		currentHealth = startingHealth;
		mySleepwalkerObject = GameObject.FindGameObjectWithTag ("Sleepwalker");
		myCheckPoint = FindObjectOfType<CheckpointScript> ();
		thornBush = FindObjectOfType<ThornBushScript> ();
		ZzzPoints = FindObjectOfType<ParticleSystem> ();
	}


	void Update ()
	{

		if(damaged)
		{
			damageImage.color = flashColour;
		}
		else
		{
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}

		damaged = false;

		deathCheck ();

		if (isDead == true) 
		{
			mySleepwalkerObject.SetActive(false);
			Invoke("Respawn", 3);
		}

	}

	void FixedUpdate ()
	{
		if (currentHealth == 3) 
		{
			ZzzPoints.startLifetime = 6;
		} 
		if (currentHealth == 2) 
		{
			ZzzPoints.startLifetime = 4;
		} 
		if (currentHealth == 1) 
		{
			ZzzPoints.startLifetime = 2;
		} 
		if (currentHealth == 0) 
		{
			ZzzPoints.startLifetime = 0;
		}
	}


	public void TakeDamage (int amount)
	{
		damaged = true;

		currentHealth -= amount;

		healthSlider.value = currentHealth;
	}

	public void deathCheck()
	{
		if (currentHealth == 0) 
		{
			isDead = true;
		} 
		else 
		{
			isDead = false;
		}
	}

	public void Respawn()
	{
		sleepWalkerLives = sleepWalkerLives -1;
		currentHealth = 3;
		healthSlider.value = 3;
		lifeCount.text = ("Lives: " + sleepWalkerLives);


		if (sleepWalkerLives > 0) 
		{
			mySleepwalkerObject.transform.position = myCheckPoint.checkPoint.transform.position;
			mySleepwalkerObject.SetActive (true);
		}
		if (sleepWalkerLives == 0) 
		{
			lifeCount.text = "Game Over";
		}
	}

}