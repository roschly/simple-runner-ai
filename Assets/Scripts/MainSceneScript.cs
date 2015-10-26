using UnityEngine;
using System.Collections;

public class MainSceneScript : MonoBehaviour {

	void OnGUI(){
		GUI.Label( new Rect(Screen.width / 2, Screen.height / 2, 200, 30), "Initialise and start the game");
		if (GUI.Button (new Rect (Screen.width / 2, Screen.height / 2 + 50, 100, 40), "Start")) {
			// setup genome

			//Application.LoadLevel(1);
			PopulationControlScript.populationControl.setup();
		}
	}
}
