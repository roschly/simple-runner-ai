using UnityEngine;
using System.Collections;

public class Algorithm {

	// GA parameters
	private static double uniformRate = 0.5;
	private static double mutationRate = 0.15; // 0.010
	private static int tournamentSize;
	private static bool elitism = true;
	private static double learningrate = 1.0;
	private static double layerMutationRate = 0.0; // 0.1
	
	
	public static Population evolvePopulation(Population pop)
	{
		Population newPopulation = new Population(pop.size(), false);
		Genome champion = pop.getFittest();
		
		// Keep our best Genome
		if (elitism)
		{
			newPopulation.saveGenome(0, champion);
		}
		
		// Crossover population
		int elitismOffset;
		if (elitism)
		{
			elitismOffset = 1;
		}
		else
		{
			elitismOffset = 0;
		}
		
		// half of the new population is the champion and mutations of him
		int halfPopSize = (int)Mathf.Floor((float) pop.size() / 2);
		tournamentSize = pop.size() - (halfPopSize + 1);

		for (int i = elitismOffset; i < halfPopSize; i++)
		{
			Genome clone = champion.copy();
			mutateGenome(clone);
			newPopulation.saveGenome(i, clone);
		}
		
		
		
		// Loop over the population size and create new genomes with
		for (int i = halfPopSize; i < pop.size(); i++)
		{
			Genome localChampion = tournamentSelection(pop);
			Genome clone = localChampion.copy();
			mutateGenome(clone);
			// Genome newGenome = crossover(genome1, genome2);
			newPopulation.saveGenome(i, clone);
		}
		
		/*
            // Mutate population
            for (int i = elitismOffset; i < newPopulation.size(); i++) {
                mutate(newPopulation.getGenome(i));
            }
            */
		
		return newPopulation;
	}
	
	
	/*
        // Crossover Genomes
        private static Genome crossover(Genome genome1, Genome genome2) {
            Genome offspring = new Genome(3,2,1);
            // Loop through genes
            for (int i = 0; i < genome1.size(); i++) {
                // Crossover
                if (Math.random() <= uniformRate) {
                    offspring.setGene(i, genome1.getGene(i));
                } else {
                    offspring.setGene(i, genome2.getGene(i));
                }
            }
            return offspring;
        }
        */
	
	// Mutate a genome
	public static void mutateGenome(Genome genome){
		foreach (GeneLink link in genome.getLinks()){
			GeneNode source = genome.nodes[link.in_layer][link.in_place];

			//if (Random.value <= source.maxDiff){ // used to be mutationRate
			if (Random.value <= mutationRate){
				if (Random.value <= uniformRate){
					link.weight = link.weight + learningrate;
				}
				else {
					link.weight = link.weight - learningrate;
				}
			}
		}
		
		foreach (GeneNode node in genome.getNodes()){
			if (Random.value <= mutationRate)
			{
				if (Random.value <= uniformRate)
				{
					node.bias = node.bias + learningrate;
				}
				else
				{
					node.bias = node.bias - learningrate;
				}
			}
		}
		
		// must not add to input or output layers
		for (int i = 1; i < genome.nrOfNodeLayers() - 1; i++)
		{
			if (Random.value <= layerMutationRate)
			{
				genome.addNewNodeToLayer(i);
			}
		}
		
	}

	public static void mutatePopulation(Population pop){
		foreach (Genome genome in pop.genomes){
			mutateGenome(genome);
		}
	}
	
	// Select Genomes for crossover
	private static Genome tournamentSelection(Population pop)
	{
		// Create a tournament population
		Population tournament = new Population(tournamentSize, false);
		// For each place in the tournament get a random Genome
		for (int i = 0; i < tournamentSize; i++)
		{
			int randomId = (int)(Random.value * pop.size());
			tournament.saveGenome(i, pop.getGenome(randomId));
		}
		// Get the fittest
		Genome fittest = tournament.getFittest();
		return fittest;
	}


}
