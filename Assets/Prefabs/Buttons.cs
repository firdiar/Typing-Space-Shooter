using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour {


	// Use this for initialization
	void Awake () {
		if (transform.GetComponentInChildren<Text> ().text.ToLower () == "bom") {
			Debug.Log ("added");
			GetComponent<Button> ().onClick.AddListener (() => GameObjManager.instance.player.GetComponent<PlayerController> ().UseBomb ());
		} else {
			GetComponent<Button> ().onClick.AddListener (() => GameObjManager.instance.player.GetComponent<PlayerController> ().Input (transform.GetComponentInChildren<Text> ().text.ToLower ()));
		}
	}

}
