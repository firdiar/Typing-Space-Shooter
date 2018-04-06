using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour {

	public Slider volume;
	public Text currKeyboard;

	public AudioSource[] audioObj;


	public void ChangeKeyboard(){

		if (PlayerPrefs.GetInt ("Keyboard") < GameObjManager.instance.prefabsKeyboard.Length - 1) {
			PlayerPrefs.SetInt ("Keyboard", PlayerPrefs.GetInt ("Keyboard") + 1);
		} else {
			PlayerPrefs.SetInt ("Keyboard", 0);
		}
		currKeyboard.text = GameObjManager.instance.prefabsKeyboard [PlayerPrefs.GetInt ("Keyboard")].name;
			
	}

	public void UpdateVolume(float f){
		PlayerPrefs.SetFloat ("Volume", f);
		foreach (AudioSource s in audioObj) {
			s.volume = f;
		}
	}


	// Use this for initialization
	void Start () {



		Invoke ("init", 1);
	}

	void init(){

		if (PlayerPrefs.GetString("First") == "") {
			PlayerPrefs.SetString ("First", "done");
			PlayerPrefs.SetInt ("Keyboard", 0);
			PlayerPrefs.SetFloat ("Volume", 1);
		}
		foreach (AudioSource s in audioObj) {
			s.volume = PlayerPrefs.GetFloat ("Volume");
		}

		currKeyboard.text = GameObjManager.instance.prefabsKeyboard [PlayerPrefs.GetInt ("Keyboard")].name;
		volume.value = PlayerPrefs.GetFloat ("Volume");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
