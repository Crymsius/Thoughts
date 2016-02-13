using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Investor : MonoBehaviour {

	public GameObject player;
	public GameObject inputAmount;
	public GameObject infoInvestment;

	public PlayerBehaviour playerStats { get; set;}

	public bool active { get; set;}
	public string resource { get; set;}
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

	public void SetActive(bool activated, string resourceInvested){
		active = activated;
		foreach (Transform child in transform)
		{
			child.gameObject.SetActive (activated);
		}
		if (activated) {
			resource = resourceInvested;
			MAJ ();
		}
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
		if (active) {
			if (resource == "ether")
				amountMaxToInvest = playerStats.Ether;
			
			if (presentInvestment > amountMaxToInvest)
				presentInvestment = amountMaxToInvest;
			
			inputAmount.GetComponent<InputField> ().text = presentInvestment.ToString ();
			infoInvestment.GetComponent<Text> ().text = "Total cost : " + presentInvestment;
		}
	}

}
