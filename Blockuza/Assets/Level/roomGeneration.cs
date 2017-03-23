using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class roomGeneration : MonoBehaviour {

	public GameObject[] sections;
	public int sectionsWide;
	public int sectionsHigh;

	private float cellSize = 1.6f; // 5 blocks x .32 wide

	// Use this for initialization
	void Start () 
	{
		Debug.Log(sections.Length);
		Debug.Log(sections[0]);
		Generate();
	}
	
	// Update is called once per frame
	void Generate() 
	{
		GameObject currentSection = sections[0];
		System.Random rand = new System.Random();

		for (int i = 0; i < sectionsWide; i++)
		{
			for (int j = 0; j < sectionsHigh; j++)
			{
				Instantiate(currentSection, new Vector2(i*cellSize, j*cellSize), Quaternion.identity);
				int next = rand.Next(0, sections.Length);
				currentSection = sections[next];
			}
		}
	}
}
