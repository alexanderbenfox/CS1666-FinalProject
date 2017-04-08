using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class placeObject : MonoBehaviour {
	public float[] objLocations;//see getCoords(double coordinate) for description 
	public Boolean[] objAbove;
	private float x; // x axis offset

	// Finds index to place the object, then calls validIndex() to place it
	public void Place (GameObject obj, float offset, int level) 
	{
		x = offset;
		System.Random rand = new System.Random();
		int index = rand.Next(0, objLocations.Length);
		Boolean found = false;
		while (!found)
		{
			if (objAbove[index] == false) //nothing above this square, so place the object
			{
				objAbove[index] = true;
				validIndex(index, obj, level);
				found = true;
			}
			else //try next index
			{
				if (index+1 >= objLocations.Length) //restart from beginning if at last index
					index = 0;
				else
					index++;
			}

		}
		return;
	}

	//Places character all the way to the left or right
	public void PlaceDoor(GameObject door, int level)
	{
		int index = objLocations.Length - 1;
		objAbove[index] = true;
		validIndex(index, door,level);
		return;
	}

	// Instantiates the object above coords of index found in Place()
	private void validIndex(int index, GameObject obj, int level)
	{
		int[] coords = getCoords(objLocations[index]);

		float xx;
		float yy = .32f * (float)(coords[1] + 1);

		GameObject c;
		if (level % 2 == 0)
		{
			xx = x - (.32f * (float)coords[0]);
			c = Instantiate(obj, new Vector2(xx, yy), Quaternion.identity);
		}
		else
		{
			xx = x + (.32f * (float)coords[0]);
			c = Instantiate(obj, new Vector2(xx, yy), Quaternion.identity); 
		}
		c.transform.parent = this.transform;
		return;
	}

	/*
	 * x coordinate is value left of decimal
	 * y coordinate is value right of decimal
	 * @return coords[0] is x value, coords[1] is y value
	 */
	private int[] getCoords(float coordinate)
	{
		int[] coords = new int[2];
		coords[0] = (int)coordinate; // x value
		coords[1] = (int)(((coordinate % 1) + .001f) * 10f); // y value
		return coords;
	}



}
