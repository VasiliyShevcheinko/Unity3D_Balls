using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
public class SpeedUpText : MonoBehaviour {
	int state;
	Text text;
	Color speadTextColor1;//цвет с нулевой альфой
	Color speadTextColor2;//a=1
	float k;//для перехода между цветами
	public UnityAction callBack;
	// Use this for initialization
	void Start () {
		text = GetComponent<Text> ();
		speadTextColor1=text.color;
		Color c=speadTextColor1;
		speadTextColor2=new Color(c.r,c.g,c.b,1);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		switch (state)
		{
			case 1:
				{
					text.color = Color.Lerp (speadTextColor1, speadTextColor2, k);
					k += 0.01f;
					if (k > 1)
					{
						state = 2;
					}
					break;
				}
			case 2:
				{
					text.color = Color.Lerp (speadTextColor1, speadTextColor2, k);
					k -= 0.005f;
					if (k < 0)
					{
						state = 0;
						if(callBack!=null)
							callBack();
					}
					break;
				}
		}
	}
	public void ShowSpeedUp()
	{
		state = 1;
	}
}
