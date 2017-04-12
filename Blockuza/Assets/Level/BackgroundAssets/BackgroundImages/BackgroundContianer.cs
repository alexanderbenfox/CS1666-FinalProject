using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundContianer : MonoBehaviour {

	public Sprite[] red1;
	public Sprite[] red2;
	public Sprite[] yellow1;
	public Sprite[] yellow2;

	public GameObject blankPrefab;

	public int Place(Vector2 roomSize){
		//divide room in to 2 sections
		int rand = (int)Random.Range(3,roomSize.y-3);

		Sprite[] placerArray;
		Sprite[] savedArrayRed, savedArrayYellow;
		int randP = Random.Range (0, 2);
		if (randP == 0) {
			placerArray = red1;
		} else {
			placerArray = red2;
		}

		savedArrayRed = placerArray;
		savedArrayYellow = placerArray;

		bool switched = false;


		for (int x = 0; x < roomSize.x; x++) {
			for (int y = 0; y < roomSize.y; y++) {
				Sprite instantiationSprite;
				if (y >= rand && !switched){ // switch to yellow blocks{
					randP = Random.Range (0, 2);
					if (randP == 0) {
						savedArrayYellow = yellow1;
					} else {
						savedArrayYellow = yellow2;
					}
					switched = true;
				}

				if (y >= rand) {
					placerArray = savedArrayYellow;
				}
				else{
					placerArray = savedArrayRed;
				}

				if (y == 0 || y == rand) {
					instantiationSprite = placerArray [0];
				} else if (y == rand - 1 || y == roomSize.y - 1) {
					instantiationSprite = placerArray [2];
				} else {
					instantiationSprite = placerArray [1];
				}

				blankPrefab.GetComponent<SpriteRenderer> ().sprite = instantiationSprite;
				blankPrefab.GetComponent<SpriteRenderer> ().sortingOrder = -4;
				Vector2 pos = new Vector2 ((float)x * .32f, (float)(y+1) * .32f);
				Instantiate (blankPrefab, pos, Quaternion.identity);
			}
		}
		return rand;

	}
}
