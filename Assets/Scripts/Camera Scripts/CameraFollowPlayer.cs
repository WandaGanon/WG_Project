﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
	public float CameraMoveSpeed = 120.0f;
	public GameObject CameraFollowObj;
	Vector3 FollowPOS;
	public float clampAngle = 80.0f;
	public float inputSensitivity = 150.0f;
	public GameObject CameraObj;
	public GameObject PlayerObj;
	public float camDistanceXToPlayer;
	public float camDistanceYToPlayer;
	public float camDistanceZToPlayer;
	public float mouseX;
	public float mouseY;
	public float finalInputX;
	public float finalInputZ;
	public float smoothX;
	public float smoothY;
	private float rotY = 0.0f;
	private float rotX = 0.0f;

	void Start () 
	{
		Vector3 rot = transform.localRotation.eulerAngles;
		rotY = rot.y;
		rotX = rot.x;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	void Update () 
	{

		// We setup the rotation of the sticks here
		/* Project Settings - Input, necesitamos cambiar algunas cosas
		- Horizontal por RightStickHorizontal
		- Axis por 4th axis(Joysticks) */
		float inputX = Input.GetAxis ("RightStickHorizontal"); 
	    /* Project Settings - Input, necesitamos cambiar algunas cosas
		- Horizontal por RightStickHorizontal
		- Axis por 5th axis(Joysticks) */
		float inputZ = Input.GetAxis ("RightStickVertical");
		mouseX = Input.GetAxis ("Mouse X");
		mouseY = Input.GetAxis ("Mouse Y");
		finalInputX = inputX + mouseX;
		finalInputZ = inputZ + mouseY;

		rotY += finalInputX * inputSensitivity * Time.deltaTime;
		rotX += finalInputZ * inputSensitivity * Time.deltaTime;

		rotX = Mathf.Clamp (rotX, -clampAngle, clampAngle);

		Quaternion localRotation = Quaternion.Euler (rotX, rotY, 0.0f);
		transform.rotation = localRotation;
	}
	void LateUpdate () 
	{
		CameraUpdater ();
	}
	void CameraUpdater() 
	{
		// Setea el objetivo a seguir
		Transform target = CameraFollowObj.transform;

		//Se mueve hasta la posicion del objetivo tarjeteada
		float step = CameraMoveSpeed * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, target.position, step);
	}
}
