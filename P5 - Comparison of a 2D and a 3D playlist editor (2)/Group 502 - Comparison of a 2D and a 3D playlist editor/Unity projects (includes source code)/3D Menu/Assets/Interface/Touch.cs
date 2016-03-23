using UnityEngine;
using UnityEngine.UI;

using System.Collections;



public class Touch : MonoBehaviour
{

   //Private Variables
    public bool imMoving;


    //For clicking on items
    private static float timer;

   
    //New vector from start to end vector 
    private float newVector;

    // Camera
    public Camera myCamera;
    

    //Zooming 
    public float zoomComfortZone;
    public byte zoomStrength;
    public byte maxFov;
    public byte minFov;

    //Bool for cabinet open or not
    public static bool cabinetOpen = false;

    //For ray casting 

    GameObject cam;
    private static Cam c;
    GameObject master;
    private static Main m;
    // Use this for initialization
    void Awake()
    {
        cam = GameObject.Find("MainCAM");
        c = (Cam)cam.GetComponent<Cam>(); //Setting the c Cam object in start so it doesn't set it every update.
        c.free();
        master = GameObject.Find("Master");
        m = (Main)master.GetComponent<Main>(); //Setting the m Main object in start so it doesn't set it every update.

    }

    //OpenPlaylist: Will open  the playlist with same name as the given parameter. It's a fucntion 
    // that is run when the user clicks on a playlist box. c.setFocus will set call the Cam script to 
    //Lerp the camera to the position where the current playlist menu is visible
    public void OpenPlayList(string name)
    {
        foreach (Playlist pl in Main.allPlaylists)
        {
            if (name == pl.name)
                Main.currentPlaylist = pl;
        }
        Debug.Log(c.name);
        Debug.Log("Go to " + Main.currentPlaylist.name);
        m.showAllPlaylists();
        m.showPlayListItems();

        c.setFocus("CurrentPlaylist");

    }
    //OpenCabinet: Opens a cabinet with the same name as the given parameter. It's checking if the cabinetopen is false so that 
    //so that it's not possible to open another cabinet while inside another. 
    public void OpenCabinet(string name)
    {
        if (imMoving == true)
            return;
        Debug.Log("Open " + name);
        GameObject cab = GameObject.Find(name);

        CabinetScript cs = (CabinetScript)cab.GetComponent<CabinetScript>();
        if (cabinetOpen == false)
        {
            c.setFocusCabinet(cab.transform.position, cab);
            cs.Open();
            cabinetOpen = true;
        }
        else {
            c.setFocus("SongpoolBack");
            cs.Close();
            cabinetOpen = false;

        }
    }
    //AddSongToPlayList(): simply adds a given song (PlaylistItem) to the current playlist
    public void AddSongToPlayList(PlaylistItem pli)
    {
        Main.currentPlaylist.add(pli);
        Debug.Log("Added " + pli.title + " to " + Main.currentPlaylist);
    }

    //The next 3 function ShowMusicLibrary, ShowCurrentPlaylist,ShowAllPlaylists will 
    //call the Cam.setFocus to the corrosponding menu
    public void ShowMusicLibrary()
    {
        c.setFocus("Songpool");
    }

    public void ShowCurrentPlaylist()
    {
        c.setFocus("CurrentPlaylist");
        //m.showAllPlaylists ();
        m.showPlayListItems();
    }
    public void ShowAllPlaylists()
    {
        c.setFocus("Playlists");
    }

    //The next 3 functions RemoveInfoBoxPlaylist, RemoveInfoBoxPlaylists, RemoveInfoBoxLibrary. Will remove the corrosponding blue infobox
    public void RemoveInfoBoxPlaylist()
    {
        GameObject infobox = GameObject.Find("InfoBox");
        GameObject.Destroy(infobox, 0.14f);
    }

    public void RemoveInfoBoxPlaylists()
    {
        GameObject infobox = GameObject.Find("InfoBoxPlayLists");
        GameObject.Destroy(infobox, 0.14f);
    }
    public void RemoveInfoBoxLibrary()
    {
        GameObject infobox = GameObject.Find("InfoBoxLibrary");
        GameObject.Destroy(infobox, 0.14f);
    }





