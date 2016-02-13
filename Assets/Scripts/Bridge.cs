using UnityEngine;
using System.Collections;

public class Bridge : MonoBehaviour {

	public int number { get; set;}
	public int position { get; set; }

	public Bridge linkedBridge { get; set;}

	public VisualHandler myHandler { get; set;}

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Renderer> ().enabled = gameObject.transform.parent.GetComponent<Renderer> ().enabled;
		myHandler = gameObject.GetComponent<VisualHandler> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown (){
		string text_sent = "From here, you can send some Ether to the physical world." +
			"\n\nThe nearest you are from the bridge, the cheaper it will be." +
			"\n\nIn the physical world, you can transform invested Ether into new tiles.";
		myHandler.OpenObjectInfo ("Bridge "+number, text_sent, true, "ether");
	}
}
