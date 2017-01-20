using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Player : NetworkBehaviour
{
	[SyncVar]
	private int m_playerNum;

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

	}

	void OnGUI()
	{
		if (!isLocalPlayer)
			return;

		GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "I'm player " + m_playerNum);
	}

	public void SetPlayerNum(int newNum)
	{
		m_playerNum = newNum;
	}

	[Command]
	public void CmdNewPlayer()
	{
		Server.Instance().AddPlayer(this);
	}

	[Command]
	public void CmdMessageToPlayer(int playerNum, string message)
	{
		/*LHF: Commands are only run on the copy of this player's object, on the server. It's not
		 possible to directly invoke another player's Player object, so it's got to go through an
		 intermediary.

		 https://docs.unity3d.com/Manual/UNetActions.html*/

		Server.Instance().SendMessageTo(playerNum, message);
	}
}
