using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MenuBihavior : MonoBehaviour {
	public static MenuBihavior instance;
	public InputPanelBihaviour statePanel;
	NetControllerBihaviour netController;
	void Awake()
	{
		instance = this;
	}
	void Start()
	{
		netController = NetControllerBihaviour.instance;
	}
	public void OnPlay()
	{

		if(netController.StartHost ()!=null)
			SceneManager.LoadScene ("game");
		
	}
	public void OnExit()
	{
		Application.Quit();
	}
	public void Connect()//для события кнопки
	{
		Connect (statePanel.inputField.text);
	}
	public void Connect(string strIP)//для события поля
	{
		if (strIP != string.Empty)
		{
			netController.networkAddress = strIP;
			netController.StartClient ();
			netController.client.RegisterHandler (MsgType.Connect, ConnectReact);
			netController.client.RegisterHandler (MsgType.Disconnect, DisconnectReact);
			netController.client.RegisterHandler (MsgType.Error, ErrorReact);
			statePanel.SetConnecting ();
		}
	}
	void ConnectReact(NetworkMessage mes)
	{
		netController.client.UnregisterHandler (MsgType.Connect);
		netController.client.UnregisterHandler (MsgType.Disconnect);
		netController.client.UnregisterHandler (MsgType.Error);
		SceneManager.LoadScene ("game");
	}
	void DisconnectReact(NetworkMessage mes)
	{
		statePanel.SetDisconnect ();
	}
	void ErrorReact(NetworkMessage mes)
	{
		statePanel.SetWrongIP ();
	}
}
