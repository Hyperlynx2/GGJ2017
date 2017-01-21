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

	public IEnumerator OnEnter()
	{
		//reset valid description array
		m_validDescription = new bool{false,false,false,false};

		yield return null;
	}
	
	public void cmdSetHand( IList<string> hand)
	{
		//get player and set selected card to describe
		
		//get cards from dealer 
		//cards[] dealercards = Dealer.Instance().getcards();
		
		
		//set the player target card
		//player.hand = dealercards[cardnumber]
		
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

		//check if too long

		//get cards from dealer 
		//cards[] candidates = Dealer.Instance().getcandidates();

		IList<string> candidateList;




		//loop through all the dealer cards 

		foreach (string candidate in candidateList) 
		{
			if(description.Contains(candidate))
			{
				return false;
			}

		}

		return true;
	}

}
