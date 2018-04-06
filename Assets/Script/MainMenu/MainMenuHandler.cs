using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainMenuHandler : MonoBehaviour {

	public static bool isPaused = false;
	public GameObject tittleScreen;
	public GameObject homeScreen;
	public GameObject pausedMenu;
	public GameObject OptionMenu;
	Animator animator;

	void Start(){
		StartCoroutine (Init());
	}

	IEnumerator Init(){
		yield return new WaitForSeconds (0.5f);
		animator = GameObjManager.instance.canvas.GetComponent<Animator> ();
	}

	public void OpenOption(bool active){
		OptionMenu.SetActive (active);
	}


	public void ChangeScreen(){
		
		if (homeScreen.activeInHierarchy) {
			tittleScreen.SetActive (true);
			homeScreen.SetActive (false);
		} else {
			tittleScreen.SetActive (false);
			homeScreen.SetActive (true);
		}
	}

	IEnumerator ActivatingObj(GameObject c , bool val , float time){
		yield return new WaitForSeconds (time);
		c.SetActive (val);
	}

	public void StartGame(){
		GameObjManager.instance.PlayGame ();
		StartCoroutine(ActivatingObj(homeScreen , false , 1.5f));
		animator.SetBool ("isPlaying", true);
		
	}

	public void BackToTittle(){
		animator.SetBool ("isPlaying", false);
		GameObjManager.instance.StopGame ();
		homeScreen.SetActive (true);
		Resume ();
	}

	public void Resume(){
		pausedMenu.SetActive (false);
		Time.timeScale = 1f;
	}

	public void Pause(){
		pausedMenu.SetActive (true);
		Time.timeScale = 0f;
	}

	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.Escape)) {
			if (!animator.GetBool ("isPlaying")) {
				// jika dia tidak sedang bermain
				if (homeScreen.activeInHierarchy) {
					ChangeScreen ();
				} else {
					Application.Quit ();
				}

			} else {
				//jika dia sedang bermain
				if (MainMenuHandler.isPaused) {
					Resume ();
				} else {
					Pause ();
				}
			
			}
		}


		
	}
}
