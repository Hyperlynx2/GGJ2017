using UnityEngine;
using System.Collections;

public class ChangeStateButton : MonoBehaviour 
{
	public MenuStateManager.MenuStates m_stateToChangeTo;
	
	public void ChangeState()
	{
		MenuStateManager.s_gameStateManager.ChangeState (m_stateToChangeTo);
	}
}
