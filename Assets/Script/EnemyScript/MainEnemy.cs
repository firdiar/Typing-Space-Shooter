using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainEnemy : MonoBehaviour {


	public int score = 0;
	TextMesh renderText;
	Transform textBackgorund;
	Rigidbody2D rb;
	//bool isLocked = false;
	// Use this for initialization
	void Awake () {
		rb = GetComponent<Rigidbody2D> ();
		transform.GetChild (0).GetComponent<Renderer> ().sortingOrder = 2;
		renderText = transform.GetChild (0).GetComponent<TextMesh> ();
		textBackgorund = transform.GetChild (0).GetChild (0);
		GetComponent<MoveToTarget> ().SetRigidbody(0.5f , 120 , GameObjManager.instance.player.transform , false) ;

	}

	public void SetText(string text){
		
			renderText.text = text;
			textBackgorund.localScale = new Vector3 (text.Length * 0.07f, 0.16f, 1);
		if (text == "") {
			GameObjManager.instance.listEnemy.Remove (gameObject);
		}
	}
	public char GetChar(){
		return renderText.text[0];
	}

	public string GetText(){
		return renderText.text;
	}


	public void SetLocked(){
		renderText.color = new Color (255, 174, 0);
	}


	
	// Update is called once per frame
	void Update () {
		renderText.transform.localEulerAngles = -transform.localEulerAngles;
		//transform.LookAt(Vector3.forward,Vector3.Cross(Vector3.forward,direction));

		if (!GameObjManager.instance.isPlaying) {
			Destroy (gameObject);
		}
		
	}

	void OnDisable(){
		Debug.Log ("Destroyed");
		GameObjManager.instance.listEnemy.Remove (gameObject);
	}

	void OnTriggerEnter2D(Collider2D col){

		if (col.gameObject.tag == "Bullet") {
			Bullet b = col.GetComponent<Bullet> ();
			if (b.getTarget () == gameObject) {
				if (b.getDestroying ()) {
					Destroy (gameObject);
				}
				Destroy (col.gameObject);
			}

			Debug.Log ("Triggered");
		
		} else if (col.gameObject.tag == "Bomb") {
			Destroy (gameObject);

		}




	}

	void OnCollisionEnter2D(Collision2D col){
		Debug.Log ("Collidered");

	}
}
