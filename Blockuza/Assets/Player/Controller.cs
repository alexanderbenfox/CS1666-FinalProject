using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Keys{
	UP, DOWN, LEFT, RIGHT
}

public class Controller : MonoBehaviour {
	public int lastDirection = 0;//0 for no move, 1 for right, 2 for left used in destroyable block placement
	private PhysicsObject physics;

	// Use this for initialization
	void Start () {
		physics = this.GetComponent<PhysicsObject> ();	
	}

	List<Keys> getKeyInput(){
		List<Keys> heldKeys = new List<Keys>();
		if (Input.GetKey (KeyCode.LeftArrow)) {
			heldKeys.Add (Keys.LEFT);
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			heldKeys.Add (Keys.RIGHT);
		}
		if (Input.GetKey (KeyCode.UpArrow)) {
			heldKeys.Add (Keys.UP);
		}
		if (Input.GetKey (KeyCode.DownArrow)) {
			heldKeys.Add (Keys.DOWN);
		}
		return heldKeys;
	}
	
	// Update is called once per frame
	void Update () {
		List<Keys> heldKeys = getKeyInput ();
		Move (heldKeys);
	}

	public void Move(List<Keys> heldKeys){
		float x = 0;
		float y = 0;
		if (heldKeys.Contains (Keys.LEFT)) {
			x = -1;
			lastDirection = 2;
		}
		if (heldKeys.Contains (Keys.RIGHT)) {
			x = 1;
			lastDirection = 1;
		}
		if (heldKeys.Contains (Keys.UP) && physics.checkGrounded())
			y = 5;

		physics.Move (x, y);
	}
}
