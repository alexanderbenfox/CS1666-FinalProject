using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTrigger : MonoBehaviour {

	public GameObject UI;
	public float sec;
	private GameObject instance;

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Player")
		{
			Debug.Log("Door hit");
			col.transform.position = new Vector2(0, 0.64f);
			instance = Instantiate(UI, new Vector2(0, 0), Quaternion.identity);
			StartCoroutine(timer());
		}
	}

	IEnumerator timer()
	{

		yield return new WaitForSeconds(sec);
		Destroy(instance);

	}
}
