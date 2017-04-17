using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueGenerator : MonoBehaviour 
{
	public float TypeSpeed;
	public GameObject DialogueBox;
	public GameObject Room;
	public Text text;

	private List<string> oneLiners;
	private bool isTyping;
	private bool cancelTyping;
	private roomGeneration roomGen;
	private int oldRoomNum;

	// Use this for initialization
	void Start () 
	{
		oneLiners = new List<string>
		{ 
			"Fohoho... You will lose.", 
			"Bow before the God of Time.",
			"Muahahahahaha!!",
			"You dare come into MY realm?!",
			"Are these the skills of the Space God?",
			"Stop this insolence at once!",
			"Damn you, servant of Space."
		};

		oldRoomNum = 1;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Room != null)
		{
			roomGen = Room.GetComponent<roomGeneration>();
		}

		int roomNum = roomGen.roomNumber;

		if (roomNum > oldRoomNum) 
		{
			int rand = Random.Range(0, oneLiners.Count);
			StartCoroutine(TextScroll((string)oneLiners[rand], TypeSpeed));

			oldRoomNum = roomNum;
		}
	}

	private IEnumerator TextScroll(string textToScroll, float typeSpeed)
	{
		DialogueBox.SetActive(true);
		int letter = 0;
		text.text = "";

		while ((letter < textToScroll.Length))
		{
			text.text += textToScroll[letter];
			letter += 1;
			yield return new WaitForSeconds(typeSpeed);
		}
			
		yield return new WaitForSeconds(typeSpeed * 10);
		DialogueBox.SetActive(false);
	}
}
