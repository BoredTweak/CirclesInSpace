using UnityEngine;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System;
using System.Collections;

public class AudioManager : MonoBehaviour {

	Collection<AudioClip> audioClips;
	private bool playNextClip;

	void Start () 
	{
		audioClips = new Collection<AudioClip> ();
		playNextClip = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(audioClips.Count > 0 && playNextClip)
		{
			StartCoroutine(PlayNextClip());
		}
	}

	public void AddAudio(AudioClip audioClip)
	{
		audioClips.Add (audioClip);
	}

	IEnumerator PlayNextClip()
	{
		playNextClip = false;
		AudioSource audioSource = gameObject.GetComponent<AudioSource> ();
		audioSource.clip = audioClips[0];
		//float waitTime = audioClips [0].length;
		audioSource.PlayOneShot (audioSource.clip);
		yield return new WaitForSeconds(audioClips[0].length);

		audioClips.RemoveAt (0);
		playNextClip = true;
	}
}
