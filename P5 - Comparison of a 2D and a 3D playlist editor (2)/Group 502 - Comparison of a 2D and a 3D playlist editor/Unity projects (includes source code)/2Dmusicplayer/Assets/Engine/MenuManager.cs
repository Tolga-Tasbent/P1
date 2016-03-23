using UnityEngine;
using System;
using System.Collections;

namespace Menu {

	public class MenuManager : MonoBehaviour {

		//Start menu should be set in the inspector. Each menu have a Menu script attached to it.
		public Menu StartMenu; 

		//Private variables
		//*******************
		private static Menu CurrentMenu; 
		private static Menu CurrentPlayList; 
		//public Animator animator; 
		private bool _IsSongListOpen; 
		

		/*Sets the current menu to be the start menu and shows it.
		 Also find the CurrentPlaylist menu and gets it component */
		public void Awake () {
			SetCurrentMenu (StartMenu);
			ShowMenu (CurrentMenu);
			CurrentPlayList = GameObject.Find ("CurrentPlayList").GetComponent<Menu> ();

		}
		/*ShowMenu(): Will take a Menu as a parameter. Set the current menu's animator to closed. 
		 * And set the paremeter menu to the current and open it. 
		 * */
		public void SetCurrentMenu(Menu menu) {
			
			CurrentMenu = menu; 
		}


		public void ShowMenu (Menu menu) {

			CurrentMenu.IsOpen = false;
			SetCurrentMenu(menu);
			CurrentMenu.IsOpen = true;
		}
		/*ShowPLMenu(): Will set the current menu's animator to closed. 
		 * And set the the playlist menu to the current menu and open it. 
		 * ShowPLMenu is a special case because the playlist GameObject are instantiated 
		 * at runtime it could no have ShowMenu() as a button OnClick function.
		 * */
		public static void ShowPLMenu () {
			CurrentMenu.IsOpen = false;
	
			CurrentMenu = CurrentPlayList; 
			CurrentMenu.IsOpen = true;
		}
	}

}
	

	

