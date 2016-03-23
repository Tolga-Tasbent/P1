using UnityEngine;
using UnityEngine.UI;

using System.Collections;
using System.Collections.Generic;
using System.Linq; 

namespace Menu {


	public class Main : MonoBehaviour{
		//allPlaylists: List containing all the playlists created in the application
		private static List<Music.Playlist> allPlaylists = new List<Music.Playlist>();
		//currentPlaylist: Variable that used for setting the current active playlist
		private static Music.Playlist currentPlaylist;
		//currentSong: Variable that sets the current active song.
		Music.PlaylistItem currentSong;
		//playlistPlay: Bool value for pausing/playing songs
		bool playlistPlay = true;
		//shuffle: Bool value for shuffle/not shuffle the next current active playlist
		bool shuffle = false;

		//clp: NOOOOOOOOOOOT SUUUUUUUUUUUUURE
		AudioClip clp;

		//songpool: Instantiates an object of the Songpool class
		private Music.Songpool songpool = new Music.Songpool();



		
		
		//Function starts when the game starts
		void Start () {
				
				
			songpool.AddSongToSongpool (new Music.PlaylistItem("none", "POP", "Justin Timberlake", "The 20/20 Experience", "Mirror"));
			songpool.AddSongToSongpool (new Music.PlaylistItem("none", "POP", "R.City feat. Adam Levine", "ALBUM", "Locked Away"));
	//		songpool.add (new PlaylistItem("none", "POP", "Ed Sheeran", "ALBUM", "Thinking Out Loud"));
	//		songpool.add (new PlaylistItem("none", "POP", "Rihanna", "ALBUM", "B Better Have My Money"));
	//		songpool.add (new PlaylistItem("none", "POP", "Calvin Harris feat. Ellie Goulding", "ALBUM", "Outside"));
	//		songpool.add (new PlaylistItem("none", "POP", "Ellie Goulding", "ALBUM", "Love Me Like You Do"));
	//		songpool.add (new PlaylistItem("none", "POP", "Charlie Puth", "ALBUM", "Marvin Gaye"));
	//		songpool.add (new PlaylistItem("none", "POP", "Sia", "ALBUM", "Chandelier"));
	//		songpool.add (new PlaylistItem("none", "POP", "Mark Random Feat. Bruno Mars", "ALBUM", "Uptown Funk"));
	//		songpool.add (new PlaylistItem("none", "POP", "Meghan Trainor feat. John Legend", "ALBUM", "Like Im Gonna Lose You"));
			songpool.AddSongToSongpool (new Music.PlaylistItem("none", "HIPHOP", "Tupac", "ALBUM", "All Eyez On Me"));
	//		songpool.add (new PlaylistItem("none", "HIPHOP", "Snoop Dogg", "ALBUM", "Drop It Like Its Hot"));
	//		songpool.add (new PlaylistItem("none", "HIPHOP", "50 Cent", "ALBUM", "We Up"));
	//		songpool.add (new PlaylistItem("none", "HIPHOP", "Fetty Wap", "ALBUM", "679"));
	//		songpool.add (new PlaylistItem("none", "HIPHOP", "B.I.G", "ALBUM", "Big Poppa"));
	//		songpool.add (new PlaylistItem("none", "HIPHOP", "Kanye West", "ALBUM", "Golddigger"));
	//		songpool.add (new PlaylistItem("none", "HIPHOP", "Dr.Dre", "ALBUM", "Still DRE"));
	//		songpool.add (new PlaylistItem("none", "HIPHOP", "50 Cent", "ALBUM", "We Up"));
	//		songpool.add (new PlaylistItem("none", "HIPHOP", "Eminem", "ALBUM", "Lose Yourself"));
	//		songpool.add (new PlaylistItem("none", "HIPHOP", "Kendrick Lamar", "ALBUM", "I"));
	//		songpool.add (new PlaylistItem("none", "RnB", "Tinashe", "ALBUM", "2 On"));
	//		songpool.add (new PlaylistItem("none", "RnB", "R.Kelly", "ALBUM", "I Believe I Can Fly"));
	//		songpool.add (new PlaylistItem("none", "RnB", "Chris Brown", "ALBUM", "Zero"));
	//		songpool.add (new PlaylistItem("none", "RnB", "Beyonce", "ALBUM", "Drunk In Love"));
	//		songpool.add (new PlaylistItem("none", "RnB", "Jeremih", "ALBUM", "Down On Me"));
	//		songpool.add (new PlaylistItem("none", "RnB", "Usher", "ALBUM", "U Dont Have To Call"));
	//		songpool.add (new PlaylistItem("none", "RnB", "Frank Ocean", "ALBUM", "Swim Good"));
	//		songpool.add (new PlaylistItem("none", "RnB", "August Alsina", "ALBUM", "Inhale"));
	//		songpool.add (new PlaylistItem("none", "RnB", "Frank Ocean", "ALBUM", "Swim Good"));
	//		songpool.add (new PlaylistItem("none", "RnB", "Jhene Aiko", "ALBUM", "The Worst"));
	//		songpool.add (new PlaylistItem("none", "Rock", "Beatles", "Help", "Help"));
	//		songpool.add (new PlaylistItem("none", "Rock", "Elvis Presley", "Any Day Now", "In the Ghetto"));
	//		songpool.add (new PlaylistItem("none", "Rock", "Queen", "News of the world", "We Will Rock You"));
	//		songpool.add (new PlaylistItem("none", "Rock", "The Rolling Stones", "ALBUM", "Paint It Black"));
	//		songpool.add (new PlaylistItem("none", "Rock", "Eagles", "ALBUM", "Hotel California"));
	//		songpool.add (new PlaylistItem("none", "Rock", "U2", "ALBUM", "Bloody Sunday"));
	//		songpool.add (new PlaylistItem("none", "Rock", "Bruce Springsteen", "ALBUM", "Born in USA"));
	//		songpool.add (new PlaylistItem("none", "Rock", "Bon Jovi", "ALBUM", "Its my life"));
	//		songpool.add (new PlaylistItem("none", "Rock", "Dire Straits", "ALBUM", "Sultans of Swing"));
	//		songpool.add (new PlaylistItem("none", "Rock", "Bob Dylan", "ALBUM", "Blowin in the wind"));
	//		songpool.add (new PlaylistItem("none", "Hard Rock", "Foo Fighters", "Sonic Highways", "Outside"));
	//		songpool.add (new PlaylistItem("none", "Hard Rock", "Led Zeppelin", "The Black Dog ", "The Black Dog"));
	//		songpool.add (new PlaylistItem("none", "Hard Rock", "Pink Floyd ", "The Wall", "In the Flesh"));
	//		songpool.add (new PlaylistItem("none", "Hard Rock", "AC/DC", "ALBUM", "Thunderstruck"));
	//		songpool.add (new PlaylistItem("none", "Hard Rock", "Aerosmith", "ALBUM", "Dream On"));
	//		songpool.add (new PlaylistItem("none", "Hard Rock", "Metallica", "ALBUM", "Enter Sandman"));
	//		songpool.add (new PlaylistItem("none", "Hard Rock", "Guns N Roses", "ALBUM", "Night train"));
	//		songpool.add (new PlaylistItem("none", "Hard Rock", "The Who", "ALBUM", "Baba ORile"));
	//		songpool.add (new PlaylistItem("none", "Hard Rock", "Van Halen", "ALBUM", "Jump"));
	//		songpool.add (new PlaylistItem("none", "Hard Rock", "Kiss", "ALBUM", "Loving you"));
	//		songpool.add (new PlaylistItem("none", "Alternative Rock", "Nirvana", "ALBUM", "Polly"));
	//		songpool.add (new PlaylistItem("none", "Alternative Rock", "Radiohead", "ALBUM", "Creep"));
	//		songpool.add (new PlaylistItem("none", "Alternative Rock", "The Doors", "ALBUM", "Light my fire"));
	//		songpool.add (new PlaylistItem("none", "Alternative Rock", "Red Hot Chilli Peppers", "ALBUM", "Heyo"));
	//		songpool.add (new PlaylistItem("none", "Alternative Rock", "R.E.M", "ALBUM", "Man on the Moon"));
	//		songpool.add (new PlaylistItem("none", "Alternative Rock", "Oasis", "ALBUM", "Wonderwall"));
	//		songpool.add (new PlaylistItem("none", "Alternative Rock", "Blur", "ALBUM", "Parklife")); 
	//		songpool.add (new PlaylistItem("none", "Alternative Rock", "Smashing Pumpkins", "ALBUM", "Today"));
	//		songpool.add (new PlaylistItem("none", "Alternative Rock", "Muse", "ALBUM", "Starlight"));
	//		songpool.add (new PlaylistItem("none", "Alternative Rock", "Arctic Monkeys", "ALBUM", "R U Mine?"));
	//		songpool.add (new PlaylistItem("none", "Alternative Rock", "The Strokes", "ALBUM", "Reptilia"));
	//		songpool.add (new PlaylistItem("none", "Disco", "Bee Gees", "ALBUM", "Stayin Alive"));
	//		songpool.add (new PlaylistItem("none", "Disco", "Kool & and the Gang", "ALBUM", "Celebration"));
	//		songpool.add (new PlaylistItem("none", "Disco", "Earth Wind and Fire", "ALBUM", "September"));
	//		songpool.add (new PlaylistItem("none", "Disco", "Village People", "ALBUM", "Y.M.C.A"));
	//		songpool.add (new PlaylistItem("none", "Disco", "ABBA", "ALBUM", "Dancing Queen"));
	//		songpool.add (new PlaylistItem("none", "Disco", "The Jackson 5", "ALBUM", "ABC"));
	//		songpool.add (new PlaylistItem("none", "Disco", "Jamiroquai", "ALBUM", "VIrtual Insanity"));
	//		songpool.add (new PlaylistItem("none", "Disco", "Scissor Sisters", "ALBUM", "I cant stop Dancing"));
	//		songpool.add (new PlaylistItem("none", "Disco", "Trammps", "ALBUM", "Disco Inferno"));
	//		songpool.add (new PlaylistItem("none", "Disco", "Barry White", "ALBUM", "Let the Music Play"));
	//		songpool.add (new PlaylistItem("none", "Electronic", "The Prodigy", "The Fat Of The Land", "Climbatize"));
	//		songpool.add (new PlaylistItem("none", "Electronic", "Pendulum", "Immersion", "Crush"));
	//		songpool.add (new PlaylistItem("none", "Electronic", "Knife Party", "Abandon Ship", "Reconnect"));
	//		songpool.add (new PlaylistItem("none", "Electronic", "deadmau5", "At Play Vol. 4", "GH – Original Mix"));
	//		songpool.add (new PlaylistItem("none", "Electronic", "Porter Robinson", "Worlds (Remixed)", "Flicker – Mat Zo Remix"));
	//		songpool.add (new PlaylistItem("none", "Electronic", "Feed Me", "Calamari Tuesday", "Dazed"));
	//		songpool.add (new PlaylistItem("none", "Electronic", "Zomboy", "The Outbreak", "Immunity – Original Mix"));
	//		songpool.add (new PlaylistItem("none", "Electronic", "Kill The Noise", "OCCULT CLASSIC", "Without A Trace"));
	//		songpool.add (new PlaylistItem("none", "Electronic", "NERO", "Welcome Reality", "Doomsday"));
	//		songpool.add (new PlaylistItem ("none", "Electronic", "Modestep", "London Road", "Poop"));
	//		
			//Calls the sortArtist() function of the Songpool class.
			//sortArtist: Sorts all songs in the songpool to their artist and the artists to their genres
			songpool.SortArtists();
			songpool.SortGenres();

			//Calls the show() function of the Songpool class.
			//show: is responsible for iterating through genres and artists where it creates a gameobject 
			//for each genre and artist. 
			songpool.Show();
			
			//Instantiates the current playlist. 
			currentPlaylist = gameObject.AddComponent<Music.Playlist>();  


		}

