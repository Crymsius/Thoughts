using UnityEngine;
using System.Collections;

public class ChoicesHandler : MonoBehaviour {

	public GameObject choiceModel;

	public int choiceNum { get; set;}

	// Use this for initialization
	void Start () {
		choiceNum = 0;
		Invoke ("SetChoice", 2f);
	}

	public void SetChoice(){
		GameObject newChoice = Instantiate(choiceModel);
		newChoice.GetComponent<Choice> ().numChoice = choiceNum;
		choiceNum++;
		newChoice.transform.parent = gameObject.transform;
		newChoice.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {

	}

}
