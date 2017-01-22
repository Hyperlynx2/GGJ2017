using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class WriteDescription  : ClientState 
{
	//protected playerref
	//public player 
	//{
	//	get
	//	{
	//		playerref = getcomponentinparent<player>();
	//	}
	//}

	//is the description valid
	public bool[] m_validDescription;

	public int m_maxDescriptionLength;

	public string[] m_possibleDescriptions;

	public override void EnterClient()
	{
		//reset valid description array
		m_validDescription =  new bool[]{false,false,false,false};

	}

	[Command]
	public void CmdSetHand( string[] hand)
	{

		//check for validation
		foreach (string description in hand) 
		{
			if(ValidateDescription(description) == false)
			{
				//exit if not valid
				return;
			}
		}

		//set the player hand
		for (int i = 0; i < m_player.GetHandList().Count; i++) 
		{
			m_player.GetHandList() [i] = hand [i];

			Dealer.Instance ().PlayerReady (m_player);
		
		}
		
	}

	public bool ValidateDescription(string description)
	{
		//check if text was entered
		if (description.Length == 0 || description.Length > m_maxDescriptionLength) 
		{
			return false;
		}

		//check if there is spaces in message
		if(description.Contains(" "))
		{
			return false;
		}

		//loop through all the dealer cards 
		foreach (string candidate in m_player.GetCandidateList()) 
		{
			//check if candidate is in message 
			if(description.Contains(candidate))
			{
				return false;
			}

		}

		return true;
	}

	public bool ReadyForNextState()
	{
		foreach (string description in m_player.GetHandList()) 
		{
			if (description != null && description.Length == 0) 
			{
				return false;
			}
		}

		return true;
	}

	public void UpdateDescription(string description, int index)
	{
		m_possibleDescriptions [index] = description;

		for (int i = 0; i <  m_possibleDescriptions.Length; i++) 
		{
			m_validDescription[i] = ValidateDescription(m_possibleDescriptions[i]);
		}
	}

	public bool AreDescriptionsValid()
	{
		foreach (bool isValid in m_validDescription) 
		{
			if(isValid == false)
			{
				return false;
			}
		}

		return true;
	}


	public void SubmitDescriptions()
	{
		CmdSetHand (m_possibleDescriptions);

	}
}
