using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ChangeWorld : MonoBehaviour {

	public Text myText;
	public GameObject player;

	public GameObject grid;
	public GameObject realGrid;

	// Use this for initialization
	void Start () {
		SetVisibility (realGrid, false);
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void ChangeScene(){
		if (myText.text == "Go to Physical\nWorld") {
			SetVisibility (grid, false);
			SetVisibility (realGrid, true);
			myText.text = "Go to Ethereal\nWorld";
		} else if (myText.text == "Go to Ethereal\nWorld") {
			SetVisibility (grid, true);
			SetVisibility (realGrid, false);
			myText.text = "Go to Physical\nWorld";
		}
	}

	public void SetVisibility(GameObject grid, bool visible){
		Renderer[] allRenderers = grid.GetComponentsInChildren<Renderer> ();
		Collider[] allColliders = grid.GetComponentsInChildren<Collider> ();
		if (visible) {
			foreach (Renderer item in allRenderers)
				item.enabled = true;
			foreach (Collider item in allColliders)
				item.enabled = true;
		} else {
			foreach (Renderer item in allRenderers)
				item.enabled = false;
			foreach (Collider item in allColliders)
				item.enabled = false;
		}
	}

}
