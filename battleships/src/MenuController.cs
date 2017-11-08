using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using SwinGameSDK;

/// <summary>
/// The menu controller handles the drawing and user interactions
/// from the menus in the game. These include the main menu, game
/// menu and the settings m,enu.
/// </summary>
static class MenuController
{

	/// <summary>
	/// The menu structure for the game.
	/// </summary>
	/// <remarks>
	/// These are the text captions for the menu items.
	/// </remarks>
	private static readonly string [] [] _menuStructure = {
		new string[] {
			"PLAY",
			"SETUP",
			"SCORES",
			//MUTE FUNCTION
			"MUSIC",
			//CHANGE THEME
			"THEME",
			//INSTRUCTION FUNCTION
			"INSTRUCTION",
			"QUIT"
		},
		new string[] {
			"RETURN",
			"SURRENDER",
			"QUIT"
		},
		new string[] {
			"EASY",
			"MEDIUM",
			"HARD"
		},

		//MUTE FUNCTION
		new string[] {
			"DEFAULT",
			"EDM",
			"MUTE"
		},

		//THEME CHANGE
		new string[] {
			"DEFAULT",
			"CLASSIC"
		},

		//INSTRUCTION FUNCTION
		new string[] {
			""
		}
	};
	private const int MENU_TOP = 575;
	private const int MENU_LEFT = 30;
	private const int MENU_GAP = 0;
	private const int BUTTON_WIDTH = 79;
	private const int BUTTON_HEIGHT = 15;
	private const int BUTTON_SEP = BUTTON_WIDTH + MENU_GAP;

	private const int TEXT_OFFSET = 0;
	private const int MAIN_MENU = 0;
	private const int GAME_MENU = 1;
	private const int SETUP_MENU = 2;
	//MUTE FUNCTION
	private const int MUSIC_MENU = 3;
	//CHANGE THEME
	private const int THEME_MENU = 4;
	//INSTRUCTION FUNCTION
	private const int INSTRUCTION_MENU = 5;
	
	private const int MAIN_MENU_PLAY_BUTTON = 0;
	private const int MAIN_MENU_SETUP_BUTTON = 1;
	private const int MAIN_MENU_TOP_SCORES_BUTTON = 2;
	//MUTE FUNCTION
	private const int MAIN_MENU_MUSIC_BUTTON = 3;
	//CHANGE THEME
	private const int MAIN_MENU_THEME_BUTTON = 4;
	//INSTRUCTION FUNCTION
	private const int MAIN_MENU_INSTRUCTION_BUTTON = 5;
	private const int MAIN_MENU_QUIT_BUTTON = 6;

	private const int SETUP_MENU_EASY_BUTTON = 0;
	private const int SETUP_MENU_MEDIUM_BUTTON = 1;
	private const int SETUP_MENU_HARD_BUTTON = 2;
	private const int SETUP_MENU_EXIT_BUTTON = 3;

	//MUTE FUNCTION
	private const int MUSIC_MENU_BGM1_BUTTON = 0;
	private const int MUSIC_MENU_BGM2_BUTTON = 1;
	private const int MUSIC_MENU_MUTE_BUTTON = 2;

	//CHANGE THEME
	private const int THEME_MENU_BLUE_BUTTON = 0;
	private const int THEME_MENU_GREY_BUTTON = 1;

	//INSTRUCTION FUNCTION
	private const int INSTRUCTION_MENU_SC_BUTTON = 0;

	private const int GAME_MENU_RETURN_BUTTON = 0;
	private const int GAME_MENU_SURRENDER_BUTTON = 1;

	private const int GAME_MENU_QUIT_BUTTON = 2;
	private static readonly Color MENU_COLOR = SwinGame.RGBAColor (2, 167, 252, 255);

	private static readonly Color HIGHLIGHT_COLOR = SwinGame.RGBAColor (1, 57, 86, 255);
	/// <summary>
	/// Handles the processing of user input when the main menu is showing
	/// </summary>
	public static void HandleMainMenuInput ()
	{
		HandleMenuInput (MAIN_MENU, 0, 0);
	}

