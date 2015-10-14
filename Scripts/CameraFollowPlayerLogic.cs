using UnityEngine;
using System.Collections;

public class CameraFollowPlayerLogic : MonoBehaviour {

	public GameObject player;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Debug.Log (Vector2.MoveTowards(this.gameObject.transform.position, player.gameObject.transform.position, Mathf.Infinity));
		if(player == null)
		{
			player = GameObject.FindGameObjectWithTag ("Player");
		}
		else
		{
			this.gameObject.transform.position = new Vector3(player.gameObject.transform.position.x, player.gameObject.transform.position.y, this.gameObject.transform.position.z);
		}
	}

	void OnPreRender()
	{
		if(player != null)
		{
			Renderer[] renderer = player.gameObject.GetComponentsInChildren<Renderer>();

			for(int i = 0; i < renderer.Length; i++)
			{
				renderer[i].material.color = Color.cyan;
				player.transform.localScale = new Vector3(100f,100f,4f);

			}
		}
	}

	void OnPostRender()
	{
		if(player != null)
		{
			Renderer[] renderer = player.gameObject.GetComponentsInChildren<Renderer>();
			
			for(int i = 0; i < renderer.Length; i++)
			{
				renderer[i].material.color = Color.white;
				player.transform.localScale = new Vector3(1f,1f,1f);
			}
		}
	}
}
