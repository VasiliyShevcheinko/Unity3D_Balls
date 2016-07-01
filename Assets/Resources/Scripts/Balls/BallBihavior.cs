using UnityEngine;
using System.Collections;
//базовый шар для шаров
public abstract class BallBihavior : MonoBehaviour {
	protected float rotateX,rotateY,rotateZ;
	protected float speedY=0.01f;//скорость по y
	protected float speedX;
	protected float widthLim;
	protected GameObject platformObj;

	protected abstract void BallUpdate();//для каждого типа шаров своё поведение
	public virtual void SetSpeed(float newSpeed)//у большинства шаров этот метод работает одинаково
	{
		speedY=newSpeed;
	}

	void Start()
	{
		rotateX=Random.Range(-5,5);
		rotateY=Random.Range(-5,5);
		rotateZ=Random.Range(-5,5);
		widthLim=GameBihavior.instance.fieldWidth/2f;
	}
	void Update () {
		BallUpdate();
		if(transform.position.y<-1)
		{
			GameBihavior.instance.GameOver();
			Destroy(gameObject);
		}
	}
	public void Destroy()
	{
		GameObject g=transform.GetChild(0).gameObject;
		g.transform.parent=null;
		g.SetActive(true);
		Destroy(g,2f);
		Destroy(gameObject);
	}
	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.tag=="platform")
		{
			GameBihavior.instance.AddBallPoint();
			SpringEffect.instance.Blow(speedY);
			Destroy();
		}
	}
}
