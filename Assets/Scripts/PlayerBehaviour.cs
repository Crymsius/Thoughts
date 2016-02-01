using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerBehaviour : MonoBehaviour {

	public AffResources printer;

	public Transform position { get; private set;}
	public Vector3 positionV { get; set; }

	public List<GameObject> myRoad = new List<GameObject>();

	public int time { get; set; }

	public int ReflexionPoints { get; set; }
	public int RPperTime { get; set; }

	public int Ether { get; set; }
	public float etherEvapRate { get; set; }

	public bool etherDiscovered = false;

	// Use this for initialization
	void Start () {
		printer = GameObject.Find ("InfoResources").GetComponent<AffResources> ();

		position = (Transform)GetComponent<Transform> ();
		positionV = Vector3.zero;

		ReflexionPoints = 0;
		RPperTime = 0;
		Ether = 0;
		etherEvapRate = 1f / 10;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.R))
			RPperTime++;
		if (!etherDiscovered && Input.GetKeyDown (KeyCode.E)) {
			etherDiscovered = true;
			foreach (GameObject caseObj in myRoad)
				caseObj.GetComponent<CaseScript>().myEther.SetActive (true);
		}
	}

	public void Wait(){
		time ++;
		ReflexionPoints += RPperTime;

		if (etherDiscovered) {
			foreach (GameObject caseObj in myRoad) 
				StartCoroutine (caseObj.GetComponentInChildren<EtherScript2> ().MAJ ());
			Ether -= (int)Mathf.Floor (Random.Range ( 0.75f * Ether * etherEvapRate ,1.25f * Ether * etherEvapRate));
		}

		printer.MAJResources ();
	}
}
