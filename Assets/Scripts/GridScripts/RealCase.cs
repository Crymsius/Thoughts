using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RealCase : MonoBehaviour {

	public Material Void;
	public Material Available;
	public Material Filled;

	public GameObject grid;
	public GridClass myGrid { get; set; }
	private List<GameObject> neighbours = new List<GameObject> ();

	private Transform myPos;

	public GameObject attachedObject { get; set;}
	public GameObject attachedPanel { get; set;}
	public GameObject attachedInfo { get; set;}

	public string state { get; set;}
	public bool inspected { get; set;}
	private bool notExpanded = true;

	// Use this for initialization
	void Start () {
		myPos = (Transform)GetComponent<Transform> ();

		myGrid = grid.GetComponent<GridClass> ();
		if (myGrid.Exists ((int)myPos.position.x / 10, (int)myPos.position.z / 10))
			Destroy(gameObject);
		else myGrid.SetCase (gameObject, (int)myPos.position.x / 10, (int)myPos.position.z / 10);
		if (myPos.position != Vector3.zero) {
			GetComponent<MeshRenderer> ().material = Void;
			state = "void";
		} else {
			Invoke ("SpecialFirstCase", 1f);
		}
		inspected = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (notExpanded) {
			float distance = Mathf.Max(Mathf.Abs (myPos.position.x) / 10, Mathf.Abs(myPos.position.z) / 10);
			if (distance<=25) {
				AutoExpand expandScript = GetComponent<AutoExpand> ();
				expandScript.expand ("real");
				expandScript.enabled = false;
				notExpanded = false;
			}
		}
	}

	void OnMouseEnter(){
		if (state == "available" && inspected) {
			attachedPanel = gameObject.transform.FindChild ("CanvasCase").gameObject;
			attachedInfo = gameObject.transform.FindChild ("CanvasCase").FindChild ("SpecialCost").gameObject;
			attachedInfo.SetActive (true);
		}
	}

	void OnMouseExit(){
		if (state == "available" && inspected) {
			attachedInfo.SetActive (false);
		}
	}

	void OnMouseDown(){
		if (state == "available" && inspected) {
			if (attachedInfo.GetComponent<TileCreationPanel> ().isAllowed) {
				inspected = false;
				BecomeFilled ();
				attachedInfo.GetComponent<TileCreationPanel> ().pay ();
				Destroy (attachedPanel);
			}
		}
	}

	public void FindNeighbours(){
		Vector2[] array = new Vector2[]{ Vector2.up, Vector2.down, Vector2.right, Vector2.left };
		foreach (Vector2 paire in array)
			neighbours.Add (myGrid.GetCase ((int)myPos.position.x / 10 + (int)paire.x, 
				(int)myPos.position.z / 10 + (int)paire.y));
	}

	public void SpecialFirstCase(){
		BecomeFilled ();
	}

	public void BecomeFilled(){

		if (myPos.position != Vector3.zero)
			myGrid.availableCases.Remove (gameObject);
		state = "filled";
		GetComponent<MeshRenderer> ().material = Filled;
		FindNeighbours ();
		foreach (GameObject neigb in neighbours)
			if (neigb.GetComponent<RealCase> ().state == "void") {
				myGrid.availableCases.Add (neigb);
				neigb.GetComponent<RealCase> ().state = "available";
				if (inspected) {
					neigb.GetComponent<RealCase> ().inspected = inspected;
					neigb.GetComponent<MeshRenderer> ().material = Available;
				}
			}	
	}

}
