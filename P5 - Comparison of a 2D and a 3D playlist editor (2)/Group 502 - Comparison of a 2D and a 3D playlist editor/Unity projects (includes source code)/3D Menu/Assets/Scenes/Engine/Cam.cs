using UnityEngine;
using System.Collections;

public class Cam : MonoBehaviour {
	bool freeMove = false;
	public bool shiftBack = false;
	//string focus = "Songpool";
	float lerper = 0;
	float lerp2 = 0;
	float speed = 0.8f;
	Vector3 cabpos;
	Vector3 prevV, newV, edge;
	float [] freedom = {0,0,0,0,0,0}; //x min, x max, y min, y max, z min, z max
	Quaternion prevQ, newQ;
	GameObject cam;
	GameObject drawer;
	string readview = "Playlists";
	public Vector3 dPosSongPool; 
	public static bool moveBool; 
	public static bool moveLeft; 
	public static bool moveUp; 
	public float TopBorder = 1.25f;
	
	// Use this for initialization
	void Start () {
		cam = GameObject.Find("MainCAM");
		prevV = new Vector3(3.552612f,8.765217f, -3.883526f);
		newV = new Vector3(3.552612f,8.765217f, -3.883526f);
		cam.transform.position = Vector3.Lerp (prevV, newV, 1);
		fix ();
		moveBool = false;

	}
	
