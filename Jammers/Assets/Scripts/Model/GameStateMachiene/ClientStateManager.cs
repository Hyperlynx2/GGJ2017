using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Networking;

public class ClientStateManager : NetworkBehaviour
{
	protected Player m_player = null;

	public Player GetPlayer()
	{
		if(m_player == null)
		{
			m_player = GetComponentInParent<Player> ();
		}

		return m_player;
	}
	
	public static ClientStateManager s_gameStateManager = null;

	public static ClientStateManager Instance()
	{
		if (s_gameStateManager == null) {
			 ClientStateManager[] stateManagers = FindObjectsOfType<ClientStateManager> ();

			foreach (ClientStateManager manager in stateManagers) {
				if (manager.isLocalPlayer == true) {
					s_gameStateManager = manager;

					return s_gameStateManager;
				}
			}
		}

		return s_gameStateManager;
	}

	[SerializeField]
	[ButtonAttribute("FillList")]
	protected bool m_button;

	[SyncVar]
	public InGameState m_clientGameState = InGameState.DEFAULT;

	public InGameState m_startingState = InGameState.WAIT_FOR_PLAYERS;
	
	public bool m_changingState = false;
	
	public List<ClientState> m_screens;
	
	public List<InGameState> m_stateChangeQue;

	public void FillList()
	{
		m_screens = new List<ClientState>( GetComponentsInChildren<ClientState> ());
	}

	//public bool ChangeState(InGameState newGameState)
	//{
	//	if (isLocalPlayer) {
	//		return false;
	//	}
	//
	//	if(m_changingState || newGameState == m_clientGameState )
	//	{
	//		return false;
	//	}
	//	
	//	this.StartCoroutine (ChangeStateCoroutine (newGameState));
	//	
	//	return true;
	//}
	
	public void ChangeStateSafe(InGameState newGameState)
	{
		if (isLocalPlayer) {
			return;
		}

		if(m_stateChangeQue == null )
		{
			m_stateChangeQue = new List<InGameState>();
		}
		
		m_stateChangeQue.Add (newGameState);
	}

	public void DoNextState()
	{
		ClientState currentState = GetState (m_clientGameState);

		InGameState nextState = currentState.NextGameState ();

		ChangeStateSafe (nextState);
	}


	public void ManageSafeStateChange()
	{
		if(m_stateChangeQue != null && m_stateChangeQue.Count > 0 && m_changingState == false) 
		{
			
			if (m_clientGameState != m_stateChangeQue [0]) 
			{
				this.StartCoroutine (ChangeStateCoroutine (m_stateChangeQue [0]));
			} else 
			{
				Dealer.Instance ().PlayerReady (GetPlayer());
			}
			
			m_stateChangeQue.RemoveAt(0);
		}
	}
	
	public IEnumerator ChangeStateCoroutine(InGameState gameState)
	{
		m_changingState = true;

		//game states
		ClientState currentState = null;
		ClientState newState = null;

		if (m_clientGameState != InGameState.DEFAULT) 
		{
			GetState (m_clientGameState);
		}

		if (gameState != InGameState.DEFAULT) 
		{
			//get new state
			newState = GetState (gameState);
		}
		
		//Debug.Log("States Fetched");

		if (currentState != null) {
			//transittion from old state
			yield return StartCoroutine (currentState.OnExit ());
		}
		
		//Debug.Log("Exited state");

		if (newState != null) {
			//transittion into new state
			yield return StartCoroutine (newState.OnEnter ());
		}

		//Debug.Log("entered state");
		
		m_clientGameState = gameState;
		
		m_changingState = false;
		
		yield return null;
		
	}
	
	public ClientState GetState(InGameState gameState)
	{
		foreach( ClientState gameScreen in m_screens)
		{
			if(gameScreen.m_targetGameState == gameState)
			{
				Debug.Log("returning state");
				
				return gameScreen;
			}
		}
		
		//throw new Exception("state does not exist");
		Debug.Log("no state found ");
		return null;
	}

	public T GetState<T>() where T : class
	{
		foreach( ClientState gameScreen in m_screens)
		{
			if(gameScreen is  T )
			{
				Debug.Log("returning state");
				
				return gameScreen as T;
			}
		}
		
		//throw new Exception("state does not exist");
		Debug.Log("no state found ");
		return null;

	}

	//link up all client states
	void LinkClientStates()
	{
		foreach (ClientState state in m_screens) 
		{
			state.m_StateManager = this;

			state.m_player = GetPlayer();
		}
	}

	// Use this for initialization
	void Start () 
	{
		//dont set static if running on server
		if (!isLocalPlayer) 
		{
			return;
		}

		//if game is running on client 

		if (s_gameStateManager != null && s_gameStateManager != this) 
		{
			Destroy(this.gameObject);
		}

		DontDestroyOnLoad (this.gameObject);

		s_gameStateManager = this;

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!isLocalPlayer) 
		{
			//run default state change 
			if (m_clientGameState == InGameState.DEFAULT) 
			{
				ChangeStateSafe (m_startingState);
			}

			ManageSafeStateChange ();

		}
	}
}