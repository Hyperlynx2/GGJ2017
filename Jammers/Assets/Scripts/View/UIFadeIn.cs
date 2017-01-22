using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFadeIn : MonoBehaviour , ILateConnectInterface
{
	public void ConnectToStates()
	{
		Connect ();
	}

	public InGameState m_TargetState;

	public CanvasGroup m_fadeTarget;

	[Range(0,1)]
	public float m_percentOfSafeTimeFadeScale;

	protected float fadeInRate;

	public float m_targetAlpha = 1;

	public void Awake()
	{
		Connect ();
	}

	public void Connect()
	{
		ClientState state =  ClientStateManager.Instance ().GetState (m_TargetState);

		state.m_OnEnterEvent -= FadeIn;
		state.m_OnEnterEvent += FadeIn;

		fadeInRate = 1.0f / state.m_onEnterDelay;

	}

	public void FadeIn()
	{
		if(m_fadeTarget == null)
		{
			m_fadeTarget = GetComponent<CanvasGroup> ();
		}

		this.StartCoroutine (Fade ());
	}

	public IEnumerator Fade()
	{
		while (m_fadeTarget.alpha < m_targetAlpha) {

			m_fadeTarget.alpha += fadeInRate * m_targetAlpha * m_percentOfSafeTimeFadeScale * Time.deltaTime;

			yield return null;
		}

		m_fadeTarget.alpha = m_targetAlpha;


	}


}
