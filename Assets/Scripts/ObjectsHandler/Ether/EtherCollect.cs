using UnityEngine;
using System.Collections;

public class EtherCollect : MonoBehaviour {

	public Transform sphere;
	public GameObject player;

	// Use this for initialization
	void Start () {
		sphere = gameObject.GetComponent<Transform> ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseEnter(){
		GetComponentInParent<EtherScript2> ().PreMAJproperties();
	}
}
