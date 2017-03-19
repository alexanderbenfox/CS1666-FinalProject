using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Keys{
	UP, DOWN, LEFT, RIGHT
}

public enum Direction{
	UP, DOWN, LEFT, RIGHT, NONE
}

public class Controller : MonoBehaviour {
	public Direction lastDirection = Direction.NONE; //used in destroyable block placement

	private PhysicsObject physics;
	private Animator anim;
	private SpriteRenderer sprite;

	// Use this for initialization
	void Start () {
		physics = this.GetComponent<PhysicsObject> ();
		anim = this.GetComponent<Animator> ();
		sprite = this.GetComponent<SpriteRenderer> ();
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
			sprite.flipX = true;
			lastDirection = Direction.LEFT;
		}
		if (heldKeys.Contains (Keys.RIGHT)) {
			x = 1;
			sprite.flipX = false;
			lastDirection = Direction.RIGHT;
		}
		if (heldKeys.Contains (Keys.UP) && physics.checkGrounded())
			y = 5;

		if (x == 0)
			anim.Play ("Idle");
		else
			anim.Play ("Run");

		physics.Move (x, y);
	}
}
