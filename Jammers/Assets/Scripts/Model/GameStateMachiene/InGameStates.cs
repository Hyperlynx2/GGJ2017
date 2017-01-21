﻿using UnityEngine;
using System.Collections;

public enum InGameState
{
	ROUNDX,
	DESCRIBER_WAIT_STATE,
	GUESSER_DESCRIBER_WIN,
	GUESSER_GUESS,
	GUESSER_WAIT,
	JAM_LOOSE,
	JAM_MESSAGE,
	JAM_WAIT_FOR_TURN,
	JAM_WAIT_FOR_FINISH,
	JAM_WIN,
	SCORE_SCREEN,
	WAIT_FOR_DESCRIBER_TO_WRITE_DESCRIPTION,
	DESCRIBER_WRITE_DESCRIPTION
}

