#pragma strict

/*

Le script a pour fonction d'animer les tiles composant une case du chemin de pensée

*/

var tiles : Transform[];
var speed : float;

function Start () {

		var i = 0;

	tiles = new Transform[transform.childCount];
		for (var t : Transform in transform) {
			tiles[i++] = t;
			t.hasChanged = false;
		}
}

function Update () {
	for (var i = 0; i< 16; i++) {
		if (!tiles[i].hasChanged) {
			Oscillate(tiles[i]);
		}
	}
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

