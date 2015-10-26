using UnityEngine;
using System.Collections;

public class GameControlScript : MonoBehaviour {

	public static GameControlScript control;

	public float health;
	public float experience;

	// for holding statistics

	void Awake () {
		if (control == null) {
			DontDestroyOnLoad (gameObject);
			control = this;
		} 
		else if (control != this) {
			Destroy(gameObject);
		}

	}

	void OnGUI(){
		//GUI.Label(new Rect(10, 10, 100, 30), "Health: " + health);
		//GUI.Label(new Rect(10, 40, 150, 30), "Experience: " + experience);

	}
}
