using UnityEngine;
using System.Collections;

public class SpringEffect : MonoBehaviour {
	public static SpringEffect instance;
	float yStart;
	float spead;
	float speadLim=0.05f;
	float speadLim1;
	float kTime;
	float d;
	// Use this for initialization
	void Start () {
		instance=this;
		yStart=transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		if(spead!=0)
		{
			d=yStart-transform.position.y;
			transform.Translate(0,spead,0);
			spead+=d/10f;

			if(spead>speadLim1)
				spead=speadLim1;
			if(spead<-speadLim1)
				spead=-speadLim1;

			if(speadLim1>0)
			{
				if(speadLim1>Mathf.Abs(d))
				{
					speadLim1=speadLim-(kTime/500f);
					kTime++;
				}
			}
			else
			{
				speadLim1=0;
			}

		}
	}

	public void Blow(float s)
	{
		spead=-0.15f;
		speadLim=s;
		speadLim1=speadLim;
		kTime=0;
	}
}
