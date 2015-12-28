using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour {

	public Transform position { get; private set;}
	public Vector3 positionV { get; set; }

	// Use this for initialization
	void Start () {
		position = (Transform)GetComponent<Transform> ();
		positionV = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
