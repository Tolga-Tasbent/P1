using UnityEngine;
using System.Collections;


namespace Menu {
	public class Menu : MonoBehaviour {
	/*Each Menu (Libary, Songlist, PlaylistsMenu, CurrentPlaylist) have a Menu.cs script attached to it.*/


		//Private variables
		//*******************
		private CanvasGroup _canvasGroup;
		private Animator _animator; 

		//Gets and sets the animator bool
		public bool IsOpen
		{ 			
			get { return _animator.GetBool("IsOpen");	}
			set { _animator.SetBool("IsOpen", value);	}
		}

		/*Gets the components, CanvasGroup, RectTransform of the component. 
		The rect.offSetMax sets the menu to be centered in the screen. (Good for editing multiple menus at the same time.*/
		public void Awake () {
			_animator = GetComponent<Animator> (); 
			_canvasGroup = GetComponent<CanvasGroup>();
			var rect = GetComponent<RectTransform> ();
			rect.offsetMax = rect.offsetMin = new Vector2 (0, 0);
		}

		/*Checks the state of the menu every frame. If its not open (closed) it will not be interactable nor will it  
		 * block raycasts. Else it will block and be interactable. 
		 */
		public void Update () {
			if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Open"))
			{
				_canvasGroup.blocksRaycasts = _canvasGroup.interactable = false;
				//IsOpen = false;

			} else _canvasGroup.blocksRaycasts = _canvasGroup.interactable = true;

		}
	}
}