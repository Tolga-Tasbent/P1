using UnityEngine;
using UnityEngine.UI;

using System.Collections;
using System.Collections.Generic;
using System.IO;

using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class Main : MonoBehaviour {
	public static Playlist currentPlaylist = new Playlist(""); //This playlist object will always be replaced by the currently selected playlist
	Playlist n = new Playlist("New Playlist"); //Creates a new playlist object that will be used to add a new playlist
	PlaylistItem currentSong; //The current song that the application is playing (This will be used for future works, when the application will be able to play music)
	bool playlistPlay = true; //This boolean will set if the user is playing a playlist or playing songs directly from the Songpool
	bool shuffle = false; //Turns shuffling of the playlist or songpool on or off
	int count = 0; //Shows the line number of the current song in the current playlist
	public static List<Playlist> allPlaylists = new List<Playlist>(); //All the playlists are being stored in a list
	List<PlaylistDisplay> disp = new List<PlaylistDisplay>(); //Since playlist is a serializable class, it cannot contain gameobject variables (because those are unserializable). Therefore, the instances of the PlaylistDisplay class are stored separately from the playlists, in this array.
	public static List<GameObject> currentListofGO = new List<GameObject>(); 
	AudioSource player; //For future works: implementing the actual playing of a file into the music player
	AudioClip clp; //The object that will be passed onto the AudioSource. This contains the music file.
	public Songpool songpool = new Songpool(); //Creating an object for the songpool
	IFormatter formatter = new BinaryFormatter();


	//Loading prefabs
	public GameObject GenrePrefab; 
	public GameObject PlayListPrefab; 
	public GameObject PlayListItemPrefab; 
	public GameObject ParentSongGroup; //SongGroup used to set playlistitemprefab as a child of it



	// Use this for initialization
	void Start () {

		

			ParentSongGroup = GameObject.Find ("SongGroup"); //Finding the parent playlist
		
		songpool.load(); //Loads all the song data that has been saved previously by the songpool class
		songpool.sortArtists (); //Sorts the loaded data into artists and genres
		songpool.show(GenrePrefab); //Setting the genreprefab 
		PlaylistDisplay.PlayListPrefab = PlayListPrefab; //Setting the static playlist prefab in PlaylistDisplay
		loadAllPlaylists (); //Loads all the playlist data
		showAllPlaylists (); //Creates the corresponding graphical objects for each playlist
			}

	void Update () {

	
	}

	//The following functions (play, pause, next, prev and shuffleToggle) are for future works. They are all related to the file playing function.

	void play(){
		player.Play();
	}
	void pause(){
		player.Pause ();
	}
	void next(){
		if(playlistPlay && !shuffle){
				currentSong = currentPlaylist.getNext(count);
			count++;
		}
	}
	void prev(){
		if(playlistPlay && !shuffle){
			currentSong = currentPlaylist.getPrev(count);
			count--;
		}
	}
	void shuffleTogle(){
		shuffle = !shuffle;
	}



	//LoadAllPlaylists will get all the binary files from the playlists folder, deserialize them and loads them into the allPlaylists list.
	void loadAllPlaylists(){
		string[] pls = Directory.GetFiles ("playlists/","*.*");
		foreach(string s in pls){
			Stream stream = new FileStream(s, FileMode.Open, FileAccess.Read, FileShare.Read);
			Playlist temp = (Playlist)formatter.Deserialize(stream);
			allPlaylists.Add(temp);
			stream.Close();
			
		}

	}

	//Show all playlists will create the corresponding objects for each playlist in the allPlaylists list.
	public void showAllPlaylists(){

		disp.Clear ();
		disp = new List<PlaylistDisplay>();
		int i = 0;
		foreach(Playlist p in allPlaylists){

			PlaylistDisplay td = new PlaylistDisplay(p);
			disp.Add(td);
			td.show(i);
			i+=2;
		}

	}

	//The showPlaylistItems will create a display for each of the songs that are contained by the current playlist.
	public void showPlayListItems () {
		if (currentListofGO != null){
		foreach (GameObject go in currentListofGO)
				GameObject.Destroy(go);
		}
		foreach (PlaylistItem pli in currentPlaylist.PL)
		{
			GameObject newPlI = MonoBehaviour.Instantiate(PlayListItemPrefab) as GameObject; //Setting the prefab from main
			newPlI.transform.SetParent(ParentSongGroup.transform, false);
			currentListofGO.Add(newPlI);
			newPlI.name = pli.title;


			newPlI.GetComponent<Button>().onClick.AddListener (
					() => {GameObject.Destroy(newPlI,0.1f);}
				); 
			newPlI.GetComponentInChildren<Text>().text = pli.artist + " - " + pli.title;
		

		}
		
	}

	//savePlaylist is going to deserialize a given playlist into a binary file in the playlists folder.
	void savePlaylist(Playlist pl){
		Stream stream = new FileStream("playlists/"+pl.name+".bin", FileMode.Create, FileAccess.Write, FileShare.None);
		formatter.Serialize(stream, pl);
		stream.Close();
	}

	//newPlaylist will add the given name to the new playlist and add it to the allPlaylists list. Also, it's going to set the new playlist as current playlist and it will reset the display of the playlists, so that the new playlist is visible.
	public void newPlaylist(string newName){
		Playlist newPl = new Playlist (newName);
		allPlaylists.Add (newPl);
		currentPlaylist = newPl;
		showAllPlaylists (); 

	}

	//deletePlaylist will remove the playlist from the allPlaylists list. Yet, it doesn't remove the binary version of the playlist from the playlist folder, therefore this function needs to be extended in future works.
	void deletePlaylist(Playlist PL){
		allPlaylists.Remove (PL);

	}

	//findPlaylist will search through the name of all playlists and if a playlist has the same name as the input string, it will set it as the current playlist. It will also change the last played order for all the playlists.
	public void findPlaylist(string n){
		Playlist pl = new Playlist("");
		foreach(Playlist p in allPlaylists){
			if(p.name == n){
				pl = p;
			}
		}
		currentPlaylist = pl;
		Debug.Log ("Playlist found: "+ pl.name);
		count = 0;

		foreach(Playlist p in allPlaylists){
			p.lastPlay += 1;
		}
		pl.lastPlay = 0;
		showAllPlaylists ();
	}

}