		//Object Creation and destruction
		//**************************************

		public void CreatePlayList (string Name){
			Music.Playlist temp = new Music.Playlist (Name); 
			allPlaylists.Add (temp);
			currentPlaylist = temp; 
		}


		void newPlaylist(string newName){
			Music.Playlist newPl = new Music.Playlist (newName);

			allPlaylists.Add (newPl);
			currentPlaylist = newPl;

		}

		void deletePlaylist(Music.Playlist PL){
			allPlaylists.Remove (PL);

		}

		public static void RemoveSongFromCurrentPlaylist(Music.PlaylistItem plItem){
			currentPlaylist.remove (plItem);
		}


		//Getters
		//**************************************
		public static List<Music.Playlist> GetAllPlaylists(){
			return allPlaylists;
		}

		public static List<Music.PlaylistItem> GetCurrentPlaylistSongs(){
			return currentPlaylist.GetSongsInPlaylist();
		}



		//Interface functionality
		//**************************************
		public void play(){
			//player.Play();
			Debug.Log("Play/Pause");
		}
		
		public void pause(){
			//player.Pause ();
			
		}
		
		public void next(){
			if(playlistPlay && !shuffle){
				/*
					currentSong = currentPlaylist.getNext(count);
					Debug.Log (currentSong.toString());
				count++;*/
				Debug.Log("Next Song");
				
			}
			
		}
		
