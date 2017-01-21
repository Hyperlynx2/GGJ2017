using UnityEngine;
using System.Collections;

public class ClientUI<T> : MonoBehaviour where T:class
{
	public T TargetState;

	
	// Use this for initialization
	void Start () 
	{
		TargetState = ClientStateManager.s_gameStateManager.GetState<T> ();

	
	}

}
