using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class makeBlock : MonoBehaviour {
	public GameObject DestroyBlockPrefab,SlideBlockPrefab,StickyBlockPrefab;
	public Controller controller;
	public Transform PlacedBlocks;
	private TileSelector selector;

	// Use this for initialization
	void Start(){
		controller= gameObject.GetComponent<Controller>();
		selector = this.GetComponentInChildren<TileSelector> ();
	}
	void Update(){
		if (Input.GetMouseButtonDown(0)) {
			if (selector.consume) {
				selector.consumeBlock ();
				selector.consume = false;
			} else {
				SpawnBlock (selector.transform.position);
			}
		}
	}
	public void SpawnBlock(Vector3 position){
		Debug.Log ("Block Count: " + selector.savedBlocks.Count);
		if (selector.savedBlocks.Count > 0) {
			BlockType[] blockList = selector.savedBlocks.ToArray();
			BlockType newBlock = blockList [selector.savedBlocks.Count - 1];
			selector.savedBlocks.RemoveAt (selector.savedBlocks.Count - 1);
			GameObject blockInst;
			if (newBlock == BlockType.Sliding) {
				blockInst = Instantiate (SlideBlockPrefab, position, gameObject.transform.rotation);
				blockInst.transform.parent = PlacedBlocks; 
			}else if (newBlock == BlockType.Sticky) {
				blockInst = Instantiate (StickyBlockPrefab, position, gameObject.transform.rotation);
				blockInst.transform.parent = PlacedBlocks;
			} else {
				blockInst = Instantiate (DestroyBlockPrefab, position, gameObject.transform.rotation);
				blockInst.transform.parent = PlacedBlocks;
			}
			blockInst.tag = "PlayerBlock";
		}
	}
}
