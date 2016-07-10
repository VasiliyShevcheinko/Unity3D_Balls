using UnityEngine;
using System.Collections;

public class BallSwingBihavior : BallBihavior {
	float amplitufe=0.03f;
	float k;
	void Start()
	{
		rotateX=Random.Range(-5,5);
		rotateY=Random.Range(-5,5);
		rotateZ=Random.Range(-5,5);
		GetComponent<Rigidbody> ().angularVelocity = new Vector3 (rotateX, rotateY, rotateZ);
		widthLim=GameBihavior.instance.fieldWidth/2f;
		amplitufe+=Random.Range(-0.02f,0.02f);
	}
	#region implemented abstract members of BallBihavior
	protected override void BallUpdate ()
	{
		speedX=Mathf.Sin(k);
		k+=0.05f;
		transform.Translate(speedX*amplitufe,-speedY,0,Space.World);
		Vector3 v=transform.position;
		if(v.x>base.widthLim)
		{
			transform.position=new Vector3(base.widthLim,v.y,0);
		}
		if(v.x<-base.widthLim)
		{
			transform.position=new Vector3(-base.widthLim,v.y,0);
		}
	}
	#endregion
}
