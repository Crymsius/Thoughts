using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bridge : MonoBehaviour {

	public int number { get; set;}
	public string location { get; set;}
	public Vector3 position { get; set; }

	public int etherStock { get; set; }
	public float etherEvapRate { get; set; }

	public Bridge linkedBridge { get; set;}

	public VisualHandler myHandler { get; set;}

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Renderer> ().enabled = gameObject.transform.parent.GetComponent<Renderer> ().enabled;
		position = gameObject.GetComponent<Transform> ().position;
		myHandler = gameObject.GetComponent<VisualHandler> ();
		etherStock = 0;
		etherEvapRate = 1f / 20;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown (){
		Dictionary<string,string> toSend = new Dictionary<string,string>();
		toSend ["name"] = "Bridge " + number;
		toSend ["resource"] = "ether";
		toSend ["button"] = "CreatePhysicalTile";

		if (location == "ethereal") {
			toSend["text"]= "From here, you can send some Ether to the physical world." +
			            "\n\nThe nearest you are from the bridge, the cheaper it will be." +
			            "\n\nIn the physical world, you can transform invested Ether into new tiles.";
			toSend["investor"] = "yes";
			toSend ["button"] = null;
		} else {
			toSend["text"]= "From here, you can use the invested Ether to create physical tiles." +
			            "\n\nThe nearest you are from the bridge, the cheaper it will be.";
			toSend["investor"] = "no";
			toSend ["button"] = "CreatePhysicalTile";
		}

		myHandler.OpenObjectInfo (gameObject , toSend);
		linkedBridge.myHandler.temporaryButton = myHandler.temporaryButton;
	}

	public void Wait(){
		addEther( -(int)Mathf.Floor (Random.Range ( 0.75f * etherStock * etherEvapRate 
			,1.25f * etherStock * etherEvapRate)));
		linkedBridge.etherStock = etherStock;
	}

	public void addEther(int num){
		etherStock += num;
		linkedBridge.etherStock = etherStock;
	}

}
