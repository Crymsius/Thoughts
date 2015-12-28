using UnityEngine;
using System.Collections;

public class ChoicesHandler : MonoBehaviour {

	public GameObject choiceModel;

	// Use this for initialization
	void Start () {
		Invoke ("SetFirstChoice", 2f);
	}

	public void SetFirstChoice(){
		GameObject newChoice = Instantiate(choiceModel);
		newChoice.transform.parent = gameObject.transform;
		newChoice.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {

	}

}
