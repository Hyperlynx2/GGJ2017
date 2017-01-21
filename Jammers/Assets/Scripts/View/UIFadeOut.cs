using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFadeOut: MonoBehaviour
{

	public InGameState m_TargetState;

	public CanvasGroup m_fadeTarget;


	protected float fadeOutRate;


	public void Awake()
	{
		ClientState state =  ClientStateManager.Instance ().GetState (m_TargetState);

		state.m_OnEnterEvent += FadeOut;

		fadeOutRate = 1.0f / state.m_onExitDelay;

	}

	public void FadeOut()
	{
		if(m_fadeTarget == null)
		{
			m_fadeTarget = GetComponent<CanvasGroup> ();
		}

		this.StartCoroutine (Fade ());
	}

	public IEnumerator Fade()
	{
		float StartAlpha = m_fadeTarget.alpha;

		while (m_fadeTarget.alpha > 0) {

			m_fadeTarget.alpha -= fadeOutRate * StartAlpha * Time.deltaTime;

			yield return null;
		}

		m_fadeTarget.alpha = 0f;


	}


}
