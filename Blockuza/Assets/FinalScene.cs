using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalScene : MonoBehaviour {

	public float TypeSpeed;
	public GameObject DialogueBox;
	public GameObject Boss;
	public GameObject winText;
	public Text text;

	private List<string> deathMonologue1;
	private List<string> deathMonologue2;

	// Use this for initialization
	void Start () 
	{
		deathMonologue1 = new List<string>
		{ 
			"Wh...what have you done?!",
			"I cannot be defeated!",
			"I am the eternal God of Timeeeeeeeeeeeeeeeeeeeeee",
			"Guaaaaahhhhh"
		};

		deathMonologue2 = new List<string>
		{
			"You are unraveling everything...",
			"Time is doomed forever...",
			"Damn you,",
			"Servant.",
			"Of..",
			"Space..."
		};

		StartCoroutine(TextScroll(TypeSpeed));
	}

	// Update is called once per frame
	void Update () 
	{
		
	}

	private IEnumerator TextScroll(float typeSpeed)
	{
		DialogueBox.SetActive(true);

		for (int i = 0; i < deathMonologue1.Count; i++)
		{
			int letter = 0;
			text.text = "";

			while ((letter < deathMonologue1[i].Length))
			{
				text.text += deathMonologue1[i][letter];
				letter += 1;
				yield return new WaitForSeconds(typeSpeed);
			}

			yield return new WaitForSeconds(typeSpeed * 15);
		}

		Animator animator = Boss.transform.gameObject.GetComponent<Animator>();
		animator.Play("Boss-Death");

		// TODO: static effect

		for (int i = 0; i < deathMonologue2.Count; i++)
		{
			int letter = 0;
			text.text = "";

			while ((letter < deathMonologue2[i].Length))
			{
				text.text += deathMonologue2[i][letter];
				letter += 1;
				yield return new WaitForSeconds(typeSpeed);
			}

			yield return new WaitForSeconds(typeSpeed * 15);
		}

		Boss.SetActive(false);
		DialogueBox.SetActive(false);
		winText.SetActive(true);
	}
}
