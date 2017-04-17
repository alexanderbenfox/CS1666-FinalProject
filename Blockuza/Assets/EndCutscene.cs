using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndCutscene : MonoBehaviour 
{
	public Camera Camera;
	public GameObject Boss;
	public GameObject DialogueBox;
	public Text text;
	public float TypeSpeed;
	public GameObject Player;

	private Controller controller;
	private List<string> evilMonologue;
	private bool playing = false;

	// Use this for initialization
	void Start ()	
	{
		evilMonologue = new List<string> 
		{ 
			"Well, well, well. Here we are at last.",
			"I assume my evil brother, the God of Space, sent you",
			"to assassinate me.",
			"Typical. He was always trying to invade my", 
			"personal Space.",
			"HAHAH.",
			"Get it?",
			"Because he's the God of...",
			"Whatever.",
			"...",
			"Before you kill me, would you permit me one final", 
			"smoke?"
		};

		controller = Player.GetComponent<Controller>();
	}

	// Update is called once per frame
	void Update () 
	{
		Vector3 screenPoint = Camera.WorldToViewportPoint(Boss.transform.position);
		bool onScreen = screenPoint.x <= 0.8;

		if (onScreen && !playing) 
		{
			playing = true;
			StartCoroutine(TextScroll(TypeSpeed));
		} 
	}

	private IEnumerator TextScroll(float typeSpeed)
	{
		DialogueBox.SetActive(true);

		for (int i = 0; i < evilMonologue.Count; i++)
		{
			int letter = 0;
			text.text = "";

			while ((letter < evilMonologue[i].Length))
			{
				text.text += evilMonologue[i][letter];
				letter += 1;
				yield return new WaitForSeconds(typeSpeed);
			}

			yield return new WaitForSeconds(typeSpeed * 15);
		}

		int letter2 = 0;
		text.text = "";

		while ((letter2 < "...".Length))
		{
			text.text += "..."[letter2];
			letter2 += 1;
			yield return new WaitForSeconds(typeSpeed * 30);
		}

		Animator animator = Boss.transform.gameObject.GetComponent<Animator>();
		animator.Play("boss-cutscene-2");
		yield return new WaitForSeconds(2);
		animator.Play("boss-cutscene-3");

		yield return new WaitForSeconds(typeSpeed * 15);

		int letter3 = 0;
		text.text = "";

		while ((letter3 < "Fohohohohohohohohoho".Length))
		{
			text.text += "Fohohohohohohohohoho"[letter3];
			letter3 += 1;
			yield return new WaitForSeconds(typeSpeed / 2);
		}

		yield return new WaitForSeconds(typeSpeed * 5);

		DialogueBox.SetActive(false);

		SceneManager.LoadScene("BossTest");
	}
}
