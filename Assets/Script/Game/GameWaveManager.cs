using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyType{ small = 0 , medium = 1 , big = 2}
public class GameWaveManager : MonoBehaviour {

	public Transform[] SpawnPos;

	public int Waves = 0;

	[Header("Enemy Prefabs")]
	[SerializeField]GameObject smallEnemy = null;
	[SerializeField]GameObject mediumEnemy = null;
	[SerializeField]GameObject bigEnemy = null;

	class EnemyWavesData{

		public EnemyType enemyType;
		public int count;

		public EnemyWavesData(EnemyType enemyT , int c){
			enemyType = enemyT;
			count = c;
		}

	}

	List<EnemyWavesData> enemyCount = new List<EnemyWavesData>();

	GameObjManager GOM;

	void Start(){
		GOM = GetComponent<GameObjManager>();
	}

	public void StartNextWave(){
		Debug.Log (Waves);
		Waves++;
		InitEnemy ();
		Debug.Log (Waves);
	}

	public void InitEnemy(){
		Debug.Log (enemyCount.Count);

		enemyCount.Add(new EnemyWavesData(EnemyType.small , Waves *2 - 2*Waves /3 - Waves /6 ));
		enemyCount.Add( new EnemyWavesData(EnemyType.medium , Waves /3));
		enemyCount.Add(new EnemyWavesData(EnemyType.big ,  Waves / 6));
		InvokeRepeating ("Summon", 0, 1);
	}

	public bool AllEnemyOut(){
		return enemyCount.Count == 0;
	}



	void Summon(){
	
		if (!GOM.isPlaying || AllEnemyOut ()) {
			CancelInvoke ();
			if(!AllEnemyOut ())
				enemyCount.Clear ();

			return;
		}

		int spawnidx = Random.Range (0, SpawnPos.Length);
		//Debug.Log (enemyCount.Count);

		///Men suffle List
		for (int i = 0; i < enemyCount.Count; i++) {
			EnemyWavesData temp = enemyCount [i];
			int random = Random.Range (i, enemyCount.Count);
			enemyCount [i] = enemyCount [random];
			enemyCount [random] = temp;
		}

		if (enemyCount [0].count > 0) {
			
		
			enemyCount [0].count--;
			GameObject temp = null;
			switch (enemyCount [0].enemyType) {

			case EnemyType.small:

				temp = Instantiate (smallEnemy, SpawnPos [Random.Range (0, SpawnPos.Length)].transform.position, Quaternion.identity);

				//GOM.listEnemy.Add ();
				Debug.Log ("Summon Small");
				break;
			case EnemyType.medium:

				temp = Instantiate (mediumEnemy, SpawnPos [Random.Range (0, SpawnPos.Length)].transform.position, Quaternion.identity);
				Debug.Log ("Summon Medium");
				break;
			case EnemyType.big:

				temp = Instantiate (bigEnemy, SpawnPos [Random.Range (0, SpawnPos.Length)].transform.position, Quaternion.identity);
				Debug.Log ("Summon Big");
				break;

			}


			if (temp != null) {
				temp.GetComponent<MainEnemy> ().SetText (GOM.GetText (enemyCount [0].enemyType));
				GOM.listEnemy.Add (temp);
			}


		}

		if (enemyCount [0].count <= 0) {
			enemyCount.RemoveAt (0);
		}

	
	}

}
