using UnityEngine;
using System.Collections;

public class VisualHandler : MonoBehaviour {

	public GameObject switchButton;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.C))
			switchButton.SetActive (!switchButton.activeSelf);
	}
}
