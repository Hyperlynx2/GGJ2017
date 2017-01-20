using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Player : NetworkBehaviour
{
	private int m_playerNum;

	// Use this for initialization
	void Start()
	{
		GameManager.Instance().CmdAddPlayer(gameObject);
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}

	//message come in from the server.
	[ClientRpc]
	public void RpcReceiveMessage(string message)
	{
		GUI.Label(new Rect (0.0f, 0.0f, 100.0f, 100.0f), message);
	}

	[ClientRpc]
	public void RpcSetPlayerNumber(int playerNumber)
	{
		m_playerNum = playerNumber;
	}

	public int GetPlayerNumber()
	{
		return m_playerNum;
	}
}
