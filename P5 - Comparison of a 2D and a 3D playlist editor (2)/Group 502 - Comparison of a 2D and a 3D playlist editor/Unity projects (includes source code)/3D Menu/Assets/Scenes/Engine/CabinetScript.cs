using UnityEngine;
using System.Collections;

public class CabinetScript : MonoBehaviour {
	public bool open = false;
	Vector3 prevV;
	Vector3 newV;
	float lerper = 0;
	// Use this for initialization
	void Start () {
		prevV = transform.position;
		newV = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.Lerp (prevV, newV,lerper);
		lerper += 3f * Time.deltaTime;

	}

	public void Open(){
		if(!open){
			prevV  = transform.position;
			newV = new Vector3(transform.position.x, transform.position.y, transform.position.z-0.4f);
			open = true;
			lerper = 0;
		}


	}
	public void Close(){
		if (open) {
			prevV  = transform.position;
			newV = new Vector3(transform.position.x, transform.position.y, transform.position.z+0.4f);
			open = false;
			lerper = 0;
		}

	}

	public void Shift(){
		if(open == false){
			prevV  = transform.position;
			newV = new Vector3(transform.position.x, transform.position.y, transform.position.z-0.4f);
			open = true;
			lerper = 0;
		}
		else if (open == true) {
			prevV  = transform.position;
			newV = new Vector3(transform.position.x, transform.position.y, transform.position.z+0.4f);
			open = false;
			lerper = 0;		
		}
	}
}
