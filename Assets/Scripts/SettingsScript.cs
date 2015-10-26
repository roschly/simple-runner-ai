using UnityEngine;
using System.Collections;

public class SettingsScript : MonoBehaviour {

	void OnGUI(){

		// TEST health + xp
		GUI.Label(new Rect(200, 10, 150, 30), "Population size: " + PopulationControlScript.populationControl.sizeOfPopulation);
		GUI.Label(new Rect(200, 40, 150, 30), "Generation cap: " + PopulationControlScript.populationControl.generationCap);
		
		if (GUI.Button( new Rect(200, 100, 150, 30), "Population up" )) {
			PopulationControlScript.populationControl.sizeOfPopulation += 5;
		}
		
		if (GUI.Button( new Rect(200, 140, 150, 30), "Population down" )) {
			PopulationControlScript.populationControl.sizeOfPopulation -= 5;
		}
		



		// TEST health + xp
		GUI.Label(new Rect(10, 10, 100, 30), "Health: " + GameControlScript.control.health);
		GUI.Label(new Rect(10, 40, 150, 30), "Experience: " + GameControlScript.control.experience);

		if (GUI.Button( new Rect(10, 100, 100, 30), "Health up" )) {
			GameControlScript.control.health += 10;
		}

		if (GUI.Button( new Rect(10, 140, 100, 30), "Health down" )) {
			GameControlScript.control.health -= 10;
		}

		if (GUI.Button( new Rect(10, 180, 100, 30), "Experience up" )) {
			GameControlScript.control.experience += 10;
		}

		if (GUI.Button( new Rect(10, 220, 100, 30), "Experience down" )) {
			GameControlScript.control.experience -= 10;
		}
	}
}
