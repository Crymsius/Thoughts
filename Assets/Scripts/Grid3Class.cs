using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid3Class : GridClass {

	private GridClass mainGrid;
	public GameObject firstCase3;

	// Use this for initialization
	void Start () {
		mainGrid = gameObject.GetComponent<GridClass> ();
		cases = new Dictionary<int, Dictionary<int, GameObject>>();

		SetCase (firstCase3, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