    // Update is called once per frame
    void Update()
    {


        //Will check if we're touching or not
        if (Input.touchCount == 1)
        {
            //Debug.Log("Debug: Touch Happened");	

            /***********      MOVING CAMERA AND SELECTING    **************

			/*A switch statement to check between 3 conditions: 
			TouchPhase.Began: True if a touch Phase has begun. A timer is created with the Time class.
			TouchPhase.Began: True if tap position has moved. Used for moving the x and y position of the myCamera 
			variable, by using the transform.Translate(x, y, z). The timer is used to see if there has gone 
			more than 0.3 seconds since touch phase begun. This is done to differentiate between a single
			tap and a move and a slide. It also check if the moveBool is set to true. In certain parts of 
            the menu we don't want the camera to move at all. It also check if we're not hitting the left 
            border. If we're hitting the camera will be moved a bit to the right. This is made so the user 
            won't move so much to the left that they are lost. 
			TouchPhase.Ended: True if touch phase has ended. If it's ended we know it's a single tap. To 
			select and object we need to send out a ray (theoretically infinite line from a point) from 
			our cameras xy plane, for setting a ray starting position in screen we use the 
			Camera.main.ScreenPointToRay(position of touch). And to check we hit anything with the 
			ray we use the Physics.Raycast(ray, out RaycastHit). If we hit we we 
			can "select" the object by using (hit.transform.name)*/
            Debug.Log(Cam.movingDontOpen);

            switch (Input.GetTouch(0).phase)
            {
                case TouchPhase.Began:
                    timer = Time.time;
                    imMoving = true;

                    break;
                case TouchPhase.Moved:
                    Debug.Log(Time.time - timer);
                    if (Cam.moveBool && !Cam.Hitleftborder && Time.time - timer >= 0.3)
                    {
                        imMoving = true;
                        myCamera.transform.Translate(-Input.GetTouch(0).deltaPosition.x * 0.009F, -Input.GetTouch(0).deltaPosition.y * 0.009F, 0);
                        imMoving = false;

                    }
                    else if (Cam.Hitleftborder)
                    {
                        {

                           myCamera.transform.position = new Vector3(myCamera.transform.position.x + 0.1f,
                                                                       myCamera.transform.position.y,
                                                                       myCamera.transform.position.z);
                        }
                    }
                    imMoving = false;



                    break;
                case TouchPhase.Ended:
                    imMoving = false;

                    break;
            }

            /***********      ZOOMING    ***************/

          /*  To check if to fingers is on the screen we use the "Input.touchCount == 2". First finger is indexed 
            at 0 and the other at 1. If both are moving then we will zoom. */
            
            		if (Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved)
            		{
            			/*This is a little confusing but to save performance, instead of assigning and removing new variable
            			every frame I put all the calculations into a single variable. 
            			1. We create a new vector by subtracting the position of the touch of finger 0 with finger 1 
            			This the position of our current frame. To find the vector of our previous frame we subtract 
            			the current position of the fingers with the deltaPosition(change in position since last frame)
            			2. Then we take the magnitude of each of the new vectors and subtract them to get the difference in
            			the distance between the two vectors. */
            			
            			newVector = (Input.GetTouch(0).position - Input.GetTouch(1).position).magnitude 
            								- ((Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition)
            							   - (Input.GetTouch(1).position - Input.GetTouch(1).deltaPosition)).magnitude;
            			
            		
            			
            							   
            			/*Checking if the absolute(- becomes +) distance is more than a preset comfort zone*/
            			if (Mathf.Abs(newVector) > zoomComfortZone) {
            				/*
            				1. If the new vector is bigger than 0, we know the that we're zooming. Since the 
            				current frame is bigger than the previous. And if the new vector is less than 0 
            				then we're zooming out
            				2. fieldOfView is used to make a zoom effect, but we're not actually moving the camera 
            				position. We use Mathf.Clamp(value, min, max) to avoid zooming to close or too far out, the 
            				function clamps a value between min and max. We use Mathf.Lerp(value current, desired value, 
            				t). Lerp is linear interpolation between the to values depending on t, which is in a range of 
            				0-1. Time.deltaTime is time in seconds it took to complete last frame, by multiplying it with 
            				zoomStrength, the higher zoomStrength the faster we will zoom. Our start value will be the 
            				current fieldOfView and the the desired value is the the current fieldOfView + the distance
            				between 
            				
            				*/
            				if (newVector > 0)
            				{
            					//Zoom in 
            					//Debug.Log("Zoom In");
            					myCamera.fieldOfView = Mathf.Clamp(
            								   Mathf.Lerp(myCamera.fieldOfView,
            								   myCamera.fieldOfView - Mathf.Abs(newVector) * zoomStrength, 
            								   Time.deltaTime * zoomStrength),
            								   minFov, maxFov);
            				} else {
            					//Zoom Out
            					//Debug.Log("Zoom Out"); 
            					myCamera.fieldOfView = Mathf.Clamp(
            								   Mathf.Lerp(myCamera.fieldOfView,
            								   myCamera.fieldOfView + Mathf.Abs(newVector) * zoomStrength, 
            								   Time.deltaTime * zoomStrength),
            								   minFov, maxFov); 
            				}
            			} else {
            				
            			}
            			
            		}
        }
    }

}
