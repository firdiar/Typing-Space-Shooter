#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor( typeof(MoveToTarget) )]
public class MoveToTargetEditor : Editor
{
	public override void OnInspectorGUI()
	{
		MoveToTarget script = (MoveToTarget)target;

		script.targetType = (MoveToTarget.TargetType)EditorGUILayout.EnumPopup("My type", script.targetType);

		if (script.targetType == MoveToTarget.TargetType.Transform) {
			script.speed = EditorGUILayout.FloatField ("Speed", script.speed);
			script.targetObject = EditorGUILayout.ObjectField ("Target Obj", script.targetObject, typeof(Transform), false) as Transform;
		} else if (script.targetType == MoveToTarget.TargetType.Vector3) {
			script.speed = EditorGUILayout.FloatField ("Speed", script.speed);
			script.targetPos = EditorGUILayout.Vector3Field ("Target Pos", script.targetPos);
		} else if (script.targetType == MoveToTarget.TargetType.RigidBody) {
			script.speed = EditorGUILayout.FloatField ("Speed", script.speed);
			script.rotateSpeed = EditorGUILayout.FloatField ("Rotate Speed", script.rotateSpeed);
			script.targetObject = EditorGUILayout.ObjectField ("Target Obj", script.targetObject, typeof(Transform), false) as Transform;
		}
		script.keepStartRot = EditorGUILayout.Toggle( "Keep Start Rotation",script.keepStartRot);
	}
}
#endif
