using UnityEngine;
using System.Collections;

public class MenuBihavior : MonoBehaviour {

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
