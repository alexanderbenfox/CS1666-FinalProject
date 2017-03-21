using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ControlType{
	MouseAndKeyboard, Keyboard
}

public class InputControl : MonoBehaviour {

	//translates key presses into actions

	public List<Keys> keysHeld;
	public List<Keys> keysPressed;
	public List<Keys> keysReleased;
	public ControlType control;

	// Use this for initialization
	public InputControl() {
		keysHeld = new List<Keys> ();
		keysPressed = new List<Keys> ();
		keysReleased = new List<Keys> ();
	}

	public void setControlType(ControlType t){
		control = t;
	}

	private List<Keys> getHeldInput(){
		List<Keys> heldKeys = new List<Keys> ();
		if (control == ControlType.Keyboard) {
			if (Input.GetKey (KeyCode.LeftArrow)) {
				heldKeys.Add (Keys.LEFT);
			}
			if (Input.GetKey (KeyCode.RightArrow)) {
				heldKeys.Add (Keys.RIGHT);
			}
			if (Input.GetKey (KeyCode.UpArrow)) {
				heldKeys.Add (Keys.UP);
			}
			if (Input.GetKey (KeyCode.DownArrow)) {
				heldKeys.Add (Keys.DOWN);
			}
			if (Input.GetKey (KeyCode.Z)) {
				heldKeys.Add (Keys.JUMP);
			}
			if (Input.GetKey (KeyCode.X)) {
				heldKeys.Add (Keys.ACTION);
			}
		}
		else if (control == ControlType.MouseAndKeyboard) {
			if (Input.GetKey (KeyCode.A)) {
				heldKeys.Add (Keys.LEFT);
			}
			if (Input.GetKey (KeyCode.D)) {
				heldKeys.Add (Keys.RIGHT);
			}
			if (Input.GetKey (KeyCode.W)) {
				heldKeys.Add (Keys.UP);
				heldKeys.Add (Keys.JUMP);
			}
			if (Input.GetKey (KeyCode.S)) {
				heldKeys.Add (Keys.DOWN);
			}
			if (Input.GetKey (KeyCode.Mouse0)) {
				heldKeys.Add (Keys.ACTION);
			}
		}
		return heldKeys;
	}

	private List<Keys> getPressedInput(){
		List<Keys> heldKeys = new List<Keys> ();
		if (control == ControlType.Keyboard) {
			if (Input.GetKeyDown (KeyCode.LeftArrow)) {
				heldKeys.Add (Keys.LEFT);
			}
			if (Input.GetKeyDown (KeyCode.RightArrow)) {
				heldKeys.Add (Keys.RIGHT);
			}
			if (Input.GetKeyDown (KeyCode.UpArrow)) {
				heldKeys.Add (Keys.UP);
			}
			if (Input.GetKeyDown (KeyCode.DownArrow)) {
				heldKeys.Add (Keys.DOWN);
			}
			if (Input.GetKeyDown (KeyCode.Z)) {
				heldKeys.Add (Keys.JUMP);
			}
			if (Input.GetKeyDown (KeyCode.X)) {
				heldKeys.Add (Keys.ACTION);
			}
		}
		else if (control == ControlType.MouseAndKeyboard) {
			if (Input.GetKeyDown (KeyCode.A)) {
				heldKeys.Add (Keys.LEFT);
			}
			if (Input.GetKeyDown (KeyCode.D)) {
				heldKeys.Add (Keys.RIGHT);
			}
			if (Input.GetKeyDown (KeyCode.W)) {
				heldKeys.Add (Keys.UP);
				heldKeys.Add (Keys.JUMP);
			}
			if (Input.GetKeyDown (KeyCode.S)) {
				heldKeys.Add (Keys.DOWN);
			}
			if (Input.GetKeyDown (KeyCode.Mouse0)) {
				heldKeys.Add (Keys.ACTION);
			}
		}
		return heldKeys;
	}

	public void updateKeys(){
		List<Keys> oldHeldKeys = keysHeld;
		keysPressed = getPressedInput ();
		keysHeld = getHeldInput ();
		keysReleased = new List<Keys> ();
		for (int i = 0; i < oldHeldKeys.Count; i++) {
			if (!keysHeld.Contains (oldHeldKeys [i])) {
				keysReleased.Add (oldHeldKeys [i]);
			}
		}
	}
}
