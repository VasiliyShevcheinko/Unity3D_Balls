using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;
//скрипт трясёт камеру
public class CamJoltBihavior : MonoBehaviour {
	public static CamJoltBihavior instance;
	Vector3 vLocStart,vLocCurr;
	Vector3 rRotStart;
	float k,dk=0.2f;//кожфицент для Lerp
	float startDr=0.2f,currDr;//изменение при тряске текущее и начальное
	int state;
	int countAll,countCurr;//количество встрясок
	// Use this for initialization
	void Awake () {
		instance=this;
		vLocStart=transform.localPosition;
		rRotStart=transform.localRotation.eulerAngles;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		switch(state)
		{
			case 1://задаём новое положение камеры
			{
				countCurr++;
				currDr=startDr/(float)countCurr;
				float x=Random.Range(-currDr,currDr);
				float y=Random.Range(-currDr,currDr);
				float z=Random.Range(-currDr,currDr);
				vLocCurr=new Vector3(vLocStart.x+x,vLocStart.y+y,vLocStart.z+z);
				transform.localPosition=vLocCurr;
				k=0;
				state=2;
				break;
			}
			case 2:
			{
				if(k<1)
				{
					transform.localPosition=Vector3.Lerp(vLocCurr,vLocStart,k);
					k+=dk;
				}
				else
				{
					if(countCurr==countAll)
						state=0;
					else
						state=1;
				}
				break;
			}
		}
	}
	public void Jolt(bool b=false)
	{
		if(b)
			Jolt(10,true);
		else
			Jolt(10);
	}
	public void Jolt(int c,bool b=false)//c количество волн при встряске, b помутнение
	{
		if(state==0)
		{
			countAll=c;
			countCurr=0;
			currDr=startDr;
			state=1;
		}
	}
}
