using UnityEngine;
using System.Collections;

public class InitMenu : MonoBehaviour 
{

	public GameObject menuPrefab;
	public GameObject bgmPrefab;
	public int tempScore{get;set;}
	public float splashTimer;
	
	// Use this for initialization
	void Start () 
	{
		DontDestroyOnLoad (this.gameObject);
		splashTimer = 3;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Application.loadedLevelName == "MainMenu")
		{
			if(GameObject.FindGameObjectsWithTag("GameManager").Length == 0)
			{
				Instantiate(menuPrefab, new Vector3(0,0,0), Quaternion.identity);
			}
			if(GameObject.FindGameObjectsWithTag("BGMManager").Length == 0)
			{
				Instantiate(bgmPrefab, new Vector3(0,0,0), Quaternion.identity);
			}
		}
		else if(Application.loadedLevelName == "Splash")
		{
			if (splashTimer > 0) 
			{
				splashTimer -= Time.deltaTime;
			}
			else
			{
				Application.LoadLevel("MainMenu");
			}
		}
	}
}
