using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class makeBlock : MonoBehaviour {
	public GameObject DestroyBlockPrefab,SlideBlockPrefab,StickyBlockPrefab;
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
		if (selector.savedBlocks.Count > 0) {
			BlockType[] blockList = selector.savedBlocks.ToArray();
			BlockType newBlock = blockList [selector.savedBlocks.Count - 1];
			selector.savedBlocks.RemoveAt (selector.savedBlocks.Count - 1);
			if (newBlock == BlockType.Sliding) {
				GameObject blockInst = Instantiate (SlideBlockPrefab, position, gameObject.transform.rotation);
			}else if (newBlock == BlockType.Sticky) {
				GameObject blockInst = Instantiate (StickyBlockPrefab, position, gameObject.transform.rotation);
			} else {
				GameObject blockInst = Instantiate (DestroyBlockPrefab, position, gameObject.transform.rotation);
			}
		}
	}
}
