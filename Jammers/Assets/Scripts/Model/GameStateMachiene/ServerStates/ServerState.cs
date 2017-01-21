using UnityEngine;
using System.Collections;

public abstract class ServerState : MonoBehaviour 
{
	public InGameState m_targetGameState;
	
	public abstract IEnumerator OnEnter ();
	
	
	public abstract IEnumerator OnExit();
	
	
}
