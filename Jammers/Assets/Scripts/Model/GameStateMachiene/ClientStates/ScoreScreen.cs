using UnityEngine;
using System.Collections;

public class ScoreScreen : ClientState 
{
	public override void EnterServer ()
	{
		Dealer.Instance ().PlayerReady (m_player);
	}
}
