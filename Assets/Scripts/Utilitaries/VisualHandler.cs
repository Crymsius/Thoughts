using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VisualHandler : MonoBehaviour {

	public GameObject switchButton;
	public GameObject panelInfo;
	public GameObject investor;

	public Text nameText { get; set;}
	public Text infoText { get; set;}

	// Use this for initialization
	void Start () {
		if (panelInfo != null) {
			nameText = panelInfo.transform.FindChild ("NameObject").GetComponent<Text>();
			infoText = panelInfo.transform.FindChild ("InfoObject").GetComponent<Text>();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (switchButton!=null && Input.GetKeyDown (KeyCode.C))
			switchButton.SetActive (!switchButton.activeSelf);
	}

	public void OpenObjectInfo(string name, string text, bool needInvestor, string resource){
		nameText.text = name;
		infoText.text = text;
		investor.GetComponent<Investor> ().SetActive (needInvestor, resource);
	}

}
