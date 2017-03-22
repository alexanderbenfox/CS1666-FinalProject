using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingBlock : MonoBehaviour {

	private SnapToGrid snap;
	private Vector3 initialPosition;
	public float speed;
	public float slideDistance;
	public int direction;
	public GameObject player;
	public bool moving = true;
	void Awake(){
		player = GameObject.FindGameObjectWithTag ("Player");
		snap = this.GetComponent<SnapToGrid> ();
		initialPosition = gameObject.transform.position;
		if (player.GetComponent<Controller> ().getCursorDirection () == Direction.LEFT || player.GetComponent<Controller> ().getCursorDirection () == Direction.DOWN_LEFT || player.GetComponent<Controller> ().getCursorDirection () == Direction.UP_LEFT) {
			direction = -1;
		} else {
			direction = 1;
		}
	}
	void FixedUpdate () {
		snap.enabled = false;
		if (gameObject.transform.position.x >= (initialPosition.x + (direction * slideDistance))) {
			moving = false;
		}
		if (gameObject.GetComponent<PhysicsObject> ().checkRightCollision ()&&direction==1) {
			Debug.Log ("Right Collision");
			moving = false;
		}
		if(gameObject.GetComponent<PhysicsObject>().checkLeftCollision()&&direction==-1)
			Debug.Log ("Left Collision");
		if (moving) {
			gameObject.transform.position = new Vector3 (gameObject.transform.position.x + speed * direction * Time.deltaTime, gameObject.transform.position.y, 0);
		}
	}
}