		public void prev(){
			
			Debug.Log("Previous Song");
			
		}
		
		void shuffleTogle(){
			shuffle = !shuffle;
			
		}

		/*AddToCurrentPlaylist: Is attached to a button OnClick script of each of the Song Objects in the library. 
		It requires the the name of the GameObject that was clicked on (Which is the Song Object itself). It will iterate 
		through a list of current songs from the SongListUI, the list of current songs is the songs of the current 
		opened/active shelf. If the object name is equal to one from the list it will add it to the current playlist.
		 */
		public void AddToCurrentPlaylist(GameObject go){
			foreach(Music.PlaylistItem pl in Music.SongListUI.GetCurrentListofSongs())
			{
				if (go.transform.name == pl.GetSongTitle())
				{
					
					currentPlaylist.add(pl); 
					Debug.Log("You just added " + pl.GetSongTitle() + " to " + currentPlaylist.GetPlaylistTitle());
				}
				
			}
			
		}
		

		/*RemoveInfoBoxPlaylist(), RemoveInfoBoxPlaylists() & RemoveInfoBoxLibrary(): Is attached to a button OnClick
		 script of one of the Info boxes. It will find it self and self destroy. 
		 A timer is added to make it look more natural, so it will self destroy in 0.14 seconds.*/
		public void RemoveInfoBoxPlaylist() {
			GameObject infobox = GameObject.Find ("InfoBox"); 
			GameObject.Destroy (infobox, 0.14f);
		}
		
		public void RemoveInfoBoxPlaylists() {
			GameObject infobox = GameObject.Find ("InfoBoxPlayLists"); 
			GameObject.Destroy (infobox, 0.14f);
		}

		public void RemoveInfoBoxLibrary() {
			GameObject infobox = GameObject.Find ("InfoBoxLibrary"); 
			GameObject.Destroy (infobox, 0.14f);
		}

		/*ReloadCurrentPlaylist(): Is attached to the button script of the Playlist button in the GUI nagivation dock.
		 It calls OpenSongPLUIWindow() from the SongListUI class which will reload the Current Playlist */
		public void ReloadCurrentPlaylist() {
			Music.SongListUI.OpenSongPLUIWindow ();
		}


	}
}