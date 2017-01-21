using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Networking;

public abstract class ClientState : NetworkBehaviour 
{
	public InGameState m_targetGameState;

	public float m_onEnterDelay;

	public float m_onExitDelay;

	public Action m_OnEnterEvent;

	public Action m_OnExitEvent;

	public IEnumerator OnEnter()
	{
		//run client code 
		EnterClient ();

		m_OnEnterEvent ();

		//wait for client code to finish
		yield return new WaitForSeconds (m_onEnterDelay);
	}

	public IEnumerator OnExit()
	{

		//run client code 
		ExitClient ();

		m_OnExitEvent ();
		
		//wait for client code to finish
		yield return new WaitForSeconds (m_onEnterDelay);
	}

	public virtual void EnterClient()
	{

	}

	public virtual void ExitClient()
	{

	}
	
}
