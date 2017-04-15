using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle2Trigger : MonoBehaviour {
	public Puzzle thisPuzzle;
	// Use this for initialization
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.transform.tag == "Player")
		{
			thisPuzzle.Triggered = true;
			Debug.Log(col.transform.tag);
			Debug.Log("Triggered");
		}

	}
}
