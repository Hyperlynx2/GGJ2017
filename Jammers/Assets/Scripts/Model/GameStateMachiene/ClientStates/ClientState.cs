using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Networking;

public abstract class ClientState : NetworkBehaviour 
{
	public ClientStateManager m_StateManager;

	public Player m_player;

	public InGameState m_targetGameState;

	public float m_onEnterDelay;

	public float m_onExitDelay;

	public Action m_OnEnterEvent;

	public Action m_OnExitEvent;

	//on server
	public IEnumerator OnEnter()
	{

			EnterServer ();

		RpcServerToClientOnEnter ();
	
		//wait for client code to finish
		yield return new WaitForSeconds (m_onEnterDelay);
	}

	public IEnumerator OnExit()
	{

		//run server code
		ExitServer ();

		//run client code
		RpcServerToClientOnExit();

		//wait for client code to finish
		yield return new WaitForSeconds (m_onEnterDelay);
	}

	[ClientRpc]
	protected void RpcServerToClientOnEnter()
	{
		EnterClient ();
		m_OnEnterEvent ();
	}


	[ClientRpc]
	protected void RpcServerToClientOnExit()
	{
		ExitClient ();
		m_OnExitEvent ();
	}

	//called on server only
	public virtual void EnterServer()
	{

	}

	public virtual void ExitServer()
	{

	}


	//called on client only 
	public virtual void EnterClient()
	{

	}

	public virtual void ExitClient()
	{

	}
	
}
