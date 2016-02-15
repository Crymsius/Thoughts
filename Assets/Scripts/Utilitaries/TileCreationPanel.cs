using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TileCreationPanel : MonoBehaviour {

	public Sprite allowed;
	public Sprite disallowed;

	public GameObject investor;
	public AllowPhysicalCreation father{ get; set;} 

	public Vector3 mypos { get; set;}
	public float distance { get; set;}
	public int cost;

	public Bridge selectedBridge { get; set;}

	public bool isAllowed { get; set;}

	// Use this for initialization
	void Start () {
		father = GameObject.Find ("ActionButtonContainer").transform.
			FindChild ("CreatePTiles(Clone)").gameObject.GetComponent<AllowPhysicalCreation> ();

		selectedBridge = investor.GetComponent<Investor> ().selected.GetComponent<Bridge>();
		mypos = gameObject.transform.GetComponentInParent<Transform> ().position;

		distance = Vector3.Distance (mypos, selectedBridge.gameObject.GetComponent<Transform> ().position)/10;

		cost = (int)(50 * Mathf.Pow (1.02f,distance));
		if (cost <= selectedBridge.etherStock) {
			gameObject.GetComponent<Image> ().sprite = allowed;
			isAllowed = true;
		} else {
			gameObject.GetComponent<Image> ().sprite = disallowed;
			isAllowed = false;
		}
		
		gameObject.GetComponentInChildren<Text> ().text = cost.ToString ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void pay(){
		selectedBridge.addEther(-cost);
		investor.GetComponent<Investor> ().MAJ ();
		father.Creation ();
	}

}
