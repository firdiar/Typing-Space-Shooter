using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBig : MonoBehaviour {

	[SerializeField]GameObject smallEnemy = null;

	// Use this for initialization
	void Awake () {
		InvokeRepeating ("Spawn", 0, 6);
	}
	
	void Spawn(){
		GameObject temp = Instantiate (smallEnemy, transform.position, transform.rotation);
		temp.GetComponent<MainEnemy> ().score = 80;
		temp.GetComponent<MainEnemy> ().SetText (GameObjManager.instance.GetText (EnemyType.small));
	}
}
