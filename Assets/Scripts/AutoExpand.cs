using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AutoExpand : MonoBehaviour {

	private GameObject aCase;

	private CaseScript script;
	private Transform myPos;
	public List<Vector3> expansion = new List<Vector3> (); // DON'T SET IT PRIVATE UNTIL SECURIZATION !

	// Use this for initialization
	void Start () {
		aCase = GameObject.Find ("Case");
		myPos = (Transform)GetComponent<Transform> ();
		script = (CaseScript)GetComponent<CaseScript> ();

		if (expansion.Count == 0)
			expansion.AddRange (new Vector3[]{ Vector3.forward, Vector3.back, Vector3.right, Vector3.left });
		expansion = expansion.Distinct ().ToList ();
	}

	public void expand(){
		foreach (Vector3 vect in expansion) {
			GameObject newCase = (GameObject)Instantiate (aCase, myPos.localPosition + 10 * vect, myPos.localRotation);

			newCase.transform.parent = GameObject.Find ("Mesh").transform;
			newCase.GetComponent<AutoExpand> ().removeFromExpansion (-vect);

			newCase.GetComponent<CaseScript>().GetChildren().Add((-vect).ToString(), gameObject);
			GetComponent<CaseScript> ().GetChildren ().Add (vect.ToString(), newCase);

			newCase.GetComponent<AutoExpand> ().enabled = true;

		}
	}

	public void removeFromExpansion(Vector3 direction){
		if(direction == Vector3.forward || direction == Vector3.back){
			expansion.Remove(Vector3.right); expansion.Remove (Vector3.left);
		}
		expansion.Remove (direction);
	}

}
