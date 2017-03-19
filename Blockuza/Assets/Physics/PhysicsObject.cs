using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Side{
	RIGHT, LEFT, TOP, BOTTOM, NONE
}

[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent (typeof(BoxCollider2D))]

//Movement totally based on moving the transform - I don't like unity physics for platformers
//In order to make anything work with this physics object, DO NOT CHANGE THE SCALE OF THE OBJECT

public class PhysicsObject : MonoBehaviour {

	class Box{
		public float left;
		public float right;
		public float top;
		public float bottom;

		private Vector2 offset;

		public Box(BoxCollider2D collider){
			Transform t = collider.transform;
			left = t.position.x - collider.size.x/2 + collider.offset.x;
			right = t.position.x + collider.size.x/2 + collider.offset.x;
			top = t.position.y + collider.size.y/2 + collider.offset.y;
			bottom = t.position.y - collider.size.y/2 + collider.offset.y;
			offset = collider.offset;
		}

		public float translateBottomCollision(float otherTop){
			return Mathf.Abs(top - bottom) / 2 + otherTop - offset.y;
		}
		public float translateRightCollision(float otherLeft){
			return otherLeft - Mathf.Abs(right - left) / 2 - offset.x;
		}
		public float translateTopCollision(float otherBottom){
			return otherBottom - Mathf.Abs(top - bottom) / 2 - offset.y;
		}
		public float translateLeftCollision(float otherRight){
			return Mathf.Abs(right - left) / 2 + otherRight - offset.x;
		}
	}

	private BoxCollider2D col;
	private Box box;
	private Transform trans;

	private bool _grounded, _right, _left, _top;

	private float _dx, _dy;

	public LayerMask collidableLayer;

	private Side getCollisionSide(Box other){
		float overlapRight, overlapLeft, overlapBottom, overlapTop;
		overlapRight = box.right - other.left;
		overlapLeft = box.left - other.right;
		overlapBottom = box.bottom - other.top;
		overlapTop = box.top - other.bottom;

		//get smallest element
		float [] overlaps = new float[]{Mathf.Abs(overlapRight), Mathf.Abs(overlapLeft), Mathf.Abs(overlapBottom), Mathf.Abs(overlapTop)};
		float minValue = Mathf.Abs(overlapRight);
		for(int i = 0; i < 4; i++){
			if (overlaps[i] < minValue)
				minValue = overlaps[i];
		}

		if (minValue == Mathf.Abs(overlapRight)) return Side.RIGHT;
		if (minValue == Mathf.Abs(overlapLeft)) return Side.LEFT;
		if (minValue == Mathf.Abs(overlapBottom)) return Side.BOTTOM;
		if (minValue == Mathf.Abs(overlapTop)) return Side.TOP;

		return Side.NONE;
	}

	private void handleCollision(Side side, Box other){
		float x, y;
		x = trans.position.x;
		y = trans.position.y;
		switch (side) {
		case Side.BOTTOM:
			y = box.translateBottomCollision (other.top);
			if(_dy <= 0) _dy = 0;
			_grounded = true;
			break;
		case Side.TOP:
			y = box.translateTopCollision (other.bottom);
			if(_dy >= 0) _dy = 0;
			_top = true;
			break;
		case Side.RIGHT:
			x = box.translateRightCollision (other.left)-.02f;
			if(_dx >= 0) _dx = 0;
			_right = true;
			break;
		case Side.LEFT:
			x = box.translateLeftCollision (other.right)+.02f;
			if(_dx <= 0) _dx = 0;
			_left = true;
			break;
		default:
			break;
		}
		trans.position = new Vector2 (x, y);
	}


	private void handleCollisionExit(Side side, Box other){
		switch (side) {
		case Side.BOTTOM:
			_grounded = false;
			break;
		case Side.TOP:
			_top = false;
			break;
		case Side.RIGHT:
			_right = false;
			break;
		case Side.LEFT:
			_left = false;
			break;
		default:
			break;
		}
	}

	void OnTriggerStay2D(Collider2D col){
		if (col.gameObject.layer == LayerMask.NameToLayer("CollisionLayer")) {
			Box collisionBox = new Box (col.gameObject.GetComponent<BoxCollider2D>());
			Side collisionSide = getCollisionSide (collisionBox);
			handleCollision (collisionSide, collisionBox);
		}
	}

	void OnTriggerExit2D(Collider2D col){
		if (col.gameObject.layer == LayerMask.NameToLayer("CollisionLayer")) {
			Debug.Log ("here");
			Box collisionBox = new Box (col.gameObject.GetComponent<BoxCollider2D>());
			Side collisionSide = getCollisionSide (collisionBox);
			handleCollisionExit (collisionSide, collisionBox);
		}
	}

	// Use this for initialization
	void Start () {
		trans = this.GetComponent<Transform> ();
		col = this.GetComponent<BoxCollider2D>();
		box = new Box(col);
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float x = trans.position.x;
		float y = trans.position.y;

		if(!_grounded)
			_dy -= (9.8f * Time.deltaTime);
		
		box = new Box (col);
		x += (_dx * Time.deltaTime);
		y += (_dy * Time.deltaTime);
		trans.position = new Vector2 (x, y);
	}

	public void Move(float x, float y){
		_dx = x;
		_dy += y;
	}

	public bool checkGrounded(){
		return _grounded;
	}

	public bool checkRightCollision(){
		return _right;
	}

	public bool checkLeftCollision(){
		return _left;
	}

	public bool checkTopCollision(){
		return _top;
	}
}
