using UnityEngine;
using System.Collections;

public class PlatformBihavior : MonoBehaviour {
	public static PlatformBihavior instance;
	public bool angleEffect;
	float angleLim=7.5f;
	float angle;

	bool left,right;
	float spead;
	float deltaSpead1=0.04f;//для ускорения
	float deltaSpead2=0.01f;//для торможения
	public float limitSpead=0.07f;
	public bool keyBoard=true;
	float widthLim;//для ограничения по x
	// Use this for initialization
	void Start () {
		instance=this;
		widthLim=GameBihavior.instance.fieldWidth/2f;
	}
	
	// Update is called once per frame
	void Update () {
		if(keyBoard)
		{
			left=Input.GetKey(KeyCode.LeftArrow);
			right=Input.GetKey(KeyCode.RightArrow);
		}
		else
		{
			right=Input.mousePosition.x>Screen.width/2;
			left=Input.mousePosition.x<Screen.width/2;
		}

		if(right)
		{
			if(spead<limitSpead)
				spead+=deltaSpead1;
		}
		if(left)
		{
			if(spead>-limitSpead)
				spead-=deltaSpead1;
		}

		if(!left && !right)//торможение
		{
			if(Mathf.Abs(spead)>deltaSpead2)
			{
				if(spead>0)
					spead-=deltaSpead2;
				if(spead<0)
					spead+=deltaSpead2;
			}
			else
				spead=0;
		}
		if(spead!=0)//движение
		{
			transform.Translate(spead,0,0,Space.World);
			if(angleEffect)
			{
				float k=Mathf.Abs(spead/limitSpead);
				float a=Mathf.Lerp(0,angleLim,k);
				if(spead>0)
					transform.rotation=Quaternion.Euler(0,0,-a);
				else if(spead<0)
					transform.rotation=Quaternion.Euler(0,0,a);
				else
					transform.rotation=Quaternion.Euler(0,0,0);
			}
		}
		//ограничение по x
		Vector3 pos=transform.position;
		if(pos.x>widthLim)
			transform.position=new Vector3(widthLim,pos.y,pos.z);
		if(pos.x<-widthLim)
			transform.position=new Vector3(-widthLim,pos.y,pos.z);
	}
}
