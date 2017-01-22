using UnityEngine;
using System.Collections;

public class RoundXScreen : ClientState 
{
	public override void EnterServer ()
	{
		Dealer.Instance ().PlayerReady (m_player);
	}
}
