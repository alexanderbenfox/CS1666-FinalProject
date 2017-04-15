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
			if (gameObject.transform.localScale.x == -1f)
			{
				foreach (Transform child in Enemies.transform)
				{
					child.localScale = new Vector2(-1f, child.localScale.y);
				}
			}

		}
		else
		{
			Enemies.SetActive(false);
		}
	}
}
