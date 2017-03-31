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
	//public float enemyConcentration; // percent of rooms we want enemies in
	public int sectionsWide; //total length is sectionsWide x 10
	public int level; // will determine if left to right, or right to left
	//public int numPuzzles; //number of puzzles in the room


	private float cellSize = 3.2f; // 10 blocks x .32 wide
	private GameObject[] puzzBlox;
	private System.Random randomSeed = new System.Random();
	private float offset;
	private int numEnemies;

	// Use this for initialization
	void Start () 
	{
		numEnemies = randomSeed.Next(1,sectionsWide);
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

			//Block placement logic
			int blocksInSection = randomSeed.Next(1, 3);
			for (int j = 0; j < blocksInSection; j++)
			{
				rand = randomSeed.Next(0, specialBlox.Length);
				current.GetComponent<placeObject>().Place(specialBlox[rand], offset);
			}


			//Enemy placement logic
			int enemiesInSection = randomSeed.Next(0, 3);
			for (int j = 0; j < enemiesInSection;j++)
			{
				current.GetComponent<placeObject>().Place(enemyPrefab, offset);
			}

			offset += cellSize;
		}




	}

	void placePuzzle(int puzzlePlace)
	{
		//get the puzzle for the room and blocks needed to complete
		int rand = randomSeed.Next(0, puzzlePrefabs.Length);
		GameObject puzzle = puzzlePrefabs[rand];
		puzzBlox = puzzle.GetComponent<BlocksRequired>().blocksRequired;
		int numBlox = puzzBlox.Length;
		int currentBlock = 0;

		//pick starting room section
		rand = randomSeed.Next(0, sectionPrefabs.Length);
		GameObject next = sectionPrefabs[rand];

		for (int i = 0; i < puzzlePlace; i++) //place level sections up until puzzle placement
		{
			GameObject current = Instantiate(next, new Vector2(offset, 0), Quaternion.identity); //place section

			if (i == 0)
				current.GetComponent<placeObject>().PlaceChar(mainCharPrefab, level); //place main char depending on level


			//Block placement logic
			int blocksInSection = randomSeed.Next(1, 3);
			for (int j = 0; j < blocksInSection; j++)
			{
				if (i == puzzlePlace - 1 && currentBlock < numBlox - 1) //last section before puzzle and not all blocks required are placed
				{
					while (currentBlock < numBlox)
					{
						current.GetComponent<placeObject>().Place(puzzBlox[currentBlock], offset);
						currentBlock++;
					}
					break;
				}
				else
				{
					rand = randomSeed.Next(1, 11);
					if (rand < 6 && currentBlock < numBlox) //place a block required by the puzzle
					{
						current.GetComponent<placeObject>().Place(puzzBlox[currentBlock], offset);
						currentBlock++;
					}
					else //choose a random block
					{
						rand = randomSeed.Next(0, specialBlox.Length);
						current.GetComponent<placeObject>().Place(specialBlox[rand], offset);
					}
				}
			}


			//Enemy placement logic
			int enemiesInSection = randomSeed.Next(0, 3);
			for (int j = 0; j < enemiesInSection;j++)
			{
				current.GetComponent<placeObject>().Place(enemyPrefab, offset);
			}


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