	// Update is called once per frame
	void Update () {
//		float yPos = cam.transform.position.y;
//		if (yPos < -1.25f)
//				moveUp = false; 



		if(Input.GetKeyUp("a")){
			fix ();
		}
		if(Input.GetKeyUp("s")){
			free ();
		}
		if(Input.GetKeyUp("d")){
			setFocus("Songpool");
			Debug.Log ("Songpool");
		}
		if(Input.GetKeyUp("f")){
			setFocus("Playlists");
			Debug.Log ("Playlists");
		}
		if(Input.GetKeyUp("g")){
			setFocus("CurrentPlaylist");
			Debug.Log ("CurrentPlaylists");
		}

		

		lerper += speed * Time.deltaTime;
		if (!freeMove || lerper < 1) {

			cam.transform.position = Vector3.Lerp(prevV, newV, lerper);
			cam.transform.rotation = Quaternion.Slerp(prevQ,newQ,lerper);
		}
		if (freeMove && lerper > 1) {
			if(cam.transform.position.x < newV.x-freedom[0]){
				cam.transform.position = new Vector3(newV.x-freedom[0],cam.transform.position.y, cam.transform.position.z);
			}
			if(cam.transform.position.x > newV.x+freedom[1]){
				cam.transform.position = new Vector3(newV.x+freedom[1],cam.transform.position.y, cam.transform.position.z);
			}
			if(cam.transform.position.y < newV.y-freedom[2]){
				cam.transform.position = new Vector3(cam.transform.position.x, newV.y-freedom[2], cam.transform.position.z);
			}
			if(cam.transform.position.y > newV.y+freedom[3]){
				cam.transform.position = new Vector3(cam.transform.position.x, newV.y+freedom[3], cam.transform.position.z);
			}
			if(cam.transform.position.z < newV.z-freedom[4]){
				cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, newV.z-freedom[4]);
			}
			if(cam.transform.position.z > newV.z+freedom[5]){
				cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, newV.z+freedom[5]);
			}
		}

		if(freeMove && isMovementAllowed(cam.transform.position) && !shiftBack){
			prevV = cam.transform.position;
			edge = getToLimitPoint(prevV);
			lerp2 = 0;
			shiftBack = true;
		}
		if(shiftBack){
			cam.transform.position = Vector3.Lerp(prevV, edge, lerp2);
			lerp2 += speed/4;
			if(lerp2 >= 1)
				shiftBack = false;
		}

	}

	public void free(){
		freeMove = true;
	}

	public void fix(){
		freeMove = false;
		prevV = new Vector3(cam.transform.position.x, cam.transform.position.y, cam.transform.position.z);
		prevQ = new Quaternion (cam.transform.rotation.x,cam.transform.rotation.y,cam.transform.rotation.z, cam.transform.rotation.w);
		lerper = 0;
	}

	public void setFocus(string dir){
		prevV = new Vector3(cam.transform.position.x, cam.transform.position.y, cam.transform.position.z);
		prevQ = cam.transform.rotation;

		switch(dir){
		case "Playlists":
			newV = new Vector3(3.552612f,8.765217f, -3.883526f);
			newQ = new Quaternion(0,0,0,cam.transform.rotation.w);
			noFreedom();
			fix ();
			readview = "Playlists";
			break;
		case "Songpool":
			newV = new Vector3(2.3f, -1.25f, -3.883526f);
			newQ = new Quaternion(0,0,0,cam.transform.rotation.w);
			readview = "Songpool";
			setFreedom(0,4,(Songpool.noOfGenres/2.5f),0,0,0);
			free ();
			moveBool = true; 
			break;
		case "Player":
			break;
		case "Cabinet":
			dPosSongPool = cam.transform.position;
			newV = new Vector3(cabpos.x, cabpos.y+0.5f, cabpos.z-1.2f);
			newQ = new Quaternion(cam.transform.rotation.x+0.3f,cam.transform.rotation.y,cam.transform.rotation.z,cam.transform.rotation.w);
			noFreedom();
			fix ();
			readview = "Cabinet";
			break;
		case "CurrentPlaylist":
			newV = new Vector3(-14f, 8.765217f, -3.883526f);
			newQ = new Quaternion(0,0,0,cam.transform.rotation.w);
			readview = "CurrentPlaylist";
			setFreedom(0,0,0,2,0,0);
			free ();
			break;
		case "SongpoolBack": 
			newV = dPosSongPool;
			newQ = new Quaternion(0,0,0,cam.transform.rotation.w);
			readview = "Songpool";
			//setFreedom(0,4,4,0,0,0);
			free ();
			moveBool = true; 

			break;
		}	
		lerper = 0;
		}

	void noFreedom(){
		setFreedom (0,0,0,0,0,0);
	}

	void setFreedom(float xMin, float xMax, float yMin, float yMax, float zMin, float zMax){
		freedom [0] = xMin;
		freedom [1] = xMax;
		freedom [2] = yMin;
		freedom [3] = yMax;
		freedom [4] = zMin;
		freedom [5] = zMax;
	}

	public float[] getFreedom(){
		return freedom;
	}

	public bool isMovementAllowed(Vector3 dir){
		Vector3 cur = cam.transform.position;
		bool ret = true;
		if(dir.x < cur.x-freedom[0] || dir.x > cur.x+freedom[1] || dir.y < cur.y-freedom[2] || dir.y > cur.y+freedom[3] || dir.z < cur.z-freedom[4] || dir.z > cur.z+freedom[5]){
			ret = false;
		}
		return ret;
	}

	public Vector3 getToLimitPoint(Vector3 dir){
		Vector3 ret = dir;
		Vector3 cur = cam.transform.position;
		if(ret.x < cur.x-freedom[0]){
			ret = new Vector3(cur.x-freedom[0], ret.y, ret.z);
		}
		if(ret.x > cur.x+freedom[1]){
			ret = new Vector3(cur.x+freedom[1], ret.y, ret.z);
		}
		if(ret.y < cur.y-freedom[2]){
			ret = new Vector3(ret.x, cur.y-freedom[2], ret.z);
		}
		if(ret.y > cur.y+freedom[3]){
			ret = new Vector3(ret.x, cur.y+freedom[3], ret.z);
		}
		if(ret.z < cur.z-freedom[4]){
			ret = new Vector3(ret.x, ret.y, cur.z-freedom[4]);
		}
		if(ret.z > cur.z+freedom[5]){
			ret = new Vector3(ret.x, ret.y, cur.z+freedom[5] );
		}

		return ret;
		}

	public bool checkIfFree(){
		if(freeMove && lerper >= 1){
			return true;
		}
		else{
			return false;
		}
	}

	public Vector3 getOrigin(){
		return newV;
	}

	public void setFocusCabinet(Vector3 tempv, GameObject tempd){
		cabpos = tempv;
		drawer = tempd;
		setFocus ("Cabinet");
	}

	public void setManualFocus(float tx, float ty, float tz){
		free ();
		prevV = new Vector3(cam.transform.position.x, cam.transform.position.y, cam.transform.position.z);
		prevQ = new Quaternion (cam.transform.rotation.x,cam.transform.rotation.y,cam.transform.rotation.z, cam.transform.rotation.w);
		newV = new Vector3 (tx, ty, tz);
		newQ = new Quaternion (cam.transform.rotation.x,cam.transform.rotation.y,cam.transform.rotation.z, cam.transform.rotation.w);

	}

	public void setManualFocus(float tx, float ty, float tz, float rx, float ry, float rz, float rw){
		free ();
		prevV = new Vector3(cam.transform.position.x, cam.transform.position.y, cam.transform.position.z);
		prevQ = new Quaternion (cam.transform.rotation.x,cam.transform.rotation.y,cam.transform.rotation.z, cam.transform.rotation.w);
		newV = new Vector3 (tx, ty, tz);
		newQ = new Quaternion (rx,ry,rz,rw);
		
	}

	public string readView(){
		return readview;
	}

	public void setSpeed(float temp){
		if (temp < 0) {
						Debug.LogError ("LERP SPEED MUST BE POSITIVE!");
				} else {
			speed = temp;		
		}
	}
}
