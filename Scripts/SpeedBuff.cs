using UnityEngine;
using System.Collections;

public class SpeedBuff : MonoBehaviour {



	void Update()
	{

	}

	/*void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.tag == "Player")
		{
			Debug.Log ("collider2D");
			PlayerInput.setBuff(true);
			Destroy(this.gameObject);
		}
	}*/

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag == "Player")
		{
			NetworkCube.hasBuff = true;
			Destroy(this.gameObject);
		}
	}




}
