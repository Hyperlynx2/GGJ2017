using UnityEngine;
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

	public enum InGameState
	{
		ENCODE_MESSAGE,
		JAM_SIGNAL,
		DECODE_MESSAGE,
		WAIT_TURN,
		NOT_INGAME,
		NONE

	}

	public GlobalGameState m_globalGameState;
	public InGameState m_inGameState;

	public List<GameStateScreen> m_scenes;

	public void ChangeScene()
	{

	}

	public IEnumerator ChangeSceenCoRouteen(GlobalGameState gameState)
	{


	}

	public GameStateScreen GetScreen(GlobalGameState gameState)
	{

	}



	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
