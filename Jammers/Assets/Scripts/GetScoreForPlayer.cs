using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetScoreForPlayer : MonoBehaviour
{
	public int playerNum = -1;

	// Update is called once per frame
	void Update ()
	{
		if (playerNum < 0)
		{
			Debug.LogError("Player number not set.");
		}
		else
		{
			Text text = gameObject.GetComponent<Text>();

			if(text)
			{
				text.text = "" + Fireball.s_instance._iScores[playerNum];
			}
			else
			{
				Debug.LogError("No Text component on this GameObject.");
			}
		}
		
	}
}
