using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {


	private GameObject room;
	//// Use this for initialization
	void Start () {
		room = GameObject.Find("Room");
	}


	void OnTriggerEnter2D(Collider2D col)
	{
		

		if (col.tag == "Player")
		{
			Debug.Log("Door hit");
			room.GetComponent<roomGeneration>().NextRoom();
		}
			

	}
}
