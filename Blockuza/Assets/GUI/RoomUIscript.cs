using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomUIscript : MonoBehaviour {
	private GameObject Room;
	private GameObject Player;
	private GameObject UI;
	public Text txt;
	public float sec;

	void OnEnable()
	{	
		Room = GameObject.Find("Room");
		Player = GameObject.Find("Player");
		UI = GameObject.FindGameObjectWithTag("RoomUI");
		int level = Room.GetComponent<roomGeneration>().Level;
		int room = Room.GetComponent<roomGeneration>().roomNumber;
		Room.SetActive(false);
		Player.SetActive(false);
		string msg = "Room " + level + "-" + room;
		txt.text = msg;
		StartCoroutine(LateCall());
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

	IEnumerator LateCall()
	{

		yield return new WaitForSeconds(sec);
		Player.SetActive(true);
		Room.SetActive(true);
		UI.SetActive(false);

	}

}
