using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaticScript : MonoBehaviour {
	private System.Random rand = new System.Random();
	private Image img;
	public Sprite static1;
	public Sprite static2;
	public Sprite static3;

	// Use this for initialization
	void OnEnable () {
		img = this.GetComponent<Image>();
		img.sprite = chooseSprite();
	}
	
	// Update is called once per frame
	void Update () {
		img.sprite = chooseSprite();
	}

	Sprite chooseSprite()
	{
		Sprite sprite;
		int num = rand.Next(1, 4);
		switch (num) { 
			case 1:
				sprite = static1;
				break;
			case 2:
				sprite = static2;
				break;
			case 3:
				sprite = static3;
				break;
			default:
				sprite = static1;
				break;
		}
		return sprite;
	}
}
