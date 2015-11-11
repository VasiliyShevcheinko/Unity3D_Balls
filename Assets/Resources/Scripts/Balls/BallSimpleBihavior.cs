using UnityEngine;
using System.Collections;

public class BallSimpleBihavior : BallBihavior {
	#region implemented abstract members of BallBihavior
	protected override void BallUpdate ()
	{
		transform.Rotate(rotateX,rotateY,rotateZ);
		transform.Translate(0,-speedY,0,Space.World);

		//при столкновении с другими шарами шар может отскочить за пределы игрового поля
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
