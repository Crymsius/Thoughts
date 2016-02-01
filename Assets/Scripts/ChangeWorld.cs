using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ChangeWorld : MonoBehaviour {

	public Text myText;
	public GameObject player;

	public GameObject grid;
	public GameObject realGrid;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void ChangeScene(){
		if (myText.text == "Go to Physical\nWorld") {
			grid.SetActive(false);
			realGrid.SetActive (true);
			myText.text = "Go to Ethereal\nWorld";
		} else if (myText.text == "Go to Ethereal\nWorld") {
			grid.SetActive(true);
			realGrid.SetActive (false);
			myText.text = "Go to Physical\nWorld";
		}
	}

}
