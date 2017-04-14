using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBlockTiled : MonoBehaviour {
	public GameObject references;
	private GameObject block32;
	private GameObject block64;
	private GameObject block128;
	private PhysicsObject physics;
	public int size;
	public bool moving;
	private float waitTime;
	public bool waitingLeft=true;
	public bool waitingTop=true;
	public bool waitingRight=true;
	private SnapToGrid snap;
	public bool waitingGrounded=true;
	public bool waitFrame=false;
	private float bossFrameTime=.2f;
	public bool gravity = false;
	public bool meCreate=true;
	public int state;//Assuming clockwise motion, 1 is moving right, on top of a block, 2 is moving down the right side of a block, 3 is moving left under a block, and 4 is moving up the left side of a block
	// Use this for initialization
	void Start () {
		state = 1;
		references = GameObject.Find("BossBlockReferences");
		block32 = references.GetComponent<BossBlockReferences>().boss32;
		block64 = references.GetComponent<BossBlockReferences>().boss64;
		block128 = references.GetComponent<BossBlockReferences>().boss128;
		physics = gameObject.GetComponent<PhysicsObject> ();
		snap = gameObject.GetComponent<SnapToGrid> ();
		if (size == 64) {
			bossFrameTime = .3f;
		}
		if (size == 128) {
			bossFrameTime = .4f;
		}
	}
	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "Boss32" && size==32) {
			Debug.Log ("Boss collide");
			if (meCreate) {
				Vector3 myPosition = gameObject.transform.position;
				col.GetComponent<BossBlockTiled> ().meCreate = false;
				references.GetComponent<BossBlockReferences> ().beingCreated = true;
				GameObject newBlock = Instantiate (block64, myPosition, Quaternion.identity);
			}
			Destroy (gameObject);
		}
		if (col.tag == "Boss64" && size==64) {
			Debug.Log ("Boss collide");
			if (meCreate) {
				Vector3 myPosition = gameObject.transform.position;
				col.GetComponent<BossBlockTiled> ().meCreate = false;
				references.GetComponent<BossBlockReferences> ().beingCreated = true;
				GameObject newBlock = Instantiate (block128, myPosition, Quaternion.identity);
			}
			Destroy (gameObject);
		}
		if (col.tag == "Block" && size==64) {
			Vector3 myPosition = gameObject.transform.position;
			GameObject newBlock = Instantiate (block32, myPosition, Quaternion.identity);
			GameObject newBlock2 = Instantiate (block32, new Vector3(myPosition.x-.64f,myPosition.y,0), Quaternion.identity);
			Destroy (gameObject);
		}
		if (col.tag == "Block" && size==128) {
			Vector3 myPosition = gameObject.transform.position;
			GameObject newBlock = Instantiate (block64, myPosition, Quaternion.identity);
			GameObject newBlock2 = Instantiate (block64, new Vector3(myPosition.x-1.28f,myPosition.y,0), Quaternion.identity);
			Destroy (gameObject);
		}
	}
	// Update is called once per frame
	void Update () {
		if (state == 1) {
			if (!gravity) {
				if (waitTime > bossFrameTime) {
					waitTime = 0;
					if (!physics.checkGrounded () && !waitingGrounded) {
						waitFrame = false;
						state = 2;
						gameObject.transform.position = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y - .32f, 0);
						if (physics.checkLeftCollision ()) {
							waitingLeft = false;
						}
					} else if (physics.checkRightCollision ()) {
						state = 4;
						waitFrame = false;
						gameObject.transform.position = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y + .32f, 0);
						if (physics.checkRightCollision ()) {
							waitingRight = false;
						}
					} else if (waitFrame) {
						gravity = true;
						waitFrame = false;

					} else {
						gameObject.transform.position = new Vector3 (gameObject.transform.position.x + .32f, gameObject.transform.position.y, 0);
					}
					if (state == 1 && physics.checkGrounded ()) {
						waitingGrounded = false;
					} else if (state == 1) {
						waitFrame = true;
					} else {
						waitingGrounded = true;
					}


				}
			} else {
				physics.effectedByGravity = true;
				snap.enabled = false;
				if (physics.checkGrounded()) {
					physics.effectedByGravity = false;
					snap.enabled = true;
					gravity = false;
				}
			}

		}
		if (state == 2) {
			if (!gravity) {
				if (waitTime > bossFrameTime) {
					waitTime = 0;
					if (!physics.checkLeftCollision () && !waitingLeft) {
						state = 3;
						gameObject.transform.position = new Vector3 (gameObject.transform.position.x - .32f, gameObject.transform.position.y, 0);
						if (physics.checkTopCollision ()) {
							waitingTop = false;
						}
					} else if (physics.checkGrounded ()) {
						state = 1;
						gameObject.transform.position = new Vector3 (gameObject.transform.position.x + .32f, gameObject.transform.position.y, 0);
						if (physics.checkGrounded ()) {
							waitingGrounded = false;
						}
					} else if (waitFrame) {
						gravity = true;
						waitFrame = false;

					} else {
						gameObject.transform.position = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y - .32f, 0);
					}
					if (state == 2 && physics.checkLeftCollision ()) {
						waitingLeft = false;
					} else if (state == 2) {
						waitFrame = true;
					} else {
						waitingLeft = true;
					}
				}
			} else {
				physics.effectedByGravity = true;
				snap.enabled = false;
				if (physics.checkGrounded()) {
					physics.effectedByGravity = false;
					snap.enabled = true;
					gravity = false;
					state = 1;
				}
			}

		}
		if (state == 3) {
			if (!gravity) {
				if (waitTime > bossFrameTime) {
					waitTime = 0;
					if (!physics.checkTopCollision () && !waitingTop) {
						state = 4;
						gameObject.transform.position = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y + .32f, 0);
						if (physics.checkRightCollision ()) {
							waitingRight = false;
						}
					} else if (physics.checkLeftCollision ()) {
						state = 2;
						gameObject.transform.position = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y - .32f, 0);
						if (physics.checkLeftCollision ()) {
							waitingLeft = false;
						}
					} else if (waitFrame) {
						gravity = true;
						waitFrame = false;

					} else {
						gameObject.transform.position = new Vector3 (gameObject.transform.position.x - .32f, gameObject.transform.position.y, 0);
					}
					if (state == 3 && physics.checkTopCollision ()) {
						waitingTop = false;
					} else if (state == 3) {
						waitFrame = true;
					} else {
						waitingTop = true;
					}
				}
			}else {
				physics.effectedByGravity = true;
				snap.enabled = false;
				if (physics.checkGrounded()) {
					physics.effectedByGravity = false;
					snap.enabled = true;
					gravity = false;
					state = 1;
				}
			}
		}
		if (state == 4) {
			if (!gravity) {
				if (waitTime > bossFrameTime) {
					waitTime = 0;
					if (!physics.checkRightCollision () && !waitingRight) {
						state = 1;
						gameObject.transform.position = new Vector3 (gameObject.transform.position.x + .32f, gameObject.transform.position.y, 0);
						if (physics.checkGrounded ()) {
							waitingGrounded = false;
						}
					} else if (physics.checkTopCollision ()) {
						state = 3;
						gameObject.transform.position = new Vector3 (gameObject.transform.position.x - .32f, gameObject.transform.position.y, 0);
						if (physics.checkTopCollision ()) {
							waitingTop = false;
						}
					} else if (waitFrame) {
						gravity = true;
						waitFrame = false;

					} else {
						gameObject.transform.position = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y + .32f, 0);
					}
					if (state == 4 && physics.checkRightCollision ()) {
						waitingRight = false;
					} else if (state == 4) {
						waitFrame = true;
					} else {
						waitingRight = true;
					}
				}
			}else {
				physics.effectedByGravity = true;
				snap.enabled = false;
				if (physics.checkGrounded()) {
					physics.effectedByGravity = false;
					snap.enabled = true;
					gravity = false;
					state = 1;
				}
			}
		}
		waitTime += Time.deltaTime;
	}
}
