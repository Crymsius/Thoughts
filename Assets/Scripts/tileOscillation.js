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
	var pos = Random.value;
	var amor = Mathf.Abs(pos - Mathf.Abs(tile.position.y));
	if(tile.position.y <=0) {
		while(tile.position.y < (pos-0.05)) {
			tile.Translate(Vector3.up * Time.deltaTime * speed);
			yield;
		}
	} else {
		while(tile.position.y > (-pos+0.05)) {
			tile.Translate(Vector3.down * Time.deltaTime * speed);
			yield;
		}
	}
	//yield WaitForSeconds(0f);
	yield;
	tile.hasChanged = false;
}

