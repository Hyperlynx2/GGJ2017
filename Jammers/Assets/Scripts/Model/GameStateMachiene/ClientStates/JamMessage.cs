using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JamMessage : ClientState
{
	//public bool m_done = false;

	public int m_maxDescriptionLength;

	//public void EnterServer()
	//{
	//	m_done = false;
	//}

	public void cmdReplaceDescription(string description, int index)
	{

		//make sure valid
		if (!ValidateDescription (description)) 
		{
			return;
		}

		//set hand description
		m_player.SetHandValue(index, description);


		//tell server i am done

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
}
