using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class StartMenuManager : MonoBehaviour {

	public Canvas PlayScreen, HowToPlay, Controls;

	public void GoToHowToPlay(){
		PlayScreen.gameObject.SetActive (false);
		HowToPlay.gameObject.SetActive (true);
		Controls.gameObject.SetActive (false);
	}

	public void GoToPlayScreen(){
		PlayScreen.gameObject.SetActive (true);
		HowToPlay.gameObject.SetActive (false);
		Controls.gameObject.SetActive (false);
	}

	public void GoToControlsScreen(){
		PlayScreen.gameObject.SetActive (false);
		HowToPlay.gameObject.SetActive (false);
		Controls.gameObject.SetActive (true);
	}

}
