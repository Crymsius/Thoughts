using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AffResources : MonoBehaviour {

	public Text textError;
	public Text amountTime;
	public Text amountReflexionPoints;
	public Text amountEther;
	public PlayerBehaviour player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player").GetComponent<PlayerBehaviour> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void MAJResources(){
		amountTime.text = player.time.ToString();
		amountReflexionPoints.text = player.ReflexionPoints.ToString();
		amountEther.text = player.Ether.ToString();
	}

	public IEnumerator PrintMessage(string type){
		if (type == "NotEnoughPR") {
			textError.text += "You don't have enough Reflexion Points to discover that thought\n";
		} else if (type == "NotEnoughEther") {
			textError.text += "You don't have enough Ether to discover that thought\n";
		}
		yield return new WaitForSeconds(2.0F);
		textError.text = "";
	}

}
