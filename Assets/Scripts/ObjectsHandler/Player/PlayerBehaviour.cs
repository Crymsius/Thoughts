using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerBehaviour : MonoBehaviour {

	public static PlayerBehaviour Instance;

	public GameObject investor;

	public AffResources printer;
	public AffResources infoText;

	public Transform position { get; private set;}
	public Vector3 etherealPosition { get; set;}
	public Vector3 physicalPosition { get; set;}
	public Vector3 positionV { get; set; }

	public List<GameObject> myRoad = new List<GameObject>();

	public int time { get; set; }

	public int ReflexionPoints { get; set; }
	public int RPperTime { get; set; }

	public int Ether { get; set; }
	public float etherEvapRate { get; set; }

	public bool etherDiscovered = false;
	public int numberBridges { get; set;}

	void Awake ()   
	{
		if (Instance == null)
		{
			DontDestroyOnLoad(gameObject);
			Instance = this;
		}
		else if (Instance != this)
		{
			Destroy (gameObject);
		}
	}

	// Use this for initialization
	void Start () {

		printer = GameObject.Find ("InfoResources").GetComponent<AffResources> ();
		infoText = GameObject.Find ("InfoActions").GetComponent<AffResources> ();

		position = (Transform)GetComponent<Transform> ();
		positionV = Vector3.zero;
		etherealPosition = Vector3.zero;
		physicalPosition = Vector3.zero;

		ReflexionPoints = 0;
		RPperTime = 0;
		Ether = 0;
		etherEvapRate = 1f / 10;

		numberBridges = 0;
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
		// Hack, to be removed when released
		if (Input.GetKeyDown (KeyCode.I)) { 
			ReflexionPoints += 100000;
			Ether += 100000;
			MAJResources ();
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

		MAJResources ();
		investor.GetComponent<Investor> ().MAJ ();
	}

	public void MAJResources(){
		printer.MAJResources ();
	}

}
