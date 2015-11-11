using UnityEngine;
using System.Collections;

public class MenuBihavior : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	public void OnPlay()
	{
		Application.LoadLevel("game");
	}
	public void OnConnect()
	{
		print("Connect");
	}
	public void OnExit()
	{
		Application.Quit();
	}
}
