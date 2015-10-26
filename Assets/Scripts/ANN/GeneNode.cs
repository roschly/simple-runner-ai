using UnityEngine;
using System.Collections;

public class GeneNode {

	public static int nextID = 0;
	
	public int id;
	public double bias = 0.0;
	public double value = 0.0;

	public double oldValue = 0.0;
	public double maxDiff = 0.0;

	public GeneNode()
	{
		this.id = GeneNode.nextID;
		GeneNode.nextID++;
	}

	// Compare this value with the previous, and save a potential max difference.
	// The difference resembles the stress or activity in the node, so we can mutate links from very active nodes more often than not so active nodes
	public void saveFlow(){
		double diff = (value > oldValue) ? value-oldValue : oldValue-value ;

		oldValue = value;
		
		if (diff > maxDiff){
			maxDiff = diff;
		}
	}
}
