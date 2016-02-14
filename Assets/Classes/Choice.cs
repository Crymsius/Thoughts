using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Choice : MonoBehaviour {

	private PlayerBehaviour player;

	public GameObject choiceModel;
	private GridVClass grid;
	private List<CaseV> smallArea;

	private int numberOfIP=3;
	private List<GameObject> cases = new List<GameObject>();
	private List<InterestPoint> myIPs = new List<InterestPoint>();

	public int numChoice { get; set; } 

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player").GetComponent<PlayerBehaviour> ();
		grid = GameObject.Find ("Grid").GetComponent<GridVClass> ();

		CaseV originCase = grid.GetCase ((int)Mathf.Floor(player.positionV.x),
			(int)Mathf.Floor(player.positionV.z)).GetComponent<CaseV>();
		smallArea = originCase.GetAround ();

		GetSpawn ();
		int IP = 1;

		foreach (GameObject caseIP in cases) {
			caseIP.AddComponent<InterestPoint> ();
			InterestPoint newIP = caseIP.GetComponent<InterestPoint> ();
			newIP.father = this;

			newIP.cost = 100 * (IP - 1);
			if (IP==3) newIP.etherCost = 50 ;
			// Just for testing, the cost needs to be loaded from external file

			newIP.IPnumber = IP; IP++;
			myIPs.Add(newIP);
			caseIP.GetComponent<CaseScript> ().SelfUpdate ("Feeling");
		}
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void GetSpawn(){
		
		CaseV[] choices = new CaseV[numberOfIP];
		List<CaseV> smallAreaCopy = new List<CaseV>(smallArea);

		for (int i = 0; i < numberOfIP; i++) {

			float sumWeights = 0;

			foreach (CaseV caseV in smallAreaCopy) {
				sumWeights += caseV.GetWeight();
			}

			float roll = Random.Range (0, sumWeights);
			float sumProgress = 0;
			bool notFound = true;

			foreach (CaseV caseV in smallAreaCopy) {
				sumProgress += caseV.GetWeight();
				if (notFound && sumProgress > roll) {
					choices [i] = caseV;
					notFound = false;
				}
			}
			smallAreaCopy.Remove (choices [i]);
		}

		foreach (CaseV caseV in choices) {
			caseV.timesWeight = caseV.timesWeight / 2;
			bool notFound = true;
			do {
				int roll = Random.Range (0, (int)Mathf.Pow(caseV.width,2));
				//Debug.Log("Roll : " + roll);
				if (caseV.myCases [roll].GetComponent<CaseScript> ().animator.GetBool("isRoad") == false) {
					notFound = false;
					cases.Add (caseV.myCases [roll]);
				}
			} while(notFound);
		}
	}

	public void SelfDestroy(){
		foreach (GameObject caseObj in cases) 
			caseObj.GetComponent<CaseScript> ().SelfUpdate ("Void");
		foreach (InterestPoint IP in myIPs) {
			IP.SelfDestroy ();
			Destroy (IP);
		}

		GetComponentInParent<ChoicesHandler> ().Invoke ("SetChoice", 0.5f);
		//SetChoice ();

		Destroy (gameObject);
	}
		
}
