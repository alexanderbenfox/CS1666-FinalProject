using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBlockReferences : MonoBehaviour {

	public GameObject boss32;
	public GameObject boss64;
	public GameObject boss128;
	public bool beingCreated=false;
	private bool waitFrame;
	void Update(){
		if (waitFrame) {
			beingCreated = false;
			waitFrame = false;
		}
		if (beingCreated) {
			waitFrame = true;
		}
	}
}
