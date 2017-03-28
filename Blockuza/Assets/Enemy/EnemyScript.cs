using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

	private PhysicsObject physics;
	private Animator anim;
	private SpriteRenderer sprite;

	[SerializeField]
	private Direction direction;

	private bool dying;
	public bool dead;

	// Use this for initialization
	void Start () {
		physics = this.GetComponent<PhysicsObject> ();
		anim = this.GetComponent<Animator> ();
		sprite = this.GetComponent<SpriteRenderer> ();
		direction = Direction.RIGHT;
		dying = false;
	}

	public PhysicsObject getCollider(){
		return physics;
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "PlayerBlock"){
			//right now, he'll just die all the time so we gotta fix that
			dying = true;
			physics.enabled = false;
			this.GetComponent<BoxCollider2D> ().enabled = false;
		}
	}

	// Update is called once per frame
	void Update () {
		if (!dying)
			Move ();
		else {
			anim.Play ("Death");
		}

		if (dead) {
			Destroy (this.gameObject);
		}
	}

	public void Move(){
		float x = 0;
		float y = 0;
		if (direction == Direction.RIGHT && physics.checkRightCollision ()) {
			direction = Direction.LEFT;
		}
		if (direction == Direction.LEFT && physics.checkLeftCollision ()) {
			direction = Direction.RIGHT;
		}

		if (direction == Direction.RIGHT) {
			x = 1;
			sprite.flipX = false;
		} else {
			sprite.flipX = true;
			x = -1;
		}

		anim.Play ("Walk");

		physics.Move (x, y);
	}
}
