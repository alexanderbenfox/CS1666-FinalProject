using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTrigger : MonoBehaviour {

	public GameObject UI;
	public float sec;
	private GameObject instance;
	private GameObject room;
	private float offset;
	private int level;

	void Start()
	{
		room = GameObject.FindWithTag("Room");
		roomGeneration temp = room.GetComponent<roomGeneration>();
		int numSections = temp.sectionsWide;
		offset = (float)numSections * 6.4f;
		level = temp.Level;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Player")
		{
			if (level % 2 == 0)
			{
				col.transform.position = new Vector2(offset, 0.64f);
			}
			else
			{
				col.transform.position = new Vector2(0, 0.64f);
			}
			Instantiate(UI, new Vector2(0, 0), Quaternion.identity);
		}
	}

}
