using UnityEngine;
using System.Collections;

public class SoundBihavior : MonoBehaviour {
	public static SoundBihavior instance;
	AudioSource source;
	AudioClip clip;
	// Use this for initialization
	void Start () {
		instance=this;
		source=GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Play(string name)
	{
		AudioClip clip = Resources.Load<AudioClip> ("Sounds/"+name);
		source.PlayOneShot(clip);
	}
}
