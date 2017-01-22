using UnityEngine;
using System.Collections;

public class JamWaitForFinish : ClientState
{
	[SerializeField]
	protected InGameState m_jammingInProcess;

	public override InGameState NextGameState ()
	{
		if (Dealer.Instance ().GetCurrentPlayer ().m_role == Player.Role.JAMMER) 
		{
			return m_jammingInProcess;
		}



		return base.NextGameState ();
	}

	public override void EnterServer ()
	{
		Dealer.Instance ().PlayerReady (m_player);
	}
}
