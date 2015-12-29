using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour {

	public AffResources printer;

	public Transform position { get; private set;}
	public Vector3 positionV { get; set; }

	public int time { get; set; }

	public int ReflexionPoints { get; set; }
	public int RPperTime = 0;

	// Use this for initialization
	void Start () {
		printer = GameObject.Find ("InfoResources").GetComponent<AffResources> ();

		position = (Transform)GetComponent<Transform> ();
		positionV = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Wait(int timeUnits){
		time += timeUnits;
		ReflexionPoints += timeUnits * RPperTime;
		printer.MAJResources ();
	}
}
