using UnityEngine;
using System.Collections;

public class PlatformBihavior : MonoBehaviour,IServerObject {
	public static PlatformBihavior instance;
	Rigidbody rigid;
	float angleLim=7.5f;
	float angle;

	bool left,right;
	float spead;
	float deltaSpead1=0.04f;//для ускорения
	float deltaSpead2=0.01f;//для торможения
	public float limitSpead=0.07f;
	float widthLim;//для ограничения по x
	// Use this for initialization
	void Start () {
		instance=this;
		rigid = GetComponent<Rigidbody> ();
		widthLim=GameBihavior.instance.fieldWidth/2f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		left = Input.GetKey (KeyCode.LeftArrow);
		right = Input.GetKey (KeyCode.RightArrow);

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
				if (rigid.velocity.x > 0)
				{
					if (spead > 0)
						spead -= deltaSpead2;
					if (spead < 0)
						spead += deltaSpead2;
				}
				else//если упёрся в бортик
					spead -= spead * 0.3f;
			}
			else
				spead=0;
		}
		if (spead != 0)//движение
		{
			transform.Translate (spead, 0, 0, Space.World);
			float k = Mathf.Abs (spead / limitSpead);
			float a = Mathf.Lerp (0, angleLim, k);
			if (spead > 0)
				transform.rotation = Quaternion.Euler (0, 0, -a);
			else if (spead < 0)
				transform.rotation = Quaternion.Euler (0, 0, a);
		}
		else
			transform.rotation = Quaternion.Euler (0, 0, 0);
		//ограничение по x
		Vector3 pos=transform.position;
		if(pos.x>widthLim)
			transform.position=new Vector3(widthLim,pos.y,pos.z);
		if(pos.x<-widthLim)
			transform.position=new Vector3(-widthLim,pos.y,pos.z);
	}

	#region IServerObject implementation

	public void Activate ()
	{
		this.enabled = true;
	}

	#endregion
}
