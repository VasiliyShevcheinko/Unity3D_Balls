using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameBihavior : MonoBehaviour {
	public static GameBihavior instance;
	int chempPoint;
	int currPoint;
	public float fieldWidth;//ширина игрового поля в котором движется платформа и шары
	public Text textChemp;//для вывода рекорда
	public Text textCurr;//для вывода текущих очков
	public GameObject buttonMenu;
	// Use this for initialization
	void Awake () {
		instance=this;
		chempPoint=PlayerPrefs.GetInt("BallsChemp");
		textChemp.text=chempPoint.ToString();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(Input.GetKey(KeyCode.Escape))Application.LoadLevel("menu");
	}

	public void GameOver()
	{
		SoundBihavior.instance.Play("explosion");
		CamJoltBihavior.instance.Jolt();
		GameOverString.instance.Visual();
		PlatformBihavior.instance.gameObject.SetActive(false);
		SpaunerBihavior.instance.Stop();
		GameObject[] balls=GameObject.FindGameObjectsWithTag("ball");
		foreach(GameObject g in balls)
		{
			g.GetComponent<BallBihavior>().Destroy();
		}
	}

	public void BackMenu()
	{
		Application.LoadLevel("menu");
	}

	public void AddBallPoint()
	{
		string s="bonus"+Random.Range(1,3).ToString();
		SoundBihavior.instance.Play(s);
		currPoint++;
		textCurr.text=currPoint.ToString();
		if(currPoint>chempPoint)
		{
			PlayerPrefs.SetInt("BallsChemp",currPoint);
		}
	}

	public void VisualMenuBatton()
	{
		buttonMenu.SetActive(true);
	}
}
