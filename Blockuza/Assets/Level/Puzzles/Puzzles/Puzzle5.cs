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

		if (gameObject.transform.localScale.x == -1f)
		{
			foreach (Transform child in enemies.transform)
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
			TriggerBlocks.SetActive(false);
		}

		if (enemies.childCount == 0 && wall.activeInHierarchy)
		{
			wall.SetActive(false);
		}
	}
}