	/// <summary>
	/// Handles the processing of user input when the main menu is showing
	/// </summary>
	public static void HandleSetupMenuInput ()
	{
		bool handled = false;
		handled = HandleMenuInput (SETUP_MENU, 1, 1);

		if (!handled) {
			HandleMenuInput (MAIN_MENU, 0, 0);
		}
	}

	//MUTE FUNCTION
	public static void HandleMusicMenuInput ()
	{
		bool handled = false;
		handled = HandleMenuInput(MUSIC_MENU, 1, 1);

		if(!handled) {
			HandleMenuInput(MAIN_MENU, 0, 0);
		}
	}

	public static void HandleThemeMenuInput ()
	{
		bool handled = false;
		handled = HandleMenuInput (THEME_MENU, 1, 1);

		if (!handled) {
			HandleMenuInput (MAIN_MENU, 0, 0);
		}	}

	public static void HandleInstructionMenuInput ()
	{
		bool handled = false;
		handled = HandleMenuInput (INSTRUCTION_MENU, 1, 1);

		if (!handled) {
			HandleMenuInput (MAIN_MENU, 0, 0);
		}
	}

	/// <summary>
	/// Handle input in the game menu.
	/// </summary>
	/// <remarks>
	/// Player can return to the game, surrender, or quit entirely
	/// </remarks>
	public static void HandleGameMenuInput ()
	{
		HandleMenuInput (GAME_MENU, 0, 0);
	}

	/// <summary>
	/// Handles input for the specified menu.
	/// </summary>
	/// <param name="menu">the identifier of the menu being processed</param>
	/// <param name="level">the vertical level of the menu</param>
	/// <param name="xOffset">the xoffset of the menu</param>
	/// <returns>false if a clicked missed the buttons. This can be used to check prior menus.</returns>
	private static bool HandleMenuInput (int menu, int level, int xOffset)
	{
		if (SwinGame.KeyTyped (KeyCode.vk_ESCAPE)) {
			GameController.EndCurrentState ();
			return true;
		}

		if (SwinGame.MouseClicked (MouseButton.LeftButton)) {
			int i = 0;
			for (i = 0; i <= _menuStructure [menu].Length - 1; i++) {
				//IsMouseOver the i'th button of the menu
				if (IsMouseOverMenu (i, level, xOffset)) {
					PerformMenuAction (menu, i);
					return true;
				}
			}

			if (level > 0) {
				//none clicked - so end this sub menu
				GameController.EndCurrentState ();
			}
		}

		return false;
	}

	/// <summary>
	/// Draws the main menu to the screen.
	/// </summary>
	public static void DrawMainMenu ()
	{
		//Clears the Screen to Black
		//SwinGame.DrawText("Main Menu", Color.White, GameFont("ArialLarge"), 50, 50)

		DrawButtons (MAIN_MENU);
	}

	/// <summary>
	/// Draws the Game menu to the screen
	/// </summary>
	public static void DrawGameMenu ()
	{
		//Clears the Screen to Black
		//SwinGame.DrawText("Paused", Color.White, GameFont("ArialLarge"), 50, 50)

		DrawButtons (GAME_MENU);
	}

	/// <summary>
	/// Draws the settings menu to the screen.
	/// </summary>
	/// <remarks>
	/// Also shows the main menu
	/// </remarks>
	public static void DrawSettings ()
	{
		//Clears the Screen to Black
		//SwinGame.DrawText("Settings", Color.White, GameFont("ArialLarge"), 50, 50)

		DrawButtons (MAIN_MENU);
		DrawButtons (SETUP_MENU, 1, 1);
	}

	//MUTE FUNCTION
	public static void MusicSettings ()
	{
		DrawButtons (MAIN_MENU);
		DrawButtons (MUSIC_MENU, 1, 1);
	}

	public static void ThemeSettings ()
	{
		DrawButtons (MAIN_MENU);
		DrawButtons (THEME_MENU, 1, 1);	}

