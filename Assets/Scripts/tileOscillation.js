#pragma strict

/*

Le script a pour fonction d'animer les tiles composant une case du chemin de pensée

*/

var tiles : Transform[];
var speed : float;
var speedApp : float;
var check : int;
var smoothTime : float;
private var yVelocity = 0.0;
private var velocity = Vector3.zero;

function Start () {

	var i = 0;
	check = 0;

	tiles = new Transform[transform.childCount];
		for (var t : Transform in transform) {
			tiles[i++] = t;
			t.hasChanged = false;
		}
}

function Update () {
	if (Input.GetKeyDown ("space")) {
			Renew();
		}
	if (Input.GetKeyDown (KeyCode.P)) {
		for (var l = 0; l< 16; l++) {
			Restart(l);
		}
	}
	if (check==15) {
		for (var j = 0; j < 16; j++) {
			if (!tiles[j].hasChanged) {
				Oscillate(tiles[j]);
			}
		}
	}
}

function Renew() {
	for (var i = 0; i< 16; i++) {
		tiles[i].position.y = -10;
		//var pos = Random.value; //position à atteindre	
		tiles[i].hasChanged = false;
	}
}

function Restart(i :int) {
	var pos = Random.value * 2 - 1; // position à atteindre entre -1 et 1
	var wait = Random.value * 1.5; // attente random avant de monter 
	yield WaitForSeconds (wait);
	while(tiles[i].position.y < pos && tiles[i].position.y<=1) {
		tiles[i].position = Vector3.SmoothDamp(tiles[i].position, Vector3(tiles[i].position.x, pos, tiles[i].position.z), velocity, smoothTime);
		yield;
	}

	//yield WaitForSeconds ();
	tiles[i].hasChanged = false;
	check++;
}

function Oscillate(tile :Transform) {
	var pos = Random.value; //position à atteindre
	var amor = Mathf.Abs(pos - Mathf.Abs(tile.position.y)); //amortissement
	
	if(tile.position.y <=0) {
		//si position actuelle <=0 :
		while(tile.position.y < (pos)) {
			tile.Translate(Vector3.up * Time.deltaTime * speed);
			yield;
		}
	} else {
		// si position actuelle > 0
		while(tile.position.y > (-pos)) {
			tile.Translate(Vector3.down * Time.deltaTime * speed);
			yield;
		}
	}
	//yield WaitForSeconds(0f);
	yield;
	tile.hasChanged = false;
}

