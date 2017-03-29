using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasmPuzzle : Puzzle {

	public GameObject Bridge;
	void Start(){
		Bridge.SetActive (false);
	}
	// Update is called once per frame
	void Update () {
		if (Triggered) {
			Bridge.SetActive (true);
		} else {
			Bridge.SetActive (false);
		}
	}
}
