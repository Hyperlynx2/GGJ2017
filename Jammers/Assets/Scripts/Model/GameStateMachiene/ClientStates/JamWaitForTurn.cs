using UnityEngine;
using System.Collections;

public class JamWaitForTurn : ClientState
{
	public override void EnterServer ()
	{
		Dealer.Instance ().PlayerReady (m_player);
	}
}
