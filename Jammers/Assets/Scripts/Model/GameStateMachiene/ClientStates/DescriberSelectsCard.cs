using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class DescriberSelectsCard : ClientState 
{
	 
	[Command]
	public void CmdSelectCard( int cardNumber)
	{

		m_player.m_TargetCard = m_player.m_candidates [cardNumber];

		//go to description write state
		m_StateManager.ChangeStateSafe (InGameState.DESCRIBER_WRITE_DESCRIPTION);

	}


}
