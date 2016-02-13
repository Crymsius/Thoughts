using UnityEngine;
using System.Collections;

public class PositionHandler : MonoBehaviour {

	public GameObject myObject;
	public Transform myPos { get; set; }

	public GridClass myGridHandler { get; set; }

	void Awake(){
		myPos = myObject.GetComponent<Transform> ();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//May be improvable
	public void SetEtherealPosition(Vector3 pos){
		myPos.position += 10*pos;
		myObject.transform.parent = myGridHandler.GetCase ((int)pos.x, (int)pos.z).transform;
		myGridHandler.GetCase ((int)pos.x, (int)pos.z).GetComponent<CaseScript> ().attachedObject = myObject;
	}

	public void SetPhysicalPosition(Vector3 pos){
		myPos.position += 10*pos;
		myObject.transform.parent = myGridHandler.GetCase ((int)pos.x, (int)pos.z).transform;
		myGridHandler.GetCase ((int)pos.x, (int)pos.z).GetComponent<RealCase> ().attachedObject = myObject;
	}
}
