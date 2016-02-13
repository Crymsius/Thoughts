using UnityEngine;
using System.Collections;

public class EtherSpriteController : MonoBehaviour {

	public Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void MajAmount(int amount) {
		animator.SetInteger("Ether", amount);
	}

	public void Vanish() {
		animator.SetInteger("Ether", 0);
	}
}
