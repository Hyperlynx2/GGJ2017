using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GameStateManager : MonoBehaviour 
{

	public enum GlobalGameState
	{
		SPLASH_SCREEN,
		CONNECT,
		CONNECT_FAIL,
		WAITING,
		NEW_GAME,
		GAME_PLAY,
		GAME_MENU,
		QUIT_CONFIRM,
		GAME_END,
		NONE
	}

	public static GameStateManager s_gameStateManager = null;

	public GlobalGameState m_globalGameState;

	public bool m_changingState = false;

	public List<GameStateScreen> m_screens;

	public void ChangeScene(GlobalGameState newGameState)
	{

	}

	public IEnumerator ChangeSceenCoRouteen(GlobalGameState gameState)
	{
		m_changingState = true;

		//get old state 
		GameStateScreen currentState = GetScreen (m_globalGameState);

		//get new state
		GameStateScreen newState = GetScreen (gameState);

		//transittion from old state
		yield return StartCoroutine( currentState.OnExit ());

		//transittion into new state
		yield return StartCoroutine(currentState.OnEnter ());

		m_changingState = false;

	}

	public GameStateScreen GetScreen(GlobalGameState gameState)
	{
		foreach( GameStateScreen gameScreen in m_screens)
		{
			if(gameScreen.m_targetGlobalGameState == gameState)
			{
				return gameScreen;
			}
		}

		throw new Exception("state does not exist");

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
	void Update () {
	
	}
}
