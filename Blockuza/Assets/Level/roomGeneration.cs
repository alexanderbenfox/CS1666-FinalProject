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
	public float enemyConcentration; // percent of rooms we want enemies in
	public int sectionsWide; //total length is sectionsWide x 10
	public int level; // will determine if left to right, or right to left
	//public int numPuzzles; //number of puzzles in the room


	private float cellSize = 3.2f; // 10 blocks x .32 wide
	private GameObject[] specBlox;
	private System.Random randomSeed = new System.Random();
	private float offset;
	private int numEnemies;

	// Use this for initialization
	void Start () 
	{
		numEnemies = (int)((float)sectionsWide * enemyConcentration);
		offset = 0;
		GenerateRoom();
	}
	
	// Update is called once per frame
	void GenerateRoom() 
	{


		//choose where puzzle will be in relation to the room
		int puzzlePlace = randomSeed.Next(1, sectionsWide + 1);
		placePuzzle(puzzlePlace);

		int sectionsLeft = sectionsWide - puzzlePlace;



		for (int i = 0; i < sectionsLeft; i++)
		{
			//pick starting room section
			int rand = randomSeed.Next(0, sectionPrefabs.Length);
			GameObject next = sectionPrefabs[rand];
			GameObject current = Instantiate(next, new Vector2(offset, 0), Quaternion.identity); //place section

			current.GetComponent<placeObject>().Place(specBlox[0], offset); //put special block in the section

			if (i == (sectionsWide - 1))
				current.GetComponent<placeObject>().Place(enemyPrefab, offset);
			offset += cellSize;
		}




	}

	void placePuzzle(int puzzlePlace)
	{
		//get the puzzle for the room and blocks needed to complete
		int rand = randomSeed.Next(0, puzzlePrefabs.Length);
		GameObject puzzle = puzzlePrefabs[rand];
		specBlox = puzzle.GetComponent<BlocksRequired>().blocksRequired;

		//pick starting room section
		rand = randomSeed.Next(0, sectionPrefabs.Length);
		GameObject next = sectionPrefabs[rand];

		for (int i = 0; i < puzzlePlace; i++) //place level sections up until puzzle placement
		{
			GameObject current = Instantiate(next, new Vector2(offset, 0), Quaternion.identity); //place section

			if (i == 0)
				current.GetComponent<placeObject>().PlaceChar(mainCharPrefab, level); //place main char depending on level

			current.GetComponent<placeObject>().Place(specBlox[0], offset); //put special block in the section

			if (i == (sectionsWide - 1))
				current.GetComponent<placeObject>().Place(enemyPrefab, offset);
			
			rand = randomSeed.Next(0, sectionPrefabs.Length);
			next = sectionPrefabs[rand];
			offset += cellSize;
		}

		//place puzzle and increment offset
		Instantiate(puzzle, new Vector2(offset, 0), Quaternion.identity);
		offset += cellSize;

		return;
	}
}
