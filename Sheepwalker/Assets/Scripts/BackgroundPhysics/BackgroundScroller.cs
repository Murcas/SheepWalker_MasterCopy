using UnityEngine;
using System.Collections;

public class BackgroundScroller : MonoBehaviour {
	
	public float scrollSpeed = 0, xPosition;
	public MeshRenderer meshRend;
	
	// Use this for initialization
	void Start () {
		
		meshRend = transform.GetComponent<MeshRenderer> ();
		meshRend.receiveShadows = false;
	}
	
	// Update is called once per frame
	void Update () {
		xPosition += Time.deltaTime * scrollSpeed;
		meshRend.material.mainTextureOffset = new Vector2 (xPosition, 0);
		
	}
}
