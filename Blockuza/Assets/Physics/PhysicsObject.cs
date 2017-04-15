using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Side{
	RIGHT, LEFT, TOP, BOTTOM, NONE
}

[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent (typeof(BoxCollider2D))]

//Movement totally based on moving the transform - I don't like unity physics for platformers
//In order to make anything work with this physics object, DO NOT CHANGE THE SCALE OF THE OBJECT

public class PhysicsObject : MonoBehaviour {

	public class Box{
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

	public struct collision
	{
		public Box box;
		public Side side;
	}

	private BoxCollider2D col;
	[SerializeField]
	public Box box;
	private Transform trans;
	public int numRays = 5;

	[SerializeField]
	private bool _grounded, _right, _left, _top;
	private bool _collisionThisFrame, _collisionLastFrame;

	[SerializeField]
	private float _dx, _dy;

	public bool effectedByGravity;

	public LayerMask collidableLayer;

	public bool stopped = false;

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
			//y = box.translateTopCollision (other.bottom);
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

	List<collision> getCollisionSides(){
		List<collision> collisionSides = new List<collision>();

		float pixelSize = .01f;

		float raySpacing_x = ((this.box.right - this.box.left) / (float)numRays);
		float rayOffset_x = raySpacing_x;
		float raySpacing_y = ((this.box.top - this.box.bottom) / (float)numRays);
		float rayOffset_y = raySpacing_y;

		//Debug.Log (raySpacing_x);
		//Debug.Log (raySpacing_y);
		//Debug.Log (rayOffset_x);
		//Debug.Log (rayOffset_y);

		float i = (this.box.left+rayOffset_x);
		while ( i < (this.box.right-rayOffset_x)) {
			Vector2 topVector = new Vector2(i, this.box.top);
			RaycastHit2D upHit = Physics2D.Raycast (topVector, Vector2.up,pixelSize,collidableLayer);
			if (upHit.collider != null && upHit.collider.gameObject != this.gameObject) {
				collision col = new collision ();
				col.box = new Box ((BoxCollider2D)upHit.collider);
				col.side = Side.TOP;
				collisionSides.Add (col);
				break;
			}
			if (upHit.collider != null && upHit.collider.gameObject == this.gameObject) {
				topVector = new Vector2 (i, this.box.top+pixelSize);
				upHit = Physics2D.Raycast (topVector, Vector2.up,pixelSize,collidableLayer);
				if (upHit.collider != null && upHit.collider.gameObject != this.gameObject) {
					collision col = new collision ();
					col.box = new Box ((BoxCollider2D)upHit.collider);
					col.side = Side.TOP;
					collisionSides.Add (col);
					break;
				}
			}
			i+=raySpacing_x;
		}

		i = (this.box.left + rayOffset_x);
		while (i < (this.box.right-rayOffset_x)) {
			Vector2 bottomVector = new Vector2 (i, this.box.bottom);
			RaycastHit2D bottomHit = Physics2D.Raycast (bottomVector, -Vector2.up, pixelSize, collidableLayer);
			if (bottomHit.collider != null && bottomHit.collider.gameObject != this.gameObject) {
				collision col = new collision ();
				col.box = new Box ((BoxCollider2D)bottomHit.collider);
				col.side = Side.BOTTOM;
				collisionSides.Add (col);
				break;
			}
			if (bottomHit.collider != null && bottomHit.collider.gameObject == this.gameObject) {
				bottomVector = new Vector2 (i, this.box.bottom-pixelSize);
				bottomHit = Physics2D.Raycast (bottomVector, -Vector2.up, pixelSize, collidableLayer);
				if (bottomHit.collider != null && bottomHit.collider.gameObject != this.gameObject) {
					collision col = new collision ();
					col.box = new Box ((BoxCollider2D)bottomHit.collider);
					col.side = Side.BOTTOM;
					collisionSides.Add (col);
					break;
				}
			}
			i+=raySpacing_x;
		}

		i = (this.box.bottom + rayOffset_y);
		while(i < (this.box.top-rayOffset_y)) {
			Vector2 rightVector = new Vector2(this.box.right, i);
			RaycastHit2D rightHit = Physics2D.Raycast (rightVector, Vector2.right,pixelSize,collidableLayer);
			if (rightHit.collider != null && rightHit.collider.gameObject != this.gameObject) {
				collision col = new collision ();
				col.box = new Box ((BoxCollider2D)rightHit.collider);
				col.side = Side.RIGHT;
				collisionSides.Add (col);
				break;
			}
			if (rightHit.collider != null && rightHit.collider.gameObject == this.gameObject) {
				rightVector = new Vector2 (this.box.right+pixelSize, i);
				rightHit = Physics2D.Raycast (rightVector, Vector2.right, pixelSize, collidableLayer);
				if (rightHit.collider != null && rightHit.collider.gameObject != this.gameObject) {
					collision col = new collision ();
					col.box = new Box ((BoxCollider2D)rightHit.collider);
					col.side = Side.RIGHT;
					collisionSides.Add (col);
					break;
				}
			}
			i += raySpacing_y;
		}

		i = (this.box.bottom + rayOffset_y);
		while (i < (this.box.top-rayOffset_y)) {
			Vector2 leftVector = new Vector2 (this.box.left, i);
			RaycastHit2D leftHit = Physics2D.Raycast (leftVector, -Vector2.right, pixelSize, collidableLayer);
			if (leftHit.collider != null && leftHit.collider.gameObject != this.gameObject) {
				collision col = new collision ();
				col.box = new Box ((BoxCollider2D)leftHit.collider);
				col.side = Side.LEFT;
				collisionSides.Add (col);
				break;
			}
			if (leftHit.collider != null && leftHit.collider.gameObject == this.gameObject) {
				leftVector = new Vector2 (this.box.left-pixelSize, i);
				leftHit = Physics2D.Raycast (leftVector, -Vector2.right, pixelSize, collidableLayer);
				if (leftHit.collider != null && leftHit.collider.gameObject != this.gameObject) {
					collision col = new collision ();
					col.box = new Box ((BoxCollider2D)leftHit.collider);
					col.side = Side.LEFT;
					collisionSides.Add (col);
					break;
				}
			}
			i += raySpacing_y;
		}

		return collisionSides;
	}

