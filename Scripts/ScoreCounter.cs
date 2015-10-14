using UnityEngine;
using System.Collections;

public class ScoreCounter : MonoBehaviour {

	public int Counter{ get; set; }
	public bool gamePlayed;


	// Use this for initialization
	void Start () {
		Counter = 0;
		gamePlayed = false;

	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	void OnGUI() {
		//int i = 0;
		//GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

		//foreach(GameObject p in players) {
			//NetworkCube pScript = p.GetComponent<NetworkCube>();
			//Counter = pScript.score;
			//GUI.Box(new Rect(Screen.width - 100, Screen.height/2 - 20 * i, 100, 20), "SCORE: " + Counter);
			//i++;
		//}

	}
}
