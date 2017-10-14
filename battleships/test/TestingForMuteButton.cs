using System;
using NUnit.Framework;

namespace Battleships
{
	[TestFixture]
	public class TestingForMuteButton
	{
		[Test]
		public void Test_mutebutton()
		{
			//ensure the music is on
			MenuController.Set_muted = true;
			//double ensure music is on
			Assert.AreEqual (true, MenuController.Get_Set_muted);

			//after user muted the music
			MenuController.PerformMusicMenuAction(2);
			//double ensure that music is off
			Assert.AreEqual (false, MenuController.Get_Set_muted);

		}
	}
}
