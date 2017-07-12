using Microsoft.VisualStudio.TestTools.UnitTesting;
using BandiEngine;
using System;

namespace BandiEngine.Tests
{
    [TestClass]
    public class GameTests
    {
        public class MockGame : Game
        {
            public class EventArgs : System.EventArgs
            {

            }
            public event EventHandler<EventArgs> Initialized;
            public event EventHandler<EventArgs> Updated;
            public event EventHandler<EventArgs> Drawed;
            public override void Initialize()
            {
                base.Initialize();
                Initialized?.Invoke(this, new EventArgs());
            }
            public override void Update()
            {
                base.Update();
                Updated?.Invoke(this, new EventArgs());
            }
            public override void Draw()
            {
                base.Draw();
                Drawed?.Invoke(this, new EventArgs());

                Exit();
            }
        }

        MockGame game;

        [TestInitialize]
        public void Initialize()
        {
            game = new MockGame();
        }
        [TestCleanup]
        public void CleanUp()
        {
            game.Dispose();
        }
        [TestMethod]
        public void RunTest()
        {
            bool initialized = false;
            bool updated = false;
            bool drawed = false;

            game.Initialized += (o, e) => initialized = true;
            game.Updated += (o, e) => updated = true;
            game.Drawed += (o, e) => drawed = true;

            game.Run();
            Assert.IsTrue(initialized, "Initialize 메소드가 호출되지 않았습니다.");
            Assert.IsTrue(updated, "Update 메소드가 호출되지 않았습니다.");
            Assert.IsTrue(drawed, "Draw 메소드가 호출되지 않았습니다.");
        }
    }
}