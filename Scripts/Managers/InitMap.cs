using UnityEngine;
using System.Collections;

public class InitMap : MonoBehaviour {
	public float BUFF_UPPER_BOUNDARY_X = 130.0f;
	public float BUFF_LOWER_BOUNDARY_X = -130.0f;
	public float BUFF_UPPER_BOUNDARY_Y = -10.0f;
	public float BUFF_LOWER_BOUNDARY_Y = -260.0f;
	public float GROUND_UPPER_BOUNDARY_X = 90.0f;
	public float GROUND_LOWER_BOUNDARY_X = -90.0f;
	public float GROUND_UPPER_BOUNDARY_Y = 200.0f;
	public float GROUND_LOWER_BOUNDARY_Y = -200.0f;
	public static float GROUND_MIN_DISTANCE_Y = 50.0f;
	public static float GROUND_MIN_DISTANCE_X = 50.0f;
	public float groundDistance = 10;
	public float randomNumber;
	public Vector3 spawnLoc; 
	public bool tooClose;
	public GameObject buffPrefab;// = (GameObject)Resources.Load("/Prefabs/SpeedBuff");
	public GameObject groundPrefab;// = (GameObject)Resources.Load("/Prefabs/Ground");

	void Start () {
		tooClose = false;
		spawnLoc = new Vector3 (); 

		GameObject gameObject = GameObject.FindGameObjectWithTag ("GameManager");
		LevelManager levelManager = gameObject.GetComponent<LevelManager> ();
		int h = 0;
		//TODO: Upgrade this to prevent asteroid overlap.
		while(h < levelManager.level.AsteroidCount){
			StartCoroutine (InstantiateGround ());
			/*if(h%10 == 0){
				InstantiateBuff ();
			}*/
			h++;
		}

	}

	void Update () {
	
	}

	void InstantiateBuff()
	{
		spawnLoc.x = Random.Range (BUFF_LOWER_BOUNDARY_X, BUFF_UPPER_BOUNDARY_X);
		spawnLoc.y = Random.Range (BUFF_LOWER_BOUNDARY_Y, BUFF_UPPER_BOUNDARY_Y);
		spawnLoc.z = 0;
		Instantiate (buffPrefab, spawnLoc, transform.rotation);
	}

	IEnumerator InstantiateGround()
	{
		spawnLoc.x = Random.Range(GROUND_LOWER_BOUNDARY_X/100,GROUND_UPPER_BOUNDARY_X/100)*100;
		spawnLoc.y = Random.Range(GROUND_LOWER_BOUNDARY_Y/100, GROUND_UPPER_BOUNDARY_Y/100)*100;
		spawnLoc.z = 0;
		float randomModifier = (float)(1 + 0.4 * (Random.value - 0.5));

		GameObject asteroid = (GameObject)Instantiate(groundPrefab, spawnLoc, Random.rotation);
		asteroid.transform.localScale *= randomModifier;


			//spawnLoc.x = spawnLoc.x + GROUND_MIN_DISTANCE_X;
			//spawnLoc.y = spawnLoc.y - GROUND_MIN_DISTANCE_Y;

			/*GameObject[] objectlist = GameObject.FindGameObjectsWithTag("Ground");

			if(objectlist.Length > 0)
			{
				j = 0;
				while(j < objectlist.Length)
				{
					groundDistance = Vector3.Distance(objectlist[j].transform.position,spawnLoc);
					if(groundDistance > GROUND_MIN_DISTANCE_X)
					{
						tooClose = true;
					}
					j++;
				}
			}*/



			//tooClose = false;
			yield return new WaitForSeconds(0.0001f);
	}
}
