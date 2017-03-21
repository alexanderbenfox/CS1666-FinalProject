using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class makeBlock : MonoBehaviour {
	public GameObject DestroyBlockPrefab;
	public Controller controller;
	private TileSelector selector;

	// Use this for initialization
	void Start(){
		controller= gameObject.GetComponent<Controller>();
		selector = this.GetComponentInChildren<TileSelector> ();
	}
	void Update(){
		if (controller.checkKeyPressed (Keys.ACTION)) {
			if (selector.consume) {
				selector.consumeBlock ();
			} else {
				SpawnBlock (selector.transform.position);
			}
		}
	}
	public void SpawnBlock(Vector3 position){
		GameObject blockInst = Instantiate (DestroyBlockPrefab,position,gameObject.transform.rotation);
	}
}
