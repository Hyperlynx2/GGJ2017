using UnityEngine;
using System.Collections;

public class GameStateScreen : MonoBehaviour 
{
	public GameStateManager.GlobalGameState m_targetGlobalGameState;
	public GameStateManager.InGameState m_targetInGameState;

	public GameObject m_Screen;

	public virtual IEnumerator OnEnter()
	{

	}

	public virtual IEnumerator OnExit()
	{

	}

}