	//INSTRUCTION FUNCTION
	public static void InstructionSettings ()
	{
		const int INST_LEFT = 10;
		const int INST_TOP = 10;

		SwinGame.DrawText ("Objective:", Color.Gold, GameResources.GameFont ("Quicksand"), INST_LEFT, INST_TOP);
		SwinGame.DrawText ("Sink all enemy ships before they destroy all of yours.", Color.Gold, GameResources.GameFont ("Quicksand"), INST_LEFT + 135, INST_TOP);
		SwinGame.DrawText ("Preparation:", Color.Gold, GameResources.GameFont ("Quicksand"), INST_LEFT, INST_TOP + 25);
		SwinGame.DrawText ("Place ship horizontally or vertically (Can be randomed).", Color.Gold, GameResources.GameFont ("Quicksand"), INST_LEFT + 135, INST_TOP + 25);
		SwinGame.DrawText ("Gameplay:", Color.Gold, GameResources.GameFont ("Quicksand"), INST_LEFT, INST_TOP + 50);
		SwinGame.DrawText ("Click on a grid to 'attack' enemy, Red indicates shots hit on", Color.Gold, GameResources.GameFont ("Quicksand"), INST_LEFT + 135, INST_TOP + 50);
		SwinGame.DrawText ("enemy battleship while Blue indicates shots missed.", Color.Gold, GameResources.GameFont ("Quicksand"), INST_LEFT + 135, INST_TOP + 75);

		SwinGame.DrawText ("MAIN MENU", Color.Gold, GameResources.GameFont ("Quicksand"), INST_LEFT, INST_TOP + 135);
		SwinGame.DrawText ("DESCRIPTION", Color.Gold, GameResources.GameFont ("Quicksand"), INST_LEFT + 225, INST_TOP + 135);
		SwinGame.DrawText ("Play", Color.Gold, GameResources.GameFont ("Quicksand"), INST_LEFT, INST_TOP + 160);
		SwinGame.DrawText ("Allow deployment of ship and start game against AI", Color.Gold, GameResources.GameFont ("Quicksand"), INST_LEFT + 225, INST_TOP + 160);
		SwinGame.DrawText ("Setup", Color.Gold, GameResources.GameFont ("Quicksand"), INST_LEFT, INST_TOP + 185);
		SwinGame.DrawText ("Allow AI difficulty to be changed", Color.Gold, GameResources.GameFont ("Quicksand"), INST_LEFT + 225, INST_TOP + 185);
		SwinGame.DrawText ("Scores", Color.Gold, GameResources.GameFont ("Quicksand"), INST_LEFT, INST_TOP + 210);
		SwinGame.DrawText ("Show TOP 10 highest score in the game", Color.Gold, GameResources.GameFont ("Quicksand"), INST_LEFT + 225, INST_TOP + 210);
		SwinGame.DrawText ("Music", Color.Gold, GameResources.GameFont ("Quicksand"), INST_LEFT, INST_TOP + 235);
		SwinGame.DrawText ("Allow background music to be changed or muted", Color.Gold, GameResources.GameFont ("Quicksand"), INST_LEFT + 225, INST_TOP + 235);
		SwinGame.DrawText ("Quit", Color.Gold, GameResources.GameFont ("Quicksand"), INST_LEFT, INST_TOP + 260);
		SwinGame.DrawText ("Close the program", Color.Gold, GameResources.GameFont ("Quicksand"), INST_LEFT + 225, INST_TOP + 260);

		SwinGame.DrawText ("SHORTCUTS", Color.Gold, GameResources.GameFont ("Quicksand"), INST_LEFT, INST_TOP + 320);
		SwinGame.DrawText ("DESCRIPTION", Color.Gold, GameResources.GameFont ("Quicksand"), INST_LEFT + 225, INST_TOP + 320);
		SwinGame.DrawText ("esc", Color.Gold, GameResources.GameFont ("Quicksand"), INST_LEFT, INST_TOP + 345);
		SwinGame.DrawText ("Allow user to pause/quit game, return to main menu", Color.Gold, GameResources.GameFont ("Quicksand"), INST_LEFT + 225, INST_TOP + 345);
		SwinGame.DrawText ("up/down arrow", Color.Gold, GameResources.GameFont ("Quicksand"), INST_LEFT, INST_TOP + 370);
		SwinGame.DrawText ("Place ship vertically in deployment stage", Color.Gold, GameResources.GameFont ("Quicksand"), INST_LEFT + 225, INST_TOP + 370);
		SwinGame.DrawText ("left/right arrow", Color.Gold, GameResources.GameFont ("Quicksand"), INST_LEFT, INST_TOP + 395);
		SwinGame.DrawText ("Place ship horizontally in deployment stage", Color.Gold, GameResources.GameFont ("Quicksand"), INST_LEFT + 225, INST_TOP + 395);
		SwinGame.DrawText ("r", Color.Gold, GameResources.GameFont ("Quicksand"), INST_LEFT, INST_TOP + 420);
		SwinGame.DrawText ("Randomize ship placement in deployment stage", Color.Gold, GameResources.GameFont ("Quicksand"), INST_LEFT + 225, INST_TOP + 420);

		DrawButtons (MAIN_MENU);
	}

