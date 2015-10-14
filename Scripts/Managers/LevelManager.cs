using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour 
{
	public Level level {get;set;}
	public Level startLevel{get;set;}

	// Use this for initialization
	public void SetupLevelStartValues() 
	{
		startLevel = new Level ();
		startLevel.CurrentNumber = 1;
		startLevel.AsteroidCount = 300;
		startLevel.CalculateRequiredAsteroidCount ();
		startLevel.Destroyed = 0;
		startLevel.MaxTimer = 300;
		startLevel.RemainingTime = startLevel.MaxTimer;
		level = new Level ();
		level.CurrentNumber = startLevel.CurrentNumber;
		level.AsteroidCount = startLevel.AsteroidCount;
		level.CalculateRequiredAsteroidCount();
		level.Destroyed = 0;
		level.MaxTimer = startLevel.MaxTimer;
		level.RemainingTime = level.MaxTimer;
	}
	
	public void IncrementDestroyed()
	{
		level.Destroyed++;
		PlayerPrefs.SetInt (SaveData.AsteroidCredits.ToString (), PlayerPrefs.GetInt (SaveData.AsteroidCredits.ToString ()) + level.CurrentNumber);
	}

	public void SetRestart(bool restart)
	{
		level = startLevel;
	}
}
