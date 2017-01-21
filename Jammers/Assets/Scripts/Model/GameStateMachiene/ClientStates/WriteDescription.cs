using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

	public List<string> m_possibleDescriptions;

	public void OnEnter()
	{
		//reset valid description array
		m_validDescription =  new bool[]{false,false,false,false};

	}
	
	public void cmdSetHand( IList<string> hand)
	{
		//get player and set selected card to describe
		
		//get cards from dealer 
		//cards[] dealercards = Dealer.Instance().getcards();
		
		
		//set the player target card
		//player.hand = dealercards[cardnumber]

		//clear the hand list
		//foreach (string description in m_player.hand) 
		//{
		//	description = "";

		//}

		//check if passed hand passes validation
		bool passesValidation = true;

		foreach (string description in hand) 
		{
			if(ValidateDescription(description) == false)
			{
				return;
			}
		}

		//set the player hand
		
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

		//get cards from dealer 
		//cards[] candidates = Dealer.Instance().getcandidates();

		//get from player
		IList<string> candidateList = null;

		//loop through all the dealer cards 
		foreach (string candidate in candidateList) 
		{
			//check if candidate is in message 
			if(description.Contains(candidate))
			{
				return false;
			}

		}

		return true;
	}

	public void UpdateDescription(string description, int index)
	{
		m_possibleDescriptions [index] = description;

		for (int i = 0; i <  m_possibleDescriptions.Count; i++) 
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
		cmdSetHand (m_possibleDescriptions);

	}
}
