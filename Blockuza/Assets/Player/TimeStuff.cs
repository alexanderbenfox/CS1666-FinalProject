using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeStuff : MonoBehaviour 
{
	public Controller controller;
	public int STACK_MAX_SIZE;
	public int HIT_PENALTY; //In seconds
	private ArrayList positions;
	private float runningTime;

	public IEnumerator goThruPoints;
	public bool lockAction;

	void Start()
	{
		controller = gameObject.GetComponent<Controller>();
		positions = new ArrayList(STACK_MAX_SIZE);
		positions.Add(this.gameObject.transform.position);
		runningTime = 0; 
		goThruPoints = goThroughAllPoints ();
		lockAction = false;
	}

	void Update()
	{
		runningTime += Time.deltaTime;

		// Debug.Log(runningTime);

		if (runningTime >= (float)HIT_PENALTY/(float)STACK_MAX_SIZE && !lockAction)
		{
			positions.Add(this.gameObject.transform.position);
			if (positions.Count > STACK_MAX_SIZE) {
				positions.RemoveAt (0);
			}
			runningTime = 0; 
		}
	}

	void OnTriggerEnter2D(Collider2D col) 
	{
		if (col.gameObject.CompareTag("Shuriken"))
		{
			StartCoroutine (goThruPoints);
		}
	}

	IEnumerator goThroughAllPoints(){
		lockAction = true;
		for (int i = positions.Count-1; i >= 0; i--) {
			this.gameObject.transform.position = (Vector3)positions [i];
			yield return new WaitForSeconds (.05f);
		}
		lockAction = false;
		goThruPoints = goThroughAllPoints ();

	}
}
