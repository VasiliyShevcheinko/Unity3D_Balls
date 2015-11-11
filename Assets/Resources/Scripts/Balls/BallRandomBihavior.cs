using UnityEngine;
using System.Collections;

public class BallRandomBihavior : BallBihavior {
	int state;
	float currX,currY,nextX,nextY;
	Vector3 currPos,nextPos;
	float k,dk=0.01f;
	float dy;//расстояние от текущего y до следующего
	float pause;
	#region implemented abstract members of BallBihavior
	protected override void BallUpdate ()
	{
		if(state==0)//выбираем очередную позицию
		{
			currPos=transform.position;
			currX=currPos.x;
			currY=currPos.y;

			nextX=Random.Range(-2.3f,2.3f);
			nextY=currY-Random.Range(0.5f,1.5f);
			nextPos=new Vector3(nextX,nextY,0);
			pause=Random.Range(0.5f,1.5f);
			k=0;
			state=1;
		}
		if(state==1)//перемещаемся в новую точку
		{
			transform.position=Vector3.Slerp(currPos,nextPos,k);
			k+=dk;
			if(k>1)
				state=2;
		}
		if(state==2)//пауза между перемещениями
		{
			if(pause<0)
				state=0;
			else
				pause-=Time.deltaTime;
		}
	}
	#endregion

	public override void SetSpeed (float newSpeed)
	{
		dk=newSpeed;
	}
}
