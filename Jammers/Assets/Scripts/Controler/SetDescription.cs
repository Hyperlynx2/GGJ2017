using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetDescription : ClientUI<WriteDescription>  
{

	public int m_CardIndex;

	public InputField m_Input;

	public void OnClick()
	{
		TargetState.UpdateDescription (m_Input.text, m_CardIndex);
	}
}
