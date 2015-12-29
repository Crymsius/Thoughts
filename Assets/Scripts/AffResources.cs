using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AffResources : MonoBehaviour {

	public Text amountRes;
	public PlayerBehaviour player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player").GetComponent<PlayerBehaviour> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void MAJResources(){
		amountRes.text = player.time + "\n\n" + player.ReflexionPoints;
	}

}
