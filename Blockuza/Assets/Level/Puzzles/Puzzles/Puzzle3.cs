using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle3 : MonoBehaviour {
	public Transform Enemies;
	// Use this for initialization
	void Start () {
		if (gameObject.transform.localScale.x == -1f)
		{
			foreach (Transform child in Enemies)
			{
				child.localScale = new Vector2(-1f, child.localScale.y);
			}
		}
	}

}
