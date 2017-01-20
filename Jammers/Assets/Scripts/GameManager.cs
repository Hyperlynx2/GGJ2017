using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

/*Other classes should get the GameManager by calling GameManager::instance()*/
public class GameManager : NetworkBehaviour
{
	private static GameManager s_instance = null;

	private IList<Player> m_playerList;

	private int m_nextPlayerNum;

	public class Exception : System.Exception
	{
		public Exception(string message) :base(message) {}
	}

	public static GameManager instance()
	{
		if (s_instance == null)
			throw new Exception("GameManager hasn't been instantiated yet. It needs to be attached as a component!");

		return s_instance;
	}

	void Awake()
	{
		if (s_instance != null)
			throw new Exception ("GameManager has been used more than once.");

		s_instance = this;
	}

	// Use this for initialization
	void Start ()
	{
		m_playerList = new List<Player> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	[Command]
	public int newPlayerNum()
	{
		return m_nextPlayerNum++;
	}

	[Command]
	public void addPlayer(Player player)
	{
		m_playerList.Add(player);
	}

	[Command]
	public void sendMessageTo(int playerNum, string message)
	{
		Player recipient = null;
		for(int i = 0; recipient == null && i < m_playerList.Count; i++)
		{
			if(m_playerList[i].getPlayerNumber() == playerNum)
				recipient = m_playerList[i];
		}

		if(recipient != null)
			recipient.receiveMessage(message);
	}

}
