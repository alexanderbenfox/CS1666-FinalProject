using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class makeBlock : MonoBehaviour {
	public GameObject DestroyBlockPrefab;
	public Controller controller;
	// Use this for initialization
	void Start(){
		controller= gameObject.GetComponent<Controller>();
	}
	void Update(){
		if (Input.GetMouseButtonDown (0)) {
			if (controller.lastDirection == 0 || controller.lastDirection == 1) {
				SpawnBlock (new Vector3 (gameObject.transform.position.x + .5f, gameObject.transform.position.y-.25f, 0));
			} else {
				SpawnBlock (new Vector3 (gameObject.transform.position.x -.5f, gameObject.transform.position.y-.25f, 0));
			}
		}
	}
	public void SpawnBlock(Vector3 position){
		GameObject blockInst = Instantiate (DestroyBlockPrefab,position,gameObject.transform.rotation);
	}
}
