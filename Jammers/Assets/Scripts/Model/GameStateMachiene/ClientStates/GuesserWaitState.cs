using UnityEngine;
using System.Collections;

public class GuesserWaitState : ClientState
{
	public override void EnterServer ()
	{
		Dealer.Instance ().PlayerReady (m_player);
	}
}