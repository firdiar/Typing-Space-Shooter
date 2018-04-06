using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RePosition : MonoBehaviour {

	[SerializeField]Vector3 posStart = new Vector3(0,0,0);
	[SerializeField]Vector3 posFinish = new Vector3(0,0,0);

	
	// Update is called once per frame
	void Update () {
		
		if (Vector3.Distance (transform.localPosition, posFinish) < 0.5) {
			transform.localPosition = posStart;	
		}
		
	}
}
