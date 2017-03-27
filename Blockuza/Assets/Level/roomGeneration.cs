using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class roomGeneration : MonoBehaviour {

	public GameObject[] sectionPrefabs;
	//public GameObject[] puzzlePrefabs;
	public GameObject mainCharPrefab;
	//public int enemyConcentration; // num enemies per section
	public int sectionsWide;
	//public int numPuzzles; //number of puzzles in the room
	//public int level; // will determine if left to right, or right to left

	private float cellSize = 1.6f; // 5 blocks x .32 wide
	//private GameObject[] 

	// Use this for initialization
	void Start () 
	{
		//int numSections = sectionsWide / numPuzzles; //number of 
		GenerateSection();
	}
	
	// Update is called once per frame
	void GenerateSection() 
	{
		GameObject currentSection = sectionPrefabs[0];
		System.Random rand = new System.Random(2);

		for (int i = 0; i < sectionsWide; i++)
		{
			Instantiate(currentSection, new Vector2(i*cellSize, 0), Quaternion.identity); //place section
			placeObject placeComp = currentSection.GetComponent<placeObject>(); //get placeObject component on section
			placeComp.Place(mainCharPrefab); //call Place() to put an object in the section
			int next = rand.Next(0, sectionPrefabs.Length);
			currentSection = sectionPrefabs[next];
		}
	}
}
