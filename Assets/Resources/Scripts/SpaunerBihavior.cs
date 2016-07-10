using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections.Generic;

public class SpaunerBihavior : MonoBehaviour {
	public static SpaunerBihavior instance;
	public GameObject prefsBallSimple;
	public GameObject prefsBallSwing;
	public GameObject prefsBallSmart;
	public GameObject prefsBallRandom;
	float createPause=3f;
	float createPauseLim=3f;
	float currSpead=0.01f;//скорость которую будем устанавливать шарам
	float speedUpTimer;
	bool isWork;
	// Use this for initialization
	void Start () {
		instance=this;
		ClientScene.RegisterPrefab (prefsBallSimple);
		ClientScene.RegisterPrefab (prefsBallSwing);
		ClientScene.RegisterPrefab (prefsBallSmart);
		ClientScene.RegisterPrefab (prefsBallRandom);
	}
	
	// Update is called once per frame
	void Update () {
		if(isWork)
		{
			if(speedUpTimer>30)//определяем когда увеличить скорость шаров
			{
				speedUpTimer = 0;
				createPause+=3;
				isWork = false;
				currSpead += 0.005f;
				GameBihavior.instance.RpcShowSpeedUp();
				if (createPauseLim > 0.5f)
				{
					createPauseLim -= createPauseLim / 6f;
				}
				return;
			}
			else
			{
				speedUpTimer+=Time.deltaTime;
			}

			if(createPause<0)
			{
				GenerateBall();
				createPause=createPauseLim;
			}
			else
				createPause-=Time.deltaTime;
		}
	}
	void GenerateBall()
	{
		int r=Random.Range(0,100);
		GameObject pref;
		if(r<40)//генерируем простой шар
		{
			pref=prefsBallSimple;
		}
		else if(r<70)//генерируем качающийся шар
		{
			pref=prefsBallSwing;
		}
		else if(r<90)//генерируем умный шар
		{
			pref=prefsBallSmart;
		}
		else//генерируем шар с случайной траекторией
		{
			pref=prefsBallRandom;
		}
		Vector3 pos=new Vector3(Random.Range(-2.5f,2.5f),5,0);
		GameObject g=Instantiate(pref,pos,Quaternion.identity) as GameObject;
		BallBihavior ballBih = g.GetComponent<BallBihavior> ();
		ballBih.Activate ();
		ballBih.SetSpeed(currSpead);
		NetworkServer.Spawn (g);
	}
	public void SpaunerStart()
	{
		isWork = true;
	}
	public void Stop()
	{
		isWork = false;
	}
}
