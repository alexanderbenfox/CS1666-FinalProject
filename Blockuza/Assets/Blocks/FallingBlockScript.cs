using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlockScript : MonoBehaviour {

	public Sprite falling, still;

	private PhysicsObject p;
	private SpriteRenderer s;

	// Use this for initialization
	void Start () {
		p = this.GetComponent<PhysicsObject> ();
		s = this.GetComponent<SpriteRenderer> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		if (p.checkGrounded ()) {
			s.sprite = still;
		} else {
			s.sprite = falling;
		}
		
	}
}
