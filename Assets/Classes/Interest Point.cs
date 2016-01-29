using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InterestPoint : MonoBehaviour {

	private PlayerBehaviour player;

	private GameObject choices; 
	public Choice father { get; set; }
	public GameObject panelAff;
	public GameObject childPanel;
	public GameObject childPanelCost;

	private CaseScript caseS;

	public int IPnumber { get; set; }
	public int cost { get; set; }
	public int etherCost { get; set; }

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player").GetComponent<PlayerBehaviour> ();
		choices= GameObject.Find ("Choices");

		caseS = gameObject.GetComponent<CaseScript> ();
		panelAff = Instantiate (GameObject.Find ("CanvasCase"));
		childPanel = panelAff.transform.FindChild("InfoCase").gameObject;
		childPanelCost = panelAff.transform.FindChild("InfoCout").gameObject;

		panelAff.transform.SetParent (caseS.transform);
		panelAff.transform.localPosition = Vector3.zero;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		if (caseS.state == "Feeling Available") {
			
			if (player.ReflexionPoints >= cost && player.Ether >= etherCost) {
				player.ReflexionPoints -= cost;
				// Effets du point d'intérêt à rajouter
				caseS.GetToRoad ();
				father.SelfDestroy ();
			} else {
				if (cost > 0 && player.ReflexionPoints < cost) {
					StartCoroutine (player.infoText.PrintMessage ("NotEnoughPR"));
				}
				if (etherCost > 0 && player.Ether < etherCost) {
					StartCoroutine (player.infoText.PrintMessage ("NotEnoughEther"));
				}
			}

		}
	}

	void OnMouseEnter(){
		childPanel.GetComponentInChildren<Text>().text = "Choice " + father.numChoice + "\n" + "Option " + IPnumber;
		childPanel.SetActive (true);
		if (cost != 0 || etherCost !=0) {
			string text = "Cost : ";
			if (cost != 0)
				text += cost + " PR ";
			if (etherCost != 0)
				text += etherCost + " Ether";
			childPanelCost.GetComponentInChildren<Text>().text = text;
			childPanelCost.SetActive (true);
		}
	}

	void OnMouseExit(){
		childPanel.SetActive (false);
		childPanelCost.SetActive (false);
	}

	public void SelfDestroy(){
		Destroy (childPanel.GetComponentInChildren<Text> ());
		Destroy (childPanel);
		Destroy (childPanelCost.GetComponentInChildren<Text> ());
		Destroy (childPanelCost);
		Destroy (panelAff);
	}
}
