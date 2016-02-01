using UnityEngine;
using System.Collections;

public class EtherScript2 : MonoBehaviour {

	public Animator animator;
	public Transform sphere;
	public GameObject player;
	public SpriteRenderer spriteRenderer;

	public int amountMax { get; set;}
	public int amount { get; set;}

	public float appearanceRate { get; set;}
	public float growthRate { get; set;}
	public float unstability { get; set;}	
	public int mediumGrowth { get; set;}

	// Use this for initialization
	void Start () {

		animator = GameObject.Find("EtherSprite").GetComponent<Animator>();

		amountMax = 10;
		amount = 0;

		appearanceRate = 1f / 100;
		growthRate = 1f / 4;
		unstability = 1f / 20;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public IEnumerator MAJ(){
		
		if (amount > 0 && Random.Range (0f, 1f) < growthRate)
			amount ++;
		else if (amount == 0 && Random.Range (0f, 1f) < appearanceRate){ //apparition
			amount ++;
			animator.SetInteger("Ether", amount);
			spriteRenderer.enabled=true;
		}
		
		
		if (amount > amountMax)
			amount = amountMax;
		
		if (Random.Range (0f, 1f) < (float)amount / amountMax * unstability)
			amount = 0;

		animator.SetInteger("Ether", amount);
		Invoke ("MAJproperties", 1 / 10f);

		//MAJproperties ();
		yield return null;
	}

	public void MAJproperties(){
		sphere.localScale = 10f * amount / amountMax * Vector3.one;
	}
	

	public void PreMAJproperties(){
		player.GetComponent<PlayerBehaviour> ().Ether += amount;
		player.GetComponent<PlayerBehaviour> ().printer.MAJResources ();
		amount = 0;
		MAJproperties ();
	}
}
