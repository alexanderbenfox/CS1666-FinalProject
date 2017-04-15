using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomUIscript : MonoBehaviour {
	private GameObject Room;
	private GameObject Player;
	public GameObject UI;
	public Sprite roomOne;
	public Sprite roomTwo;
	public Sprite roomThree;
	public float sec;

	void OnEnable()
	{	
		Room = GameObject.Find("Room");
		Player = GameObject.Find("Player");
		int room = Room.GetComponent<roomGeneration>().roomNumber;
		Room.SetActive(false);
		Player.SetActive(false);
		if (room == 1)
		{
			this.GetComponent<Image>().sprite = roomOne;
		}
		else if (room == 2)
		{
			this.GetComponent<Image>().sprite = roomTwo;
		}
		else if (room == 3)
		{
			this.GetComponent<Image>().sprite = roomThree;
		}

		StartCoroutine(LateCall());
	}

	IEnumerator LateCall()
	{

		yield return new WaitForSeconds(sec);
		Player.SetActive(true);
		Room.SetActive(true);
		UI.SetActive(false);

	}

}
