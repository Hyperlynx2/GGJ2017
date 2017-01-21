using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dealer : MonoBehaviour
{
	////////
	//public
	////////
	public static Dealer Instance()
	{
		if(s_instance == null)
			throw new Exception("Dealer has not been attached to any GameObject!");

		return s_instance;
	}

	/*Signal to the dealer that you've made your move and are ready for the next turn.

	called by the server code of the Player gameObject (which is the state machine).
	We decide what to do based on what state we're in.*/
	public void PlayerReady(Player player)
	{
		//TODO: do this on all playeres if we've determined that they're all ready to change state.
		player.gameObject.GetComponent<ClientStateManager>().ChangeState();
	}

	public class Exception : System.Exception
	{
		Exception(string message) : base(message) {}
	}
		
	/////////
	//private
	/////////

	private static Dealer s_instance = null;

	enum State
	{
		/*Pick the guesser, describer, candidate words, target words
		wait for the describer to describe.*/
		DESCRIBER,
		/*wait for a jammer to mess with the message.
		repeat this state for each jammer*/
		JAMMERS,
		//wait for the guesser to guess
		GUESSER,
		//update scores. if nobody has won, go back to DESCRIBER.
		SCORE,
		//sweet victory. wait for the scene to be destroyed (when it goes back to the OutOfGame state)
		VICTORY
	}

	void Awake()
	{
		if(s_instance != null)
			throw new Exception("Dealer has been assigned to more than one GameObject. It's a singleton!");

		s_instance = this;
	}

	// Use this for initialization
	void Start ()
	{
		//TODO: LHF: grab all the Players in the scene and register them.	
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
