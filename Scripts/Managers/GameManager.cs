using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	public string Version = "0.1.0.0";
	//TODO: Menu music from http://freemusicarchive.org/music/Tentacles/Dance "Disco Time"
	//http://freemusicarchive.org/music/Avaren/Avaren possible in game music
	//Dance - "Bubbles"

	public Level level = new Level();
	public float ButtonWidth{ get; set; }
	public float ButtonHeight{get;set;}
	public Transform playerPrefab;
	public MenuPage menuPage = new MenuPage ();
	public bool restart{ get; set; }
	public bool paused{ get; set; }
	public GUIStyle style;
	public Texture2D SoundOn;
	public Texture2D SoundOff;
	public Texture2D MusicOn;
	public Texture2D MusicOff;
	public bool clear;
	public string clearData;

	void Awake () 
	{
		DontDestroyOnLoad (this.gameObject);
		SetDefaultValues ();
		SetupLevelManager ();
	}

	void Update () 
	{
		PauseCheck ();
		if(Application.loadedLevelName == "Scene")
		{
			if(level.RemainingTime <= 0)
			{
				restart = true;
			}
			level.RemainingTime -= Time.deltaTime;
			if(restart == true)
			{
				SetDefaultValues ();
				Application.LoadLevel("MainMenu");
				foreach(GameObject player in GameObject.FindGameObjectsWithTag("Player"))
		        {
					Destroy (player);
				}
				LevelManager levelManager = this.gameObject.GetComponent<LevelManager>();
				levelManager.SetRestart(true);
			}
		}
	}

	void OnGUI()
	{
		GUI.BeginGroup (new Rect (Screen.width - (SoundOn.width + MusicOn.width) , Screen.height - ButtonHeight * 2, SoundOn.width + MusicOn.width, SoundOff.height + MusicOn.height));
		{
			//TODO Correctly rearrange and such here.
			if (PlayerPrefs.GetInt (SaveData.SoundMuted.ToString()) == 0) 
			{
				if (GUI.Button (new Rect (0, 0, SoundOn.width, SoundOn.height), SoundOn, style)) 
				{
					GetComponent<AudioSource> ().volume = 0;
					PlayerPrefs.SetInt(SaveData.SoundMuted.ToString(), 1);
				}
			}
			else
			{
				if (GUI.Button (new Rect (0, 0, SoundOff.width, SoundOff.height), SoundOff, style)) 
				{
					GetComponent<AudioSource> ().volume = 1;
					PlayerPrefs.SetInt(SaveData.SoundMuted.ToString(), 0);
				}
			}


			if (PlayerPrefs.GetInt (SaveData.MusicMuted.ToString()) == 0) 
			{
				if (GUI.Button (new Rect (MusicOn.width, 0, MusicOn.width, MusicOn.height), MusicOn, style)) 
				{
					GameObject.FindGameObjectWithTag ("BGMManager").GetComponent<AudioSource>().volume = 0;
					PlayerPrefs.SetInt(SaveData.MusicMuted.ToString(), 1);
				}
			}
			else
			{
				if (GUI.Button (new Rect (MusicOff.width, 0, MusicOff.width, MusicOff.height), MusicOff, style)) 
				{
					GameObject.FindGameObjectWithTag ("BGMManager").GetComponent<AudioSource>().volume = 1;
					PlayerPrefs.SetInt(SaveData.MusicMuted.ToString(), 0);
				}
			}
		}
		GUI.EndGroup();

		if (Application.loadedLevelName == "MainMenu") 
		{
			if(menuPage.pageIndex == (int)PageName.Main)
			{
				if(PlayerPrefs.GetInt (SaveData.BestLevel.ToString()) < level.CurrentNumber)
				{
					PlayerPrefs.SetInt(SaveData.BestLevel.ToString(), level.CurrentNumber);
				}
				if (GUI.Button (new Rect (Screen.width / 2 - ButtonWidth, ButtonHeight, ButtonWidth, ButtonHeight), "Start Level " + level.CurrentNumber.ToString(), style)) 
				{
					Application.LoadLevel ("Scene");
					Instantiate (playerPrefab, new Vector3 (0, 50, 0), playerPrefab.rotation);
				}
				if (GUI.Button (new Rect (Screen.width / 2 - ButtonWidth, ButtonHeight + 30/*small offset*/, ButtonWidth, ButtonHeight), "Upgrade Store", style)) 
				{
					menuPage.pageIndex = (int)PageName.Store;
				}
				if (GUI.Button (new Rect (Screen.width / 2 - ButtonWidth, ButtonHeight + 60/*small offset*/, ButtonWidth, ButtonHeight), "Controls", style)) 
				{
					menuPage.pageIndex = (int)PageName.Controls;
				}
				if (GUI.Button (new Rect (Screen.width / 2 - ButtonWidth, ButtonHeight + 90/*small offset*/, ButtonWidth, ButtonHeight), "Credits", style)) 
				{
					menuPage.pageIndex = (int)PageName.Credits;
				}
				GUI.Label(new Rect (Screen.width / 2 -  ButtonWidth, ButtonHeight + 120/*small offset*/, ButtonWidth * 2, ButtonHeight), "Asteroid Credits Available: " + PlayerPrefs.GetInt(SaveData.AsteroidCredits.ToString()));

			}
			else if(menuPage.pageIndex == (int)PageName.Controls)
			{
				if (GUI.Button (new Rect (Screen.width / 2 - ButtonWidth, ButtonHeight, ButtonWidth, ButtonHeight), "Back", style)) 
				{
					menuPage.pageIndex = (int)PageName.Main;
				}
				//GUI.Label (new Rect (20, ButtonHeight * 4, ScreenWidth - 40, ScreenHeight - ButtonHeight * 3), "An asteroid is heading towards earth and the crew sent to destroy it has died trying! As an astronaut on the closest space station, the responsibility to stop this asteroid falls to you. Destroy the indicated amount of smaller asteroids on your way to the primary asteroid to minimize damage to earth. After you complete your task you will need to take the first crew's shuttle back to safety.");

				GUI.Label (new Rect (Screen.width / 2 - 5 * ButtonWidth / 8, ButtonHeight * 7 /*13*/, Screen.width - 40, Screen.height - ButtonHeight * 3), "Left Mouse Button: Jetpack to cursor");
				GUI.Label (new Rect (Screen.width / 2 - 5 * ButtonWidth / 8, ButtonHeight * 8 /*14*/, Screen.width - 40, Screen.height - ButtonHeight * 3), "Warning: Jetpacking uses fuel. Watch the fuel gage carefully!");
			}
			else if (menuPage.pageIndex == (int)PageName.Credits)
			{
				if (GUI.Button (new Rect (Screen.width / 2 - ButtonWidth, ButtonHeight, ButtonWidth, ButtonHeight), "Back", style)) 
				{
					menuPage.pageIndex = (int)PageName.Main;
				}

				//<div xmlns:cc="http://creativecommons.org/ns#" xmlns:dct="http://purl.org/dc/terms/" about="http://freemusicarchive.org/music/Tentacles/Dance/"><span property="dct:title">Dance</span> (<a rel="cc:attributionURL" property="cc:attributionName" href="http://freemusicarchive.org/music/Tentacles/">Tentacles</a>) / <a rel="license" href="http://creativecommons.org/licenses/by/4.0/">CC BY 4.0</a></div>

				GUI.Label (new Rect (Screen.width / 2 - 5 * ButtonWidth / 8, ButtonHeight * 7, Screen.width - 20, 100), "Music:");
				//GUI.Label (new Rect (ScreenWidth / 2 - 5 * ButtonWidth / 8, ButtonHeight * 8, ScreenWidth - 40, ScreenHeight - ButtonHeight * 3), "Main Menu");
				GUI.Label (new Rect (Screen.width / 2 - 5 * ButtonWidth / 8, ButtonHeight * 9, Screen.width - 40, Screen.height - ButtonHeight * 3), "'Bubbles' By Tentacles");
				GUI.Label (new Rect (Screen.width / 2 - 5 * ButtonWidth / 8, ButtonHeight * 10, Screen.width - 40, Screen.height - ButtonHeight * 3), "Licensed under 'CC BY 4.0'");
			}
			else if (menuPage.pageIndex == (int)PageName.Store)
			{

				if (GUI.Button (new Rect (Screen.width / 2 - ButtonWidth, ButtonHeight, ButtonWidth, ButtonHeight), "Back", style)) 
				{
					menuPage.pageIndex = (int)PageName.Main;
				}

				if(GUI.Button (new Rect(Screen.width * (3/4), ButtonHeight, ButtonWidth, ButtonHeight), clearData, style))
			    {
					if(clear == false)
					{
					clear = true;
					clearData = "Confirm?";
					}
					else
					{
						clearData = "Clear Data";
						clear = false;
						PlayerPrefs.DeleteAll ();
						this.gameObject.GetComponent<UpgradeManager>().SetUpgradeLevels();
					}
				}

				//<div xmlns:cc="http://creativecommons.org/ns#" xmlns:dct="http://purl.org/dc/terms/" about="http://freemusicarchive.org/music/Tentacles/Dance/"><span property="dct:title">Dance</span> (<a rel="cc:attributionURL" property="cc:attributionName" href="http://freemusicarchive.org/music/Tentacles/">Tentacles</a>) / <a rel="license" href="http://creativecommons.org/licenses/by/4.0/">CC BY 4.0</a></div>
				
				GUI.Label (new Rect (Screen.width / 2 - 5 * ButtonWidth / 8, ButtonHeight * 5, Screen.width - 20, 100), "Store:");
				GUI.Label (new Rect (Screen.width / 2 - 5 * ButtonWidth / 8, ButtonHeight * 7, Screen.width - 40, Screen.height - ButtonHeight * 3), "Asteroid Credits Available: " + PlayerPrefs.GetInt(SaveData.AsteroidCredits.ToString()));
				//List buyables here somehow.
				int i = 2; 
				foreach(Upgrade upgrade in gameObject.GetComponent<UpgradeManager>().upgrades)
				{
					GUI.BeginGroup(new Rect(Screen.width / 8, ButtonHeight * (5 + 2 * i), Screen.width, Screen.height / gameObject.GetComponent<UpgradeManager>().upgrades.Count));
					GUI.Label(new Rect(0,0,Screen.width / 4, ButtonHeight), upgrade.ToString ());
					if(GUI.Button (new Rect(Screen.width / 4, 0, ButtonWidth * 4, ButtonHeight), upgrade.AsteroidUpgradeCost().ToString () + " Asteroid Credits    -  " + upgrade.Level.ToString () + " / " + upgrade.MaxLevel()))
					{
						if(PlayerPrefs.GetInt (SaveData.AsteroidCredits.ToString()) >= upgrade.AsteroidUpgradeCost())
						{
							PlayerPrefs.SetInt (SaveData.AsteroidCredits.ToString (), (PlayerPrefs.GetInt(SaveData.AsteroidCredits.ToString ()) - upgrade.AsteroidUpgradeCost()));
							upgrade.Level++;
							PlayerPrefs.SetInt (upgrade.ToString (), upgrade.Level);
						}
					}
					GUI.EndGroup();
					i++;
				}
			}
		} 
		else if (Application.loadedLevelName == "Scene") 
		{
			
			GUI.Label (new Rect (Screen.width / 2 - 5 * ButtonWidth / 8, ButtonHeight * 4, Screen.width - 20, 100), "Time Remaining: " + level.RemainingTime.ToString ("0.00"));
			if(level.Destroyed < level.RequiredAsteroidCount)
			{
				GUI.Label (new Rect (Screen.width / 2 - 5 * ButtonWidth / 8, ButtonHeight * 3, Screen.width - 20, 100), "Mini-Asteroids Destroyed: " + level.Destroyed + " / " + level.RequiredAsteroidCount);
			}
			else 
			{
				GUI.Label (new Rect (Screen.width / 2 - 5 * ButtonWidth / 8, ButtonHeight * 3, Screen.width - 20, 100), "Get to the main asteroid to complete the level!");
			}
		}
	}


	void SetDefaultValues()
	{
		clear = false;
		clearData = "Clear Data";
		paused = false;
		ButtonWidth = 120;
		ButtonHeight = 20;
		menuPage.pageIndex = (int)PageName.Main;
		style.alignment = TextAnchor.MiddleCenter;
		if (PlayerPrefs.GetInt (SaveData.SoundMuted.ToString()) == 1) 
		{
			GetComponent<AudioSource> ().volume = 0;
		}
	}

	void SetupLevelManager()
	{
		LevelManager levelManager = gameObject.GetComponent<LevelManager>();
		if(levelManager != null)
		{
			levelManager.SetupLevelStartValues ();
			level = levelManager.level;
		}
	}

	/*void SetupUpgradeManager()
	{
		UpgradeManager upgradeManager = gameObject.GetComponent<UpgradeManager>();
		if(upgradeManager != null)
		{
			upgrade.SetupLevelStartValues ();
			level = levelManager.level;
		}
	}*/

	void PauseCheck()
	{
		if(paused == true)
		{
			Time.timeScale = 0;
		}
		else
		{
			Time.timeScale = 1;
		}
	}
}
