using UnityEngine;
using System.Collections;
using System.Collections.Generic; 
using System.Runtime.Serialization.Formatters.Binary; 
using System.IO;


public class PopulationControlScript : MonoBehaviour {

	public static PopulationControlScript populationControl;

	public double championFitness;

	public int sizeOfPopulation;
	public int generationCap;
	public int maxFitness;

	public int generationCount;

	public Population pop;
	public int index;

	private string filePath = "C:/Users/Roschly/Documents/GitHub/simple-runner/Data/";

	// ensure singleton design
	void Awake () {
		if (populationControl == null) {
			DontDestroyOnLoad (gameObject);
			populationControl = this;
		} 
		else if (populationControl != this) {
			Destroy(gameObject);
		}
		
	}

	public void setup(){
		this.index = 0;
		this.sizeOfPopulation = 20;
		this.generationCap = 20;
		this.maxFitness = 100;

		this.pop = new Population (this.sizeOfPopulation, true);

		// initial mutation
		Algorithm.mutatePopulation (this.pop);

		// clear data file
		this.clearTxtFile ();
		//this.saveDatFile();
		//this.saveTxtFile ();

		//Debug.Log (pop.getGenome (0).toString ());
		//Debug.Log (1 / (1 + Mathf.Exp((float) -0 )));

		testNextGenome ();
	}

	public void testNextGenome(){
		Genome genome = this.pop.getGenome(this.index);
		BrainControlScript.brainControl.updateBrain (genome);

		//TODO: either switch scene here or via button on main scene
		Application.LoadLevel("GameTestScene");
	}

	public void testHasEnded(){
		if (index == 0) {
			if (this.pop.getGenome (index).fitness < BrainControlScript.brainControl.fitness) {
				this.pop.getGenome (index).fitness = BrainControlScript.brainControl.fitness;
			}
		} else {
			this.pop.getGenome(index).fitness = BrainControlScript.brainControl.fitness;
		}

		if (this.index == this.sizeOfPopulation - 1) {
			evolveNextGeneration ();
		} 
		else {
			this.index += 1; 
			testNextGenome();
		}

	}

	public void evolveNextGeneration(){
		// save data before evolving
		this.saveTxtFile ();

		this.pop = Algorithm.evolvePopulation(this.pop);
		this.championFitness = this.pop.getGenome (0).fitness;
		this.generationCount++;
		this.index = 0;
		testNextGenome ();

	}

	public void saveDatFile(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (filePath + "/PopulationData.dat");

		Debug.Log (Application.persistentDataPath);

		PopulationData data = new PopulationData(this.pop);

		bf.Serialize (file, data);
		file.Close();
	}

	public void saveTxtFile(){
		string fileName = "PopulationData.txt";

		string fitness = "";

		foreach (Genome genome in this.pop.genomes) {
			fitness += genome.fitness + " ";
		}

		string data = System.String.Format("{0} {1} {2}", this.generationCount, fitness, System.Environment.NewLine);

		System.IO.File.AppendAllText(filePath + fileName, data);

		Debug.Log ("WRITE TO FILE!!!!!!!!!!!!!!!!!");

		//System.IO.File.WriteAllText("C:/Users/Roschly/Documents/GitHub/simple-runner/Data/PopulationData.txt", "This is text that goes into the text file");
	}

	public void clearTxtFile(){
		string fileName = "PopulationData.txt";

		//System.IO.File.AppendAllText(filePath + fileName, "");
		System.IO.File.WriteAllText (filePath + fileName, "");


	}

	[System.Serializable]
	class PopulationData {
		public int generationCount;
		public double[] fitnesses; 

		public PopulationData(Population pop){
			this.generationCount = populationControl.generationCount;

			this.fitnesses = new double[pop.size()];

			for (int i = 0; i < pop.size(); i++){
				this.fitnesses[i] = pop.genomes[i].fitness;
			}
		}
	}

}
