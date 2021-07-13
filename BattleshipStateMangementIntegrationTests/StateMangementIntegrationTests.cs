using System;
using System.Collections.Generic;
using BattleshipStateMangement;
using BattleshipStateMangement.Interfaces;
using BattleshipStateMangement.Models;
using SharedBattleTestFramework;
using Xunit;

namespace BattleshipStateMangementIntegrationTests
{
    public class StateMangementIntegrationTests
    {

        private IBoard _board1;
        private Board _board2;
        private Player _player;
        public StateMangementIntegrationTests()
        {
            _board1 = new Board();
            _board2 = new Board();
            _player = new Player(_board1, _board2);
        }

        [Fact]
        public void Player_SinksAllBattleShips_WinIsCorrectlyLogged()
        {
            //Arrange
            using (var consoleOutput = new ConsoleOutput())
            {
                _board2.CreateBoard();
                _board2.AddShip(new Ship
                {
                    Name = "DESTORYER",
                    HitCount = 0,
                    Position = new List<Coordinate>() { new Coordinate(0, 0), new Coordinate(0, 1) },
                    Size = 2
                });
                _board2.AddShip(new Ship
                {
                    Name = "SUBMARINE",
                    HitCount = 0,
                    Position = new List<Coordinate>() { new Coordinate(2, 3), new Coordinate(3, 3), new Coordinate(4, 3) },
                    Size = 3
                });

                //Act
                _player.AttackBoard(new Coordinate(0, 0));
                _player.AttackBoard(new Coordinate(0, 1));
                _player.AttackBoard(new Coordinate(2, 3));
                _player.AttackBoard(new Coordinate(3, 3));
                _player.AttackBoard(new Coordinate(4, 3));

                //Asserts
                Assert.EndsWith("All ships have been sunk, you win!\r\n", consoleOutput.GetOuput());
            }
        }
    }
}
