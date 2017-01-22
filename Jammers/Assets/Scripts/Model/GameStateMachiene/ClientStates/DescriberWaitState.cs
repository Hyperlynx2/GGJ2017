using UnityEngine;
using System.Collections;

public class DescriberWaitState : ClientState
{
	[SerializeField]
	protected InGameState m_nonActiveJamerRole; 

	public override InGameState NextGameState ()
	{
		if (m_player.m_role == Player.Role.JAMMER) 
		{
			if (Dealer.Instance ().GetCurrentPlayer () != m_player) 
			{
				return m_nonActiveJamerRole;
			}
		}

				return base.NextGameState ();
	}

	public override void EnterServer ()
	{
		Dealer.Instance ().PlayerReady (m_player);
	}
}
