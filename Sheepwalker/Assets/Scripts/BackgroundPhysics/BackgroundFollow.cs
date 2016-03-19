using UnityEngine;
using System.Collections;

public class BackgroundFollow : MonoBehaviour {
	
	public Transform camera;
	public Vector3 offset;
	
	void Update () 
	{
		transform.position = new Vector3 (camera.position.x + offset.x, camera.position.y + offset.y, offset.z * Time.deltaTime); // Camera follows the player with specified offset position
	}
	
}
