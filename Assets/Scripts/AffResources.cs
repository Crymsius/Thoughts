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
		amountRes.text = player.time + "\n\n" + player.ReflexionPoints + "\n" + player.Ether + "\n";
	}

	public IEnumerator PrintMessage(string type){
		if (type == "NotEnoughPR") {
			amountRes.text += "You don't have enough Reflexion Points to discover that thought\n";
		} else if (type == "NotEnoughEther") {
			amountRes.text += "You don't have enough Ether to discover that thought\n";
		}
		yield return new WaitForSeconds(2.0F);
		amountRes.text = "";
	}

}
