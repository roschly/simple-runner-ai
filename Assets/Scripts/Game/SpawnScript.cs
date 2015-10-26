using UnityEngine;
using System.Collections;

public class SpawnScript : MonoBehaviour {

	public GameObject[] obj;
	public float spawnMin = 1f;
	public float spawnMax = 2f;


	// Use this for initialization
	void Start () {
		Spawn();
		//Invoke ("Spawn", Random.Range (spawnMin, spawnMax));
	}

	void Spawn() {
	
		Instantiate (obj [Random.Range (0, obj.Length)], transform.position - new Vector3(0, -9.62f, 0), Quaternion.identity);
		//Instantiate (obj [Random.Range (0, obj.Length)], new Vector3(-10,2,0), Quaternion.identity);

		Invoke ("Spawn", Random.Range (spawnMin, spawnMax));
	}

}
