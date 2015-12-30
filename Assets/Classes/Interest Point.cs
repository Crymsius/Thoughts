using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InterestPoint : MonoBehaviour {

	private GameObject choices; 
	public Choice father { get; set; }
	public GameObject panelAff;
	public GameObject childPanel;

	private CaseScript caseS;

	public int IPnumber { get; set; }

	// Use this for initialization
	void Start () {
		choices= GameObject.Find ("Choices");

		caseS = gameObject.GetComponent<CaseScript> ();
		panelAff = Instantiate (GameObject.Find ("CanvasCase"));
		childPanel = panelAff.transform.FindChild("InfoCase").gameObject;

		panelAff.transform.parent = caseS.transform;
		panelAff.transform.localPosition = Vector3.zero;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		if (caseS.state == "Feeling Available") {
			// Effets du point d'intérêt à rajouter
			childPanel.SetActive (false);
			caseS.GetToRoad();
			father.SelfDestroy();
		}
	}

	void OnMouseEnter(){
		childPanel.GetComponentInChildren<Text>().text = "Choice " + father.numChoice + "\n" + "Option " + IPnumber;
		childPanel.SetActive (true);
	}

	void OnMouseExit(){
		childPanel.SetActive (false);
	}

}
