using UnityEngine;
using System.Collections;

public class WinGame : MonoBehaviour {

	public AudioClip victoryClip;
	void OnCollisionEnter2D(Collision2D col)
	{
		if(col.collider.tag == "Player")
		{
			GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
			foreach (GameObject player in players) 
			{
				//NetworkCube playerScript = player.GetComponent<NetworkCube> ();
				Destroy (player);
			}
			//GameObject scoreCounter = GameObject.FindGameObjectWithTag ("ScoreCounter");
			//ScoreCounter scoreCounterScript = scoreCounter.GetComponent<ScoreCounter> ();
			//int tempscore = scoreCounterScript.Counter;

			GameObject gameObject = GameObject.FindGameObjectWithTag("GameManager");
			LevelManager levelManager = gameObject.GetComponent<LevelManager>();
			if (levelManager.level.Destroyed >= levelManager.level.RequiredAsteroidCount && levelManager.level.RemainingTime > 0) 
			{
				levelManager.GetComponent<AudioManager>().AddAudio(victoryClip);
				levelManager.level.CurrentNumber += 1;
				levelManager.level.AsteroidCount += 5;
				levelManager.level.CalculateRequiredAsteroidCount ();
				levelManager.level.Destroyed = 0;
				levelManager.level.MaxTimer += 15;
				levelManager.level.RemainingTime = levelManager.level.MaxTimer;
			}
			else
			{
				levelManager.level.CurrentNumber = levelManager.startLevel.CurrentNumber;
				levelManager.level.AsteroidCount = levelManager.startLevel.AsteroidCount;
				levelManager.level.CalculateRequiredAsteroidCount();
				levelManager.level.Destroyed = 0;
				levelManager.level.MaxTimer = levelManager.startLevel.MaxTimer;
				levelManager.level.RemainingTime = levelManager.level.MaxTimer;

			}
			Application.LoadLevel("MainMenu");

			/*GameObject gameManager = GameObject.FindGameObjectWithTag ("GameManager");
			InitMenu gameManagerScript = gameManager.GetComponent<InitMenu>();
			gameManagerScript.tempScore = tempscore;*/
		}
	}
}
