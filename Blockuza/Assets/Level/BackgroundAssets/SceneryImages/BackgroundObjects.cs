using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundObjects : MonoBehaviour {

	public Sprite[] forgroundObjects;
	public Sprite[] midgroundObjectsGround;
	public Sprite[] midgroundObjectsFloat;
	public Sprite[] backgroundObjects;

	public BackgroundContianer backgrounds;
	public roomGeneration generator;

	private int totalRoomSize; //in tiles

	public GameObject blankPrefab;
	public LayerMask collidableLayer;

	[Range(0,1)]
	public float chanceToPlaceForground;
	[Range(0,1)]
	public float chanceToPlaceMidGround;
	[Range(0,1)]
	public float chanceToPlaceBackGround;



	void Awake(){
		totalRoomSize = generator.sectionsWide * 20;
	}

	public void placeWallPapers(){
		Vector2 roomSize = new Vector2 (80, 10);
		int partition = backgrounds.Place (roomSize);

		int depth = -3;

		//first place background objects
		placeObjects(2, roomSize,chanceToPlaceBackGround,backgroundObjects,-3);
		//place midground floaters
		placeObjects(1, roomSize,chanceToPlaceMidGround,midgroundObjectsFloat,-2);

		placeObjects(1, roomSize,chanceToPlaceMidGround,midgroundObjectsGround,-2, true);
		placeObjects(1, roomSize,chanceToPlaceForground,forgroundObjects,-1, true);
	}

	public void placeObjects(int incrementValue, Vector2 roomSize, float chanceToPlace, Sprite[] objects,int depth, bool needsToBeGrounded = false){
		for (int x = 0; x < roomSize.x; x+=incrementValue) {
			for (int y = 0; y < roomSize.y; y+=incrementValue) {
				
				int chance = (int)Random.Range (0, 100);
				if (chance < chanceToPlace * 100) {
					bool grounded = true;
					if (needsToBeGrounded) {
						if(!checkForGround(new Vector2((float)x * .32f+.05f, (float)(y-1) * .32f -.05f)))
							grounded = false;
					}
					if (grounded) {
						int randIndex = Random.Range (0, objects.Length - 1);
						Sprite obj = objects [randIndex];
						SpriteRenderer renderer = blankPrefab.GetComponent<SpriteRenderer> ();
						renderer.sprite = obj;
						float height = renderer.bounds.size.y;
						float offset = .32f * 2;
						if ((float)y * .32f + offset < roomSize.y && (float)y * .32f - height + offset > 0) {
							renderer.sortingOrder = depth;
							Vector2 pos = new Vector2 ((float)x * .32f, (float)y * .32f);
							Instantiate (blankPrefab, pos, Quaternion.identity);
						}
					}
				}
			}
		}
	}

	public bool checkForGround(Vector2 checkPoint){
		RaycastHit2D hit = Physics2D.Raycast (checkPoint, Vector2.up,.02f,collidableLayer);
		if (hit.collider != null) {
			return true;
		} else {
			return false;
		}
	}
}
