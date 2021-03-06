﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle4 : Puzzle 
{
	public GameObject TriggerBlocks;
	public Transform Enemies;
	// Use this for initialization
	void Start () 
	{
		TriggerBlocks.SetActive(false);	
		if (gameObject.transform.localScale.x == -1f)
		{
			foreach (Transform child in Enemies)
			{
				child.localScale = new Vector2(-1f, child.localScale.y);
			}
		}
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
