using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class NetControllerBihaviour : NetworkManager{
	public static NetControllerBihaviour instance { get; private set;}
	public bool isHost{get;private set;}
	void Awake()
	{
		instance = this;
	}

	public override void OnStartHost ()
	{
		base.OnStartHost ();
		isHost = true;
	}

	public override void OnStopHost ()
	{
		base.OnStopHost ();
		isHost = false;
	}

	public override void OnServerError (NetworkConnection conn, int errorCode)
	{
		SceneManager.LoadScene ("menu");
	}

	public override void OnClientError (NetworkConnection conn, int errorCode)
	{
		base.OnClientError (conn, errorCode);
		SceneManager.LoadScene ("menu");
	}
}
