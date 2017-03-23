using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeStuff : MonoBehaviour 
{
	public Controller controller;
	public int STACK_MAX_SIZE;
	public int HIT_PENALTY;
	private ArrayList positions;
	private float runningTime;

	void Start()
	{
		controller = gameObject.GetComponent<Controller>();
		positions = new ArrayList(STACK_MAX_SIZE);
		positions.Add(this.gameObject.transform.position);
		runningTime = 0; 
	}

	void Update()
	{
		if (positions.Count == STACK_MAX_SIZE)
		{
			positions.RemoveAt(0);
		}

		runningTime += Time.deltaTime;

		// Debug.Log(runningTime);

		if (runningTime >= 1f)
		{
			positions.Add(this.gameObject.transform.position);
			runningTime = 0; 

			// Debug.Log(this.gameObject.transform.position);
		}
	}

	void OnTriggerEnter2D(Collider2D col) 
	{
		if (col.gameObject.CompareTag("Shuriken"))
		{
			while (positions.Count > STACK_MAX_SIZE - HIT_PENALTY)
			{
				this.gameObject.transform.position = (Vector3) positions[positions.Count - 1];
				positions.RemoveAt(positions.Count - 1);
			}
			positions.Add(this.gameObject.transform.position);
		}
	}
}
