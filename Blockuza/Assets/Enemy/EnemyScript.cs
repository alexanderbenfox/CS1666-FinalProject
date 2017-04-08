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
	public LayerMask floor;

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

	bool IsAboutToFall(){
		float x = 0;
		float y = -1;
		if (direction == Direction.RIGHT) {
			x = 1;
		} else {
			x = -1;
		}
		Vector2 dir = new Vector2 (x, y);
		float dist = Mathf.Sqrt (2) * 1f;

		RaycastHit2D hit = Physics2D.Raycast (this.transform.position, dir, dist,floor);
		if (hit.collider == null) {
			return true;
		} else {
			//Debug.Log (hit.collider.gameObject.name);
			return false;
		}
	}

	public void Move(){
		float x = 0;
		float y = 0;

		if (direction == Direction.RIGHT && (physics.checkRightCollision () || IsAboutToFall())) {
			direction = Direction.LEFT;
		}
		if (direction == Direction.LEFT && (physics.checkLeftCollision () || IsAboutToFall())) {
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
