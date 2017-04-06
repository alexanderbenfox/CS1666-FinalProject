using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {

	public GameObject roomOne;
	public GameObject roomTwo;
	public GameObject roomThree;
	public Transform mainChar;

	private GameObject current;
	//// Use this for initialization
	void Start () {
		current = roomOne;
	}


	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.transform.tag == "Player")
		{
			Debug.Log("Player");
			if (current == roomOne)
			{
				roomOne.SetActive(false);

				foreach (Transform child in roomOne.transform)
				{
					Destroy(child.gameObject);
				}

				roomTwo.SetActive(true);
				mainChar.position = new Vector2(0f, 0.32f);
				current = roomTwo;
			}
			else if (current == roomTwo)
			{
				roomTwo.SetActive(false);

				foreach (Transform child in roomTwo.transform)
				{
					Destroy(child.gameObject);
				}

				roomThree.SetActive(true);
				mainChar.position = new Vector2(0f, 0.32f);
				current = roomThree;
			}
			else if (current == roomThree) //Change scene
			{
				roomThree.SetActive(false);

				foreach (Transform child in roomThree.transform)
				{
					Destroy(child.gameObject);
				}

				roomOne.SetActive(true);
				mainChar.position = new Vector2(0f, 0.32f);
				current = roomOne;
			}	
		}

	}
}
