using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TimeHandler : MonoBehaviour {

	public GameObject player;
	public PlayerBehaviour playerStats{ get; set;}
	public GameObject investor;

	public AffResources printer;

	public List<Bridge> allBridges { get; set;}

	// Use this for initialization
	void Start () {
		allBridges = new List<Bridge> ();
		playerStats = player.GetComponent<PlayerBehaviour> ();
		printer = GameObject.Find ("InfoResources").GetComponent<AffResources> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Wait(){
		playerStats.time ++;
		playerStats.ReflexionPoints += playerStats.RPperTime;

		if (playerStats.etherDiscovered) {
			foreach (GameObject caseObj in  playerStats.myRoad) 
				StartCoroutine (caseObj.GetComponentInChildren<EtherScript2> ().MAJ ());
			playerStats.Ether -= (int)Mathf.Floor (Random.Range ( 0.75f * playerStats.Ether 
				* playerStats.etherEvapRate ,1.25f * playerStats.Ether * playerStats.etherEvapRate));
		}

		playerStats.MAJResources ();

		foreach (Bridge br in allBridges)
			br.Wait ();

		investor.GetComponent<Investor> ().MAJ ();
	}

	public void MAJResources(){
		printer.MAJResources ();
	}

}
