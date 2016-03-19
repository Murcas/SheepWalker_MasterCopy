using UnityEngine;
using System.Collections;

public class SawScript : MonoBehaviour {

	public LevelManager levelManager;
	public Sleepwalker sleepwalker;
	private Animator anim;

	// Use this for initialization
	void Start () {
		
		levelManager = FindObjectOfType<LevelManager> ();
		sleepwalker = FindObjectOfType<Sleepwalker> ();
		anim = gameObject.GetComponent<Animator>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter2D(Collider2D other){
		
		if (other.gameObject.tag == "Sleepwalker") {
		
		sleepwalker.IsSplat();
		anim.SetBool ("isIdle",true);
		
			
			
		}
		
	}
}
