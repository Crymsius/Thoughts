using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridClass : MonoBehaviour{

	protected Dictionary<int, Dictionary<int, GameObject>> cases { get; set; }
	public List<GameObject> availableCases { get; set; }

	void Awake(){
		cases = new Dictionary<int, Dictionary<int, GameObject>>();
	}

	void Start(){
		availableCases = new List<GameObject>();
	}

	public void SetCase(GameObject aCase, int X, int Z){
		if (!cases.ContainsKey (X))
			cases.Add(X, new Dictionary<int, GameObject>());
		cases [X].Add (Z, aCase);
	}

	public GameObject GetCase(int X, int Z){
		return cases [X] [Z];
	}

	public bool Exists(int X, int Z){
		if (cases.ContainsKey (X))
			return cases [X].ContainsKey (Z);
		else
			return false;
	}

}
