using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AutoExpand : MonoBehaviour {

	private GameObject aCase;
	private GridClass grid;

	private Transform myPos;

	// Use this for initialization
	void Start () {
		aCase = gameObject;
		myPos = (Transform)GetComponent<Transform> ();
	}

	public void expand(string type){

		int ratio;

		if (type == "main") {
			grid = GameObject.Find ("Grid").GetComponent<GridClass> ();
			ratio = 10;
		} else {
			grid = GameObject.Find ("Grid").GetComponent<Grid3Class> ();
			ratio = 1;
		}

		Vector3[] array = new Vector3[]{ Vector3.forward, Vector3.back, Vector3.right, Vector3.left };
		foreach (Vector3 triplet in array) {
			if(!grid.Exists ((int)myPos.position.x / ratio + (int)triplet.x,
				(int)myPos.position.z/ ratio + (int)triplet.z)){
				GameObject newCase = (GameObject)Instantiate (aCase, myPos.localPosition + ratio * triplet,
					myPos.localRotation);
				if (type == "main")
					newCase.name = "Case";
				else
					newCase.name = "Case3";
				newCase.transform.parent = GameObject.Find ("Grid").transform;
				newCase.GetComponent<AutoExpand> ().enabled = true;
			}
		}
	}

}
