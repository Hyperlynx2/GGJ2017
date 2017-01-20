using UnityEngine;
using System.Collections;

public class HelloButton : MonoBehaviour
{
	private Player m_callMeBack;

	public void SetCallBack(Player player)
	{
		m_callMeBack = player;
	}

	// Use this for initialization
	void Start()
	{
	
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}

	public void OnPressed()
	{
		print ("HelloButton.OnPressed()");
		m_callMeBack.CmdMessageToPlayer(1, "hi player 1");
	}


}
