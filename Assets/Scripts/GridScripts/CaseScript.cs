using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CaseScript : MonoBehaviour {

	public Material Road;
	public Material Available;
	public Material Void;
	public Material Feeling;
	public Material FeelingAvailable;

	public Animator animator;

	private GameObject player;

	public GameObject ether;
	public GameObject myEther { get; set; }

	public GameObject attachedObject { get; set; }

	private GameObject father { get; set; }
	public GridClass grid { get; private set;}
	public CaseV caseV { get; set; }
	private List<GameObject> neighbours = new List<GameObject> ();

	private Transform posPlayer;
	private Transform myPos;

	// public string state { get; set;}
	private bool notExpanded = true;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		posPlayer = (Transform)player.GetComponent<Transform> ();
		myPos = (Transform)GetComponent<Transform> ();

		animator = gameObject.GetComponent<Animator>();

		grid = GameObject.Find ("Grid").GetComponent<GridClass> ();
		if (grid.Exists ((int)myPos.position.x / 10, (int)myPos.position.z / 10))
			Destroy(gameObject);
		else grid.SetCase (gameObject, (int)myPos.position.x / 10, (int)myPos.position.z / 10);
		GetComponent<MeshRenderer> ().material = Void;
		animator.SetBool("isVoid", true);
	}

	// Update is called once per frame
	void Update () {
		if (notExpanded) {
			float distance = Mathf.Max(Mathf.Abs (myPos.position.x - posPlayer.position.x) / 10,
				Mathf.Abs(myPos.position.z - posPlayer.position.z) / 10);
			if (distance<=15) {
				AutoExpand expandScript = GetComponent<AutoExpand> ();
				expandScript.expand ("main");
				expandScript.enabled = false;
				notExpanded = false;
			}
			if (myPos.position == Vector3.zero)
				Invoke ("wait", 0.1f);
		}
	}

	void OnMouseDown(){
		if (animator.GetBool("isAvailable") && animator.GetBool("isChoice")) {
		} else if (animator.GetBool("isAvailable") && !animator.GetBool("isChoice")) {
			GetToRoad();
		}
	}

	public void GetToRoad(){ // corresponds to one move = one time unit
		
		player.GetComponent<Transform> ().position = myPos.position;
		player.GetComponent<PlayerBehaviour> ().etherealPosition = myPos.position / 10;
		player.GetComponent<PlayerBehaviour> ().myRoad.Add (gameObject);
		SelfUpdate ("Road");
		foreach (GameObject oldNeighbour in father.GetComponent<CaseScript>().GetNeighbours())
			oldNeighbour.GetComponent<CaseScript> ().SelfUpdate ("Void");
		caseV.GetComponent<CaseV> ().SetPlayer ();
		
		myEther = (GameObject)Instantiate(ether, gameObject.GetComponent<Transform> ().position,
			gameObject.GetComponent<Transform> ().rotation);
		myEther.transform.parent = gameObject.transform;
		if (player.GetComponent<PlayerBehaviour> ().etherDiscovered)
			myEther.SetActive(true);

		player.GetComponent<PlayerBehaviour> ().Wait ();

	}

	// public void SelfUpdate(string newState){ // to be reorganized & improved
	// 	if (state != "Road" && newState == "VoidForced") {
	// 		GetComponent<MeshRenderer> ().material = Void;
	// 		state = "Void";
	// 	} else if (state != "Road") {
	// 		if (newState == "Void") {
	// 			if (state == "Available") {
	// 				GetComponent<MeshRenderer> ().material = Void;
	// 				state = newState;
	// 			} else {
	// 				GetComponent<MeshRenderer> ().material = Feeling;
	// 				state = "Feeling";
	// 			}
	// 		} else if (state == "Available" && newState == "Feeling"
	// 		           || state == "Feeling" && newState == "Available") {
	// 			GetComponent<MeshRenderer> ().material = FeelingAvailable;
	// 			state = "Feeling Available";
	// 		} else if (newState == "Available") {
	// 			GetComponent<MeshRenderer> ().material = Available;
	// 			state = newState;
	// 		} else if (newState == "Feeling") {
	// 			GetComponent<MeshRenderer> ().material = Feeling;
	// 			state = newState;
	// 		} else if (newState == "Road") {
	// 			GetComponent<MeshRenderer> ().material = Road;
	// 			state = newState;
	// 			FindNeighbours ();
	// 			foreach (GameObject aCase in neighbours) {
	// 				aCase.GetComponent<CaseScript> ().SelfUpdate ("Available");
	// 				aCase.GetComponent<CaseScript> ().father = gameObject;
	// 			}
	// 		}
	// 	}
	// }

	public void SelfUpdate(string newState){ // to be reorganized & improved
		
		switch(newState) {

			case "Void":
				if (animator.GetBool("isChoice") && animator.GetBool("isAvailable")){
					animator.SetBool("isChoice", true);
					animator.SetBool("isAvailable", false);
				} else {
					animator.SetBool("isVoid", true);
					animator.SetBool("isChoice", false);
					animator.SetBool("isAvailable", false);
				}
				break;
			case "Available":
				if(!animator.GetBool("isRoad")) {
					animator.SetBool("isVoid", false);
					animator.SetBool("isAvailable", true);
				}
				break;
			case "Feeling":
				animator.SetBool("isVoid", false);
				animator.SetBool("isChoice", true);
				break;
			case "Road":
				animator.SetBool("isRoad", true);
				animator.SetBool("isChoice", false);
				animator.SetBool("isAvailable", false);
				// state = "Road";
				FindNeighbours ();
				foreach (GameObject aCase in neighbours) {
					aCase.GetComponent<CaseScript> ().SelfUpdate ("Available");
					aCase.GetComponent<CaseScript> ().father = gameObject;
				}
				break;
			case "VoidForced":
				animator.SetBool("isVoid", true);
				// animator.SetBool("isRoad", false);
				// animator.SetBool("isChoice", false);
				// animator.SetBool("isAvailable", false);
				// state = "Void";
				break;
			// default:
			// 	animator.SetBool("isVoid", true);
			// 	animator.SetBool("isRoad", false);
			// 	animator.SetBool("isChoice", false);
			// 	animator.SetBool("isAvailable", false);
			// 	state="Void";
			// 	break;
		}
	}

	public void wait(){
		//player.GetComponent<PlayerBehaviour> ().myRoad.Add (gameObject);
		SelfUpdate("Road");
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
