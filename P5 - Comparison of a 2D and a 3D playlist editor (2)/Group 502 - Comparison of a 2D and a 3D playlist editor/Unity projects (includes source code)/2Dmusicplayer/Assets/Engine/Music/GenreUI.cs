using UnityEngine;
using UnityEngine.UI;

using System.Collections.Generic;
using System.Collections;
using System.IO; 


namespace Menu.Music {

	public class GenreUI {

		//Private variables
		//******************

		private string _genreName = ""; 
		private Text _genreNameUIT = null; 	//_genreNameUIT: Is a variable for the UI.Text component of the genre GameObject
		private Transform childText = null; 
		private static Transform genreSidebarTF = null;
		private List<Music.ArtistUI> artistsInGenre = new List<Music.ArtistUI>();

		//Constructors
		//******************

		public GenreUI() {
			_genreName = null; 
		}
		
		public GenreUI(string genreName) {
			_genreName = genreName; 
		} 

		//Getters
		//******************

		//getGenreName(): Gets the genrename
		public string getGenreName(){
			return _genreName;
		}
		//getGenreName(): Gets the list of artists in the genre
		public List<Music.ArtistUI> getArtistsInGenre(){
			return artistsInGenre;		
		}

		//Setters
		//******************

		//setGenreSidebar(): Finds and sets the genresidebar. It's called from Songpool.Show()
		public void setGenreSidebar(){
			
			var genreSidebarGO = GameObject.Find ("GenreSideBar");
			genreSidebarTF = genreSidebarGO.transform;
		}


		//Gameobject creation 
		//******************

		/*CreateGenreGO(): Is responsible for instantiating a new GameObject. It loads it from the resources folder in the assets
		 folder. It then: sets the GameObject name to the corrosponding genre name. Add the object to a the genreSidebar so it 
		 automatically are positioned correctly. It then find the Text component (which is it's child indexed at 1) and changes
		 the text to be the same as the corrosponding genre. This function is called in Songpool.Show().*/
		public void CreateGenreGO() {

			GameObject newGenreItemGO = MonoBehaviour.Instantiate(Resources.Load("Genre Tile", typeof(GameObject))) as GameObject;
			newGenreItemGO.name = _genreName;
			newGenreItemGO.transform.SetParent (genreSidebarTF.transform, false);
			childText = newGenreItemGO.gameObject.transform.GetChild(1);
			_genreNameUIT = childText.GetComponent<Text>();
			_genreNameUIT.text = _genreName; 

		}
		//Adders
		//******************
		//Adds an artist to it's genre
		public void AddArtistToGenre (Music.ArtistUI aui){

			artistsInGenre.Add(aui);
		}


	}

}
