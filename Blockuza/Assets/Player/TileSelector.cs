using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSelector : MonoBehaviour {

	private Controller player;
	private float snapValue = .32f;
	public LayerMask blockLayer;
	public bool consume;

	private BlockBehavior savedBlock;

	// Use this for initialization
	void Start () {
		player = GetComponentInParent<Controller> ();
	}

	private void moveCursor(){
		float x, y;
		float blockSize = .32f;
		x = player.transform.position.x;
		y = player.transform.position.y;

		switch (player.getCursorDirection ()) {
		case(Direction.UP_LEFT):
			x -= blockSize;
			y += blockSize;
			break;

		case(Direction.LEFT):
			x -= blockSize;
			break;

		case(Direction.DOWN_LEFT):
			x -= blockSize;
			y -= blockSize;
			break;

		case(Direction.UP_RIGHT):
			x += blockSize;
			y += blockSize;
			break;

		case(Direction.RIGHT):
			x += blockSize;
			break;

		case(Direction.DOWN_RIGHT):
			x += blockSize;
			y -= blockSize;
			break;
		default:
			break;	
		}

		float snapInverse = 1/snapValue;
		x = Mathf.Round(x * snapInverse)/snapInverse;
		y = Mathf.Round(y * snapInverse)/snapInverse;  

		transform.position = new Vector2(x, y);
	}

	void OnTriggerStay2D(Collider2D col){
		if (((1 << col.gameObject.layer) & blockLayer) != 0) {
			savedBlock = col.gameObject.GetComponent<BlockBehavior> ();
			consume = true;
		}
	}

	public void consumeBlock(){
		if (savedBlock != null) {
			Destroy (savedBlock.gameObject);
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		moveCursor ();

		if (savedBlock == null) {
			consume = false;
		}

		savedBlock = null;
	}
}
