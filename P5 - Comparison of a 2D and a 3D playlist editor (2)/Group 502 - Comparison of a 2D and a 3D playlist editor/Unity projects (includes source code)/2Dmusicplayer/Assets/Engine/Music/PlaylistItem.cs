using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization;


namespace Menu.Music {

	public class PlaylistItem : System.Object {

		//Private variables
		//*******************
		private string location;
		private string genre;
		private string artist;
		private string album;
		private string title;


		//Contructors 
		//*******************
		public PlaylistItem(string location, string genre, string artist, string album, string title){
			this.location = location;
			this.album = album;
			this.artist = artist;
			if (genre == null)
				this.genre = "Other"; 
			else this.genre = genre;
			this.title = title;

		}


		public PlaylistItem(string location){
			this.location = location;
			this.album = "Unknown album";
			this.artist = "Unknown artist";
			this.genre = "Unknown genre";
			this.title = "Unknown title";

		}

		//Getters
		//****************
		public string GetSongTitle(){
			return title;
			
		}
		public string GetArtistName(){
			return artist;
			
		}
		public string GetGenreOfSong(){
			return genre;
			
		}
		

		public string toString(){
			return(title + " by " + artist + " from " + album + ". Genre: " + genre);

		}

	}

}
