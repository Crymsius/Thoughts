using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Case3 : MonoBehaviour {

	private GameObject attachedTo;
	private GridClass mainGrid;
	private Grid3Class myGrid;
	private PlayerBehaviour player;

	public GameObject[] myCases { get; private set;}

	public Vector3 myPosition { get; set;}
	public float absoluteWeight { get; set; }
	public float timesWeight { get; set; }

	private bool notExpanded = true;

	void Start(){
		
		attachedTo = gameObject;
		myPosition = gameObject.GetComponent<Transform> ().position;
		mainGrid = GameObject.Find ("Grid").GetComponent<GridClass> ();
		myGrid = GameObject.Find ("Grid").GetComponent<Grid3Class> ();
		player = GameObject.Find ("Player").GetComponent<PlayerBehaviour> ();
		myCases = new GameObject[9];
		timesWeight = 1f;

		if (myPosition != Vector3.zero) {
			absoluteWeight = 9;
			if (myGrid.Exists ((int)myPosition.x, (int)myPosition.z))
				Destroy (gameObject);
			else
				myGrid.SetCase (gameObject, (int)myPosition.x, (int)myPosition.z);
		} else
			absoluteWeight = 8;
		Invoke ("GetMyCases", 1f);
	}

	void Update(){
		if (notExpanded) {
			float distance = Mathf.Max(Mathf.Abs (myPosition.x - player.positionV.x) ,
				Mathf.Abs(myPosition.z - player.positionV.z)) ;
			if (distance<=2) {
				AutoExpand expandScript = GetComponent<AutoExpand> ();
				expandScript.expand ("CaseV");
				expandScript.enabled = false;
				notExpanded = false;
			}
		}
	}

	public void GetMyCases(){
		for (int i = 0; i < 3; i++) {
			for (int j = 0; j < 3; j++) {
				GameObject aCase = mainGrid.GetCase ( 3 * (int)myPosition.x + i - 1, 3 * (int)myPosition.z + j - 1);
				aCase.GetComponent<CaseScript> ().caseV = gameObject.GetComponent<Case3> ();
				myCases [3 * i + j] = aCase;
			}
		}
	}

	public void SetPlayer(){
		player.positionV = myPosition;
		absoluteWeight--;
	}

	public List<Case3> GetAround(){
		List<Case3> around = new List<Case3>();
		for (int i = 0; i < 3; i++) {
			for (int j = 0; j < 3; j++) {
				if (i != 1 || j != 1)
					around.Add(myGrid.GetCase ((int)myPosition.x + i - 1,
						(int)myPosition.z + j - 1).GetComponent<Case3>());
			}
		}
		return around;
	}

	public float GetWeight(){
		return absoluteWeight * timesWeight;
	}

}
