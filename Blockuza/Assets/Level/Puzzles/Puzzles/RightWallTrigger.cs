using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightWallTrigger : Puzzle {

	public GameObject RightWall;

	// Use this for initialization
	void Start () 
	{
		RightWall.SetActive(true);	
	}

	// Update is called once per frame
	void Update () 
	{
		if (Triggered)
		{
			RightWall.SetActive(false);
		}
		else
		{
			RightWall.SetActive(true);
		}
	}
}
