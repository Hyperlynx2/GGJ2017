using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class MenuStateManager : MonoBehaviour 
{

	public enum MenuStates
	{
		SPLASH_SCREEN,
		CONNECT,
		CONNECT_FAIL,
		NEW_GAME,
		IN_GAME,
		NONE
	}

	public static MenuStateManager s_gameStateManager = null;

	public MenuStates m_menuState;

	public bool m_changingState = false;

	public List<MenuState> m_screens;

	public List<MenuStates> m_stateChangeQue;

	public bool ChangeState(MenuStates newGameState)
	{
		if(m_changingState || newGameState == m_menuState )
		{
			return false;
		}

		this.StartCoroutine (ChangeStateCoroutine (newGameState));

		return true;
	}

	public void ChangeStateSafe(MenuStates newGameState)
	{
		if(m_stateChangeQue == null )
		{
			m_stateChangeQue = new List<MenuStates>();
		}

		m_stateChangeQue.Add (newGameState);
	}

	public void ManageSafeStateChange()
	{
		if(m_stateChangeQue != null && m_stateChangeQue.Count > 0 && m_changingState == false) 
		{

			if(m_menuState != m_stateChangeQue[0])
			{
				this.StartCoroutine( ChangeStateCoroutine(m_stateChangeQue[0]));
			}

			m_stateChangeQue.RemoveAt(0);
		}
	}

	public IEnumerator ChangeStateCoroutine(MenuStates gameState)
	{
		m_changingState = true;

		//get old state 
		MenuState currentState = GetState (m_menuState);

		//get new state
		MenuState newState = GetState (gameState);

		Debug.Log("States Fetched");

		//transittion from old state
		yield return StartCoroutine( currentState.OnExit());

		Debug.Log("Exited state");

		//transittion into new state
		yield return StartCoroutine(newState.OnEnter());

		Debug.Log("entered state");

		m_menuState = gameState;

		m_changingState = false;

		yield return null;

	}

	public MenuState GetState(MenuStates gameState)
	{
		foreach( MenuState gameScreen in m_screens)
		{
			if(gameScreen.m_targetMenuStates == gameState)
			{
				Debug.Log("returning state");

				return gameScreen;
			}
		}

		//throw new Exception("state does not exist");
		Debug.Log("no state found ");
		return null;
	}



	// Use this for initialization
	void Start () 
	{
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
