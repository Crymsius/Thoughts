using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour {

	private Transform position;

	// Use this for initialization
	void Start () {
		position = (Transform)GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
