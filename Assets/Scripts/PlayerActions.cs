using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerActions : MonoBehaviour {

	public GameObject originalBridge;

	public PlayerBehaviour playerStats {get;set;}

	// Use this for initialization
	void Start () {
		playerStats = gameObject.GetComponent<PlayerBehaviour> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.B))
			makeNewBridge ();
	}

	public void makeNewBridge(){
		playerStats.numberBridges++;
		Bridge B1 = newBridge ("Ethereal");
		Bridge B2 = newBridge ("Physical");
		B1.linkedBridge = B2; B2.linkedBridge = B1;
	}

	public Bridge newBridge(string type){
		GameObject newBridge = (GameObject)Instantiate (originalBridge);
		newBridge.SetActive (true);
		newBridge.name = "Bridge";

		newBridge.GetComponent<Bridge> ().number = playerStats.numberBridges;

		if (type == "Ethereal") {
			newBridge.GetComponent<PositionHandler> ().myGridHandler = 
				GameObject.Find ("Grid").GetComponent<GridClass> ();
			newBridge.GetComponent<PositionHandler> ().SetEtherealPosition (playerStats.etherealPosition);
		} else {
			newBridge.GetComponent<PositionHandler> ().myGridHandler = 
				GameObject.Find ("RealGrid").GetComponent<GridClass> ();
			newBridge.GetComponent<PositionHandler> ().SetPhysicalPosition (playerStats.physicalPosition);
		}
		return newBridge.GetComponent<Bridge> ();
	}

}
