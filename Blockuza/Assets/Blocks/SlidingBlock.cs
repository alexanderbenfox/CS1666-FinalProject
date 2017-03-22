using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingBlock : BlockBehaviour {
	private Vector3 initialPosition;
	public float speed;
	public float slideDistance;
	public int direction;
	public GameObject player;
	public bool moving = true;
	void Awake(){
		player = GameObject.FindGameObjectWithTag ("Player");
		type = BlockType.Sliding;
		snap = this.GetComponent<SnapToGrid> ();
		initialPosition = gameObject.transform.position;
		if (player.GetComponent<Controller> ().getCursorDirection () == Direction.LEFT || player.GetComponent<Controller> ().getCursorDirection () == Direction.DOWN_LEFT || player.GetComponent<Controller> ().getCursorDirection () == Direction.UP_LEFT) {
			direction = -1;
		} else {
			direction = 1;
		}
		if (this.gameObject.GetComponent<PhysicsObject> () != null) {
			physics = this.gameObject.GetComponent<PhysicsObject> ();
		}
	}
	void Start(){
		snap.enabled = false;
	}
	void Update () {
		
		if (gameObject.transform.position.x >= (initialPosition.x + (direction * slideDistance))) {
			moving = false;

		}

		if (physics.checkRightCollision ()) {
			moving = false;
		}

		if (moving) {
			gameObject.GetComponent<PhysicsObject> ().Move (initialPosition.x + (direction * slideDistance*speed), 0);
			if (gameObject.GetComponent<PhysicsObject> ().checkRightCollision()) {
				Debug.Log ("Right Collision");
				moving = false;


			}
			if (gameObject.GetComponent<PhysicsObject> ().checkLeftCollision () && direction == -1) {
				Debug.Log ("Left Collision");
				moving = false;


			}
		} else {
			gameObject.GetComponent<PhysicsObject> ().Move (0, 0);
		}
	}
}
