using UnityEngine;
using System.Collections;

public class StarStartMenu : MonoBehaviour 
{
	public AudioClip buttonClip;
	public float ScreenWidth{get;set;}
	public float ScreenHeight{get;set;}
	public float ButtonWidth{ get; set; }
	public float ButtonHeight{get;set;}
	public Transform cubePrefab;
	public bool tutorial;

	// Use this for initialization
	void Start () 
	{
		ScreenHeight = Screen.height;
		ScreenWidth = Screen.width;	
		ButtonWidth = 120;
		ButtonHeight = 20;
		tutorial = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		ScreenHeight = Screen.height;
		ScreenWidth = Screen.width;

	}

	void OnGUI()
	{
		if(Application.loadedLevelName == "MainMenu")
		{
			if(tutorial == false)
			{
				if (GUI.Button(new Rect(ScreenWidth/2 - ButtonWidth, ButtonHeight, ButtonWidth, ButtonHeight), "Start Game"))
				{
					GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>().AddAudio(buttonClip);
					Application.LoadLevel ("Scene");
					Instantiate(cubePrefab, new Vector3(0,300,0), cubePrefab.rotation);
				}
				if (GUI.Button(new Rect(ScreenWidth/2 - ButtonWidth, ButtonHeight + 30/*small offset*/, ButtonWidth, ButtonHeight), "Controls"))
				{
					GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>().AddAudio(buttonClip);
					tutorial = true;
				}
				GameObject gameManager = GameObject.FindGameObjectWithTag ("GameManager");
				InitMenu gameManagerScript = gameManager.GetComponent<InitMenu>();
				if (gameManagerScript.tempScore != 0)
				{
					GUI.Label (new Rect(ScreenWidth/2 - ButtonWidth, ScreenHeight/2, ButtonWidth, ButtonHeight), "Your Score: " + gameManagerScript.tempScore.ToString() + "!");
				}
			}
			else
			{
				if (GUI.Button(new Rect(ScreenWidth/2 - ButtonWidth, ButtonHeight, ButtonWidth, ButtonHeight), "Back"))
				{
					GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>().AddAudio(buttonClip);
					tutorial = false;
				}
				GUI.Label (new Rect(ScreenWidth/2 - 5*ButtonWidth/8, ButtonHeight * 3, ScreenWidth - 20, 100), "Story:");
				//GUI.Label (new Rect(20, ButtonHeight * 4, ScreenWidth - 40, ScreenHeight - ButtonHeight*3),"An asteroid is heading towards earth and the crew sent to destroy it has died trying! As an astronaut on the closest space station, the responsibility to stop this asteroid falls to you. Destroy as many smaller asteroids as possible on your way to the primary asteroid to minimize damage to earth. After you complete your task you will need to take the first crew's shuttle back to safety.");

				GUI.Label (new Rect(ScreenWidth/2 - 5*ButtonWidth/8, ButtonHeight * 7, ScreenWidth - 20, 100), "Controls:");
				GUI.Label (new Rect(ScreenWidth/2 - 5*ButtonWidth/8, ButtonHeight * 8, ScreenWidth - 40, ScreenHeight - ButtonHeight*3),"Space: Jump");
				GUI.Label (new Rect(ScreenWidth/2 - 5*ButtonWidth/8, ButtonHeight * 9, ScreenWidth - 40, ScreenHeight - ButtonHeight*3),"A: Left");
				GUI.Label (new Rect(ScreenWidth/2 - 5*ButtonWidth/8, ButtonHeight * 10, ScreenWidth - 40, ScreenHeight - ButtonHeight*3),"D: Right");
				GUI.Label (new Rect(ScreenWidth/2 - 5*ButtonWidth/8, ButtonHeight * 11, ScreenWidth - 40, ScreenHeight - ButtonHeight*3),"Q: Rotate Counterclockwise");
				GUI.Label (new Rect(ScreenWidth/2 - 5*ButtonWidth/8, ButtonHeight * 12, ScreenWidth - 40, ScreenHeight - ButtonHeight*3),"E: Rotate Clockwise");

			}
		}
	}
}
