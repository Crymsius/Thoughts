using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CaseV : MonoBehaviour {

	private GameObject attachedTo;
	private GridClass mainGrid;
	private GridVClass myGrid;
	private PlayerBehaviour player;

	public GameObject[] myCases { get; private set;}

	public int width { get; private set;}

	public Vector3 myPosition { get; set;}
	public float absoluteWeight { get; set; }
	public float timesWeight { get; set; }

	private bool notExpanded = true;

	void Start(){

		width = 5;

		attachedTo = gameObject;
		myPosition = gameObject.GetComponent<Transform> ().position;
		mainGrid = GameObject.Find ("Grid").GetComponent<GridClass> ();
		myGrid = GameObject.Find ("Grid").GetComponent<GridVClass> ();
		player = GameObject.Find ("Player").GetComponent<PlayerBehaviour> ();
		myCases = new GameObject[(int)Mathf.Pow(width,2)];
		timesWeight = 1f;

		if (myPosition != Vector3.zero) {
			absoluteWeight = Mathf.Pow(width,2);
			if (myGrid.Exists ((int)myPosition.x, (int)myPosition.z))
				Destroy (gameObject);
			else
				myGrid.SetCase (gameObject, (int)myPosition.x, (int)myPosition.z);
		} 
		else
			absoluteWeight = Mathf.Pow(width,2)-1;
		Invoke ("GetMyCases", 3/2f);
	}

	void Update(){
		if (notExpanded) {
			float distance = Mathf.Max(Mathf.Abs (myPosition.x - player.positionV.x) ,
				Mathf.Abs(myPosition.z - player.positionV.z)) ;
			if (distance<=1) {
				AutoExpand expandScript = GetComponent<AutoExpand> ();
				expandScript.expand ("CaseV");
				expandScript.enabled = false;
				notExpanded = false;
			}
		}
	}

	public void GetMyCases(){
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < width; j++) {
				GameObject aCase = mainGrid.GetCase ( width * (int)myPosition.x + i - (width-1)/2,
					width * (int)myPosition.z + j - (width-1)/2);
				aCase.GetComponent<CaseScript> ().caseV = gameObject.GetComponent<CaseV> ();
				myCases [width * i + j] = aCase;
			}
		}
	}

	public void SetPlayer(){
		player.positionV = myPosition;
		absoluteWeight--;
	}

	public List<CaseV> GetAround(){
		List<CaseV> around = new List<CaseV>();
		for (int i = 0; i < 3; i++) {
			for (int j = 0; j < 3; j++) {
				if (i != 1 || j != 1) {
					around.Add (myGrid.GetCase ((int)myPosition.x + i - 1,
						(int)myPosition.z + j - 1).GetComponent<CaseV> ());
				}
			}
		}
		return around;
	}

	public float GetWeight(){
		return absoluteWeight * timesWeight;
	}

}
