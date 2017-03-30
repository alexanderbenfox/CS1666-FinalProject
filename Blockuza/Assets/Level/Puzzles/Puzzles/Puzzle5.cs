using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle5 : Puzzle 
{
	public GameObject[] TriggerBlocks;

	// Use this for initialization
	void Start () 
	{
		TriggerBlocks[0].SetActive(true);
		TriggerBlocks[1].SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Triggered)
		{
			TriggerBlocks[0].SetActive(false);
			TriggerBlocks[1].SetActive(true);
		}
	}
}
