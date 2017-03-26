using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickBlock : BlockBehaviour {

	void Awake(){
		
		type = BlockType.Sticky;
		snap = this.GetComponent<SnapToGrid> ();

	}
}
