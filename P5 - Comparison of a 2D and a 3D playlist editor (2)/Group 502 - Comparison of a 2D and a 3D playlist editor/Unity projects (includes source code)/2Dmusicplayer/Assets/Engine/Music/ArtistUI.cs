using UnityEngine;
using UnityEngine.UI;

using System.Collections.Generic;
using System.Collections;


namespace Menu.Music {

	public class ArtistUI{

			//Private variables
			//********************
			private string _genre = "";
			private string _artistName = "";
	        private Text _artistNameUIT = null;
			private Transform childText = null;
			private static GameObject  artistRowGroupGO = null;

	        //For creating a artist object that saves songs
	        private List<PlaylistItem> _artistSongs = new List<PlaylistItem>();
	       

			//Contructors 
			//********************
			public ArtistUI(){}

	        public ArtistUI(string name, string genre)
	        {
	            _genre = genre;
			_artistName = name;

			}
			
			//Getters
			//********************
			public string getArtistName (){
				return _artistName; 
			}
			
			public string getArtistGenre (){
				return _genre; 
			}

			public List<PlaylistItem> getArtistSongs() {
				return _artistSongs;
			}

			//Setters
			//********************
					
			public void setArtistRowGroup(){
				artistRowGroupGO = GameObject.Find ("RowGroup");

			}

			//Adders
			//********************	

			public void addToArtistSongs(PlaylistItem plItem){
				_artistSongs.Add (plItem);
			}


			//GameObject Creation 
			//********************
			/*CreateArtistGO(): Is responsible for instantiating a new GameObject. It loads it from the resources folder in the assets
			 folder. It then: sets the GameObject name to the corrosponding genre name. Add the object to a the artisRowGroup so it 
			 automatically are positioned correctly. It then find the Text component (which is it's child indexed at 1) and changes
			 the text to be the same as the corrosponding . This function is called in Songpool.Show().*/
	        public void CreateArtistGO(int t)
	        {
	            GameObject newItem = MonoBehaviour.Instantiate(Resources.Load("Artist Tile", typeof(GameObject))) as GameObject;

				Transform artistGenreRowContainer = artistRowGroupGO.transform.GetChild(t).transform;
				newItem.name = _artistName;
				newItem.transform.SetParent(artistGenreRowContainer.transform, false);

	            childText = newItem.gameObject.transform.GetChild(1);
	            _artistNameUIT = childText.GetComponent<Text>();
				_artistNameUIT.text = _artistName;
	        }

	}

}
