using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fireball : MonoBehaviour 
{
	public static Fireball s_instance;

	public int _iRounds;

	public int _iJamers;

	public int[] _iScores = new int[]{0,0,0,0}; 

	public int[] _iTargets = new int[]{0,0,0,0};

	public string[] _strClues = new string[]{"","","",""};

	public int _iGuesser;

	public string[] _strCandidates;

	public string _hand;

	public GameObject _objIntermission;

	public GameObject _objRoundStart;

	public GameObject _objWriteDescriptions;

	public Text _txtDescriptionNumber;

	public InputField _txtDescriptionInputField;

	public GameObject _objSelectJamTarget;

	public GameObject _objTypeNewSignal;

	public InputField _txtNewMessage;

	public GameObject _objGuesserSelect;

	public GameObject _objGuesserWin;

	public GameObject _objJamWin;

	public GameObject _objScoreScreen;

	public Text _txtPlayerWinner;

	// Use this for initialization
	void Start () 
	{
		s_instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public IEnumerator RunGame()
	{
		//

		//loop roiunds 
		for (int i = 0; i < _iRounds; i++) 
		{
			//display round start
			_objRoundStart.SetActive(true);

			GameContinueButton.s_bContiune = false;

			//wait for player to start
			while (GameContinueButton.s_bContiune == false) 
			{
				yield return null;
			}

			//get candidate set

			//randomly select target card
			//_iDescribeTarget = Random.Range(0,_strCandidates.Length);

			//set random targets for everyone 
			for (int j = 0; j < _iTargets.Length -1; j++) 
			{
				bool bPasses = false;
				
				while(bPasses == false)
				{
					bPasses = true;

					_iTargets [j] = Random.Range (0, _iTargets.Length - 1);

					for (int k = 0; k < _iTargets.Length; k++) 
					{
						if (_iTargets [j] == _iTargets [k -1] ) {
							bPasses = false;
						}
					}
				}
			}
			//set guesser target
			_iTargets[0] == _iTargets[_iTargets.Length -1];

			_objRoundStart.SetActive(false);

			//display clue enter
			_objWriteDescriptions.SetActive(true);

			for (int j = 0; j < _strClues.Length; j++) 
			{
				_txtDescriptionNumber.text = (j + 1);

				GameContinueButton.s_bContiune = false;

				while (GameContinueButton.s_bContiune == false) 
				{
					yield return null;
				}

				_strClues [j] = _txtDescriptionInputField.text;

			}

			_objWriteDescriptions.SetActive(false);

		

			//loop through all jammers

			for (int j = 1; j < _iTargets.Length - 1; j++) 
			{

				_objIntermission.SetActive (true);

				GameContinueButton.s_bContiune = false;

				//wait for player to start
				while (GameContinueButton.s_bContiune == false) 
				{
					yield return null;
				}

				_objIntermission.SetActive (false);


				_objSelectJamTarget.SetActive (true);

				SelectCard.s_bSelected = false;

				while (SelectCard.s_bSelected == false) 
				{
					yield return null;
				}

				_objSelectJamTarget.SetActive (false);

				_objTypeNewSignal.SetActive (true);

				GameContinueButton.s_bContiune = false;
				while(GameContinueButton.s_bContiune == false)
				{
					yield return false;
				}

				_strClues [SelectCard.s_iSelectedCard] = _txtNewMessage.text;

				_objTypeNewSignal.SetActive (false);

			}

			_objIntermission.SetActive (true);

			GameContinueButton.s_bContiune = false;

			//wait for player to start
			while (GameContinueButton.s_bContiune == false) 
			{
				yield return null;
			}

			_objIntermission.SetActive (false);

			_objGuesserSelect.SetActive (true);


			SelectCard.s_bSelected = false;

			while (SelectCard.s_bSelected == false) 
			{
				yield return null;
			}

			_objGuesserSelect.SetActive (false);

			int iwinner = 0; 
		
			for (int j = 0; j < _iTargets.Length; j++) 
			{
				if (_iTargets [j] == SelectCard.s_bSelected) 
				{
					_iScores [j] += 1;
					iwinner = j;
				}
			}

			if (_iTargets [0] == SelectCard.s_bSelected) {
				_objGuesserWin.SetActive (true);
				GameContinueButton.s_bContiune = false;

				//wait for player to start
				while (GameContinueButton.s_bContiune == false) 
				{
					yield return null;
				}
				_objGuesserWin.SetActive (false);
			} else 
			{
				_objJamWin.SetActive (true);

				_txtPlayerWinner.text = iwinner + 1;

				GameContinueButton.s_bContiune = false;

				//wait for player to start
				while (GameContinueButton.s_bContiune == false) 
				{
					yield return null;
				}
				_objJamWin.SetActive (false);
			}

			_objScoreScreen.SetActive (true);

			GameContinueButton.s_bContiune = false;

			//wait for player to start
			while (GameContinueButton.s_bContiune == false) 
			{
				yield return null;
			}

			_objScoreScreen.SetActive (false);

		}
	}

}
