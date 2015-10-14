using UnityEngine;
using System.Collections;

public class NetworkCube : MonoBehaviour 
{
	public static float JETPACK_FORCE = 30f;
	public float BASE_BUFF_TIMER = 10.0f;
	public float MAX_JET_FUEL = 100f;
	public float MAX_ANGULAR_VELOCITY = 250f;
	public bool winScreen;
	public static bool hasBuff;
	public GameObject myCam;
	public static Color BACKGROUND_STANDARD_COLOR;
	public bool warning;
	public float jetpackFuel;
	public float jetpackForce;
	public float maxSpin;
	public float spinCorrection;
	GUIStyle style;
	public Texture2D fuelBarColor;
	public Texture2D barEmpty;
	public Texture2D barFull;
	public GUIStyle fuelStyle;
	public float ButtonWidth;
	public float ButtonHeight;
	public AnimationClip leftWalk;
	public AnimationClip rightWalk;
	public AnimationClip idleLeft;
	public AnimationClip idleRight;
	public GameManager gameManagerScript;
	public Animation animation;

	/*public void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
	{
		if (stream.isWriting)
		{
			Vector3 myPosition = transform.position;
			stream.Serialize(ref myPosition);
		} else
		{
			Vector3 receivedPosition = Vector3.zero;
			stream.Serialize(ref receivedPosition);      
			transform.position = receivedPosition;
		}
	}*/

	void Awake()
	{
		DontDestroyOnLoad (this.gameObject);

		GameObject gameManager = (GameObject)GameObject.FindGameObjectWithTag ("GameManager");
		if(gameManager != null)
		{
			gameManagerScript = gameManager.GetComponent<GameManager>();
			style = gameManagerScript.style;
		}
		animation = gameObject.GetComponent<Animation> ();
		SetupPlayerStats ();
		SetupPlayerGUI ();
		SetupCamera ();
	}

	void Start () 
	{
		hasBuff = false;

	}

	void Update () 
	{
		HandleInput ();
		//Debug.Log ("Angular Velocity " + this.gameObject.GetComponent<Rigidbody2D> ().angularVelocity.ToString ());
		float zRatio = Mathf.Abs (this.gameObject.transform.rotation.eulerAngles.z) / 180;

		//if(Mathf.Abs (this.gameObject.GetComponent<Rigidbody2D> ().angularVelocity) < 100)
		if(this.gameObject.GetComponent<Rigidbody2D> ().angularVelocity > spinCorrection)
		{
			this.gameObject.GetComponent<Rigidbody2D>().AddTorque(-spinCorrection);// * zRatio);
		}
		else if(this.gameObject.GetComponent<Rigidbody2D> ().angularVelocity < spinCorrection)
		{
			this.gameObject.GetComponent<Rigidbody2D>().AddTorque(spinCorrection);// * zRatio);
		}

			/*if(this.gameObject.transform.rotation.eulerAngles.z > 0 && this.gameObject.transform.rotation.eulerAngles.z < 180)
			{
				Debug.Log ("Negative Rotation " + this.gameObject.transform.rotation.eulerAngles);

				this.gameObject.GetComponent<Rigidbody2D>().AddTorque(-spinCorrection * zRatio);
			}
			else if(this.gameObject.transform.rotation.eulerAngles.z < 360 && this.gameObject.transform.rotation.eulerAngles.z > 180)
			{
				Debug.ClearDeveloperConsole();
				Debug.Log ("Positive Rotation " + this.gameObject.transform.rotation.eulerAngles);

				this.gameObject.GetComponent<Rigidbody2D>().AddTorque(spinCorrection);
			}
			else if(this.gameObject.transform.rotation.eulerAngles.z > -180 && this.gameObject.transform.rotation.eulerAngles.z < 0)
			{
				Debug.ClearDeveloperConsole();
				Debug.Log ("Positive Rotation " + this.gameObject.transform.rotation.eulerAngles);
				
				this.gameObject.GetComponent<Rigidbody2D>().AddTorque(spinCorrection * zRatio);
			}
			else if(this.gameObject.transform.rotation.eulerAngles.z < -180 && this.gameObject.transform.rotation.eulerAngles.z > -360)
			{
				Debug.ClearDeveloperConsole();
				Debug.Log ("Negative Rotation " + this.gameObject.transform.rotation.eulerAngles);
				
				this.gameObject.GetComponent<Rigidbody2D>().AddTorque(-spinCorrection);
			}*/

		/*else
		{
			if(this.gameObject.GetComponent<Rigidbody2D> ().angularVelocity > 100)
			{
				this.gameObject.GetComponent<Rigidbody2D> ().AddTorque(-spinCorrection);
			}
			else if(this.gameObject.GetComponent<Rigidbody2D> ().angularVelocity < -100)
			{
				this.gameObject.GetComponent<Rigidbody2D> ().AddTorque(spinCorrection);
			}
		}*/
		//Debug.Log(this.gameObject.GetComponent<Rigidbody2D> ().angularVelocity);
		if (this.gameObject.GetComponent<Rigidbody2D> ().angularVelocity > maxSpin) 
		{
			this.gameObject.GetComponent<Rigidbody2D> ().angularVelocity = maxSpin;
		} 
		else if (this.gameObject.GetComponent<Rigidbody2D> ().angularVelocity < -maxSpin) 
		{
			this.gameObject.GetComponent<Rigidbody2D> ().angularVelocity = -maxSpin;
		}



		//}
		CheckAndResolveOutOfBounds ();

		if(hasBuff == true)
		{
			StartCoroutine(StartSpeedBuff());
		}

		if (winScreen == true)
		{
			LoadWinScreen();
		}


	}

