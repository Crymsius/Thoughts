using UnityEngine;
using System.Collections;

public class InterestPoint : MonoBehaviour {

	private GameObject choices; 
	public Choice father { get; set; }

	private CaseScript caseS;

	// Use this for initialization
	void Start () {
		choices= GameObject.Find ("Choices");

		caseS = gameObject.GetComponent<CaseScript> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		if (caseS.state == "Feeling Available") {
			// Effets du point d'intérêt à rajouter
			caseS.GetToRoad();
			father.SelfDestroy();
		}
	}

}
