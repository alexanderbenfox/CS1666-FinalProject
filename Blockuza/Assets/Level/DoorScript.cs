using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {

	private bool hit = false;

	private GameObject room;
	//// Use this for initialization
	void Start () {
		room = GameObject.FindGameObjectWithTag("Room");

	}


	void OnTriggerEnter2D(Collider2D col)
	{
		Debug.Log("Door hit");

		if (col.tag == "Player" && !hit)
		{
			room.GetComponent<roomGeneration>().NextRoom();
		}
			

	}
}
