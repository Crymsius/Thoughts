using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CaseScript : MonoBehaviour {

	public Material Road;
	public Material Available;
	public Material Void;

	private GameObject player;

	private GameObject father;
	// WARNING : "children" is a bidirectional relation, must be renamed "linked"
	private Dictionary<string, GameObject> children = new Dictionary<string, GameObject>();
	private Dictionary<string, GameObject> neighbours = new Dictionary<string, GameObject>();

	private Transform posPlayer;
	private Transform myPos;

	private string state;
	private float ecartZ;
	private float distanceZ;
	private float distanceX;
	private bool notExpanded = true;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		posPlayer = (Transform)player.GetComponent<Transform> ();
		myPos = (Transform)GetComponent<Transform> ();

		GetComponent<MeshRenderer> ().material = Void;
		state = "Void";
	}

	// Update is called once per frame
	void Update () {
		if (notExpanded) {
			distanceZ = (Mathf.Abs(myPos.position.z)  - Mathf.Abs(posPlayer.position.z))/10;
			ecartZ = Mathf.Sign(myPos.position.z*posPlayer.position.z);
			distanceX = Mathf.Abs(myPos.position.x  - posPlayer.position.x)/10;
			if (Mathf.Max(distanceX, distanceZ)<=10 && ecartZ>=0) {
				AutoExpand expandScript = GetComponent<AutoExpand> ();
				expandScript.expand ();
				expandScript.enabled = false;
				notExpanded = false;
			}
			if (myPos.position == Vector3.zero)
				Invoke ("wait", 0.1f);
		}
	}

	void OnMouseDown(){
		if (state == "Available") {

			foreach (GameObject oldNeighbour in father.GetComponent<CaseScript>().GetNeighbours().Values)
				oldNeighbour.GetComponent<CaseScript> ().SelfUpdate ("Void");

			player.GetComponent<Transform> ().position = myPos.position;
			SelfUpdate ("Road");
		}
	}

	public void SelfUpdate(string newState){
		if (state == "Available" && newState == "Void") {
			GetComponent<MeshRenderer> ().material = Void;
			state = newState;
		}
		if (state == "Void" && newState == "Available") {
			GetComponent<MeshRenderer> ().material = Available;
			state = newState;
		} else if (newState == "Road") {
			GetComponent<MeshRenderer> ().material = Road;
			state = newState;
			FindNeighbours ();
			foreach (KeyValuePair<string, GameObject> entry in neighbours) {
				entry.Value.GetComponent<CaseScript> ().SelfUpdate ("Available");
				entry.Value.GetComponent<CaseScript> ().SetFather (gameObject);
			}
		}
	}

	public void wait(){
		SelfUpdate("Road");
	}

	public void SetFather(GameObject f){
		father = f;
	}

	public GameObject getFather(){
		return father;
	}

	public Dictionary<string, GameObject> GetChildren(){
		return children;
	}
	
	public Dictionary<string, GameObject> GetNeighbours(){
		return neighbours;
	}

	public void FindNeighbours(){
		foreach(KeyValuePair<string, GameObject> entry in children)
			neighbours.Add (entry.Key, entry.Value);
		if (neighbours.Count == 2) {
			string dirFurther, dirBack;
			if (Mathf.Sign (myPos.position.x) > 0) {
				dirFurther = Vector3.right.ToString ();
				dirBack = Vector3.left.ToString ();
			} else {
				dirFurther = Vector3.left.ToString ();
				dirBack = Vector3.right.ToString ();
			}
			neighbours.Add (dirFurther, FindNeighboursRecursive (1, myPos.position.z));
			neighbours.Add (dirBack, FindNeighboursRecursive (-1, myPos.position.z));
		}
	}

	public GameObject FindNeighboursRecursive(int further, float z){
		if (further != 0 && myPos.position.z != 0) {
			string next;
			if (Mathf.Sign (z) > 0)
				next = Vector3.back.ToString ();
			else
				next = Vector3.forward.ToString ();
			return children [next].GetComponent<CaseScript> ().FindNeighboursRecursive (further, z);
		} else if (further != 0 && myPos.position.z == 0) {
			string next;
			if (Mathf.Sign (myPos.position.x)*further == 1)
				next = Vector3.right.ToString ();
			else
				next = Vector3.left.ToString ();
			return children [next].GetComponent<CaseScript> ().FindNeighboursRecursive (0, z);
		} else if (further == 0 && myPos.position.z != z) {
			string next;
			if (Mathf.Sign (z) < 0)
				next = Vector3.back.ToString ();
			else
				next = Vector3.forward.ToString ();
			return children [next].GetComponent<CaseScript> ().FindNeighboursRecursive (0, z);
		} else {
			return gameObject;
		}
	}

}
