using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridVClass : GridClass {

	private GridClass mainGrid;
	public GameObject firstCaseV;

	// Use this for initialization
	void Start () {
		mainGrid = gameObject.GetComponent<GridClass> ();
		cases = new Dictionary<int, Dictionary<int, GameObject>>();

		SetCase (firstCaseV, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
