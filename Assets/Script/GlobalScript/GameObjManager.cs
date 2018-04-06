using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjManager : MonoBehaviour {


	public static GameObjManager instance;

	public GameObject player;
	public GameObject canvas;
	public GameObject GameConsole;
	public GameObject[] prefabsKeyboard;
	public List<GameObject> listEnemy = new List<GameObject>();
	public bool isPlaying = false;


	GameObject keyboard;

	// Use this for initialization
	void Start () {

		instance = this;
		
	}
	[Header("Word Data")]
	public string[] small = new string[5];
	public string[] medium  = new string[5];
	public string[] big  = new string[5];

	public string GetText(EnemyType et){
	
		switch (et) {

				case EnemyType.small:
					return small[Random.Range(0,small.Length)];
				
				case EnemyType.medium:
					return medium[Random.Range(0,medium.Length)];
				
				case EnemyType.big:
					return big[Random.Range(0,big.Length)];

		}
		return "";
	
	}

	public void MovePlayerToReadyPos(){
		player.GetComponent<MoveToTarget> ().SetVector3( 2 , target: new Vector3(0,-1f,0) , keep: true) ;

	}
	public void MovePlayerToSteadyPos(){
		player.GetComponent<MoveToTarget> ().SetVector3(2 , Vector3.zero , true);
	}


	public void PlayGame(){
		keyboard = Instantiate (prefabsKeyboard [PlayerPrefs.GetInt ("Keyboard")], GameConsole.transform.GetChild (1).transform.position, Quaternion.identity, GameConsole.transform.GetChild (1).transform);
		MovePlayerToReadyPos ();

		GetComponent<GameWaveManager> ().StartNextWave ();
		isPlaying = true;

	}

	public void StopGame(){
		MovePlayerToSteadyPos ();
		Destroy (keyboard);
		isPlaying = false;

		listEnemy.Clear ();

		GetComponent<GameWaveManager> ().Waves = 0;
	}


	public bool IsNoEnemy(){
		return listEnemy.Count == 0;
	}

	public MainEnemy GetEnemy(char c){

		for(int i = 0 ; i < listEnemy.Count ; i++) {
			if (listEnemy [i].GetComponent<MainEnemy> ().GetChar () == c) {
				Debug.Log ("ketemu");
				MainEnemy me = listEnemy [i].GetComponent<MainEnemy> ();
				me.SetLocked ();
				return me;
			}
		}
		return null;

	}

	void Update(){
	
		if (isPlaying && IsNoEnemy () && GetComponent<GameWaveManager>().AllEnemyOut()) {
			GetComponent<GameWaveManager> ().StartNextWave ();
		}
	
	}
	

}
