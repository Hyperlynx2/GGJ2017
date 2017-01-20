using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Player : NetworkBehaviour
{
	private int m_playerNum;

	// Use this for initialization
	void Start ()
	{
		m_playerNum = GameManager.instance().newPlayerNum();
		GameManager.instance ().addPlayer(this);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	//message come in from the server.
	[ClientRpc]
	public void receiveMessage(string message)
	{
		GUI.Label(new Rect (0.0, 0.0, 100.0, 100.0), message);
	}

	[ClientRpc]
	public int getPlayerNumber()
	{
		return m_playerNum;
	}
}
