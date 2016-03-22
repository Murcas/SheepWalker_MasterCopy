using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartMenu : MonoBehaviour 
{

	public GameObject myStartMenuText;
	public GameObject myMenuText;

	public GameObject myStartMenuButton;
	public GameObject myMenuButton;

	Text startGameText;
	Text menuText;

	Button startGame;
	Button menu;

	void Awake () 
	{
		startGameText = myStartMenuText.GetComponent<Text> ();
		menuText = myMenuText.GetComponent<Text> ();

		startGame = myStartMenuButton.GetComponent<Button> ();
		menu = myMenuButton.GetComponent<Button> ();
	
	}
		

	public void loadGame()
	{
		Application.LoadLevel ("Main");
	}
	
}