	void OnTriggerStay2D(Collider2D col){
		/*if (((1<<col.gameObject.layer) & collidableLayer) != 0) {
			_collisionThisFrame = true;
			Box collisionBox = new Box (col.gameObject.GetComponent<BoxCollider2D>());
			Side collisionSide = getCollisionSide (collisionBox);
			handleCollision (collisionSide, collisionBox);
		}*/
	}

	void OnTriggerExit2D(Collider2D col){
		/*if (((1<<col.gameObject.layer) & collidableLayer) != 0) {
			Debug.Log ("here");
			Box collisionBox = new Box (col.gameObject.GetComponent<BoxCollider2D>());
			Side collisionSide = getCollisionSide (collisionBox);
			handleCollisionExit (collisionSide, collisionBox);
		}*/
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
		if (!stopped) {
			box = new Box (col);
			_grounded = false;
			_left = false;
			_right = false;
			_top = false;
			List<collision> frameCollisions = getCollisionSides ();
			for (int i = 0; i < frameCollisions.Count; i++) {
				handleCollision (frameCollisions [i].side, frameCollisions [i].box);
			}

			if (!effectedByGravity) {
				_dy = 0;
			}
			/*if (!_collisionThisFrame && !_collisionLastFrame) 
			_grounded = false; _left = false; _right = false; _top = false;*/
		
			float x = trans.position.x;
			float y = trans.position.y;

			if (!_grounded && effectedByGravity && _dy > -500f)
				_dy -= (9.8f * Time.deltaTime);
		
			x += (_dx * Time.deltaTime);
			y += (_dy * Time.deltaTime);
			trans.position = new Vector2 (x, y);
			_collisionLastFrame = _collisionThisFrame;
			_collisionThisFrame = false;
		}
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

	public float getdx(){
		return _dx;
	}

	public float getdy(){
		return _dy;
	}
}
