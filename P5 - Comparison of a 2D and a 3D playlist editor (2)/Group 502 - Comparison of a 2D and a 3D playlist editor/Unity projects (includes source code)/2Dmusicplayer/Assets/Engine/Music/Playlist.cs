
using UnityEngine;
using UnityEngine.UI; 

using System.Collections;
using System.Collections.Generic;


namespace Menu.Music {

	public class Playlist : MonoBehaviour {

		//Private variables
		//**********************
		private ArrayList PLI;
		private List<PlaylistItem> _SongList = new List<PlaylistItem>();
		private string _PlaylistTitle;
		private int length;
		private int lastPlay;

		private static Transform parent;
		private Transform childText;
		private Text _artistName;

		private Menu Currentplaylist; 
		

		void Awake () {
			GameObject parentGO = GameObject.Find("GridMaker");
			parent = parentGO.transform;
		}

		//Constructors 
		//**********************

		public Playlist(){
			this._PlaylistTitle = "";
			Debug.Log ("Created playlist " + this._PlaylistTitle);
		}

		public Playlist(string name){
			this._PlaylistTitle = name;
			Debug.Log ("Created playlist " + this._PlaylistTitle);

			CreatePlaylistGO ();
		}


		//Getters
		//**********************
		public string GetPlaylistTitle(){
			return _PlaylistTitle;
			
		}
		
		public List<PlaylistItem> GetSongsInPlaylist (){
			return _SongList;
		}

		
		public int getNoOfItems(){
			return length;
			
		}
		
		//getNext() & getPrev(): Used for music player functionlity getting the next and previous song (PlaylistItem)
		public PlaylistItem getNext(int count){
			if (count + 1 < length) {
				return _SongList [count + 1];
			} else {
				return _SongList [length-1]		;
			}
		}
		
		public PlaylistItem getPrev(int count){
			if (count - 1 >= 0) {
				return _SongList [count - 1];
			} else {
				return _SongList[0];		
			}
		}


		//Adders / Remover
		//**********************
		public void add(PlaylistItem item){
			_SongList.Add (item);
			length++;
			
		}
		
		public void remove(PlaylistItem item){
			_SongList.Remove (item);
			length--;
			
		}

		//GameObject Creation 
		//**********************
		/*CreatePlaylistGO(); Loads it from the resources folder in the assets
		 * folder. It then: sets the GameObject name to the corrosponding title. Add the object to a the _SongList parent object so it 
		 * automatically are positioned correctly. It then find the Text component (which is it's child indexed at 0) and changes
		 * the text to be the same as the corrosponding title and artist name. An extra detail is the adding a function to the 
		 * button component of the new GameObject(newPlGO). We're using a lambda expression to send the function to the onClick
		 */
		public void CreatePlaylistGO(){

			GameObject newPLGO = Instantiate(Resources.Load("NewPL", typeof(GameObject))) as GameObject;
			
			newPLGO.name = _PlaylistTitle; 
			newPLGO.transform.SetParent(parent.transform, false);
			childText = newPLGO.gameObject.transform.GetChild(0);
			_artistName = childText.GetComponent<Text>();
			_artistName.text = _PlaylistTitle; 
			
			newPLGO.GetComponent<Button>().onClick.AddListener (
				() => {MenuManager.ShowPLMenu();}
			); 
		}

	}
}