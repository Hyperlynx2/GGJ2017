using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Player : NetworkBehaviour
{
	[SyncVar]
	private int m_playerNum;

	private string m_currentMessage;

	// Use this for initialization
	void Start()
	{
		if (!isLocalPlayer)
			return;

		CmdNewPlayer();

	}
	
	// Update is called once per frame
	void Update()
	{
		GameObject button = GameObject.Find("SayHelloButton");

		if(button)
			button.GetComponent<HelloButton>().SetCallBack(this);

	}

	void OnGUI()
	{
		if (!isLocalPlayer)
			return;

		if(m_currentMessage == null)
			GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "I'm player " + m_playerNum);
		else
			GUI.Box(new Rect(0, 0, Screen.width, Screen.height), m_currentMessage);

	}

	public void SetPlayerNum(int newNum)
	{
		m_playerNum = newNum;
	}

	public int GetPlayerNum()
	{
		return m_playerNum;
	}

	[Command]
	public void CmdNewPlayer()
	{
		Dealer.Instance().AddPlayer(this);
	}

	[Command]
	public void CmdMessageToPlayer(int playerNum, string message)
	{
		/*LHF: Commands are only run on the copy of this player's object, on the server. It's not
		 possible to directly invoke another player's Player object, so it's got to go through an
		 intermediary.

		 https://docs.unity3d.com/Manual/UNetActions.html*/

		print("CmdMessageToPlayer");

		Dealer.Instance().SendMessageTo(playerNum, message);
	}

	[ClientRpc]
	public void RpcReceiveMessage(string message)
	{
		print ("RpcReceiveMessage");
		m_currentMessage = message;
	}
}
