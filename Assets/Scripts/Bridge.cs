using UnityEngine;
using System.Collections;

public class Bridge : MonoBehaviour {

	public int number { get; set;}
	public int position { get; set; }

	public Bridge linkedBridge { get; set;}

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Renderer> ().enabled = gameObject.transform.parent.GetComponent<Renderer> ().enabled;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
