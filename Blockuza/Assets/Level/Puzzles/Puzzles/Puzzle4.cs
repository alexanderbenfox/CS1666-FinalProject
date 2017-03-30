using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle4 : Puzzle 
{
	public GameObject TriggerBlocks;

	// Use this for initialization
	void Start () 
	{
		TriggerBlocks.SetActive(false);	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Triggered)
		{
			TriggerBlocks.SetActive(true);
		}
		else
		{
			TriggerBlocks.SetActive(false);
		}
	}
}
