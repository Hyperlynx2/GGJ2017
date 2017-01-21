using UnityEngine;
using System.Collections;

public class StateMachineUnitTest : MonoBehaviour 
{
	[Button("ChangeState")]
	public bool m_changeState;

	public MenuStateManager.MenuStates m_stateToChangeTo;

	public void ChangeState()
	{
		MenuStateManager.s_gameStateManager.ChangeState (m_stateToChangeTo);
	}
}
