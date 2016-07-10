using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class InputPanelBihaviour : MonoBehaviour {
	public InputField inputField;
	public Text textState;
	public Text placeholdText;
	Coroutine whiteCorutine;

	public void SetStarting()
	{
		placeholdText.enabled = true;
		inputField.interactable = true;
		textState.text = "Enter IP";
		inputField.text = "127.0.0.1";
	}
	public void SetConnecting()
	{
		inputField.interactable = false;
		textState.alignment = TextAnchor.MiddleLeft;
		whiteCorutine = StartCoroutine ("WaitEffect");
	}
	public void SetDisconnect()
	{
		inputField.interactable = true;
		StopCoroutine (whiteCorutine);
		textState.alignment = TextAnchor.MiddleCenter;
		textState.text = "Address not available";
	}
	public void SetWrongIP()
	{
		StopCoroutine (whiteCorutine);
		inputField.interactable = true;
		textState.alignment = TextAnchor.MiddleCenter;
		textState.text = "Wrong IP";
	}
	void OnEnable()
	{
		SetStarting ();
	}
	IEnumerator WaitEffect()
	{
		int j=0;
		string s;
		for (;;)
		{
			s = "Connect";
			for (int i = 0; i < j; i++)
			{
				s += '.';
			}
			textState.text = s;
			j++;
			if (j > 3)
				j = 0;
			yield return new WaitForSeconds (0.3f);
		}
	}
}
