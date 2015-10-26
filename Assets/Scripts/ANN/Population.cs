using UnityEngine;
using System.Collections;

public class Population {

	public Genome[] genomes;
	
	// Create a population
	public Population(int populationSize, bool initialise)
	{
		genomes = new Genome[populationSize];
		
		if (initialise)
		{
			/*
			int nrInput = 25000; // 25000
			int[] nrHiddenNodes = { 1, 1 };
			int nrOutput = 1;
			*/

			int[] layers = {25000, 1};
			
			for (int i = 0; i < size(); i++)
			{
				//Genome newGenome = new Genome(nrInput, nrHiddenNodes, nrOutput);
				Genome newGenome = new Genome(layers);
				saveGenome(i, newGenome);
			}
		}
	}
	
	/* Getters */
	public Genome getGenome(int index)
	{
		return genomes[index];
	}
	
	public Genome getFittest()
	{
		Genome fittest = genomes[0];
		// Loop through individuals to find fittest
		
		for (int i = 0; i < size(); i++)
		{
			if (fittest.fitness <= getGenome(i).fitness)
			{
				fittest = getGenome(i);
			}
		}
		
		return fittest;
	}
	
	/* Public methods */
	// Get population size
	public int size()
	{
		return genomes.Length;
	}
	
	// Save individual
	public void saveGenome(int index, Genome genome)
	{
		genomes[index] = genome;
	}
	
}
