using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Genome{

	public List<List<GeneNode>> nodes = new List<List<GeneNode>>();
	//public ArrayList<ArrayList<GeneNode>> nodes = new ArrayList<ArrayList<GeneNode>>();
	public List<List<GeneLink>> links = new List<List<GeneLink>>();
	
	private double initialWeight = 0.0;
		
	private int nrHiddenLayers, nrLayers;
		
	public double fitness = 0.0;
	

	public Genome(int[] layers){
		int nrInput = layers[0];
		int nrOutput = layers[layers.Length - 1];
		int[] nrHiddenNodes = new int[layers.Length-2];
		
		for (int i = 0; i < layers.Length-2; i++){
			nrHiddenNodes[i] = layers[i+1];
		}
		
		nrHiddenLayers = nrHiddenNodes.Length;
		nrLayers = nrHiddenLayers + 2;
		
		// initialize layers of nodes and links
		for (int i = 0; i < nrLayers; i++){
			this.nodes.Add(new List<GeneNode>());
		}
		
		for (int i = 0; i < nrLayers - 1; i++){
			this.links.Add(new List<GeneLink>());
		}
		
		
		// Create nodes
		for (int i = 0; i < nrInput; i++){
			this.nodes[0].Add( new GeneNode() );
		}
		for (int i = 0; i < nrHiddenLayers; i++){
			for (int j = 0; j < nrHiddenNodes[i]; j++){
				this.nodes[i + 1].Add(new GeneNode());
			}
		}
		for (int i = 0; i < nrOutput; i++){
			this.nodes[nrHiddenLayers + 1].Add(new GeneNode());
		}
		
		// Create links and fully connect all layers
		for (int i = 1; i < nrLayers; i++){
			fullyConnectTwoLayers(i-1, i);
		}	
	}
	
	private void fullyConnectTwoLayers(int layer1, int layer2)
	{
		for (int i = 0; i < nodes[layer1].Count; i++)
		{
			for (int j = 0; j < nodes[layer2].Count; j++)
			{
				this.links[layer1].Add(new GeneLink(layer1, i, layer2, j, this.initialWeight));
			}
		}
	}
	
	//public double sendThroughNetwork(Color[] inputVector)
	public List<double> sendThroughNetwork(Color[] inputVector)
	{

		// send input_vector to inputlayer
		for (int i = 0; i < this.nodes[0].Count; i++)
		{
			float colors = 0;
			int factor = 2;
			for (int j = 0; j < factor; j++){
				 colors += inputVector[factor*i + j].grayscale;
			}

			this.nodes[0][i].value = (double) ( colors / ((float)factor) );
			this.nodes[0][i].saveFlow();

			/*
			// search for pixels with a high diff
			//if (7511 < i && i < 7515 && this.nodes[0][i].value != 0 ){
			if (i == 7514){
				Debug.Log ( this.nodes[0][i].value );
			}
			*/
		}
		
		for (int i = 0; i < nrLayers - 1; i++)
		{
			feedForwardFrom(i);
			computeValuesFromLayer(i + 1);
		}
		
		List<double> outputValues = new List<double>();
		foreach (GeneNode outputNode in nodes[nrLayers-1]){
			outputValues.Add( outputNode.value );
			outputNode.value = 0;
		}


		return outputValues;
	}
	
	private double activationFunc(double x)
	{
		// tanh
		//return Math.tanh(x);
		
		// sigmoid
		//Debug.Log (x);
		return 1 / (1 + Mathf.Exp((float) -x ));
	}
	
	private void computeValuesFromLayer(int fromLayer)
	{
		for (int i = 0; i < this.nodes[fromLayer].Count; i++)
		{
			GeneNode node = this.nodes[fromLayer][i];
			//Debug.Log ("value: " + node.value + " bias: " + node.bias);
			node.value = activationFunc(node.bias + node.value);
		}
	}
	
	private void feedForwardFrom(int fromLayer)
	{
		for (int i = 0; i < this.links[fromLayer].Count; i++)
		{
			GeneLink link = this.links[fromLayer][i];
			
			int in_layer = link.in_layer;
			int in_place = link.in_place;
			
			int out_layer = link.out_layer;
			int out_place = link.out_place;
			
			GeneNode inNode = this.nodes[in_layer][in_place];
			GeneNode outNode = this.nodes[out_layer][out_place];

			outNode.value += (inNode.value * link.weight);
			inNode.value = 0; // So it doesnt retain value from the last iteration; cause of the activationFunc(0) = 0.5 -> activationFunc(0.5) = 0.62.. -> activationFunc(0.62..)
		}
	}
	
	
	// PUBLIC FUNCTIONS
	
	public void addNewNodeToLayer(int layer)
	{
		// layer must not be input or output layers
		
		int layerBefore = layer - 1;
		int layerAfter = layer + 1;
		int nodePlace = nodes[layer].Count;
		GeneNode node = new GeneNode();
		nodes[layer].Add(node);
		// fully connect node
		// layer before
		for (int i = 0; i < nodes[layerBefore].Count; i++)
		{
			int in_layer = layerBefore;
			int in_place = i;
			int out_layer = layer;
			int out_place = nodePlace;
			
			links[layerBefore].Add(new GeneLink(in_layer, in_place, out_layer, out_place, this.initialWeight));
		}
		
		
		// layer after
		for (int i = 0; i < nodes[layerAfter].Count; i++)
		{
			int in_layer = layer;
			int in_place = nodePlace;
			int out_layer = layerAfter;
			int out_place = i;
			
			links[layer].Add(new GeneLink(in_layer, in_place, out_layer, out_place, this.initialWeight));
		}
		
		
	}
	
	public List<GeneLink> getLinks()
	{
		List<GeneLink> result = new List<GeneLink>();
		
		for (int i = 0; i < this.links.Count; i++)
		{
			for (int j = 0; j < this.links[i].Count; j++)
			{
				result.Add(this.links[i][j]);
			}
		}
		return result;
	}
	
	public List<GeneNode> getNodes()
	{
		List<GeneNode> result = new List<GeneNode>();
		
		for (int i = 0; i < this.nodes.Count; i++)
		{
			for (int j = 0; j < this.nodes[i].Count; j++)
			{
				result.Add(this.nodes[i][j]);
			}
		}
		return result;
	}
	
	public List<GeneNode> getNodes(int index)
	{
		return this.nodes[index];
	}
	
	public int nrOfNodeLayers()
	{
		return this.nodes.Count;
	}
	
	public int nrOfLinkLayers()
	{
		return this.links.Count;
	}
	
	/*
        private ArrayList<GeneNode> getNodesOfType(int type){
            ArrayList<GeneNode> nodesOfType = new ArrayList<GeneNode>();
            for (GeneNode node : this.nodes){
                if (node.type == type){
                    nodesOfType.add(node);
                }
            }

            return nodesOfType;
        }
        */
	
	public string toString()
	{
		string str = "";
		
		for (int i = 0; i < nrLayers; i++)
		{
			for (int j = 0; j < this.nodes[i].Count; j++)
			{
				GeneNode node = this.nodes[i][j];
				str += "(" + node.value + " : " + node.bias + ") ";
			}
			
			str += "\n";
			
			if (i < nrLayers - 1)
			{
				for (int j = 0; j < this.links[i].Count; j++)
				{
					GeneLink link = this.links[i][j];
					str += "(" + link.in_layer + "," + link.in_place + ")=" + link.weight + "=(" + link.out_layer + "," + link.out_place + ")  ";
				}
			}
			
			str += "\n";
		}
		return str;
	}
	
	public Genome copy()
	{
		/*
		int nrInput = this.nodes[0].Count;
		int nrOutput = this.nodes[this.nrLayers - 1].Count;
		int[] nrHiddenNodes = new int[this.nrHiddenLayers];
		*/

		int[] layers = new int[nrLayers];
		
		for (int i = 0; i < this.nodes.Count; i++){
			layers[i] = this.nodes[i].Count;
		}
		/*
		for (int i = 0; i < this.nrHiddenLayers; i++)
		{
			nrHiddenNodes[i] = this.nodes[i + 1].Count;
		}
		*/

		//Genome copy = new Genome(nrInput, nrHiddenNodes, nrOutput);
		Genome copy = new Genome(layers);
		
		// copy all nodes
		for (int i = 0; i < this.nrLayers; i++)
		{
			for (int j = 0; j < this.nodes[i].Count; j++)
			{
				GeneNode nodeCopy = copy.nodes[i][j];
				GeneNode nodeOriginal = this.nodes[i][j];
				
				nodeCopy.bias = nodeOriginal.bias;
				nodeCopy.id = nodeOriginal.id;

				// value should always be zero. it is only used for calculating the feed through it
				//nodeCopy.value = nodeOriginal.value;
				
			}
		}
		
		// copy all links
		for (int i = 0; i < this.nrLayers - 1; i++)
		{
			for (int j = 0; j < this.links[i].Count; j++)
			{
				GeneLink linkCopy = copy.links[i][j];
				GeneLink linkOriginal = this.links[i][j];
				
				linkCopy.in_layer = linkOriginal.in_layer;
				linkCopy.in_place = linkOriginal.in_place;
				linkCopy.out_layer = linkOriginal.out_layer;
				linkCopy.out_place = linkOriginal.out_place;
				
				linkCopy.weight = linkOriginal.weight;
				
			}
		}
		
		return copy;
	}

}
