using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*TODO: this will not work! the Dealer object will exist in the inGame state before the game has started yet!*/

public class Dealer : MonoBehaviour
{
	////////
	//public
	////////
	public int numPlayersRequired = 4; 
	public int scoreToWin = 10;

	public static Dealer Instance()
	{
		if(s_instance == null)
			throw new Exception("Dealer has not been attached to any GameObject!");

		return s_instance;
	}

	//register this player with the dealer
	public void AddPlayer(GameObject playerObj)
	{
		Player player = playerObj.GetComponent<Player>();

		if(player == null)
			throw new Exception("Not a player!");

		m_players.Add(player);
		player.m_playerNum = m_nextAvailablePlayerNum++;
	}

	public Player GetCurrentPlayer()
	{
		return m_players[m_currentPlayerNum];
	}

	public Player GetNextPlayer()
	{
		int nextPlayer = (m_currentPlayerNum +1) % m_players.Count;
		return m_players[nextPlayer];
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

		if(m_readyPlayers.Count == numPlayersRequired
		&& !m_gameIsRunning)
		{
			StartGame();
		}
		else if(m_gameIsRunning && m_readyPlayers.Count == m_players.Count)
		{
			m_readyPlayers.Clear();

			Player currentPlayer = m_players[m_currentPlayerNum];

			if(currentPlayer.m_role == Player.Role.GUESSER)
			{
				DoScores();
			}
			else
			{
				int nextPlayerNum = (m_currentPlayerNum + 1) % m_players.Count;
				Player nextPlayer = m_players[nextPlayerNum];
				nextPlayer.SetHandList(currentPlayer.GetHandList());

				m_currentPlayerNum = nextPlayerNum;
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

	//this is for allocating to connecting players.
	private int m_nextAvailablePlayerNum = 0;

	private IList<Player> m_players;
	private IList<Player> m_readyPlayers;

	//false if waiting for players to join
	bool m_gameIsRunning;

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
		m_players = new List<Player>();
	}

	//from scratch!
	void StartGame()
	{
		m_readyPlayers.Clear();

		//totally fresh start, so new roles for all players.
		m_players[0].m_role = Player.Role.DESCRIBER;
		//player 1's turn to act.
		m_currentPlayerNum = 0;
		m_players[m_players.Count - 1].m_role = Player.Role.GUESSER;

		//the players in the middle are jammers
		for(int p = 1; p < m_players.Count - 1; p++)
		{
			m_players[p].m_role = Player.Role.JAMMER;
		}

		StartRound();
	}

	//at start of a round (whether it's the game start or not)
	void StartRound()
	{
		m_candidates = GenerateCandidates();

		//can't just allocate the targets in the same order as the players know them...
		List<string> shuffled = new List<string>(m_candidates);

		int c = 0;
		foreach(Player player in m_players)
		{
			player.SetCandidateList(m_candidates);

			//the person guessing doesn't have a target.
			if(player.m_role != Player.Role.GUESSER )
			{
				//player.SetTarget(shuffled[c]);
				player.m_targetCard = shuffled[c];
				c++;
			}
		}
	}

	/*check the guesser's guess against each player's target. invoke victory/loss callback on*/
	private void DoScores()
	{
		Player guesser = FindPlayer(Player.Role.GUESSER);

		bool foundScorer = false;
		for(int p = 0; !foundScorer && p < m_players.Count; p++)
		{
			Player player = m_players[p];

			if(player != guesser)
			{
				if(guesser.m_guess == player.m_targetCard)
				{
					foundScorer = true;

					player.m_score++;

					if(player.m_role == Player.Role.JAMMER)
					{
						//bonus point!
						player.m_score++;
					}
					else
					{
						//teamwork point!
						guesser.m_score++;
					}
				}
			}		
		}

		//see if anyone won
		Player winner = null;
		for(int p = 0; winner == null && p < m_players.Count; p++)
		{
			if(m_players[p].m_score >= scoreToWin)
				winner = m_players[p];
		}

		if(winner == null)
		{
			//next round!

			Player.Role lastPlayerRole = m_players[m_players.Count - 1].m_role;

			for(int p = m_players.Count; p > 0; p--)
			{
				m_players[p].m_role = m_players[p-1].m_role;
			}

			m_players[0].m_role = lastPlayerRole;

			//deal new round
			StartRound();

		}
		else
		{
			//victory!

			m_gameIsRunning = false;

			winner.DoVictoryState();

			foreach (Player player in m_players)
			{
				if(player != winner)
					player.DoLossState();
			}
		}
	}
	
	// Update is called once per frame
	void Update()
	{
		
	}

	private Player FindPlayer(Player.Role role)
	{
		Player result = null;

		for(int p = 0; result == null && p < m_players.Count; p++)
		{
			if(m_players[p].m_role == role)
				result = m_players[p];
		}

		return result;
	}

	private IList<string> GenerateCandidates()
	{
		List<string> result = new List<string>();

		//TODO: load up the list from the arrays.

		if(result.Count < m_players.Count - 1)
			throw new Exception("Not enough candidate words for all the players that need them.");

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
}
