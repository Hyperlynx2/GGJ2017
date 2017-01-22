using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class GuesserGuess : ClientState 
{
	public bool m_ComandRecieved;

	public override void EnterServer ()
	{
		base.EnterServer ();

		m_ComandRecieved = true;
	}

	[Command]
	public void CmdSetGuesserSelection(int candidateIndex )
	{

		//stop multiple inputs
		if (m_ComandRecieved) {
			return;
		}

		m_ComandRecieved = true;



		//error check selection
		if (candidateIndex < m_player.m_candidateSize) 
		{

			//set target value 
			m_player.m_targetCard = m_player.GetCandidateList () [candidateIndex];
		}

		//throw player ready 
	}


}
