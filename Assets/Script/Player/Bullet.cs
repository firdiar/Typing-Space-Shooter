using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MoveToTarget))]
public class Bullet : MonoBehaviour {

	bool destroying = false;
	MoveToTarget MT;
	public bool getDestroying(){
		return destroying;
	}
	public void setDestroying(bool val){
		destroying = val;
	}

	public GameObject getTarget(){
		return MT.targetObject.gameObject;
	}

	public void setTarget(Transform obj){
		MT.targetObject = obj;
	}

	public void SettingTarget(Transform Target , bool isDestroying){
		Debug.Log (GetComponent<MoveToTarget> ());
		GetComponent<MoveToTarget> ().SetTransform (5, Target, false);
		setDestroying (isDestroying);
	}

	// Use this for initialization
	void Start () {
		MT = GetComponent<MoveToTarget> ();
	}
		
	

}
