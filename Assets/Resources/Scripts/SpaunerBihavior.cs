using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpaunerBihavior : MonoBehaviour {
	public static SpaunerBihavior instance;
	public Text textSpeadUp;
	public GameObject[] prefsBallSimple;
	public GameObject[] prefsBallSwing;
	public GameObject[] prefsBallSmart;
	public GameObject[] prefsBallRandom;
	float createPause;
	float createPauseLim=3f;
	int state;
	float currSpead=0.01f;//скорость которую будем устанавливать шарам
	float speadUpTimer;
	bool speadUpFlag;
	Color speadTextColor1;//цвет с нулевой альфой
	Color speadTextColor2;//a=1
	float k;//для перехода между цветами
	// Use this for initialization
	void Start () {
		instance=this;
		speadTextColor1=textSpeadUp.color;
		Color c=speadTextColor1;
		speadTextColor2=new Color(c.r,c.g,c.b,1);
		SpaunerStart(3);
	}
	
	// Update is called once per frame
	void Update () {
		switch(state)
		{
			case 1://пауза перед началом||продолжением
			{
				if(speadUpTimer>30 && !speadUpFlag)//определяем когда увеличить скорость шаров
				{
					speadUpFlag=true;
					createPause+=3;
				}
				else
				{
					speadUpTimer+=Time.deltaTime;
				}
	
				if(createPause<0)
				{
					if(!speadUpFlag)
						state=2;//создадим шар
					else
						state=3;//увеличим скорость
				}
				else
					createPause-=Time.deltaTime;
				break;
			}
			case 2://генерируем первый||очередной шар
			{
				GenerateBall();
				createPause=createPauseLim;;
				state=1;
				break;
			}
			case 3://выводим сообщение об увеличении скорости
			{
				textSpeadUp.color=Color.Lerp(speadTextColor1,speadTextColor2,k);
				k+=0.01f;
				if(k>1)
				{
					state=4;
				}
				break;
			}
			case 4://убераем сообщение и увеличиваем скорость
			{
				textSpeadUp.color=Color.Lerp(speadTextColor1,speadTextColor2,k);
				k-=0.01f;
				if(k<0)
				{
					currSpead+=0.005f;
					if(createPauseLim>0.5f)
					{
						createPauseLim-=createPauseLim/6f;
					}
					speadUpFlag=false;
					speadUpTimer=0;
					state=1;
				}
				break;
			}
		}
	}
	void GenerateBall()
	{
		int r=Random.Range(0,100);
		GameObject pref;
		if(r<40)//генерируем простой шар
		{
			pref=prefsBallSimple[Random.Range(0,prefsBallSimple.Length)];
		}
		else if(r<70)//генерируем качающийся шар
		{
			pref=prefsBallSwing[Random.Range(0,prefsBallSwing.Length)];
		}
		else if(r<90)//генерируем умный шар
		{
			pref=prefsBallSmart[Random.Range(0,prefsBallSmart.Length)];
		}
		else//генерируем шар с случайной траекторией
		{
			pref=prefsBallRandom[Random.Range(0,prefsBallRandom.Length)];
		}
		Vector3 pos=new Vector3(Random.Range(-2.5f,2.5f),5,0);
		GameObject g=Instantiate(pref,pos,Quaternion.identity) as GameObject;
		g.GetComponent<BallBihavior>().SetSpeed(currSpead);
	}
	void SpaunerStart(float p=0)
	{
		if(state==0)
		{
			createPause=p;
			state=1;
		}
	}
	public void Stop()
	{
		state=5;
	}
}