	/// <summary>
	/// Draw the buttons associated with a top level menu.
	/// </summary>
	/// <param name="menu">the index of the menu to draw</param>
	private static void DrawButtons (int menu)
	{
		DrawButtons (menu, 0, 0);
	}

	/// <summary>
	/// Draws the menu at the indicated level.
	/// </summary>
	/// <param name="menu">the menu to draw</param>
	/// <param name="level">the level (height) of the menu</param>
	/// <param name="xOffset">the offset of the menu</param>
	/// <remarks>
	/// The menu text comes from the _menuStructure field. The level indicates the height
	/// of the menu, to enable sub menus. The xOffset repositions the menu horizontally
	/// to allow the submenus to be positioned correctly.
	/// </remarks>
	private static void DrawButtons (int menu, int level, int xOffset)
	{
		int btnTop = 0;

		btnTop = MENU_TOP - (MENU_GAP + BUTTON_HEIGHT) * level;
		int i = 0;
		for (i = 0; i <= _menuStructure [menu].Length - 1; i++) {
			int btnLeft = 0;
			btnLeft = MENU_LEFT + BUTTON_SEP * (i + xOffset);
			//SwinGame.FillRectangle(Color.White, btnLeft, btnTop, BUTTON_WIDTH, BUTTON_HEIGHT)
			SwinGame.DrawTextLines (_menuStructure [menu] [i], MENU_COLOR, Color.Black, GameResources.GameFont ("Menu"), FontAlignment.AlignCenter, btnLeft + TEXT_OFFSET, btnTop + TEXT_OFFSET, BUTTON_WIDTH, BUTTON_HEIGHT);

			if (SwinGame.MouseDown (MouseButton.LeftButton) & IsMouseOverMenu (i, level, xOffset)) {
				SwinGame.DrawRectangle (HIGHLIGHT_COLOR, btnLeft, btnTop, BUTTON_WIDTH, BUTTON_HEIGHT);
			}
		}
	}

	/// <summary>
	/// Determined if the mouse is over one of the button in the main menu.
	/// </summary>
	/// <param name="button">the index of the button to check</param>
	/// <returns>true if the mouse is over that button</returns>
	private static bool IsMouseOverButton (int button)
	{
		return IsMouseOverMenu (button, 0, 0);
	}

	/// <summary>
	/// Checks if the mouse is over one of the buttons in a menu.
	/// </summary>
	/// <param name="button">the index of the button to check</param>
	/// <param name="level">the level of the menu</param>
	/// <param name="xOffset">the xOffset of the menu</param>
	/// <returns>true if the mouse is over the button</returns>
	private static bool IsMouseOverMenu (int button, int level, int xOffset)
	{
		int btnTop = MENU_TOP - (MENU_GAP + BUTTON_HEIGHT) * level;
		int btnLeft = MENU_LEFT + BUTTON_SEP * (button + xOffset);

		return UtilityFunctions.IsMouseInRectangle (btnLeft, btnTop, BUTTON_WIDTH, BUTTON_HEIGHT);
	}

