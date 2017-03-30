using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

	public Transform followObject;
	public float followSpeed;
	
	// Update is called once per frame
	void Update () {
		float x, y;
		float currentX = this.transform.position.x;
		float currentY = this.transform.position.y;
		x = followObject.position.x - currentX;
		y = followObject.position.y - currentY;

		x = x * (followSpeed*Time.deltaTime);
		y = y * (followSpeed*Time.deltaTime);

		this.transform.position = new Vector2 (currentX+x, currentY+y);
	}
}
