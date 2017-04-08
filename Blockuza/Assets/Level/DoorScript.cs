using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {

	public Transform mainChar;
	private int roomNum = 1;
	private int Level = 1;

	private GameObject current;
	//// Use this for initialization
	void Start () {
	}


	void OnTriggerEnter2D(Collider2D col)
	{
		if (roomNum == 3)
		{
			roomNum = 1;
			Level++;
		}
		else
		{
			roomNum++;
		}
			

	}
}
