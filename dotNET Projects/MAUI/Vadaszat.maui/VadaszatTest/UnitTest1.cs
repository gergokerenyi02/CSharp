using Moq;
using System.Drawing;
using Vadaszat.Model;
using Vadaszat.Persistence;

namespace VadaszatTest
{
    [TestClass]
    public class UnitTest1
    {
        private Game _model = null!; // a tesztelendõ modell
        private GameTable _mockedTable = null!; // mockolt játéktábla
        private Mock<IVadaszatDataAccess> _mock = null!; // az adatelérés mock-ja

        [TestInitialize]
        public void Initialize()
        {
            _mockedTable = new GameTable(3);


            _mock = new Mock<IVadaszatDataAccess>();
            _mock.Setup(mock => mock.LoadAsync(It.IsAny<String>()))
                .Returns(() => Task.FromResult(_mockedTable));

            _model = new Game(_mockedTable.MapSize, _mock.Object);

        }

        [TestMethod]

        public void TestStartOfANewGame()
        {
            Assert.AreEqual(0, _model.GameTable.currentSteps);
            Assert.AreEqual(12, _model.GameTable.maxSteps);
            Assert.AreEqual(PlayerTurn.Player1, (PlayerTurn)_model.GameTable.currentPlayer);
            Assert.AreEqual(0, _model.SelectedCharacterIndex);
        }

        [TestMethod]

        public void TestSwitchSelectedCharacter()
        {
            _model.SwitchSelectedCharacter();
            Assert.AreEqual(1, _model.SelectedCharacterIndex);
        }

        [TestMethod]

        public void TestStartTurn()
        {
            Assert.AreEqual(PlayerTurn.Player1, (PlayerTurn)_model.GameTable.currentPlayer);
            _model.StartTurn(Direction.Up);
            Assert.AreEqual(PlayerTurn.Player2, (PlayerTurn)_model.GameTable.currentPlayer);

            if (_model.GameTable.player1 != null)
            {
                Assert.AreEqual(new Point(0, 1), _model.GameTable.player1.Location());
            }

        }

        [TestMethod]

        public void TestPlayerPosition()
        {
            if (_model.GameTable.player2 != null && _model.GameTable.player1 != null)
            {
                Point p2_p1 = _model.GameTable.player2.Location(0);
                Point p2_p2 = _model.GameTable.player2.Location(1);
                Point p2_p3 = _model.GameTable.player2.Location(2);
                Point p2_p4 = _model.GameTable.player2.Location(3);


                Assert.AreEqual(1, (_model.GameTable.player1.Location()).X);
                Assert.AreEqual(1, (_model.GameTable.player1.Location()).Y);
                Assert.AreEqual(new Point(0, 0), p2_p1);
                Assert.AreEqual(new Point(0, 2), p2_p2);
                Assert.AreEqual(new Point(2, 0), p2_p3);
                Assert.AreEqual(new Point(2, 2), p2_p4);
            }

        }

        [TestMethod]

        public void TestRestart()
        {
            _model.StartTurn(Direction.Up);
            Assert.AreEqual(PlayerTurn.Player2, (PlayerTurn)_model.GameTable.currentPlayer);
            if (_model.GameTable.player1 != null)
            {
                Assert.AreEqual(new Point(0, 1), _model.GameTable.player1.Location());

                _model.Restart();
                Assert.AreEqual(PlayerTurn.Player1, (PlayerTurn)_model.GameTable.currentPlayer);
                Assert.AreEqual(new Point(1, 1), _model.GameTable.player1.Location());
            }
        }

        [TestMethod]
        public void TestWinner()
        {

            bool gotWinner = false;

            _model.StartTurn(Direction.Up);
            _model.StartTurn(Direction.Down);
            _model.StartTurn(Direction.Left);

            _model.SwitchSelectedCharacter();

            _model.GotWinner += (sender, args) =>
            {
                gotWinner = true;
            };
            _model.StartTurn(Direction.Left);

            Assert.IsTrue(gotWinner);

        }

        [TestMethod]
        public void TestFalseMove()
        {
            if (_model.GameTable.player1 != null && _model.GameTable.player2 != null)
            {
                _model.StartTurn(Direction.Up);
                _model.StartTurn(Direction.Left);
                _model.StartTurn(Direction.Up);



                Assert.AreEqual(new Point(0, 0), _model.GameTable.player2.Location(0));
                Assert.AreEqual(new Point(0, 1), _model.GameTable.player1.Location());
            }
        }
    }
}