using UnityEngine;
using System.Collections;

public class Parallaxing : MonoBehaviour {

	public Transform [] backgrounds; //Array (list) of all the backgrounds to be parallaxed
	private float [] parallaxScales;  // Proportion of the cameras movement to move backgrounds by
	public float smoothing = 1f;     //How smooth the paralax will be

	private Transform cam;          //Reference to main camera's transform
	private Vector3 previousCamPos; // position of the camera in the previous frame



	//Is called before Start (). Great for references
	void Awake () {

		//Set up camera reference

		cam = Camera.main.transform;

	}


	// Use this for initialization
	void Start () {



		//The previous frame had the current frames start position

		previousCamPos = cam.position;

		//Assigning corrisponding parallaxScale

		parallaxScales = new float [backgrounds.Length];

		for (int i = 0; i < backgrounds.Length; i++) {
			parallaxScales[i] = backgrounds[i].position.x;
		}
	
	}
	
	// Update is called once per frame
	void Update () {



		//for each background

		for (int i = 0; i < backgrounds.Length; i++) {

			//The parallax is the opposite of the cameras movment multiplied by scale
			float parallax = (previousCamPos.x - cam.position.x) * parallaxScales [i];

			//set a target x position which is the current position plus parallax

			float backgroundTargetPosX = backgrounds[i].position.x + parallax;

			//Crate a target position which is the backgrounds current position with it's target x position

			Vector3 backgroundTargetPos = new Vector3 (backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);
			                                       
			//Fade between current position and target possition using lerp

			backgrounds[i].position = Vector3.Lerp (backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
		}

		//Reset previousCamPos to the camera's positio9n at the end of the frame

		previousCamPos = cam.position;


	
	}
}
