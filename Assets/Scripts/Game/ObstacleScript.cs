using UnityEngine;
using System.Collections;

public class ObstacleScript : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			
			//Debug.Log("av");
			Application.LoadLevel("GameOverScene");
			return;
			
		}
	}
}
