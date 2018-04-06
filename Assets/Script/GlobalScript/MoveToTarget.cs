using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTarget : MonoBehaviour {
	
	public enum TargetType { Transform = 0, Vector3 = 1 , RigidBody = 2 , Kosong = 3}

	public TargetType targetType = TargetType.Vector3;


	public float speed = 1;
	public float rotateSpeed = 1;
	public Vector3 targetPos = Vector3.zero;
	public Transform targetObject = null;
	public bool keepStartRot = false;
	Quaternion startRot;

	public void SetTransform(float speed , Transform obj , bool keep){
		targetType = TargetType.Transform;
		this.speed = speed;
		targetObject = obj;
		keepStartRot = keep;
	}

	public void SetVector3(float speed , Vector3 target , bool keep){
		targetType = TargetType.Vector3;
		this.speed = speed;
		targetPos = target;
		keepStartRot = keep;
	}

	public void SetRigidbody(float speed  , float rotatespd , Transform obj , bool keep){
		targetType = TargetType.RigidBody;
		this.speed = speed;
		rotateSpeed = rotatespd;
		targetObject = obj;
		keepStartRot = keep;
	}



	void Awake(){
		startRot = transform.rotation;
	}


	// Update is called once per frame
	void Update () {
		if (targetType == TargetType.Kosong) {
			return;
		}

		if (targetType == TargetType.Transform) {
			targetPos = targetObject.position;
		}

		if (targetType == TargetType.Transform || targetType == TargetType.Vector3) {
			transform.position = Vector3.MoveTowards (transform.position, targetPos, speed * Time.deltaTime);

		} else if (targetType == TargetType.RigidBody) {
			Rigidbody2D rb = GetComponent<Rigidbody2D> ();
			Vector3 dir = (targetObject.position - transform.position).normalized;

			if (dir == Vector3.down) {
				dir = Vector3.right;
			}
			float rotate = Vector3.Cross(dir , transform.up).z;
			rb.angularVelocity = -rotate * rotateSpeed;
			rb.velocity = transform.up * speed;
		}

		if (keepStartRot) {
			transform.rotation = Quaternion.Lerp (transform.rotation, startRot, 2f*Time.deltaTime);
		}
	}
}
