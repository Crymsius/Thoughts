using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CaseScript : MonoBehaviour {

	public Material Road;
	public Material Available;
	public Material Void;

	private GameObject player;

	private GameObject father;
	public GridClass grid { get; private set;}
	private List<GameObject> neighbours = new List<GameObject>();

	private Transform posPlayer;
	private Transform myPos;

	private string state;
	private bool notExpanded = true;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		posPlayer = (Transform)player.GetComponent<Transform> ();
		myPos = (Transform)GetComponent<Transform> ();

		grid = GameObject.Find ("Grid").GetComponent<GridClass> ();
		if (grid.Exists ((int)myPos.position.x / 10, (int)myPos.position.z / 10))
			Destroy(gameObject);
		else grid.SetCase (gameObject, (int)myPos.position.x / 10, (int)myPos.position.z / 10);
		GetComponent<MeshRenderer> ().material = Void;
		state = "Void";
	}

	// Update is called once per frame
	void Update () {
		if (notExpanded) {
			float distance = Mathf.Max(Mathf.Abs (myPos.position.x - posPlayer.position.x) / 10,
				Mathf.Abs(myPos.position.z - posPlayer.position.z) / 10);
			if (distance<=10) {
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
			player.GetComponent<Transform> ().position = myPos.position;
			SelfUpdate ("Road");
			foreach (GameObject oldNeighbour in father.GetComponent<CaseScript>().GetNeighbours())
				oldNeighbour.GetComponent<CaseScript> ().SelfUpdate ("Void");
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
			foreach(GameObject aCase in neighbours) {
				aCase.GetComponent<CaseScript> ().SelfUpdate ("Available");
				aCase.GetComponent<CaseScript> ().SetFather (gameObject);
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
		
	public List<GameObject> GetNeighbours(){
		return neighbours;
	}

	public void FindNeighbours(){
		Vector2[] array = new Vector2[]{ Vector2.up, Vector2.down, Vector2.right, Vector2.left };
		foreach (Vector2 paire in array)
			neighbours.Add (grid.GetCase ((int)myPos.position.x / 10 + (int)paire.x, 
				(int)myPos.position.z / 10 + (int)paire.y));
	}

}
