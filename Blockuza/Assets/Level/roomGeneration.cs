using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class roomGeneration : MonoBehaviour {

	public GameObject[] sectionPrefabs;
	public GameObject[] puzzlePrefabs;
	public GameObject mainCharPrefab;
	public GameObject enemyPrefab;
	public int enemyConcentration; // num enemies per section
	public int sectionsWide;
	//public int numPuzzles; //number of puzzles in the room
	//public int level; // will determine if left to right, or right to left

	private float cellSize = 1.6f; // 5 blocks x .32 wide
	private GameObject[] specialBlox;

	// Use this for initialization
	void Start () 
	{
		//int numSections = sectionsWide / numPuzzles; //number of 

		GenerateSection();
	}
	
	// Update is called once per frame
	void GenerateSection() 
	{
		//GameObject currentSection = sectionPrefabs[0];

		GameObject puzzle = puzzlePrefabs[0];
		//BlocksRequired puzz = puzzle.GetComponent<BlocksRequired>();
		GameObject[] blox = puzzle.GetComponent<BlocksRequired>().blocksRequired;

		System.Random rand = new System.Random(2);


		int i=0;
		while (i < sectionsWide)
		{
			GameObject currentSection = Instantiate(sectionPrefabs[0], new Vector2(i*cellSize, 0), Quaternion.identity); //place section
			//placeObject placeComp = currentSection.GetComponent<placeObject>(); //get placeObject component on section

			if(i==0)
				currentSection.GetComponent<placeObject>().Place(mainCharPrefab); //put main character in first section
			
			currentSection.GetComponent<placeObject>().Place(blox[0]); //put special block in the section

			if(i==(sectionsWide-1))
				currentSection.GetComponent<placeObject>().Place(enemyPrefab);
			//int next = rand.Next(0, sectionPrefabs.Length);
			//currentSection = sectionPrefabs[next];
			i++;
		}

		Instantiate(puzzle, new Vector2(i * cellSize, 0), Quaternion.identity);
	}
}
