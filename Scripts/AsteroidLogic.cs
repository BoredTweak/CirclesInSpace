using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AsteroidLogic : MonoBehaviour {
	public bool touched = false;
	public Vector2 adjustment;
	public float explosiveTimer{get;set;}
	public float MAXEXPLOSIVETIMER;
	public GameObject explosion;
	public Animator animator;
	public bool exploding;
	public AudioClip hitClip;
	public AudioClip explosionClip;

	public GameObject countdownText;


	void Start()
	{
		exploding = false;
		MAXEXPLOSIVETIMER = 2f;
	}

	void Update()
	{
		if(touched == true)
		{
			if(explosiveTimer >= 0.5f)
			{
				explosiveTimer -= Time.deltaTime;
				countdownText.gameObject.GetComponent<TextMesh>().text = ((int)explosiveTimer).ToString ();
			}
			else if(explosiveTimer < 0.5f && exploding == false)
			{
				Instantiate (explosion, this.transform.position, this.transform.rotation);

				AudioManager audioManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>();
				audioManager.AddAudio(explosionClip);
				exploding = true;
			}
			else
			{
				Destroy(countdownText.gameObject);
				Destroy (this.gameObject);
			}
		}
	}
	void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.tag == "Player")
		{
			AudioManager audioManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>();
			audioManager.AddAudio(hitClip);
			
			if(touched == false)
			{
				this.GetComponentInChildren<Renderer>().material.color = Color.green;
				GameObject gameObject = GameObject.FindGameObjectWithTag("GameManager");
				//GameManager gameManager = gameObject.GetComponent<GameManager>();
				LevelManager levelManager = gameObject.GetComponent<LevelManager>();
				levelManager.IncrementDestroyed();
				//NetworkCube colScript = col.gameObject.GetComponent<NetworkCube>();
				//colScript.score += 1;
				touched = true;
				explosiveTimer = MAXEXPLOSIVETIMER;
				countdownText = (GameObject)Instantiate(countdownText, this.gameObject.transform.position, Quaternion.identity);
			}

			if(col.gameObject.tag == "Ground")
			{
				DestroyObject(this.gameObject);
			}
		}


	}

}
