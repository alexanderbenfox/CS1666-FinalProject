using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle2 : Puzzle {

	public GameObject Enemies;

	// Use this for initialization
	void Start () 
	{
		Enemies.SetActive(false);	
	}

	// Update is called once per frame
	void Update () 
	{
		if (Triggered)
		{
			Enemies.SetActive(true);
		}
		else
		{
			Enemies.SetActive(false);
		}
	}
}
