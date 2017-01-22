using UnityEngine;
using System.Collections;

public class WaitForDescriberToWriteDescription : ClientState 
{
	public override void EnterServer ()
	{
		Dealer.Instance ().PlayerReady (m_player);
	}
}

