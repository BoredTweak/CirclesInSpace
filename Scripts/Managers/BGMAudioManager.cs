using UnityEngine;
using System.Collections;

public class BGMAudioManager : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		DontDestroyOnLoad (this.gameObject);
		if (PlayerPrefs.GetInt (SaveData.MusicMuted.ToString()) == 1) 
		{
			//GameObject.FindGameObjectWithTag ("BGMManager").
			GetComponent<AudioSource>().volume = 0;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	void OnGUI()
	{

	}
}
