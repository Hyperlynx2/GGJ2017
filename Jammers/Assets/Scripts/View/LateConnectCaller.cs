using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LateConnectCaller : MonoBehaviour 
{
	public ILateConnectInterface[] m_lateConnectComponents;

	[SerializeField]
	[ButtonAttribute("GetLateComponents")]
	protected bool m_button;

	// Update is called once per frame
	void Start() 
	{
		ConnectAllUIElements ();
	}

	public void GetLateComponents()
	{
			m_lateConnectComponents = GetComponentsInChildren<ILateConnectInterface> (true);

	}

	//call connect on all kids
	public void ConnectAllUIElements()
	{
		foreach(ILateConnectInterface lateComponent in m_lateConnectComponents)
		{
			if (lateComponent != null) {
			
				lateComponent.ConnectToStates ();
			}
		}
	}
}
