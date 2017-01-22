using UnityEngine;
using System.Collections;

public class JamWin :ClientState
{
	public override void EnterServer ()
	{
		Dealer.Instance ().PlayerReady (m_player);
	}
}
