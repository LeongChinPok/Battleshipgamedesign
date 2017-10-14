using System;
using SwinGameSDK;
using NUnit.Framework;

using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

[TestFixture ()]
public class HighScoreTest
{
	[Test ()]
	public void TestHightScore ()
	{

		SwinGame.OpenGraphicsWindow ("Battle Ships", 800, 600);

		// Start the game
		GameResources.LoadResources ();
		GameController.StartGame ();

		Assert.AreEqual (GameController.CurrentState, GameState.Deploying);


		// Check for ai difficulty
		Assert.AreEqual (GameController.getDifficulty (), AIOption.Easy);

		// Start and end deployment
		GameController.HumanPlayer.RandomizeDeployment ();
		GameController.EndDeployment ();

		// Check is at discovering window
		Assert.AreEqual (GameController.CurrentState, GameState.Discovering);

		int row = 0;
		int column = 0;

		Random _Random = new Random ();
		AttackResult result = default (AttackResult);

		int hits = 0;
		int shots = 0;
		int score = 0;

		//keep hitting until a miss
		do {
			row = _Random.Next (0, GameController.HumanPlayer.EnemyGrid.Height);
			column = _Random.Next (0, GameController.HumanPlayer.EnemyGrid.Width);
			//generate coordinates for shot
			result = GameController.HumanPlayer.Shoot (row, column);
			if (result.Value == ResultOfAttack.Hit || result.Value == ResultOfAttack.Destroyed) {
				hits++;
				shots++;
			}
			if (result.Value == ResultOfAttack.Miss) {
				shots++;
			}
		} while (result.Value != ResultOfAttack.Destroyed && result.Value != ResultOfAttack.GameOver && !SwinGame.WindowCloseRequested ());

		score = (hits * 12) - shots - (GameController.HumanPlayer.PlayerGrid.ShipsKilled * 20);

		Assert.AreEqual (score, GameController.HumanPlayer.Score);

		GameController.EndCurrentState ();

		SwinGame.StopMusic ();

		//Free Resources and Close Audio, to end the program.
		GameResources.FreeResources ();
	}
}
