using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class GameBihavior : NetworkBehaviour {
	public static GameBihavior instance;
	[SyncVar]
	int chempPoint;
	int currPoint;
	public float fieldWidth;//ширина игрового поля в котором движется платформа и шары
	public Text textChemp;//для вывода рекорда
	public Text textCurr;//для вывода текущих очков
	public GameObject buttonMenu;
	public PlatformBihavior platform;
	public SpaunerBihavior spauner;
	public SpeedUpText speedUpText;
	// Use this for initialization
	void Awake () {
		instance=this;
		textChemp.text=chempPoint.ToString();
		if (NetControllerBihaviour.instance.isHost)
		{
			platform.Activate ();
			spauner.SpaunerStart ();
			speedUpText.callBack = spauner.SpaunerStart;
			chempPoint = PlayerPrefs.GetInt ("BallsChemp");
			textChemp.text = chempPoint.ToString();//для сервера
		}
		NetControllerBihaviour.instance.client.RegisterHandler (MsgType.Error,(a)=>BackMenu());
		NetControllerBihaviour.instance.client.RegisterHandler (MsgType.Disconnect,(a)=>BackMenu());
	}
	void Start()
	{
		textChemp.text = chempPoint.ToString();//для клиента
	}
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Escape))
			BackMenu ();
	}
	[ClientRpc]
	public void RpcGameOver()
	{
		SoundBihavior.instance.Play("explosion");
		CamJoltBihavior.instance.Jolt();
		GameOverString.instance.Visual();
		platform.gameObject.SetActive(false);
		SpaunerBihavior.instance.Stop();
		GameObject[] balls=GameObject.FindGameObjectsWithTag("ball");
		foreach(GameObject g in balls)
		{
			g.GetComponent<BallBihavior> ().Destroy ();
		}
	}
	[ClientRpc]
	public void RpcAddBallPoint()
	{
		string s="bonus";
		SoundBihavior.instance.Play(s);
		currPoint++;
		textCurr.text=currPoint.ToString();
		if (isServer)
		{
			if (currPoint > chempPoint)
			{
				PlayerPrefs.SetInt ("BallsChemp", currPoint);
			}
		}
	}
	[ClientRpc]
	public void RpcShowSpeedUp()
	{
		speedUpText.ShowSpeedUp ();
	}
	public void BackMenu()
	{
		NetControllerBihaviour.Shutdown ();
		Destroy(NetControllerBihaviour.instance.gameObject);
		Application.LoadLevel("menu");
	}
	public void VisualMenuBatton()
	{
		buttonMenu.SetActive(true);
	}
}
