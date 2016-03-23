using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;

using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class Songpool : System.Object{
	public List<PlaylistItem> songpool = new List<PlaylistItem>();
	IFormatter formatter = new BinaryFormatter();
	GameObject cube;
	GameObject vinyl;
	GameObject smpltxt;
	List<GameObject> labels = new List<GameObject>();
	MonoBehaviour m = new MonoBehaviour();
	List<Artist> artists = new List<Artist>();
	List<Genre> genres = new List<Genre>();
	int swipeNo = 0;
	public static int noOfGenres; 


	// Use this for initialization
	void Start () {
		load ();
		sortArtists ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Songpool returnThis(){
		return this;
	}

	public void show(GameObject GenrePrefab){ //NEW CODE
		float row = 0;
		float col = 0;
		//cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
		cube = GameObject.FindGameObjectWithTag("Cabinet");
		vinyl = GameObject.Find("Vinyl");
		foreach(Genre g in genres){
			col = 0;
			noOfGenres += 1; 
			foreach(Artist a in g.artists){
				GameObject c = (GameObject)MonoBehaviour.Instantiate(cube) as GameObject;
				col+=0.6f;
				c.transform.position = new Vector3(cube.transform.position.x+col, cube.transform.position.y-row, cube.transform.position.z);

//**CHANGED*****************************************************************//

				c.name = a.name;
				Transform child = c.gameObject.transform.GetChild(0);
				Transform grandChild = child.gameObject.transform.GetChild(0); 
				Text _artistName = grandChild.GetComponent<Text>();
				_artistName.text = a.name;
				Touch m = new Touch(); 

				child.gameObject.transform.GetChild(1).GetComponent<Button>().onClick.AddListener (
					() => {m.OpenCabinet(c.name);}
				); 

				/*GameObject label = new GameObject(" label");
				label.transform.parent = c.transform;
				TextMesh tml = (TextMesh)label.AddComponent("TextMesh");
				MeshRenderer mrl = (MeshRenderer)label.AddComponent("MeshRenderer");
				tml.text = c.name;
				tml.transform.position = new Vector3(c.transform.position.x,c.transform.position.y-0.1f,c.transform.position.z-0.4f);
				tml.offsetZ = 0;
				tml.characterSize = 0.05f;
				tml.lineSpacing = 1;
				tml.anchor = TextAnchor.MiddleCenter;
				tml.alignment = TextAlignment.Center;
				tml.font = (Font)Resources.GetBuiltinResource (typeof(Font), "Arial.ttf");
				tml.color = new Color(0,0,0);
				MeshRenderer rend2 = label.GetComponentInChildren<MeshRenderer>();
				rend2.material = tml.font.material;
				tml.gameObject.layer = 2;*/
//**************************************************************************//

				float tmpv = 0.1f;
				foreach(PlaylistItem pli in a.songs){
					//GameObject v = (GameObject)MonoBehaviour.Instantiate(vinyl) as GameObject;
//**CHANGED*****************************************************************//
					Transform v = c.gameObject.transform.GetChild(1);

					v.transform.SetParent(c.transform, false);
					//v.transform.position = new Vector3(c.transform.position.x, c.transform.position.y, c.transform.position.z-0.3f+tmpv);
					v.name = pli.title;

					Transform childVinyl = v.gameObject.transform.GetChild(0);
					Transform grandChildVinyl = childVinyl.gameObject.transform.GetChild(0); 
					Text _songTitle = grandChildVinyl.GetComponent<Text>();
					_songTitle.text = v.name;

					childVinyl.gameObject.transform.GetChild(1).GetComponent<Button>().onClick.AddListener (
						() => {m.AddSongToPlayList(pli);}
					); 




					tmpv += 0.1f;
					v.gameObject.AddComponent<Vin>();
//**************************************************************************//


					/*GameObject vinlabel = new GameObject(" vinlabel");
					vinlabel.transform.parent = v.transform;
					TextMesh vintml = (TextMesh)vinlabel.AddComponent<TextMesh>();
					MeshRenderer vinmrl = (MeshRenderer)vinlabel.AddComponent<MeshRenderer>();
					vintml.text = v.name.TrimEnd('*');
					vintml.transform.position = new Vector3(v.transform.position.x,v.transform.position.y,v.transform.position.z-0.1f);
					vintml.transform.localScale = new Vector3(1,1,1);
					vintml.offsetZ = 0;
					vintml.characterSize = 0.05f;
					vintml.lineSpacing = 1;
					vintml.anchor = TextAnchor.MiddleCenter;
					vintml.alignment = TextAlignment.Center;
					vintml.font = (Font)Resources.GetBuiltinResource (typeof(Font), "calibri.ttf");
					vintml.color = new Color(1,1,1);
					MeshRenderer rend3 = vinlabel.GetComponentInChildren<MeshRenderer>();
					//Material mat = tml.font.material;
					//mat.shader = Shader.Find("Custom/txt");
					//rend3.material = mat;
					vintml.gameObject.layer = 2;*/
				}

				//c.tag = "drawer";

				//MonoBehaviour.Instantiate(Resources.Load("Cabinet", typeof(GameObject)));
				//GameObject c = (GameObject)MonoBehaviour.Instantiate(Resources.Load("Cabinet"));
				//c.transform.position = new Vector3(c.transform.position.x+col, c.transform.position.y+row, c.transform.position.z);


			}

			GameObject genlabel = MonoBehaviour.Instantiate(GenrePrefab) as GameObject;
			
			genlabel.name = g.name;
			Transform childTextGenre = genlabel.gameObject.transform.GetChild(0);
			Transform grandChildGenre = childTextGenre.gameObject.transform.GetChild(0);

			Text _genreName = grandChildGenre.GetComponent<Text>();
			_genreName.text = g.name; 

			//GameObject genlabel = new GameObject(" Genre");
			genlabel.transform.position = new Vector3(cube.transform.position.x+0.05f, cube.transform.position.y-row, cube.transform.position.z-0.3f);
			/*TextMesh gentml = (TextMesh)genlabel.AddComponent<TextMesh>();
			MeshRenderer genmrl = (MeshRenderer)genlabel.AddComponent<MeshRenderer>();
			gentml.text = g.name;
			gentml.transform.position = new Vector3(genlabel.transform.position.x,genlabel.transform.position.y-0.1f,genlabel.transform.position.z-0.4f);
			gentml.offsetZ = 0;
			gentml.characterSize = 0.05f;
			gentml.lineSpacing = 1;
			gentml.anchor = TextAnchor.MiddleCenter;
			gentml.alignment = TextAlignment.Center;
			gentml.font = (Font)Resources.GetBuiltinResource (typeof(Font), "Arial.ttf");
			gentml.color = new Color(1,0,0);
			MeshRenderer rend5 = genlabel.GetComponentInChildren<MeshRenderer>();
			rend5.material = gentml.font.material;
			gentml.gameObject.layer = 2;*/
			row += 0.5f;




		}
		GameObject.Destroy (cube);
		GameObject.Destroy (GameObject.Find("Vinyl"));

		/*GameObject[] labelling = GameObject.FindGameObjectsWithTag("drawer");
		foreach(GameObject g in labelling){
			GameObject label = new GameObject(g.name+" label");
			TextMesh tml = (TextMesh)label.AddComponent("TextMesh");
			MeshRenderer mrl = (MeshRenderer)label.AddComponent("MeshRenderer");
			tml.text = g.name;
			tml.transform.position = g.transform.position;
			tml.offsetZ = 0;
			tml.characterSize = 0.1f;
			tml.lineSpacing = 1;
			tml.anchor = TextAnchor.UpperLeft;
			tml.alignment = TextAlignment.Center;
			tml.font = (Font)Resources.GetBuiltinResource (typeof(Font), "Arial.ttf");
			tml.color = new Color(1,1,1);
			MeshRenderer rend = label.GetComponentInChildren<MeshRenderer>();
			rend.material = tml.font.material;

		}

		cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
		float i = 0;
		foreach(PlaylistItem p in songpool){
			
			MonoBehaviour.Instantiate(cube);
			i+=0.1f;
			cube.transform.position = new Vector3(cube.transform.position.x+i, cube.transform.position.y, cube.transform.position.z);
			cube.name = p.title;
			GameObject label = new GameObject(cube.name+" label");
			TextMesh tml = (TextMesh)label.AddComponent("TextMesh");
			MeshRenderer mrl = (MeshRenderer)label.AddComponent("MeshRenderer");
			tml.text = cube.name;
			tml.transform.position = cube.transform.position;
			tml.offsetZ = 0;
			tml.characterSize = 0.1f;
			tml.lineSpacing = 1;
			tml.anchor = TextAnchor.UpperLeft;
			tml.alignment = TextAlignment.Center;
			tml.font = (Font)Resources.GetBuiltinResource (typeof(Font), "Arial.ttf");
			tml.color = new Color(1,1,1);
			MeshRenderer rend = label.GetComponentInChildren<MeshRenderer>();
			rend.material = tml.font.material;
			
			labels.Add(label);
		}
		*/
	}
	
	public void hide(){
	}

	public void searchForFiles(string baseDirectory){
		DirectoryInfo dir = new DirectoryInfo(baseDirectory);
		DirectoryInfo[] directories = dir.GetDirectories();
		foreach (DirectoryInfo d in directories) {
			searchForFiles (d.FullName);		
			
		}
		
		string fn = dir.FullName;
		string[] musicFiles = Directory.GetFiles (@fn,"*.mp3");
		foreach (string musicFile in musicFiles) {
			//using (var mp3 = new Mp3File(musicFile)) {
			Mp3ID3 tag = new Mp3ID3(musicFile);
			
			//Id3Tag tag = mp3.GetTag (Id3TagFamily.FileStartTag);
			
			
			if(tag != null){
				
				/*
					Debug.Log ("Title: " + tag.Title.Value);
					Debug.Log ("Artist: " + tag.Artists.Value);
					Debug.Log ("Album: " + tag.Album.Value);
					Debug.Log("Genre: " + tag.Genre.Value);
*/					
				
				PlaylistItem newest = new PlaylistItem(musicFile,tag.Genre, tag.Artist, tag.Album, tag.Title);
				Debug.Log (newest.toString());
				songpool.Add(newest);
				Stream stream = new FileStream("songpool/"+songpool.Count+".bin", FileMode.Create, FileAccess.Write, FileShare.None);
				formatter.Serialize(stream, newest);
				stream.Close();
					
				
				
				
			}
		}
		
		
	}

	public void add(PlaylistItem newest){
		songpool.Add(newest);
		Stream stream = new FileStream("songpool/"+songpool.Count+".bin", FileMode.Create, FileAccess.Write, FileShare.None);
		formatter.Serialize(stream, newest);
		stream.Close();
	}
	
	public void load(){
		
		// *Get the size of the songpool by getting the largest no. bin file. Afterwards intialize a new songpool array and fill it with the deserialized bin files.
		string[] spItems = Directory.GetFiles ("songpool/","*.*");
		foreach(string s in spItems){
			Stream stream = new FileStream(s, FileMode.Open, FileAccess.Read, FileShare.Read);
			PlaylistItem temp = (PlaylistItem) formatter.Deserialize(stream);
			songpool.Add(temp);
			stream.Close();
			
		}
	}

	public void swipeIn(GameObject GenrePrefab){//NEW CODE
		if(true){
		swipeNo++;
		show (GenrePrefab);//NEW CODE
		}
	}

	public void swipeOut(){

	}

	public PlaylistItem find(string s){
		PlaylistItem res = new PlaylistItem("empty");
		foreach(PlaylistItem pl in songpool){
			if(pl.title == s){
				res = pl;
			}
			
		}
		if (res.location != "empty") {
			return res;
		} else {
			return(new PlaylistItem("null","null","null","null","null"));
		}
	}

	public void sortArtists(){
		foreach(PlaylistItem pl in songpool){
			bool temp = false;
			foreach(Artist a in artists){
				if(a.name == pl.artist){
					temp = true;
					a.songs.Add(pl);
				}
			}
			if(temp == false){
				Artist tempa = new Artist(pl.artist, pl.genre);
				tempa.songs.Add (pl);
				artists.Add(tempa);
				//Song needs to be added for the artist
			}
		}

		foreach (Artist ar in artists) {
						bool temp = false;
						foreach (Genre g in genres) {
								if (g.name == ar.genre) {
										temp = true;
										g.artists.Add (ar);
								}
						}
						if (temp == false) {
								Genre tempg = new Genre (ar.genre);
								tempg.artists.Add (ar);
								genres.Add (tempg);
								//Song needs to be added for the artist
						}
				}

		foreach(Artist a in artists){
			//Debug.Log (a.name);
		}
				foreach(Genre g in genres){
					//Debug.Log (g.name);
				}
	}
}

public class Artist : System.Object{
	public string name;
	public string genre;
	public List<PlaylistItem> songs = new List<PlaylistItem>();

	public Artist(string tname, string tgenre){
		name = tname;
		genre = tgenre;

	}
}

public class Genre : System.Object{
	public string name;
	public List<Artist> artists = new List<Artist>();
	
	public Genre(string tname){
		name = tname;
		
	}
}