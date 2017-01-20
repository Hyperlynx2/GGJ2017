using UnityEngine;
using System.Collections;

public class Player : Networking.NetworkBehavior
{
	//TODO: LHF: player number. probably needs to be assigned by the server.

	private int m_playerNum;

	// Use this for initialization
	void Start ()
	{
		m_playerNum = GameManager::instance().newPlayerNum();
		GameManager::instance ().addPlayer(this);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	//message come in from the server.
	[ClientRpc]
	public void receiveMessage(string message)
	{
		GUI.Label(Rect (0, 0, 100, 100), message);
	}

	[ClientRpc]
	public int getPlayerNumber()
	{
		return m_playerNum;
	}
}
