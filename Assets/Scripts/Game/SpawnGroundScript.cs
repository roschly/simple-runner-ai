using UnityEngine;
using System.Collections;

public class SpawnGroundScript : MonoBehaviour {

	public GameObject ground;
	public float interval = 1.1f;

	// Use this for initialization
	void Start () {
		Spawn ();
	
	}
	
	void Spawn(){
		Instantiate (ground, transform.position, Quaternion.identity);
		Invoke ("Spawn", interval);
	}
}
