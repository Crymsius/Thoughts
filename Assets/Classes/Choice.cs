using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Choice : MonoBehaviour {

	private PlayerBehaviour player;

	public GameObject choiceModel;
	private Grid3Class grid;
	private List<Case3> smallArea;

	private int numberOfIP=3;
	private List<GameObject> cases = new List<GameObject>();
	private List<InterestPoint> myIPs = new List<InterestPoint>();

	public int numChoice { get; set; } 

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player").GetComponent<PlayerBehaviour> ();
		grid = GameObject.Find ("Grid").GetComponent<Grid3Class> ();

		Case3 originCase = grid.GetCase ((int)Mathf.Floor(player.positionV.x),
			(int)Mathf.Floor(player.positionV.z)).GetComponent<Case3>();
		smallArea = originCase.GetAround ();

		GetSpawn ();
		int IP = 1;

		foreach (GameObject caseIP in cases) {
			caseIP.AddComponent<InterestPoint> ();
			InterestPoint newIP = caseIP.GetComponent<InterestPoint> ();
			newIP.father = this;
			newIP.IPnumber = IP; IP++;
			myIPs.Add(newIP);
			caseIP.GetComponent<CaseScript> ().SelfUpdate ("Feeling");
		}
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void GetSpawn(){
		
		Case3[] choices = new Case3[numberOfIP];
		List<Case3> smallAreaCopy = new List<Case3>(smallArea);

		for (int i = 0; i < numberOfIP; i++) {

			float sumWeights = 0;

			foreach (Case3 caseV in smallAreaCopy) {
				sumWeights += caseV.GetWeight();
			}

			float roll = Random.Range (0, sumWeights);
			float sumProgress = 0;
			bool notFound = true;

			foreach (Case3 caseV in smallAreaCopy) {
				sumProgress += caseV.GetWeight();
				if (notFound && sumProgress > roll) {
					choices [i] = caseV;
					notFound = false;
				}
			}
			smallAreaCopy.Remove (choices [i]);
		}

		foreach (Case3 caseV in choices) {
			caseV.timesWeight = caseV.timesWeight / 2;
			bool notFound = true;
			do {
				int roll = Random.Range (0, 9);
				if (caseV.myCases [roll].GetComponent<CaseScript> ().state != "Road") {
					notFound = false;
					cases.Add (caseV.myCases [roll]);
				}
			} while(notFound);
		}
	}

	public void SelfDestroy(){
		foreach (GameObject caseObj in cases) 
			caseObj.GetComponent<CaseScript> ().SelfUpdate ("VoidForced");
		foreach (InterestPoint IP in myIPs)
			Destroy (IP);

		GetComponentInParent<ChoicesHandler> ().Invoke ("SetChoice", 0.5f);
		//SetChoice ();

		Destroy (gameObject);
	}
		
}
