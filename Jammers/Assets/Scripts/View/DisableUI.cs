using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableUI : MonoBehaviour , ILateConnectInterface
{

	public InGameState m_TargetState;

	public void ConnectToStates()
	{
		Connect ();
	}
		
	public void Awake()
	{
		Connect ();
	}

	public void Connect()
	{
		ClientState state =  ClientStateManager.Instance ().GetState (m_TargetState);

		state.m_OnExitEvent -= DeactivateUI;
		state.m_OnExitEvent += DeactivateUI;


	}

	public void DeactivateUI()
	{
		this.gameObject.SetActive (false);
	}


}

