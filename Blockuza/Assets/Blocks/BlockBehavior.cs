using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBehavior : MonoBehaviour {

	private SnapToGrid snap;

	void Awake(){
		snap = this.GetComponent<SnapToGrid> ();
	}

	void FixedUpdate () {
		if(snap.snapped)
			snap.enabled = false;
	}
}
