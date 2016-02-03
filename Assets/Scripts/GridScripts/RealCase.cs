using UnityEngine;
using System.Collections;

public class RealCase : MonoBehaviour {

	public GameObject grid;
	public GridClass myGrid { get; set; }

	private Transform myPos;

	public GameObject attachedObject { get; set;}

	// Use this for initialization
	void Start () {
		myPos = (Transform)GetComponent<Transform> ();

		myGrid = grid.GetComponent<GridClass> ();
		myGrid.SetCase (gameObject, (int)myPos.position.x / 10, (int)myPos.position.z / 10);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
