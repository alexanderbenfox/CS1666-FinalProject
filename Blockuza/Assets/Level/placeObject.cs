using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class placeObject : MonoBehaviour {
	public double[] objLocations;//see getCoords(double coordinate) for description 
	public Boolean[] objAbove;

	// Finds index to place the object, then calls validIndex() to place it
	public void Place (GameObject obj) 
	{
		System.Random rand = new System.Random();
		int index = rand.Next(0, objLocations.Length);

		while (index < objLocations.Length)
		{
			if (objAbove[index] == false) //nothing above this square, so place the object
			{
				validIndex(index, obj);
				break;
			}
			else //try next index
			{
				if (index++ >= objLocations.Length) //restart from beginning if at last index
				{
					index = 0;
				}
			}

		}
			
	}

	// Instantiates the object above coords of index found in Place()
	private void validIndex(int index, GameObject obj)
	{
		objAbove[index] = true;
		int[] coords = getCoords(objLocations[index]);
		Instantiate(obj, new Vector2(coords[0], coords[1]+1), Quaternion.identity);
	}

	/*
	 * x coordinate is value left of decimal
	 * y coordinate is value right of decimal
	 * @return coords[0] is x value, coords[1] is y value
	 */
	private int[] getCoords(double coordinate)
	{
		int[] coords = new int[2];
		coords[0] = (int)coordinate; // x value
		coords[1] = (int)((coordinate - coords[0]) * 10); // y value
		return coords;
	}



}
