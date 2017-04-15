using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffMapUI : MonoBehaviour {
	public float sec;

	// Use this for initialization
	void Start () {
		StartCoroutine(timer());
	}
	
	// Update is called once per frame
	IEnumerator timer () {
		yield return new WaitForSeconds(sec);
		Destroy(gameObject);
	}
}
