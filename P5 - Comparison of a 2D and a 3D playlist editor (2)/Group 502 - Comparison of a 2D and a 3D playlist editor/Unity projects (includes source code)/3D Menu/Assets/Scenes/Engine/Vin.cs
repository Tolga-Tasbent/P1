using UnityEngine;
using System.Collections;

public class Vin : MonoBehaviour {
	string title;
	// Use this for initialization
	public Vin(string tempt){
		title = tempt;
	}

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public string getTitle(){
		return title;
	}
}
