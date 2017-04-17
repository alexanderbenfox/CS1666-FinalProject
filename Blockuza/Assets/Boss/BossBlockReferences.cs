using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossBlockReferences : MonoBehaviour {

	public GameObject boss32;
	public GameObject boss64;
	public GameObject boss128;
	public bool beingCreated=false;
	public int boss32Count=0;
	public int boss64Count=0;
	public int bossLivesLeft = 2;
	public bool destroying=true;
	public bool Victory = false;
	private bool waitFrame;
	void Start(){
		
	}
	void Update(){
		if (waitFrame) {
			beingCreated = false;
			waitFrame = false;
		}
		if (beingCreated) {
			waitFrame = true;
		}
		if (destroying && boss32Count==4) {
			destroying = false;
		}
		if (!destroying && boss32Count == 0 && boss64Count == 0) {
			destroying = true;
		}
		if (bossLivesLeft == 0) {
			Victory = true;
			SceneManager.LoadScene (3);
		}
	}
}
