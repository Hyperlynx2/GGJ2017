using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Networking;

public class ClientStateManager : NetworkBehaviour
{
	
	public static ClientStateManager s_gameStateManager = null;
	
	public InGameState m_clientGameState;
	
	public bool m_changingState = false;
	
	public List<ClientState> m_screens;
	
	public List<InGameState> m_stateChangeQue;
	
	public bool ChangeState(InGameState newGameState)
	{
		if(m_changingState || newGameState == m_clientGameState )
		{
			return false;
		}
		
		this.StartCoroutine (ChangeStateCoroutine (newGameState));
		
		return true;
	}
	
	public void ChangeStateSafe(InGameState newGameState)
	{
		if(m_stateChangeQue == null )
		{
			m_stateChangeQue = new List<InGameState>();
		}
		
		m_stateChangeQue.Add (newGameState);
	}
	
	public void ManageSafeStateChange()
	{
		if(m_stateChangeQue != null && m_stateChangeQue.Count > 0 && m_changingState == false) 
		{
			
			if(m_clientGameState != m_stateChangeQue[0])
			{
				this.StartCoroutine( ChangeStateCoroutine(m_stateChangeQue[0]));
			}
			
			m_stateChangeQue.RemoveAt(0);
		}
	}
	
	public IEnumerator ChangeStateCoroutine(InGameState gameState)
	{
		m_changingState = true;
		
		//get old state 
		ClientState currentState = GetState (m_clientGameState);
		
		//get new state
		ClientState newState = GetState (gameState);
		
		Debug.Log("States Fetched");
		
		//transittion from old state
		yield return StartCoroutine( currentState.OnExit());
		
		Debug.Log("Exited state");
		
		//transittion into new state
		yield return StartCoroutine(newState.OnEnter());
		
		Debug.Log("entered state");
		
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
		ManageSafeStateChange ();
	}
}