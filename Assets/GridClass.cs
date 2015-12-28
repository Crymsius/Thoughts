using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridClass : MonoBehaviour{

	private Dictionary<int, Dictionary<int, GameObject>> cases = new Dictionary<int, Dictionary<int, GameObject>>();

	void Start(){
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
		return (cases.ContainsKey (X) && cases [X].ContainsKey (Z));
	}

}
