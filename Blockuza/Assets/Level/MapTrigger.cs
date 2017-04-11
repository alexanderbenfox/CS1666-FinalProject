using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTrigger : MonoBehaviour {



	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Player")
		{
			Debug.Log("Door hit");
			col.transform.position = new Vector2(0, 0.64f);
		}
	}
}
