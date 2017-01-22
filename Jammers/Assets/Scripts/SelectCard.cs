using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCard : MonoBehaviour 
{
	public static int s_iSelectedCard;

	public static bool s_bSelected;

	public int _iTargetInt;



	public void OnClick()
	{
		s_iSelectedCard = _iTargetInt;
		s_bSelected = true;
	}
}
