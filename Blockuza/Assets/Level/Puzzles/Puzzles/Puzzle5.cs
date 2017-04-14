using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle5 : Puzzle 
{
	public GameObject TriggerBlocks;
	public GameObject wall;
	public Transform enemies;
	// Use this for initialization
	void Start () 
	{
		TriggerBlocks.SetActive(true);
		wall.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Triggered)
		{
			TriggerBlocks.SetActive(false);
		}

		if (enemies.childCount == 0 && wall.activeInHierarchy)
		{
			wall.SetActive(false);
		}
	}
}
