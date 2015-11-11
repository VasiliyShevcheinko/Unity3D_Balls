using UnityEngine;
using System.Collections;

public class BallSmartBihavior : BallBihavior {
	Transform platformT;
	float finishX;
	float randomOffset;
	// Use this for initialization
	void Start () {
		widthLim=GameBihavior.instance.fieldWidth/2f;
		platformT=PlatformBihavior.instance.gameObject.transform;
		speedX=PlatformBihavior.instance.limitSpead*Random.Range(0.3f,1.5f);
		randomOffset=Random.Range(-1f,1f);
	}

	#region implemented abstract members of BallBihavior

	protected override void BallUpdate ()
	{
		finishX=CalculateFinishX();
		finishX+=randomOffset;
		float k=Mathf.Abs(transform.position.x-finishX);
		float sx;
		if((transform.position.x-finishX)>speedX)
		{
			sx=Mathf.Lerp(0,speedX,k);//для плавности
			transform.Translate(-sx,-speedY,0,Space.World);
		}
		else if((transform.position.x-finishX)<-speedX)
		{
			sx=Mathf.Lerp(0,speedX,k);//для плавности
			transform.Translate(sx,-speedY,0,Space.World);
		}
		else
		{
			transform.Translate(0,-speedY,0,Space.World);
		}
	}

	float CalculateFinishX()
	{
		if(platformT.position.x>0)//если платформа ближе к правому краю
		{
			//определяем середину между платформой и левым краем
			return platformT.position.x-((platformT.position.x+2.5f)/2f);
		}
		else if(platformT.position.x<0)//если платформа ближе к левому краю
		{
			//определяем середину между платформой и правым краем
			return platformT.position.x+((-platformT.position.x+2.5f)/2f);
		}
		else// если платформа по иксу в нуле (по середине)
		{
			//целимся между платформой и правым краем
			return 1.75f;
		}
	}
	#endregion
}
