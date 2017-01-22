using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LateConnectCaller : MonoBehaviour 
{
	public MonoBehaviour[] m_lateConnectComponents;

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
		ILateConnectInterface[] lateConnect = GetComponentsInChildren<ILateConnectInterface> (true);
		m_lateConnectComponents = new MonoBehaviour[lateConnect.Length];

		for (int i = 0; i < lateConnect.Length; i++) 
		{
			if (lateConnect [i] is MonoBehaviour) {
				m_lateConnectComponents [i] = lateConnect [i] as MonoBehaviour;
			}
		}
	}

	//call connect on all kids
	public void ConnectAllUIElements()
	{

		ILateConnectInterface[] lateConnect = GetComponentsInChildren<ILateConnectInterface> (true);

		foreach(ILateConnectInterface lateComponent in lateConnect)
		{
			if (lateComponent != null) {
			
				lateComponent.ConnectToStates ();
			}
		}
	}
}
