using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle7 : MonoBehaviour {
	public Transform Enemies;
	// Use this for initialization
	void Start () {
		if (gameObject.transform.localScale.x == -1f)
		{
			foreach (Transform child in Enemies.transform)
			{
				child.localScale = new Vector2(-1f, child.localScale.y);
			}
		}
	}

}
