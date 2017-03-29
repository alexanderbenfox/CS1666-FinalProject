using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class roomGeneration : MonoBehaviour {

	public GameObject[] sectionPrefabs;
	public GameObject[] puzzlePrefabs;
	public GameObject[] specialBlox;
	public GameObject mainCharPrefab;
	public GameObject enemyPrefab;
	public int enemyConcentration; // num enemies per section
	public int sectionsWide;
	public int level;
	//public int numPuzzles; //number of puzzles in the room
	//public int level; // will determine if left to right, or right to left

	private float cellSize = 1.6f; // 5 blocks x .32 wide
	private GameObject[] specBlox;

	// Use this for initialization
	void Start () 
	{
		//int numSections = sectionsWide / numPuzzles; //number of 

		GenerateSection();
	}
	
	// Update is called once per frame
	void GenerateSection() 
	{
		System.Random randomSeed = new System.Random();

		//get the puzzle for the room
		int rand = randomSeed.Next(0, puzzlePrefabs.Length);
		GameObject puzzle = puzzlePrefabs[rand]; 
		specBlox = puzzle.GetComponent<BlocksRequired>().blocksRequired;

		//pick starting room section
		rand = randomSeed.Next(0, sectionPrefabs.Length);
		GameObject next = sectionPrefabs[rand];

		int i=0;
		while (i < sectionsWide)
		{
			GameObject current = Instantiate(next, new Vector2(i * cellSize, 0), Quaternion.identity); //place section

			if(i==0)
				current.GetComponent<placeObject>().PlaceChar(mainCharPrefab,level); //put main character in first section
			
			current.GetComponent<placeObject>().Place(specBlox[0],i * cellSize); //put special block in the section

			if(i==(sectionsWide-1))
				current.GetComponent<placeObject>().Place(enemyPrefab,i*cellSize);
			rand = randomSeed.Next(0, sectionPrefabs.Length);
			next = sectionPrefabs[rand];
			i++;
		}

		Instantiate(puzzle, new Vector2(i * cellSize, 0), Quaternion.identity);
	}
}
