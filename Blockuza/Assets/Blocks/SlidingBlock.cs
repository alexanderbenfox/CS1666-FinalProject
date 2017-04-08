using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingBlock : BlockBehaviour {
	private Vector3 initialPosition;
	public float speed;
	public float slideDistance;
	public int direction;
	public GameObject player;
	public bool moving = false;
	public bool moving2=false;
	[SerializeField]
	private Controller control;
	void Awake(){
		player = GameObject.Find ("Player");
		type = BlockType.Sliding;
		snap = this.GetComponent<SnapToGrid> ();
		initialPosition = gameObject.transform.position;
		control = player.GetComponent<Controller> ();

		if (this.gameObject.GetComponent<PhysicsObject> () != null) {
			physics = this.gameObject.GetComponent<PhysicsObject> ();
		}
	}
	void Start(){
		snap.enabled = false;
		if (control.moving) {
			if (control.getCursorDirection ().Equals (Direction.RIGHT) || control.getCursorDirection ().Equals (Direction.DOWN_RIGHT) || control.getCursorDirection ().Equals (Direction.UP_RIGHT)) {
				direction = 1;
				moving = true;
				moving2 = true;
			} else {
				direction = -1;
				moving = true;
				moving2 = true;
			}
		} else {
			direction = 0;
		}
	}
	void Update () {
		
		if (gameObject.transform.position.x >= (initialPosition.x + (direction * slideDistance))&& direction==1) {
			moving = false;
			moving2 = false;


		}
		if (gameObject.transform.position.x <= (initialPosition.x + (direction * slideDistance)) && direction == -1) {
			moving = false;
			moving2 = false;

		}

		if (moving) {
			gameObject.GetComponent<PhysicsObject> ().Move ((direction * slideDistance * speed), 0);
			if (gameObject.GetComponent<PhysicsObject> ().checkRightCollision ()&&direction ==1) {
				//Debug.Log ("Right Collision");
				moving = false;


			}
			if (gameObject.GetComponent<PhysicsObject> ().checkLeftCollision () && direction == -1) {
				//Debug.Log ("Left Collision");
				moving = false;


			}
		} else if (!moving && moving2) {
			gameObject.GetComponent<PhysicsObject> ().Move ((direction * slideDistance * speed), 0);
			if (gameObject.GetComponent<PhysicsObject> ().checkRightCollision () && direction == 1) {
				//Debug.Log ("Right Collision 2");
				moving2 = false;


			} else if (gameObject.GetComponent<PhysicsObject> ().checkLeftCollision () && direction == -1) {
				//Debug.Log ("Left Collision 2");
				moving2 = false;


			} else {
				moving = true;
			}
		}else {
			gameObject.GetComponent<PhysicsObject> ().Move (0, 0);
		}
	}
}
