using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameContinueButton : MonoBehaviour 
{

	public static bool s_bContiune = false;


	public void OnClick()
	{
		s_bContiune = true;
	}
		
}
