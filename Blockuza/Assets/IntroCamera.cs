using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroCamera : MonoBehaviour 
{
	public float Speed;
	public float TypeSpeed;
	public float SpriteHeight;
	public Image image;
	public GameObject DialogueBox;
	public Text text;

	private string dialogue;
	private bool playing;

	// Use this for initialization
	void Start () 
	{
		dialogue = 
			"Scale the tower and defeat the God of Time.\n" +
			"Do this for me, my servant...";

		playing = false;
	}
	
	// Update is called once per frame
	void Update () 
	{

		if (!playing)
		{	
			StartCoroutine(TextScroll(TypeSpeed));

			playing = true;
		}

		image.transform.position = new Vector3(image.transform.position.x, Mathf.Lerp(image.transform.position.y, 
			image.transform.position.y - SpriteHeight, Speed * Time.deltaTime), image.transform.position.z);
	}

	private IEnumerator TextScroll(float typeSpeed)
	{
		yield return new WaitForSeconds(typeSpeed * 5);

		DialogueBox.SetActive(true);

		int letter = 0;
		text.text = "";

		while ((letter < dialogue.Length))
		{
			text.text += dialogue[letter];
			letter += 1;
			yield return new WaitForSeconds(typeSpeed);
		}

		yield return new WaitForSeconds(typeSpeed * 15);


		DialogueBox.SetActive(false);

		SceneManager.LoadScene("RoomGenTest");
	}
}
