﻿using UnityEngine;
using System.Collections;

public class ButtonHandlingWindow : MonoBehaviour {

	public GameObject cible;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown(){
		ActiveGoal ();
	}

	public void ActiveGoal(){
		cible.SetActive (!cible.activeSelf);
	}
}