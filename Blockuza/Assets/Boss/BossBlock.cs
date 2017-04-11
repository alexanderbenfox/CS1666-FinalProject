using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBlock : MonoBehaviour {
	private PhysicsObject physics;
	public float Speed;
	bool waitFrame=false;
	public bool moving;
	public bool waitingLeft=true;
	public bool waitingTop=true;
	public bool waitingRight=true;
	public bool waitingGrounded=true;
	public int state;//Assuming clockwise motion, 1 is moving right, on top of a block, 2 is moving down the right side of a block, 3 is moving left under a block, and 4 is moving up the left side of a block
	// Use this for initialization
	void Start () {
		state = 1;
		physics = gameObject.GetComponent<PhysicsObject> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (state == 1) {
			Debug.Log ("State 1");
			if (!physics.checkGrounded () && !waitingGrounded) {
				if (waitFrame) {
					state = 2;
					moving = false;
					physics.Move (0, 0);
					waitFrame = false;
					waitingGrounded = true;
				} else {
					waitFrame = true;
				}
			} else if (physics.checkRightCollision ()) {
				if (waitFrame) {
					state = 4;
					moving = false;
					physics.Move (0, 0);
					waitFrame = false;
					waitingGrounded = true;
				} else {
					waitFrame = true;
				}
			} else {
				if (physics.checkGrounded()) {
					waitingGrounded = false;
				}

					physics.Move (1.5f, 0);

			}
		}
		if (state == 2) {
			Debug.Log ("State 2");
			if (physics.checkGrounded ()) {
				if (waitFrame) {
					state = 1;
					moving = false;
					physics.Move (0, 1.5f);
					waitFrame = false;
					waitingLeft = true;
				} else {
					waitFrame = true;
				}
			} else if (!physics.checkLeftCollision () && !waitingLeft) {
				if (waitFrame) {
					state = 3;
					moving = false;
					physics.Move (0, 1.5f);
					waitingLeft = true;
					waitFrame = false;
				} else {
					waitFrame = true;
				}
			} else {
				if (physics.checkLeftCollision ()) {
					waitingLeft = false;
				}
				if (!moving) {
					physics.Move (0, -1.5f);
					moving = true;
				}
			}
		}
		if (state == 3) {
			Debug.Log ("State 3:");
			if (!physics.checkTopCollision ()&& !waitingTop) {
				Debug.Log("no top no waiting top");
				if (waitFrame) {
					state = 4;
					moving = false;
					physics.Move (0, 0);
					waitFrame = false;
					waitingTop = true;
				} else {
					waitFrame = true;
				}
			} else if (physics.checkLeftCollision ()) {
				Debug.Log("Left Collision");
				if (waitFrame) {
					state = 2;
					moving = false;
					physics.Move (0, 0);
					waitFrame = false;
					waitingTop = true;
				} else {
					waitFrame = true;
				}
			} else {
				if (physics.checkTopCollision ()) {
					waitingTop = false;
				}

					physics.Move (-1.5f, 0);
					moving = true;

			}
		}
		if (state == 4) {
			Debug.Log ("State 4");
			if (!physics.checkRightCollision ()&& !waitingRight) {
				if (waitFrame) {
					state = 1;
					moving = false;
					physics.Move (0, -1.5f);
					waitFrame = false;
					waitingRight = true;
				} else {
					waitFrame = true;
				}
			} else if (physics.checkTopCollision ()) {
				
				if (waitFrame) {
					state = 3;
					moving = false;
					physics.Move (0, -1.5f);
					waitFrame = false;
					waitingRight = true;
				} else {
					waitFrame = true;
				}
			} else {
				if (physics.checkRightCollision ()) {
					waitingRight = false;
				}
				if (!moving) {
					physics.Move (0, 1.5f);
					moving = true;
				}
			}
		}
	}
}