	/// <summary>
	/// A button has been clicked, perform the associated action.
	/// </summary>
	/// <param name="menu">the menu that has been clicked</param>
	/// <param name="button">the index of the button that was clicked</param>
	private static void PerformMenuAction (int menu, int button)
	{
		switch (menu) {
		case MAIN_MENU:
			PerformMainMenuAction (button);
			break;
		case SETUP_MENU:
			PerformSetupMenuAction (button);
			break;
		case GAME_MENU:
			PerformGameMenuAction (button);
			break;
			//MUTE FUNCTION
		case MUSIC_MENU:
			PerformMusicMenuAction (button);
			break;
		case THEME_MENU:
			PerformThemeMenuAction (button);
			break;


		}
	}

	/// <summary>
	/// The main menu was clicked, perform the button's action.
	/// </summary>
	/// <param name="button">the button pressed</param>
	private static void PerformMainMenuAction (int button)
	{
		switch (button) {
		case MAIN_MENU_PLAY_BUTTON:
			GameController.StartGame ();
			break;
		case MAIN_MENU_SETUP_BUTTON:
			GameController.AddNewState (GameState.AlteringSettings);
			break;
		case MAIN_MENU_TOP_SCORES_BUTTON:
			GameController.AddNewState (GameState.ViewingHighScores);
			break;
			//MUTE FUNCTION
		case MAIN_MENU_MUSIC_BUTTON:
			GameController.AddNewState (GameState.MusicSettings);
			break;
			//CHANGE THEME
		case MAIN_MENU_THEME_BUTTON:
			GameController.AddNewState (GameState.ChangeTheme);
			break;
			//INSTRUCTION FUNCTION
		case MAIN_MENU_INSTRUCTION_BUTTON:
			GameController.AddNewState (GameState.ViewInstruction);
			break;

		case MAIN_MENU_QUIT_BUTTON:
			GameController.EndCurrentState ();
			break;

		}
	}

	/// <summary>
	/// The setup menu was clicked, perform the button's action.
	/// </summary>
	/// <param name="button">the button pressed</param>
	private static void PerformSetupMenuAction (int button)
	{
		switch (button) {
		case SETUP_MENU_EASY_BUTTON:
			GameController.SetDifficulty (AIOption.Easy);
			break;
		case SETUP_MENU_MEDIUM_BUTTON:
			GameController.SetDifficulty (AIOption.Medium);
			break;
		case SETUP_MENU_HARD_BUTTON:
			GameController.SetDifficulty (AIOption.Hard);
			break;
		}
		//Always end state - handles exit button as well
		GameController.EndCurrentState ();
	}

	/// <summary>
	/// The game menu was clicked, perform the button's action.
	/// </summary>
	/// <param name="button">the button pressed</param>
	private static void PerformGameMenuAction (int button)
	{
		switch (button) {
		case GAME_MENU_RETURN_BUTTON:
			GameController.EndCurrentState ();
			break;
		case GAME_MENU_SURRENDER_BUTTON:
			GameController.EndCurrentState ();
			//end game menu
			GameController.EndCurrentState ();
			//end game
			break;
		case GAME_MENU_QUIT_BUTTON:
			GameController.AddNewState (GameState.Quitting);
			break;
		}
	}

	//MUTE FUNCTION
	private static void PerformMusicMenuAction(int button)
	{
		if (button == MUSIC_MENU_BGM1_BUTTON) 
		{
			SwinGame.PlayMusic (GameResources.GameMusic ("Background"));
		} 
		else if (button == MUSIC_MENU_BGM2_BUTTON) 
		{
			SwinGame.PlayMusic (GameResources.GameMusic ("Electronic"));
		} 
		else if (button == MUSIC_MENU_MUTE_BUTTON)
		{
			SwinGame.StopMusic();
		}

		GameController.EndCurrentState ();
	}

	private static void PerformThemeMenuAction (int button)
	{
		switch (button) {
		case THEME_MENU_BLUE_BUTTON:
			do {
			GameController.HandleUserInput ();
			GameController.DrawScreen ();
		} while (!(SwinGame.WindowCloseRequested () == true | GameController.CurrentState == GameState.Quitting));
			break;

		case THEME_MENU_GREY_BUTTON:
			do {
			GameController.HandleUserInput ();
			GameController.DrawScreen2 ();
		} while (!(SwinGame.WindowCloseRequested () == true | GameController.CurrentState == GameState.Quitting));
			break;
		}
	}}
