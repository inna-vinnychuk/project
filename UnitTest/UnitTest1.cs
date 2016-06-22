using VideoPlayer;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTest;

namespace Window1.Tests
{
    [TestClass()]
    public class UnitTest1
    {
        [TestMethod()]
        public void Window1Test()
        {

        }

        [TestMethod()]
        public void Window_LoadedTest()
        { 
        }

        [TestMethod()]
        public void player_MediaOpenedTest()
        {

        }

        [TestMethod()]
        public void ShowPositionTest()
        { 
        }

        [TestMethod()]
        public void EnableButtonsTest()
        {
            boolean is_playing == true;

            boolean expected = btnPlay.isEnabled == !is_playing && btnPause == is_playing && btnPlay.Opacity == 1 / (1 + is_playing) && btnPause.Opacity == 1 / (1 + !is_playing);

            boolean actual = !btnPlay.isEnabled && btnPause.isEnabled && btnPlay.Opacity == 0.5 && btnPause.Opacity == 1;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void btnOpen_ClickTest()
        {

        }

        [TestMethod()]
        public void btnPlay_ClickTest()
        {

        }

        [TestMethod()]
        public void btnPause_ClickTest()
        {

        }

        [TestMethod()]
        public void btnStop_ClickTest()
        {

        }

        [TestMethod()]
        public void btnSetPosition_ClickTest()
        {

        }

        [TestMethod()]
        public void timer_TickTest()
        {

        }
    }

    internal class boolean
    {
    }
}

namespace VideoPlayer.Tests
{
    [TestClass()]
    public class UnitTest1
    {
        [TestMethod()]
        public void EnableButtonsTest()
        {

        }
    }
}

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
