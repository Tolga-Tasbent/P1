using UnityEngine;
using UnityEngine.UI;
using System; 
using System.Linq; 
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


namespace Menu.Music {

	public  class Songpool : System.Object{

		//songpool: List for all the songs loaded from the device
		private List<Music.PlaylistItem> songpool = new List<Music.PlaylistItem>();

		//allArtists & allGenres: List for all the artists and genres 
		private static List<Music.ArtistUI> allArtists = new List<Music.ArtistUI>();
		private static List<Music.GenreUI> allGenres = new List<Music.GenreUI>();



		//Getters
		//*****************

		//GetSongPool: Will return the songpool 
		public List<Music.PlaylistItem> GetSongpool(){
			return songpool;
		} 
		//GetAllArtist(): Will return all the artists
		public static List<Music.ArtistUI> GetAllArtist(){
			
			return allArtists;
		}

		//Adding, loading and creation of songs
		//*****************

		//AddSongToSongpool(): Will add a playlistitem to the songpool
		public void AddSongToSongpool(Music.PlaylistItem SongItem){
			songpool.Add(SongItem);
		}

		/*SortArtist(): Will find iterate each song (PlaylistItem) in the songpool and if it's already to the
		 * list the it will add the song to the list of songs of the artist (ArtistUI). If it's not added 
		 * it will create a new ArtistUI object and add it to the allArtist list.
		 * */
		public List<Music.ArtistUI> SortArtists(){
			
			foreach(Music.PlaylistItem plItem in songpool){
				bool isAlreadyAdded = false;
				
				foreach(Music.ArtistUI am in allArtists){
					
					if(am.getArtistName() == plItem.GetArtistName()){ //<--- checking here if the artist already exist in the list of all artists
						isAlreadyAdded = true;
						am.addToArtistSongs(plItem);
					}
				}
				
				if(isAlreadyAdded == false){ //<--- If it wasn't already added then we will create a new artistUI object
					Music.ArtistUI tempArtist = new Music.ArtistUI(plItem.GetArtistName(), plItem.GetGenreOfSong());
					tempArtist.addToArtistSongs(plItem);
					allArtists.Add(tempArtist);	
				}
			}
			return allArtists;
		}


		/*SortGenres(): Will find iterate each artist of all the artist and then for each genre of the artist 
		 * it will check if the genre already is created. If it is it will just add the artist to the 
		 * genre. If not it will it will create a new ArtistUI object and add it to the allArtist list.
		 * */
		public void SortGenres(){

			//Here we create genre o
			foreach (Music.ArtistUI ar in allArtists) {
				bool isAlreadyAdded = false;

				foreach (Music.GenreUI g in allGenres) {
					if (g.getGenreName() == ar.getArtistGenre()) {
						isAlreadyAdded = true;
						g.AddArtistToGenre(ar);
					}
				}

				if (isAlreadyAdded == false) {
					Music.GenreUI tempGenre = new Music.GenreUI (ar.getArtistGenre());
					tempGenre.AddArtistToGenre(ar);
					allGenres.Add (tempGenre);
				}
			}
			
		}



		/*Show(): Will load the library. It starts by setting the locations of the GenreSidebar & ArtistRowGroup Gameobjects 
		 trough the functions setGenreSidebar() & setArtistRowGroup(). Then in a nested for loop it goes trough each genre 
		 followed by each artist. For each genre and artist is creates a corrosponding object. The IndexOfRowGroup will 
		 make sure the GameObject of the ArtistUI object will be instantiated at the correct row in the UI. */
		public void Show(){
			Music.GenreUI genreUI = new Music.GenreUI ();
			genreUI.setGenreSidebar();

			Music.ArtistUI artistUI = new Music.ArtistUI (); 
			artistUI.setArtistRowGroup();

			int IndexOfRowContainer  = 0; 
			foreach(Music.GenreUI g in allGenres){
			
				g.CreateGenreGO(); 

				foreach(Music.ArtistUI a in g.getArtistsInGenre()){

					a.CreateArtistGO(IndexOfRowContainer );

				}
				IndexOfRowContainer++;
			}

		}//End of Show()

	}
}
