﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomUIscript : MonoBehaviour {
	private GameObject Room;
	private GameObject Player;
	private GameObject UI;
	public Text txt;

	void OnEnable()
<<<<<<< HEAD:Blockuza/Assets/GUI/RoomUIscript.cs
	{	
		Room = GameObject.Find("Room");
		Player = GameObject.Find("Player");
		UI = GameObject.FindGameObjectWithTag("RoomUI");
=======
	{
		//Room.SetActive(false);
>>>>>>> a58f0c1cad92b126bc9f8fd04f21b758f1800f85:Blockuza/Assets/Level/RoomUIscript.cs
		int level = Room.GetComponent<roomGeneration>().Level;
		int room = Room.GetComponent<roomGeneration>().roomNumber;
		Room.SetActive(false);
		Player.SetActive(false);
		string msg = "Room " + level + "-" + room;
		txt.text = msg;
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Player.SetActive(true);
			Room.SetActive(true);
			UI.SetActive(false);
		}
	}

}