using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class Player : NetworkBehaviour
{
	public enum Role
	{
		DESCRIBER,
		JAMMER,
		GUESSER
	}

	protected static Player s_Instance;

	public static Player Instance()
	{
		if (s_Instance == null) {
			Player[] playerList = FindObjectsOfType<Player> ();

			foreach( Player  player in playerList)
			{

				if (player.isLocalPlayer) {
					s_Instance = player;
				}
			}
		}

		return s_Instance;
	}

	public int m_handSize;

	public int m_candidateSize;

	protected SyncListString m_hand;

	public List<string> GetHandList()
	{
		List<string> hand = new List<string> (m_hand);

		if (hand == null) {
			hand = new List<string> ();
		}

		while (hand.Count < m_handSize) {
			hand.Add ("");

		}

		return hand;
	}

	public void SetHandList(List<string> newValues)
	{
		for (int i = 0; i < m_hand.Count; i++) 
		{
			SetStringAtIndex (m_hand, i, newValues [i]);
		}
	}

	public void SetHandValue(int index, string value )
	{
		SetStringAtIndex (m_hand, index, value);
	}

	protected SyncListString m_candidates;

	public List<string> GetCandidateList()
	{
		List<string> candidates = new List<string> (m_candidates);

		if (candidates == null) {
			candidates = new List<string> ();
		}

		while (candidates.Count < m_candidateSize) 
		{
			candidates.Add ("");

		}

		return candidates;
	}

	public void SetCandidateList(IList<string> newValues)
	{
		for (int i = 0; i < m_candidates.Count; i++) 
		{
			SetStringAtIndex (m_candidates, i, newValues [i]);
		}
	}





	public void SetStringAtIndex(SyncListString stringList, int iIndex, string value)
	{
		stringList [iIndex] = value;
		stringList.Dirty (iIndex);
	}

	public void DoVictoryState()
	{
		ClientStateManager stateMachine = gameObject.GetComponentInChildren<ClientStateManager>();

		if(!stateMachine)
			throw new UnityException("No state machine");

		stateMachine.ChangeStateSafe(InGameState.JAM_WIN); //TODO: clean up state names (one win/loss)
	}

	public void DoLossState()
	{
		ClientStateManager stateMachine = gameObject.GetComponentInChildren<ClientStateManager>();

		if(!stateMachine)
			throw new UnityException("No state machine");

		stateMachine.ChangeStateSafe(InGameState.JAM_LOSS); //TODO: clean up state names (one win/loss)
	}
		
	[SyncVar]
	public string m_targetCard;

	[SyncVar]
	public string m_guess;

	[SyncVar]
	public int m_score;

	public void Awake()
	{
		//add 4 empty items 
		if(!isLocalPlayer)
		{
			while(m_hand.Count < m_handSize)
			{
				m_hand.Add("");
			}

			while(m_hand.Count < m_candidateSize)
			{
				m_candidates.Add("");
			}
		}
	}

	[SyncVar]
	public Role m_role;

	//LHF: proooobably not ever going to go out of sync, but so what?
	[SyncVar]
	public int m_playerNum;
}
