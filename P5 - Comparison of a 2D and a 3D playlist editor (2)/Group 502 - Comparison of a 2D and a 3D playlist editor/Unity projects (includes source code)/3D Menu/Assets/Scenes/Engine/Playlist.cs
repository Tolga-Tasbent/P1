using UnityEngine;
using UnityEngine.UI;

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

[Serializable()]
public class Playlist : System.Object {
	public List<PlaylistItem> PL = new List<PlaylistItem>();
	public string name;
	public int length;
	public int lastPlay;


	public Playlist(string name){
		this.name = name;

//		GameObject playlistCube = MonoBehaviour.Instantiate(PlaylistDisplay.PlayListPrefab) as GameObject; //Setting the prefab from main
//		
//		//OLD CODE: playlistCube = GameObject.CreatePrimitive (PrimitiveType.Cube); 
//		//OLD CODE: playlistCube.transform.localScale += new Vector3 (-0.3f,2,0);
//		playlistCube.name = name;
//		//Getting the greatgrandchild (Playlistprefab ->  Canvas -> Button -> Text) and
//		//Changing the text to the name of the playlist.
//		
//		playlistCube.gameObject.transform.GetChild (0).gameObject.transform.GetChild (0).gameObject.transform.GetChild (1).GetComponent<Text> ().text = playlistCube.name; 
//		
//		Transform neww = playlistCube.gameObject.transform.GetChild(0).gameObject.transform.GetChild(0);
//		Debug.Log (neww.name);
//		Touch m = new Touch(); 
//		
//		neww.GetComponent<Button>().onClick.AddListener (
//			() => {m.OpenPlayList(playlistCube.name);}
//		); 
//		playlistCube.transform.position = new Vector3 (-0.241426f + pos, 7.3f,6.220327f+order);

	}

	public void add(PlaylistItem item){
		PL.Add (item);
		length++;

	}

	public PlaylistItem find(string a){
		PlaylistItem res = new PlaylistItem("empty");
		foreach(PlaylistItem p in PL){
			if(p.title == a){
				res = p;
			}

		}
		if (res.location != "empty") {
			return res;
		} else {
			return(new PlaylistItem("null"));
		}
	}

	public void remove(PlaylistItem item){
		PL.Remove (item);
		PL.TrimExcess ();
		length--;

	}

	public int getNoOfItems(){
		return length;

	}

	public PlaylistItem getNext(int count){
		if (count + 1 < length) {
						return PL [count + 1];
				} else {
			return PL[length-1]		;
		}
	}

	public PlaylistItem getPrev(int count){
		if (count - 1 >= 0) {
						return PL [count - 1];
				} else {
			return PL[0];		
		}
	}

	public void show(int pos){
		
	}



}

public class PlaylistDisplay: System.Object{

	//Old code: GameObject playlistCube;
	GameObject label;
	public static GameObject PlayListPrefab;
	int order;
	public bool icon = false;
	Playlist p;

	public PlaylistDisplay(Playlist p){
		this.p = p;
	}
	public void testFunction () {
		Debug.Log ("Man"); 
	}



	public void show(int pos){
		GameObject.Destroy (GameObject.Find(p.name+"*"));
		GameObject.Destroy (GameObject.Find(p.name+"* label"));
		//OLD CODE: playlistCube = new GameObject ();
		// load from prefab 		
		GameObject playlistCube = MonoBehaviour.Instantiate(PlayListPrefab) as GameObject; //Setting the prefab from main

		//OLD CODE: playlistCube = GameObject.CreatePrimitive (PrimitiveType.Cube); 
		//OLD CODE: playlistCube.transform.localScale += new Vector3 (-0.3f,2,0);
		playlistCube.name = p.name;
		//Getting the greatgrandchild (Playlistprefab ->  Canvas -> Button -> Text) and
		//Changing the text to the name of the playlist.

		playlistCube.gameObject.transform.GetChild (0).gameObject.transform.GetChild (0).gameObject.transform.GetChild (1).GetComponent<Text> ().text = playlistCube.name; 

		Transform neww = playlistCube.gameObject.transform.GetChild(0).gameObject.transform.GetChild(0);
		Debug.Log (neww.name);
		Touch m = new Touch(); 

			neww.GetComponent<Button>().onClick.AddListener (
													() => {m.OpenPlayList(playlistCube.name);}
															); 
		if(p.lastPlay< 3){
			order = 0;
		}
		else if(p.lastPlay < 10){
			order = 1;
		}
		else{
			order = 2;
		}
		if (icon) {
			Renderer rend = playlistCube.GetComponent<Renderer>();
			rend.material.shader = Shader.Find("Transparent/Diffuse");
			rend.material.SetColor("_SpecColor", Color.red);
			order = 0;
		}
		playlistCube.transform.position = new Vector3 (-0.241426f + pos, 7.3f,6.220327f+order);

		//playlistCube.transform.position = new Vector3 (playlistCube.transform.position.x + pos, playlistCube.transform.position.y,playlistCube.transform.position.z+order);


		// OLD CODE: label = new GameObject(playlistCube.name+" label");
		// OLD CODE:TextMesh tml = (TextMesh)label.AddComponent<TextMesh>();
		// OLD CODE:MeshRenderer mrl = (MeshRenderer)label.AddComponent<MeshRenderer>();
		// OLD CODE:tml.text = p.name;
		// OLD CODE:tml.transform.position = new Vector3(playlistCube.transform.position.x,playlistCube.transform.position.y,playlistCube.transform.position.z-0.6f);
		//tml.transform.position = new Vector3 ();
		// OLD CODE:tml.offsetZ = 0;
		// OLD CODE:tml.characterSize = 0.4f;
		// OLD CODE:tml.lineSpacing = 1;
		// OLD CODE:tml.anchor = TextAnchor.MiddleCenter;
		// OLD CODE:tml.alignment = TextAlignment.Center;
		// OLD CODE:tml.font = (Font)Resources.GetBuiltinResource (typeof(Font), "Arial.ttf");
		// OLD CODE:tml.color = new Color(1,0,0);
		// OLD CODE:MeshRenderer rend2 = label.GetComponentInChildren<MeshRenderer>();
		// OLD CODE:rend2.material = tml.font.material;
		// OLD CODE:rend2.material.shader = Shader.Find ("Custom/txt");
		
	}
	

}

