using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*TODO: this will not work! the Dealer object will exist in the inGame state before the game has started yet!*/

public class Dealer : MonoBehaviour
{
	////////
	//public
	////////
	 
	public static Dealer Instance()
	{
		if(s_instance == null)
			throw new Exception("Dealer has not been attached to any GameObject!");

		return s_instance;
	}

	/*Signal to the dealer that you've made your move and are ready for the next turn.

	called by the server code of the Player gameObject (which is the state machine).
	We decide what to do based on what state we're in.*/
	public void PlayerReady(Player player)
	{
		if(!m_readyPlayers.Contains(player))
		{
			m_readyPlayers.Add(player);
		} 

		if(m_readyPlayers.Count == m_players.Count)
		{
			m_readyPlayers.Clear();

			Player currentPlayer = m_players[m_currentPlayerNum];

			if(currentPlayer.GetRole() == Player.Role.GUESSER)
			{
				DoScores();
			}
			else
			{
				int nextPlayerNum = (m_currentPlayerNum + 1) % m_players.Count;
				Player nextPlayer = m_players[nextPlayerNum];
				nextPlayer.SetHand(currentPlayer.GetHand());

				foreach(Player p in m_players)
				{
					p.DoNextState();
				}
			}
		}

	}

	class Exception : System.Exception
	{
		public Exception(string message) : base(message) {}
	}



	/////////
	//private
	/////////
	private static Dealer s_instance = null;

	private IList<Player> m_players;
	private IList<Player> m_readyPlayers;

	private int m_startingPlayerNum;
	private int m_currentPlayerNum;

	//the list of strings the describer gets to pick from.
	private IList<string> m_candidates;

	void Awake()
	{
		if(s_instance != null)
			throw new Exception("Dealer has been assigned to more than one GameObject. It's a singleton!");

		s_instance = this;
	}

	// Use this for initialization
	void Start()
	{
		m_candidates = GenerateCandidates();

		m_players = new List<Player>();
		foreach(Player player in GameObject.FindObjectsOfType(typeof(Player)))
		{
			m_players.Add(player);

			player.SetCandidates(m_candidates);
		}

		if (m_players.Count < 3)
			throw new Exception("Cannot play with less than three players.");

		if(m_candidates.Count < m_players.Count - 1)
			throw new Exception("Not enough candidate words for all the players that need them.");

		m_readyPlayers.Clear();

		//totally fresh start, so new roles for all players.
		m_players[0].SetRole(Player.Role.DESCRIBER);
		//player 1's turn to act.
		m_currentPlayerNum = 0;
		m_startingPlayerNum = 0;
		m_players[m_players.Count - 1].SetRole(Player.Role.GUESSER);

		//the players in the middle are jammers
		for(int p = 1; p < m_players.Count - 1; p++)
		{
			m_players[p].SetRole(Player.Role.JAMMER);
		}

		//give target words to the players that need them
		{
			//can't just allocate the targets in the same order as the players know them...
			List<string> shuffled = new List<string>(m_candidates);

			int c = 0;
			foreach(Player player in m_players)
			{
				//the person guessing doesn't have a target.
				if(player.GetRole() != Player.Role.GUESSER )
				{
					player.SetTarget(shuffled[c]);
					c++;
				}
			}
		}

	}

	/*check the guesser's guess against each player's target. invoke victory/loss callback on*/
	void DoScores()
	{
		
	}
	
	// Update is called once per frame
	void Update()
	{
		
	}

	private IList<string> GenerateCandidates()
	{
		List<string> result = new List<string>();

		//TODO: load up the list from the arrays.

		return result;
	}

	//thanks to http://stackoverflow.com/questions/273313/randomize-a-listt
	private static System.Random rng = new System.Random();  

	public static void Shuffle<T>(IList<T> list)  
	{  
		int n = list.Count;  
		while (n > 1) {  
			n--;  
			int k = rng.Next(n + 1);  
			T value = list[k];  
			list[k] = list[n];  
			list[n] = value;  
		}  
	}

	///////////////////
	//state transitions
	///////////////////
}
