using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjManager : MonoBehaviour {


	public static GameObjManager instance;

	public GameObject player;
	public GameObject canvas;
	public GameObject GameConsole;
	public GameObject PrefabsWaveInfo;
	public GameObject[] prefabsKeyboard;
	public List<GameObject> listEnemy = new List<GameObject>();
	public bool isPlaying = false;


	MainMenuHandler MH;
	GameObject keyboard;

	// Use this for initialization
	void Start () {
		MH = canvas.transform.GetChild (1).GetComponent<MainMenuHandler> ();
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
		PlayerController p = player.GetComponent<PlayerController> ();
		p.score = 0;
		p.bonus = 0;
		p.LimitBomb = 3;
		GetComponent<GameWaveManager> ().StartNextWave ();
		isPlaying = true;

	}



	public void StopGame(){
		MovePlayerToSteadyPos ();
		Destroy (keyboard);
		isPlaying = false;
		listEnemy.Clear ();

		MH.OpenResultMenu (true);
		int score = player.GetComponent<PlayerController> ().score;
		if (PlayerPrefs.GetInt ("HighScore") < score) {
			PlayerPrefs.SetInt ("HighScore", score);
		}
		MH.ResultMenu.transform.GetChild (1).GetChild (0).GetComponent<UnityEngine.UI.Text> ().text = " Your Score : " + score.ToString();
		MH.ResultMenu.transform.GetChild (2).GetChild (0).GetComponent<UnityEngine.UI.Text> ().text = " High Score : "+PlayerPrefs.GetInt ("HighScore").ToString();
		GetComponent<GameWaveManager> ().Waves = 0;
	}

	public void ShowWaves(string wavs){

		GameObject temp = Instantiate (PrefabsWaveInfo, new Vector3(-109, Screen.height* (7 / 8.0f) , 0), Quaternion.identity, canvas.transform);
		temp.transform.SetSiblingIndex (1);
		temp.transform.GetChild (0).GetComponent<UnityEngine.UI.Text> ().text = " Game Wave " + wavs;
		temp.transform.GetChild (1).GetComponent<UnityEngine.UI.Text> ().text = "Score : " + player.GetComponent<PlayerController>().score+"  ";
		temp.GetComponent<MoveToTarget> ().SetVector3 (120, new Vector3 (109, Screen.height * (7 / 8.0f), 0), true);
		Destroy (temp, 4);

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
