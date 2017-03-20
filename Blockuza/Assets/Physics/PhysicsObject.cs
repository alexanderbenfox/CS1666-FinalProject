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
	[SerializeField]
	private Box box;
	private Transform trans;

	[SerializeField]
	private bool _grounded, _right, _left, _top;
	private bool _collisionThisFrame, _collisionLastFrame;

	[SerializeField]
	private float _dx, _dy;

	public bool effectedByGravity;

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
		if (minValue == Mathf.Abs(overlapBottom)) return Side.BOTTOM;
		if (minValue == Mathf.Abs(overlapRight) && _dx > 0) return Side.RIGHT;
		if (minValue == Mathf.Abs(overlapLeft) && _dx < 0) return Side.LEFT;
		if (minValue == Mathf.Abs(overlapTop) && _dy > 0) return Side.TOP;

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
			_right = true;
			x = box.translateRightCollision (other.left);
			if (_dx >= 0) {}
				//_dx = 0;
			else
				_right = false;
			break;
		case Side.LEFT:
			_left = true;
			x = box.translateLeftCollision (other.right);
			if (_dx <= 0){}
				//_dx = 0;
			else
				_left = false;
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
		if (((1<<col.gameObject.layer) & collidableLayer) != 0) {
			_collisionThisFrame = true;
			Box collisionBox = new Box (col.gameObject.GetComponent<BoxCollider2D>());
			Side collisionSide = getCollisionSide (collisionBox);
			handleCollision (collisionSide, collisionBox);
		}
	}

	void OnTriggerExit2D(Collider2D col){
		if (((1<<col.gameObject.layer) & collidableLayer) != 0) {
			Debug.Log ("here");
			Box collisionBox = new Box (col.gameObject.GetComponent<BoxCollider2D>());
			Side collisionSide = getCollisionSide (collisionBox);
			handleCollisionExit (collisionSide, collisionBox);
		}
	}

	// Use this for initialization
	void Start () {
		Rigidbody2D rigidbody = this.GetComponent<Rigidbody2D> ();
		rigidbody.gravityScale = 0;
		rigidbody.mass = 0;
		rigidbody.velocity = new Vector2 (0, 0);
		trans = this.GetComponent<Transform> ();
		col = this.GetComponent<BoxCollider2D>();
		box = new Box(col);
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!_collisionThisFrame && !_collisionLastFrame) 
			_grounded = false; _left = false; _right = false; _top = false;
		float x = trans.position.x;
		float y = trans.position.y;

		if(!_grounded && effectedByGravity)
			_dy -= (9.8f * Time.deltaTime);
		
		box = new Box (col);
		x += (_dx * Time.deltaTime);
		y += (_dy * Time.deltaTime);
		trans.position = new Vector2 (x, y);
		_collisionLastFrame = _collisionThisFrame;
		_collisionThisFrame = false;
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
