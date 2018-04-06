using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour {


	GameObject playerTarget;
	public GameObject _PlayerTarget{
		get{
			return playerTarget;
		}
		set{ 
			playerTarget = value;
		}

	}

	public int score;
	AudioSource ad;

	public int bonus = 0;
	public int LimitBomb = 3;
	[SerializeField]GameObject bullet = null;
	[SerializeField]GameObject bomb = null;

	void Start(){

		ad = GetComponent<AudioSource> ();
	}

	public void UseBomb(){

		if (LimitBomb < 1)
			return;

		LimitBomb--;
		GameObject temp = Instantiate (bomb, transform.position, Quaternion.identity);
		StartCoroutine (Expand (temp));
	}

	IEnumerator Expand(GameObject c){

		while (c.transform.localScale.x < 0.5f) {
			c.transform.localScale += new Vector3 (0.01f, 0.01f,  0);
			yield return new WaitForSeconds (0.1f);
		}
		Destroy (c);
	
	}


	public void Input(string a){
		char c = a [0];
		Debug.Log (_PlayerTarget != null);
		if (_PlayerTarget != null) {
			ProcessAns (c, _PlayerTarget.GetComponent<MainEnemy> ());
		} else {
			MainEnemy me = GameObjManager.instance.GetEnemy (c);
			if(me!= null)
				_PlayerTarget = me.gameObject;
			ProcessAns (c, me);
		}
	}

	void ProcessAns(char c , MainEnemy enemy){

		if (enemy == null)
			return;

		string temp = enemy.GetText ();
		if (temp [0] == c) {
			GameObject obj = Instantiate (bullet, transform.position, Quaternion.identity);
			if (temp.Length > 1) {
				obj.GetComponent<Bullet> ().SettingTarget (enemy.transform, false);
				enemy.SetText (temp.Substring (1));
			} else {

				score += enemy.score;
				obj.GetComponent<Bullet> ().SettingTarget (enemy.transform, true);
				_PlayerTarget = null;
				enemy.SetText ("");

			}
			score += bonus;
			bonus++;
			ad.Play ();

		} else {
			bonus = 0;
			Debug.Log ("Wrong Ans");
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.transform.tag == "Enemy") {
			
			GameObjManager.instance.canvas.transform.GetChild (1).GetComponent<MainMenuHandler> ().BackToTittle ();
			Debug.Log ("Die");
		}


	}
}
