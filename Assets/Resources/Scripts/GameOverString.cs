using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//скрипт выводит надпись "Game Over"
public class GameOverString : MonoBehaviour {
	public static GameOverString instance;
	public Text text;

	char[] cm={'G', 'a', 'm', 'e', '\n', 'O', 'v', 'e', 'r'};
	string s;
	int state;
	int i;//итератор для массива символов
	float pause;
	// Use this for initialization
	void Start () {
		instance=this;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(state==1)//добавляем очередной символ
		{
			if(i<cm.Length)
			{
				s+=cm[i];
				text.text=s;
				i++;
				state=2;
				pause=0.5f;
			}
			else
			{
				GameBihavior.instance.VisualMenuBatton();
				state=3;//вывод окончен
				string s="laugh"+Random.Range(1,4).ToString();
				SoundBihavior.instance.Play(s);
			}
		}
		if(state==2)//пауза между выводом символов
		{
			if(pause<0)
			{
				state=1;
			}
			else
			{
				pause-=Time.deltaTime;
			}
		}
	}

	public void Visual()
	{
		if(state==0)
			state=1;
	}
}
