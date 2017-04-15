using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle1 : MonoBehaviour {
	public Transform enemy;
	// Use this for initialization
	void Start () {
		if (gameObject.transform.localScale.x == -1f)
		{
			enemy.localScale = new Vector2(-1f, enemy.localScale.y);
		}
	}
}
