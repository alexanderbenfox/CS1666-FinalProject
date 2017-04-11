using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomUIscript : MonoBehaviour {
	public GameObject Room;
	public GameObject UI;
	private Text txt;

	void Start () {
		txt = this.GetComponent<Text>();
	}

	void OnEnable()
	{
		Room.SetActive(false);
		int level = Room.GetComponent<roomGeneration>().Level;
		int room = Room.GetComponent<roomGeneration>().roomNumber;

		string msg = "Room " + level + "-" + room;
		txt.text = msg;
	}

	void Update()
	{
		if (Input.anyKey)
		{
			Room.SetActive(true);
		}
	}

}
