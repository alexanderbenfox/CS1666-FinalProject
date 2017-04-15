using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeStuff : MonoBehaviour 
{
	public Controller controller;
	public int STACK_MAX_SIZE;
	public int HIT_PENALTY; //In seconds
	public ArrayList positions;
	public GameObject StaticEffect;
	private float runningTime;

	public float invulnerableTime = 0;

	public IEnumerator goThruPoints;
	public bool lockAction;

	private SpriteRenderer renderer;

	int frameCounter;
	bool colorSwitch;

	void Start()
	{
		controller = gameObject.GetComponent<Controller>();
		positions = new ArrayList(STACK_MAX_SIZE);
		positions.Add(this.gameObject.transform.position);
		runningTime = 0; 
		goThruPoints = goThroughAllPoints ();
		lockAction = false;
		frameCounter = 0;
	}

	void Awake(){
		renderer = GetComponentInParent<SpriteRenderer> ();
	}

	void Update()
	{
		runningTime += Time.deltaTime;

		if (invulnerableTime > 0) {
			invulnerableTime -= Time.deltaTime;
			frameCounter++;
			if (frameCounter == 10) {
				colorSwitch = true;
				frameCounter = 0;
			}

			if (colorSwitch && renderer.color == Color.white)
				renderer.color = Color.black;
			else if (colorSwitch && renderer.color == Color.black)
				renderer.color = Color.white;
			colorSwitch = false;
		} else {
			renderer.color = Color.white;
			frameCounter = 0;
		}

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
			if(invulnerableTime <= 0)
				StartCoroutine (goThruPoints);
		}
	}

	IEnumerator goThroughAllPoints(){
		StaticEffect.SetActive(true);
		lockAction = true;
		for (int i = positions.Count-1; i >= 0; i--) {
			this.gameObject.transform.position = (Vector3)positions [i];
			yield return new WaitForSeconds (.05f);
		}
		lockAction = false;
		goThruPoints = goThroughAllPoints ();
		invulnerableTime = (float)HIT_PENALTY/2;
		StaticEffect.SetActive(false);
	}
}
