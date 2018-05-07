using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(PlayerController))]
public class PlayerControllerEditor : Editor {
	
	PlayerController po;
	void OnEnable(){
		po = target as PlayerController;
		po.myRigidbody = po.gameObject.GetComponent<Rigidbody2D> ();
	}
	public override void OnInspectorGUI ()
	{
		//base.OnInspectorGUI();	//Muestra la interfaz predeterminada
		GUILayout.Label ("Type of Movement",EditorStyles.boldLabel);
		po.movement = (Movimiento)EditorGUILayout.EnumPopup("Movement",po.movement);
		GUILayout.Label ("Settings",EditorStyles.boldLabel);
		if (po.movement == Movimiento.Directions_4) {
			po.directions_4.speed = (float)EditorGUILayout.FloatField ("Speed", po.directions_4.speed);
			//po.myRigidbody.bodyType = RigidbodyType2D.Kinematic;
		} 
		else if (po.movement == Movimiento.Free) {
			po.directions_8.speed = (float)EditorGUILayout.FloatField ("Speed", po.directions_8.speed);
			//po.myRigidbody.bodyType = RigidbodyType2D.Kinematic;
		}
		else if (po.movement == Movimiento.Platform) {
			po.platform.maxSpeed = (float)EditorGUILayout.FloatField ("Max Speed", po.platform.maxSpeed);
			po.platform.jumpForce = (float)EditorGUILayout.FloatField ("Jump Force", po.platform.jumpForce);
			po.platform.groundCheck = (Transform)EditorGUILayout.ObjectField("GroundCheck",po.platform.groundCheck,typeof(Transform),true);
			po.platform.jumpClip = (AudioClip)EditorGUILayout.ObjectField("JumpClip",po.platform.jumpClip,typeof(AudioClip),true);
			//po.myRigidbody.bodyType = RigidbodyType2D.Dynamic;
			po.myRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
			GUILayout.Label ("Physics Settings", EditorStyles.boldLabel);
			po.myRigidbody.mass = EditorGUILayout.FloatField ("Mass", po.myRigidbody.mass);
			po.myRigidbody.gravityScale = EditorGUILayout.FloatField ("Gravity", po.myRigidbody.gravityScale);
			po.myRigidbody.drag = EditorGUILayout.FloatField ("Drag", po.myRigidbody.drag);
		}	
	}
}
