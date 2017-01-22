using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableUI : MonoBehaviour , ILateConnectInterface
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

		state.m_OnEnterEvent -= ActivateUI;
		state.m_OnEnterEvent += ActivateUI;


	}

	public void ActivateUI()
	{
		this.gameObject.SetActive (true);
	}


}

