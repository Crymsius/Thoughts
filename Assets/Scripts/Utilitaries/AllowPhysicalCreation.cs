using UnityEngine;
using System.Collections;

public class AllowPhysicalCreation : MonoBehaviour {

	public Material Available;
	public GameObject mobilePanel;

	public GridClass realGrid { get; set;}

	// Use this for initialization
	void Start () {
		realGrid = GameObject.Find ("RealGrid").GetComponent<GridClass> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Creation(){
		foreach (GameObject tile in realGrid.availableCases) {
			tile.GetComponent<MeshRenderer> ().material = Available;
			tile.GetComponent<RealCase> ().inspected = true;

			foreach(Transform tr in tile.GetComponentsInChildren<Transform>())
				if(tr.gameObject.name == "CanvasCase")
					Destroy (tr.gameObject);

			GameObject panelAff = Instantiate (mobilePanel);
			panelAff.name = "CanvasCase";
			panelAff.transform.SetParent (tile.transform);
			panelAff.GetComponent<Transform> ().position = tile.GetComponent<Transform> ().position;
		}
	}
}
