using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class Player : NetworkBehaviour
{
	public int m_handSize;

	public int m_candidateSize;

	public SyncListString m_hand;

	public SyncListString m_candidates;

	[SyncVar]
	public string m_TargetCard;

	public void Awake()
	{
		//add 4 empty items 
		if(!isLocalPlayer)
		{
			while(m_hand.Count < m_handSize)
			{
				m_hand.Add("");
			}

			while(m_hand.Count < m_candidateSize)
			{
				m_candidates.Add("");
			}
		}
	}

}
