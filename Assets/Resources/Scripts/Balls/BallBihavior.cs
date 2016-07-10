using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
//базовый шар для шаров
public abstract class BallBihavior : NetworkBehaviour,IServerObject {
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
		widthLim=GameBihavior.instance.fieldWidth/2f;
	}
	void FixedUpdate () {
		BallUpdate ();
		if (transform.position.y < -1)
		{
			GameBihavior.instance.RpcGameOver ();
			Destroy (gameObject);
		}
	}
	[ClientRpc]
	public void RpcDestroy()
	{
		Destroy ();
	}
	public void Destroy()
	{
		GameObject g=transform.GetChild(0).gameObject;
		g.transform.parent=null;
		g.SetActive(true);
		SpringEffect.instance.Blow (speedY);
		Destroy(g,2f);
		Destroy(gameObject);
	}
	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "platform")
		{
			RpcDestroy ();
			GameBihavior.instance.RpcAddBallPoint();
		}
	}
		
	#region IServerObject implementation

	public void Activate ()
	{
		enabled = true;
		GetComponent<Collider> ().enabled = true;
	}

	#endregion
}
