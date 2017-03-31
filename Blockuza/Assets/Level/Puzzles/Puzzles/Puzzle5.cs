using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle5 : Puzzle 
{
	public GameObject[] TriggerBlocks;
	public GameObject wall;

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

		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Puzzle");
		if (enemies.Length == 0)
			wall.SetActive(false);
	}
}
