using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Menu.Music {

	public class SongListUI : MonoBehaviour {
	/* The SonglistUI class is attached to the SongList GameObject (It's the shelve that drops down)*/



		//Private variables
		//******************
		private static List<PlaylistItem> _currentList = new List<PlaylistItem>();
		private static List<GameObject> _songPrefabList = new List<GameObject>(); 
		private static List<GameObject> _songPlayListPrefabList = new List<GameObject>(); 
		private static List<PlaylistItem> _emptyList = new List<PlaylistItem>();

		private static Animator _animator; //Animator is a component belonging to each Menu(Like: Playlist menu, library menu, current playlist menu. 

		private static GameObject _SonglistPL;
		private static GameObject _UIContainerSong; 



		public void Awake () {
			_animator = GetComponent<Animator> (); //Get the animator component of this GameObject 
			_UIContainerSong =  GameObject.Find ("UIContainerSongList"); //Finding the UiContainerSongList for shelve dropdown
			_SonglistPL = GameObject.Find ("SongGroup"); //Finding the SongGroup for showing the current playlist
		}

		//Getters 
		//******************

		//Gets the the current list of songs (used in Main.AddToCurrentPlaylist())
		public static List<PlaylistItem> GetCurrentListofSongs() {
			return _currentList;
		}

		//Opening/Closing of windows
		//******************

		/*OpenSongPLUIWindow(): Will open the song of the playlist which was clicked on (GameObject) 
		 * It will first remove any existing GameObjects (Songs in the playlist). Then it will look for the playlists with  
		 * the same name as the GameObject. When its found it will send the list of songs in the found playlist the the 
		 * SetCurrentPlaylist(). The SetCurrentPlaylist() is written further down under GameObject Creation.
		*/
		public void OpenSongPLUIWindow (GameObject PlaylistGO) {

			foreach (GameObject GO in _songPlayListPrefabList)
				GameObject.Destroy(GO);
			
			foreach (Music.Playlist pl in Main.GetAllPlaylists())
			{
				if (PlaylistGO.transform.name == pl.GetPlaylistTitle())
				{
					SetCurrentPlaylist(pl.GetSongsInPlaylist());
				}
			}
		}

		/*OpenSongPLUIWindow(): This function is essentially utilizing the polymorphism albility of C#. It will do the same 
		 * as above function except it know already which playlist to send to SetCurrentPlaylist. It is used for the border 
		 * GUI navigation button: Playlist.
		*/
		public static void OpenSongPLUIWindow () {
			foreach (GameObject GO in _songPlayListPrefabList)
				GameObject.Destroy(GO);
			
			SetCurrentPlaylist(Main.GetCurrentPlaylistSongs());
			
		}


		/*OpenSongUIWindow(): Is used for opening artists shelves in the library menu. It functions the same a OpenSongPLUIWindow()
		 * but is also starting the open animation of the Songlist menu. Extra code as List.Clear() and _currentList = 
		 * emptyList is needed to make sure that there is no remainding list objects or prefablist object
		*/
		public void OpenSongUIWindow (GameObject ArtistGO) {
			
			foreach (GameObject child in _songPrefabList)
				GameObject.Destroy(child);
			
			_songPrefabList.Clear();
			
			_currentList = _emptyList;
			_animator.SetBool ("IsOpen", true);
			
			foreach (Music.ArtistUI am in Songpool.GetAllArtist())
			{
				if (ArtistGO.transform.name == am.getArtistName())
				{
					SetCurrentSongList(am.getArtistSongs());
					
				}
			}
		}
		//CloseUIWindow(): Simply closes the Songlist menu by setting is Animator to closed (IsOpen = false = Animator.Closed)
		public void CloseUIWindow () {
			
			_currentList = _emptyList;
			_animator.SetBool ("IsOpen", false);
			
		}


		//GameObject creation 
		//******************

		/*SetCurrentPlaylist(); Is responsilbe for showing the current playlist in the Current Playlist menu. 
		 * It start by clearing the prefab list to remove any remainding list objects. It then check if 
		 * the list sent is not NULL. If it's not NULL it will go through each song (PlaylistItem) in the list
		 * and instantiate a GameObject. t loads it from the resources folder in the assets
		 * folder. It then: sets the GameObject name to the corrosponding title. Add the object to a the _SongList parent object so it 
		 * automatically are positioned correctly. It then find the Text component (which is it's child indexed at 0) and changes
		 * the text to be the same as the corrosponding title and artist name. An extra detail is the adding a function to the 
		 * button component of the new GameObject(newPlItemGO). We're using a lambda expression to send the function to teh onClick
		 */

		public static void SetCurrentPlaylist(List<PlaylistItem> auiS) 
		{
			_songPlayListPrefabList.Clear();
			if (auiS != null){
				foreach (PlaylistItem plItem in auiS)
				{

					GameObject newPLItemGO = MonoBehaviour.Instantiate(Resources.Load("SongButton", typeof(GameObject))) as GameObject;

					_songPlayListPrefabList.Add(newPLItemGO);
					
					newPLItemGO.transform.SetParent(_SonglistPL.transform, false);
					newPLItemGO.name = plItem.GetSongTitle();

					newPLItemGO.GetComponent<Button>().onClick.AddListener (
						() => {RemoveSong(newPLItemGO, plItem);}
					); 

					Transform childText = newPLItemGO.gameObject.transform.GetChild(0);
					Text _songName = childText.GetComponent<Text>();
					_songName.text = plItem.GetArtistName() + " - " + plItem.GetSongTitle(); 
	//				SongItem newSong = newItem.GetComponent<SongItem>(); 
	//				newSong.setPLitem(_currentList[i]);
				}
			}
			
		}


		/*SetCurrentSongList(); Is much the same as above SetCurrentPlaylist(), but handles the instantiation of GameObjects in the songList menu.
		 * */
		public static void SetCurrentSongList(List<PlaylistItem> auiS) 
		{
			_songPrefabList.Clear();
			_currentList = auiS; 
			if (_currentList != null){
				for (int i = 0; i < 1; i++)
				{
					GameObject newSongGO = MonoBehaviour.Instantiate(Resources.Load("SongPrefab", typeof(GameObject))) as GameObject;
					
					_songPrefabList.Add(newSongGO);
					
					newSongGO.transform.SetParent(_UIContainerSong.transform, false);
					newSongGO.name = _currentList[i].GetSongTitle();

					//Bonus: below line of code is finding the grandchild at index 1. (newSongGO -> child(index 0) -> child(index 1)
					Transform childText = newSongGO.gameObject.transform.GetChild(0).gameObject.transform.GetChild(1);
					Text _songName = childText.GetComponent<Text>();
					_songName.text = _currentList[i].GetSongTitle(); 

				}
			}
			
		}
		
		//GameObject destruction 
		//******************

		//RemoveSong(): Removes the same song which was clicked on. 
		public static void RemoveSong(GameObject GO, PlaylistItem plItem){
			GameObject.Destroy(GO);
			Main.RemoveSongFromCurrentPlaylist (plItem);

		}

		//DeleteSongsInPrefabList(): Deletes all the songs in the list of GameObjects 
		public static void DeleteSongsInPrefabList() {

			foreach (GameObject child in _songPrefabList)
				GameObject.Destroy(child);
		}
	}
}
