using UnityEngine;
using System.Collections;

public class MenuState : MonoBehaviour 
{
	public MenuStateManager.MenuStates m_targetMenuStates;

	public GameObject m_Screen;

	public virtual IEnumerator OnEnter()
	{
		Debug.Log("De activating state object");
		m_Screen.SetActive (true);
		yield return null;
	}

	public virtual IEnumerator OnExit()
	{
		Debug.Log("Activating state object");
		m_Screen.SetActive (false);
		yield return null;
	}

}
