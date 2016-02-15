using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Investor : MonoBehaviour {

	public GameObject player;
	public GameObject inputAmount;
	public GameObject infoInvestment;

	public Dictionary<string, string> infos;
	public GameObject textInfo;
	public string textBasic;
	public string textBonus;

	public GameObject selected { get; set;}
	public float distanceToSelected { get; set;}

	public PlayerBehaviour playerStats { get; set;}

	public bool active { get; set;}
	public string resource { get; set;}
	public int amountMax { get; set;}
	public int amountMaxToInvest { get; set;}
	public int presentInvestment { get; set;}

	// Use this for initialization
	void Start () {
		playerStats = player.GetComponent<PlayerBehaviour> ();
		active = false;
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void SetActive(Dictionary<string, string> received){
		infos = received;

		if (infos ["investor"] == "yes")
			active = true;
		else
			active = false;
		
		foreach (Transform child in transform)
		{
			child.gameObject.SetActive (active);
		}

		textBasic = infos["text"];
		resource = infos["resource"];
		MAJ ();

	}

	public void GetInput(){
		presentInvestment = int.Parse(inputAmount.GetComponent<InputField> ().text);
		MAJ ();
	}

	public void SetMax(){
		presentInvestment = amountMaxToInvest;
		MAJ ();
	}

	public void MAJ(){
		if (selected!=null) {
			if (resource == "ether") {
				distanceToSelected = Vector3.Distance(playerStats.position.position,selected.GetComponent<Bridge> ().position)/10;
				amountMax = playerStats.Ether;
				amountMaxToInvest = (int)((float)amountMax / Mathf.Pow (1.015f, distanceToSelected));
				textBonus = "\n\nEther stocked : " + selected.GetComponent<Bridge> ().etherStock;
				textInfo.GetComponent<Text> ().text = textBasic + textBonus;
			}
			if (active) {
				if (presentInvestment > amountMaxToInvest)
					presentInvestment = amountMaxToInvest;
			
				inputAmount.GetComponent<InputField> ().text = presentInvestment.ToString ();
				infoInvestment.GetComponent<Text> ().text = "Total cost : " +
				(int)(presentInvestment * Mathf.Pow (1.015f, distanceToSelected));
			}
		}
	}

	public void invest(){
		if (resource == "ether") {
			playerStats.Ether-=(int)(presentInvestment * Mathf.Pow (1.015f, distanceToSelected));
			selected.GetComponent<Bridge> ().addEther(presentInvestment);
		}
		playerStats.MAJResources ();
		MAJ ();
	}

}
