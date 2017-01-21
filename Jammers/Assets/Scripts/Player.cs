using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

public class Player : NetworkBehaviour
{
	enum Role
	{
		GUESSER,
		DESCRIBER,
		JAMMER
	}

	[SyncVar]
	private int m_playerNum;

	[SyncVar]
	private Role m_role;

	//the list of cards that the targets are drawn from. everyone knows this.
	[SyncVar]
	private IList<string> m_candidates;

	//the card that this player is trying to have the guesser guess (unless they ARE the guesser)
	private string m_targetCard;

	/*the current cards this player is fiddling with. either authoring to describe the target (if they're the
	describer), or looking at and replacing a card in (if they're a jammer), or guessing from (if they're the
	guesser)*/
	private IList<string> m_hand;

	// Use this for initialization
	void Start()
	{
		if (!isLocalPlayer)
			return;

		CmdNewPlayer();

	}
	
	// Update is called once per frame
	void Update()
	{
		GameObject button = GameObject.Find("SayHelloButton");

		if(button)
			button.GetComponent<HelloButton>().SetCallBack(this);

	}

	void OnGUI()
	{
		if (!isLocalPlayer)
			return;

		if(m_currentMessage == null)
			GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "I'm player " + m_playerNum);
		else
			GUI.Box(new Rect(0, 0, Screen.width, Screen.height), m_currentMessage);

	}

	public void SetPlayerNum(int newNum)
	{
		m_playerNum = newNum;
	}

	public int GetPlayerNum()
	{
		return m_playerNum;
	}

	public Role GetRole()
	{
		return m_role;
	}

	public void SetRole(Role role)
	{
		m_role = role;
	}

	[Command]
	public void CmdNewPlayer()
	{
		Dealer.Instance().AddPlayer(this);
	}

	[Command]
	public void CmdGiveHandToDealer()
	{
		/*LHF: Commands are only run on the copy of this player's object, on the server. It's not
		 possible to directly invoke another player's Player object, so it's got to go through an
		 intermediary. That's the Dealer.

		 https://docs.unity3d.com/Manual/UNetActions.html*/

		Dealer.Instance().GiveHandToDealer(m_playerNum, m_hand);
	}

	[ClientRpc]
	public void RpcReceiveMessage(string message)
	{
		print ("RpcReceiveMessage");
		m_currentMessage = message;
	}

	//call from the dealer.
	public void SetCandidates(IList<string> candidates)
	{
		//NOTE: this doesn't need to be an RPC because m_candidates is synched anyway.
		m_candidates = candidates;
	}
}
