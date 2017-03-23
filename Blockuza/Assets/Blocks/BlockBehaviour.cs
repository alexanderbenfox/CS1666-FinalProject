using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockType{
	Destroyable, Sliding
};

public class BlockBehaviour : MonoBehaviour {

	protected SnapToGrid snap;
	[SerializeField]
	protected BlockType type;
	protected PhysicsObject physics;

	void Awake(){
		type = BlockType.Destroyable;
		snap = this.GetComponent<SnapToGrid> ();
	}

	void FixedUpdate () {
		if(snap.snapped)
			snap.enabled = false;
	}

	public BlockType getType(){
		return type;
	}
}
