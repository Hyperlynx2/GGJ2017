using UnityEngine;
using System.Collections;

public class GameStateScreen : MonoBehaviour 
{
	public GameStateManager.GlobalGameState m_targetGlobalGameState;

	public GameObject m_Screen;

	public virtual IEnumerator OnEnter()
	{

		return null;
	}

	public virtual IEnumerator OnExit()
	{

		return null;
	}

}
