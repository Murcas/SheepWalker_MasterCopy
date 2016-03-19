using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SheepStamina : MonoBehaviour
{
	public int startingStamina = 3; 
	public int currentStamina;  
	public float ramboForm;

	public Slider staminaSlider;                                 
	public Image damageImage;     

	public float flashSpeed = 5f;                               
	public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     

	public ThornBushScript thornBush;

	bool damaged;                                          


	void Awake ()
	{
		currentStamina = startingStamina;
		thornBush = FindObjectOfType<ThornBushScript> ();
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

	}
		

	public void TakeStaminaDamage (int amount)
	{
		damaged = true;

		currentStamina -= amount;

		staminaSlider.value = currentStamina;
	}

}