	void OnGUI()
	{
		if(gameManagerScript.paused == false)
		{
			if(warning == true)
			{
				GUI.Label (new Rect(20, 20, Screen.width - 40, 30), "Careful! You're about to float off into space! Move back towards the asteroids!");
			}
			GUI.Label (new Rect (Screen.width - 120, Screen.height / 2, 100 * (jetpackFuel / MAX_JET_FUEL), 20), ((int)(jetpackFuel / MAX_JET_FUEL * 100)).ToString () , fuelStyle);
		}         
		else
		{
			GUI.Label (new Rect(Screen.width / 2, 40, Screen.width - 40, 30), "Paused!");
			if (GUI.Button (new Rect (Screen.width / 2 - ButtonWidth / 3, ButtonHeight, ButtonWidth, ButtonHeight), "Return to Menu", style)) 
			{
				gameManagerScript.paused = false;
				Application.LoadLevel ("MainMenu");
				DestroyImmediate (this.gameObject);
			}
		}
	}

	/* Disabled to try jetpack only
	void Jump()
	{
		isJumping = true;
		GetComponent<Rigidbody2D>().AddForce(new Vector2 (0, 300));
	}*/

	void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.tag == "Buff")
		{
			hasBuff = true;
			Destroy(col.gameObject);
		}

		/*if(col.gameObject.tag == "Ground")
		{
			isJumping = false;

		}*/

	}

	void HandleInput()
	{
		Rigidbody2D rigidbody = GetComponent<Rigidbody2D> ();
			//change animations to state based and a animation manager?
		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			if(gameManagerScript.paused == false)
			{
				gameManagerScript.paused = true;
			}
			else 
			{
				gameManagerScript.paused = false;
			}
		}
		if(gameManagerScript.paused == false)
		{
			float xSpeed = rigidbody.velocity.x;
			if (xSpeed > 0) 
			{
				//animation.AddClip(rightWalk,rightWalk.name);
				//animation.clip = rightWalk;
				if(!animation.IsPlaying(rightWalk.name))
				{
					animation.Play (rightWalk.name);
				}
			}
			else if(xSpeed < 0)
			{
				//animation.AddClip (leftWalk,leftWalk.name);
				//animation.clip = leftWalk;
				if(!animation.IsPlaying(leftWalk.name))
				{
					animation.Play (leftWalk.name);
				}
			}
			else if(xSpeed == 0)
			{
				//animation.AddClip(idleAnimationClip, idleAnimationClip.name);
				//animation.clip = idleAnimationClip;
				if(!animation.IsPlaying(idleLeft.name) & !animation.IsPlaying(idleRight.name))
				{
					if(animation.IsPlaying(leftWalk.name))
					{
						animation.Play (idleLeft.name);
					}
					else if (animation.IsPlaying(leftWalk.name))
					{
						animation.Play (idleRight.name);
					}
				}
			}

			/*disabled to try jetpack only
			float move = Input.GetAxis("Horizontal");
			*/

			GameObject jetPack = GameObject.FindGameObjectWithTag("JetPack");
			ParticleSystem particles = jetPack.GetComponent<ParticleSystem>();
			if(Input.GetMouseButton (0))
			{
				if(jetpackFuel > 0)
				{
					jetPack.transform.LookAt(2 * transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition));
					particles.maxParticles = 1000;
					
					
					rigidbody.AddForce((Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.gameObject.transform.position).normalized * jetpackForce);
					//Debug.Log ((this.gameObject.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition)).normalized.ToString());
					jetpackFuel--;
				}
				else
				{
					particles.maxParticles = 0;
				}
			}
			if(Input.GetMouseButtonUp(0))
			{
				//jetPack.transform.rotation = Quaternion.FromToRotation(gameObject.transform, Input.mousePosition);
				particles.maxParticles = 0;
			}
			
			//this velocity thing seems weird. change to force?
			
			//rigidbody.velocity = new Vector2 (move * currentSpeed, GetComponent<Rigidbody2D>().velocity.y);
			/*rigidbody.AddForce(new Vector2 (move * currentSpeed, 0));
			
			
			if(Input.GetKey(KeyCode.Space) & isJumping == false)
			{
				Jump();
			}
			if(Input.GetKey (KeyCode.Q))
			{
				this.GetComponent<Rigidbody2D>().AddTorque(1);
				
				//transform.Rotate (Vector3.forward);
			}
			if(Input.GetKey (KeyCode.E))
			{
				this.GetComponent<Rigidbody2D>().AddTorque(-1);
				//transform.Rotate (Vector3.forward * -1);
			}*/
		}
	}


	IEnumerator StartSpeedBuff() {
	float timer = 0.0f;
	while (timer <= BASE_BUFF_TIMER) {
		//currentSpeed = VELOC_SPEED;
		//currentAccel = ACCEL_SPEED;
		timer += Time.deltaTime ;
		yield return null;
		}
		//currentSpeed = STANDARD_SPEED;
		//currentAccel = STANDARD_ACCEL;
		hasBuff = false;
				
	}

	void OnLevelWasLoaded(int level)
	{
		if(myCam.GetComponent<Camera>().enabled == false)
		{
			myCam.GetComponent<Camera>().enabled = true;
		}
	}

	void LoadWinScreen()
	{

		Application.LoadLevel ("MainMenu");
		winScreen = false;
		DestroyImmediate(this.gameObject);

	}

	void CheckAndResolveOutOfBounds()
	{
		if(this.transform.position.x < -250 | this.transform.position.x > 250)
		{
			//TODO: Fix this
			//GameObject scoreCounter = GameObject.FindGameObjectWithTag ("ScoreCounter");
			//ScoreCounter scoreCounterScript = scoreCounter.GetComponent<ScoreCounter> ();
			//scoreCounterScript.gamePlayed = false;
			gameManagerScript.paused = false;
			Application.LoadLevel ("MainMenu");
			DestroyImmediate (this.gameObject);
		}
		if(this.transform.position.x < -170 | this.transform.position.x > 170)
		{
			warning = true;
		}
	}

	void SetupPlayerStats()
	{
		warning = false;
		winScreen = false;

		//currentSpeed = STANDARD_SPEED;
		//currentAccel = STANDARD_ACCEL;



		GameObject gameManagerObject = GameObject.FindGameObjectWithTag ("GameManager");
		UpgradeManager upgradeManager = gameManagerObject.GetComponent<UpgradeManager> ();
		if(upgradeManager != null)
		{
			int index = upgradeManager.upgrades.FindIndex(x => x.ToString() == Upgrade.JetpackFuel.ToString ());
			jetpackFuel = MAX_JET_FUEL * (1 + upgradeManager.upgrades[index].MultiplyingValue());

			index = upgradeManager.upgrades.FindIndex (x => x.ToString () == Upgrade.JetpackForce.ToString ());
			jetpackForce = JETPACK_FORCE * (1 + upgradeManager.upgrades[index].MultiplyingValue());

			index = upgradeManager.upgrades.FindIndex (x => x.ToString () == Upgrade.MaxSpin.ToString ());
			maxSpin = MAX_ANGULAR_VELOCITY * (1 - upgradeManager.upgrades[index].MultiplyingValue());

			index = upgradeManager.upgrades.FindIndex (x => x.ToString () == Upgrade.SpinCorrection.ToString ());
			if(upgradeManager.upgrades[index].Level > 0)
			{
				spinCorrection = (upgradeManager.upgrades[index].MultiplyingValue());
			}
			else
			{
				spinCorrection = 0;
			}
		}
		else
		{
			maxSpin = MAX_ANGULAR_VELOCITY;
			jetpackFuel = MAX_JET_FUEL;
			jetpackForce = JETPACK_FORCE;
			spinCorrection = 0;
		}

	}

	void SetupPlayerGUI()
	{
		ButtonWidth = 120;
		ButtonHeight = 20;
		fuelBarColor = new Texture2D(1,1);
		fuelBarColor.SetPixel (1, 1,Color.green);
		fuelBarColor.wrapMode = TextureWrapMode.Repeat;
		fuelBarColor.Apply ();
		fuelStyle = new GUIStyle ();
		fuelStyle.normal.background = fuelBarColor;

	}

	void SetupCamera()
	{
		//if (GetComponent<NetworkView>().isMine)
		//{
		if(myCam.GetComponent<Camera>().enabled == false)
		{
			myCam.GetComponent<Camera>().enabled = true;
		}
	}

}
