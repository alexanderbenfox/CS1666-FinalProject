using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class roomGeneration : MonoBehaviour {

	public GameObject[] sectionPrefabs;
	public GameObject[] puzzlePrefabs;
	public GameObject[] specialBlox;
	public GameObject enemyPrefab;
	public GameObject mainChar;
	public GameObject door;
	public GameObject mapTrigger;
	public GameObject PlacedBlocks;
	public int sectionsWide; //total length is sectionsWide x 10
	public int Level = 1; 
	public int roomNumber;

	public BackgroundObjects backgroundObjects;

	//public int numPuzzles; //number of puzzles in the room

	private float cellSize = 6.4f; // 20 blocks x .32 wide
	private GameObject[] puzzBlox;
	private System.Random randomSeed = new System.Random();
	private float offset;
	public GameObject LevelOneUI;
	public GameObject LevelTwoUI;
	public GameObject LevelThreeUI;
	//private int numEnemies;

	// Use this for initialization

	void OnEnable()
	{
		if (roomNumber == 0)
		{
			roomNumber++;
			LevelOneUI.SetActive(true);
		}
		else
		{
			backgroundObjects.destroyAttachedObjects ();
			GenerateRoom(Level);
			backgroundObjects.placeWallPapers();
		}
	}
	// Pick where the puzzle is in the room, place sections before and after it
	public void GenerateRoom(int level) 
	{
		//Decide what offset should be depending on level
		if (level % 2 == 0) //evens generate right to left
		{
			offset = (float)sectionsWide * cellSize;
			Debug.Log("Offset: "+offset);
			mainChar.transform.position = new Vector2(offset, 0.64f);//place character back at start of room
		}
		else //odd generates left to right
		{
			offset = 0f;
			mainChar.transform.position = new Vector2(0f, 0.64f);//place character back at start of room
		}

		int puzzlePlace = randomSeed.Next(1, sectionsWide-1); //choose where puzzle will be in relation to the room
		Debug.Log(puzzlePlace);
		//get the puzzle for the room and blocks needed to complete
		int rand = randomSeed.Next(0, puzzlePrefabs.Length);
		GameObject puzzle = puzzlePrefabs[rand];
		puzzBlox = puzzle.GetComponent<BlocksRequired>().blocksRequired;

		placeSections(puzzlePlace, level, 0); //place sections before puzzle

		//place puzzle
		GameObject o = Instantiate(puzzle, new Vector2(offset, 0), Quaternion.identity);
		o.transform.parent = this.transform;
		//Put map trigger under
		GameObject trigg = Instantiate(mapTrigger, new Vector2(offset, 0), Quaternion.identity);
		trigg.transform.parent = gameObject.transform;
		//increment offset
		if ((level % 2) == 0)
		{
			offset -= cellSize;
			o.transform.localScale = new Vector2(o.transform.localScale.x * -1, o.transform.localScale.y);
			trigg.transform.localScale = new Vector2(trigg.transform.localScale.x * -1, trigg.transform.localScale.y);
		}
		else
		{
			offset += cellSize;
		}

		//calculate remaining sections and place remaining sections
		int sectionsLeft = sectionsWide - puzzlePlace - 1;
		placeSections(sectionsLeft, level, (puzzlePlace + 1));

		return;
	}

	/*
	 * @param number The number of sections to place
	 * @param level The current level
	 * @param roomPlace The section number the generating begins
	 */
	void placeSections(int number, int level, int roomPlace)
	{
		int currentBlock = 0;
		int numBlox = puzzBlox.Length;
		//pick starting room section
		int rand = randomSeed.Next(0, sectionPrefabs.Length);
		GameObject next = sectionPrefabs[rand];

		for (int i = 0; i < number; i++) //place level sections up until puzzle placement
		{
			GameObject current;
			current = Instantiate(next, new Vector2(offset, 0), Quaternion.identity); //place section
			placeObject placer = current.GetComponent<placeObject>();
			current.transform.parent = this.transform;

			if ((level % 2) == 0) //first section needs force flipped
				current.transform.localScale = new Vector2(current.transform.localScale.x * -1f, current.transform.localScale.y);

			//Block placement logic
			int blocksInSection = randomSeed.Next(1, 2);
			for (int j = 0; j < blocksInSection; j++)
			{
				if (i == number - 1 && currentBlock < numBlox - 1) //last section before puzzle and not all blocks required are placed
				{
					while (currentBlock < numBlox)
					{
						placer.Place(puzzBlox[currentBlock], offset, level);
						currentBlock++;
					}
					break;
				}
				else
				{
					rand = randomSeed.Next(1, 11);
					if (rand < 6 && currentBlock < numBlox) //place a block required by the puzzle
					{
						placer.Place(puzzBlox[currentBlock], offset, level);
						currentBlock++;
					}
					else //choose a random block
					{
						rand = randomSeed.Next(0, specialBlox.Length);
						placer.Place(specialBlox[rand], offset, level);
					}
				}
			}

			//Enemy placement logic
			int enemiesInSection = randomSeed.Next(0, 3);
			for (int j = 0; j < enemiesInSection; j++)
			{
				placer.Place(enemyPrefab, offset, level);
			}


			//Put map trigger under
			GameObject trigg = Instantiate(mapTrigger, new Vector2(offset, 0), Quaternion.identity);
			trigg.transform.parent = gameObject.transform;
			//increment offset
			if ((level % 2) == 0)
			{
				trigg.transform.localScale = new Vector2(trigg.transform.localScale.x * -1, trigg.transform.localScale.y);
			}

			//increment offset based on level
			if ((level % 2) == 0)
			{
				offset -= cellSize;
			}
			else
			{
				offset += cellSize;
			}

			if (i + roomPlace == sectionsWide-1) //place door if last section in room
				placer.PlaceDoor(door, level);

			//choose next section to place
			rand = randomSeed.Next(0, sectionPrefabs.Length);
			next = sectionPrefabs[rand];
		}
		return;
	}

	public void NextRoom()
	{
		if (roomNumber == 3)
		{
			if (Level == 3)
			{
				SceneManager.LoadScene("BossCutscene");
			}
			else 
			{ 
				roomNumber = 1;
				Level++;
			}
		}
		else
		{
			roomNumber++;
		}
		//clear positions array on player
		TimeStuff t = mainChar.GetComponent<TimeStuff>();
		t.positions = new ArrayList(t.STACK_MAX_SIZE);

		// Deletes room
		foreach (Transform child in this.transform)
		{
			Destroy(child.gameObject);
		}
		foreach (Transform child in PlacedBlocks.transform)
		{
			Destroy(child.gameObject);
		}

		if (Level == 1)
		{ 
			LevelOneUI.SetActive(true);
		}
		if (Level == 2)
		{
			LevelTwoUI.SetActive(true);
		}
		if (Level == 3)
		{
			LevelThreeUI.SetActive(true);
		}
	}

}
