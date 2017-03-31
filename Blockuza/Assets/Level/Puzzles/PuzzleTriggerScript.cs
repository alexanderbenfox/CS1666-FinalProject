using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTriggerScript : MonoBehaviour {
	public Puzzle thisPuzzle;
	// Use this for initialization
	void OnTriggerEnter2D(Collider2D col){
		if (col.transform.tag == "Player" ||col.transform.tag == "Shuriken"){
			Debug.Log("Player :)");
		
		}else
		{
			thisPuzzle.Triggered = true;
			Debug.Log(col.transform.tag);
			Debug.Log("Triggered");
		}

	}
	void OnTriggerExit2D(Collider2D col){
		if (col.transform.tag == "Player"|| col.transform.tag == "Shuriken")
		{
			Debug.Log("Player :)");

		}
		else
		{
			thisPuzzle.Triggered = false;
			Debug.Log(col.transform.tag);
			Debug.Log("Triggered");
		}
	}
}
