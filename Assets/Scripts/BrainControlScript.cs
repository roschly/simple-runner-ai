using UnityEngine;
using System.Collections;

public class BrainControlScript : MonoBehaviour {

	public static BrainControlScript brainControl;

	public int fitness;
	public Genome genome;

	void Awake () {
		if (brainControl == null) {
			DontDestroyOnLoad (gameObject);
			brainControl = this;
		} 
		else if (brainControl != this) {
			Destroy(gameObject);
		}
		
	}

	public void updateBrain(Genome genome){
		this.genome = genome;
		this.fitness = 0;
	}

}
