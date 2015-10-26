using UnityEngine;
using System.Collections;

public class HUDScript : MonoBehaviour {

	private float bonus = 0;
	private float playerScore = 0;
	public Transform player;
	
	SpawnScript ss;

	void Awake(){
		GameObject spawner = GameObject.Find ("ObstacleSpawn Low");
		this.ss = spawner.GetComponent<SpawnScript>();
	}

	// Update is called once per frame
	void Update () {
		playerScore = player.position.x + bonus;

		if (playerScore > 1000) {
			// gameover
			Application.LoadLevel("GameOverScene");
		}
		else if (playerScore > 600) {
			ss.spawnMax = 0.8f;
			ss.spawnMin = 2.5f;
		}
		else if (playerScore > 500) {
			ss.spawnMax = 1.0f;
			ss.spawnMin = 2.0f;
		}
		else if (playerScore > 400) {
			ss.spawnMax = 1.0f;
			ss.spawnMin = 1.5f;
		}
		else if (playerScore > 300) {
			ss.spawnMax = 2.0f;
			ss.spawnMin = 2.0f;
		}
		else if (playerScore > 200) {
			ss.spawnMax = 1.5f;
			ss.spawnMin = 1.5f;
		}
		else if (playerScore > 100) {
			ss.spawnMax = 0.8f;
			ss.spawnMin = 0.8f;
		}

	}

	public void IncreaseScore(int amount) {
		bonus += amount;
	}

	void OnDisable() {
		PlayerPrefs.SetInt ("Score", (int)playerScore);
	}

	void OnGUI() {
		GUI.Label (new Rect (10, 10, 100, 30), "Fitness: " + (int)(playerScore));
	}
}
