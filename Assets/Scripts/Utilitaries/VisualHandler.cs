﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class VisualHandler : MonoBehaviour {

	public GameObject switchButton;
	public GameObject panelInfo;
	public GameObject investor;
	public GameObject specialButton;
	public GameObject temporaryButton { get; set;}

	public GameObject AttachableScripts;

	public Text nameText { get; set;}
	public Text infoText { get; set;}

	// Use this for initialization
	void Start () {
		if (panelInfo != null) {
			nameText = panelInfo.transform.FindChild ("NameObject").GetComponent<Text>();
			infoText = panelInfo.transform.FindChild ("InfoObject").GetComponent<Text>();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (switchButton!=null && Input.GetKeyDown (KeyCode.C))
			switchButton.SetActive (!switchButton.activeSelf);
	}

	public void OpenObjectInfo(GameObject selectedObject ,Dictionary<string, string> infos){

		investor.GetComponent<Investor> ().selected = selectedObject;
		nameText.text = infos["name"];
		infoText.text = infos["text"];
		investor.GetComponent<Investor> ().SetActive (infos);

		if (temporaryButton != null)
			Destroy (temporaryButton);
		if (infos ["button"] == "CreatePhysicalTile") {
			InstanciateObj (AttachableScripts.transform.FindChild ("CreatePTiles").gameObject, specialButton);
		} 
	}

	public void InstanciateObj(GameObject toCopy, GameObject father){
		temporaryButton = Instantiate (toCopy);
		temporaryButton.transform.SetParent (father.transform);
		temporaryButton.GetComponent<Transform> ().position = father.GetComponent<Transform> ().position;
	}

}
