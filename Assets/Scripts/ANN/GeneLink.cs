using UnityEngine;
using System.Collections;

public class GeneLink {

	public int in_layer;
	public int in_place;
	
	public int out_layer;
	public int out_place;
	
	public double weight;
	
	// constructor
	public GeneLink(int in_layer, int in_place, int out_layer, int out_place, double weight)
	{
		this.in_layer = in_layer;
		this.in_place = in_place;
		
		this.out_layer = out_layer;
		this.out_place = out_place;
		
		this.weight = weight;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

