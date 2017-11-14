using System;
using NUnit.Framework;
using SwinGameSDK;

namespace Battleships
{
	[TestFixture ()]
	public class MuteMusicButton
	{
		[Test ()]
		public void MuteMusicButtonTest ()
		{
			SwinGame.OpenGraphicsWindow ("Battle Ships", 800, 600);

			GameResources.LoadResources ();

			//Set music to mute 
			MenuController.PerformMusicMenuAction (2);
			bool result = MenuController.getMusic;

			//Confirm button works after changing music to mute
			Assert.AreEqual (false, result);

			//Set music to electronic
			MenuController.PerformMusicMenuAction (1);
			bool result2 = MenuController.getMusic;

			//Confirm button works after changing music to electronic
			Assert.AreEqual (true, result2);
		}
	}
}
