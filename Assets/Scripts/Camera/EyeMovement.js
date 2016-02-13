#pragma strict
/*
	Script permettant les mouvements de la caméra via souris et clavier.
*/

/*

	INPUTS :

	- MOUSE POSITION (x et y)
	- MOUSE SCROLL
	- KEYBOARD ("horizontal" et "vertical") = flèches
	- KEYBOARD (shift)


*/

// vitesse de translation rapide (shift enfoncé)

var translationSpeed : float = 2;

function Start ()
{

}

function Update () {

	/*----------------------------*/
	/* ----keyboard scrolling ----*/
	/*----------------------------*/

	var translationX : float = Input.GetAxis("Horizontal");
	var translationY : float = Input.GetAxis("Vertical");
	var fastTranslationX : float = translationSpeed * Input.GetAxis("Horizontal");
	var fastTranslationY : float = translationSpeed * Input.GetAxis("Vertical");

	if (Input.GetKey(KeyCode.LeftShift))
		// Translation plus rapide 
		{
			// ft = ftx(1,0,1) + fty(-1,0,1)
			transform.Translate(fastTranslationX - fastTranslationY, 0, fastTranslationX + fastTranslationY);
		}

	else
		// Translation vitesse normale
		{
			// t = tx(1,0,1) + ty(-1,0,1)
			transform.Translate(translationX - translationY, 0, translationX + translationY);
		}

	/*----------------------------*/
	/* -----Mouse scrolling ------*/
	/*----------------------------*/

	// Prend pour input les positions de la souris "Input.mousePosition" (.x et .y)

	var mousePosX = Input.mousePosition.x;
	var mousePosY = Input.mousePosition.y;
	var scrollDistance : int = 5;
	var scrollSpeed : float = 70;

	//Horizontal camera movement

	if (mousePosX < scrollDistance)
		//horizontal, left
		{
			transform.Translate(-1, 0, -1);
		}
	if (mousePosX >= Screen.width - scrollDistance)
		//horizontal, right
		{
			transform.Translate(1, 0, 1);
		}

	//Vertical camera movement
	if (mousePosY < scrollDistance)
		//scrolling down
		{
			transform.Translate(1, 0, -1);
		}
	if (mousePosY >= Screen.height - scrollDistance)
		//scrolling up
		{
			transform.Translate(-1, 0, 1);
		}

	/*----------------------------*/
	/* --------- Zooming ---------*/
	/*----------------------------*/
	
	var Eye : GameObject = GameObject.Find("Eye");

	//
	if (Input.GetAxis("Mouse ScrollWheel") > 0 && Eye.GetComponent.<Camera>().orthographicSize > 4)
		{
			Eye.GetComponent.<Camera>().orthographicSize = Eye.GetComponent.<Camera>().orthographicSize - 4;
		}

	//
	if (Input.GetAxis("Mouse ScrollWheel") < 0 && Eye.GetComponent.<Camera>().orthographicSize < 80)
		{
			Eye.GetComponent.<Camera>().orthographicSize = Eye.GetComponent.<Camera>().orthographicSize + 4;
		}

	//default zoom
	if (Input.GetKeyDown(KeyCode.Mouse2))
		{
			Eye.GetComponent.<Camera>().orthographicSize = 50;
		}
	
}
