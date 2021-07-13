using System.Collections.Generic;
using System.Linq;
using BattleshipStateMangement;
using BattleshipStateMangement.Exceptions;
using BattleshipStateMangement.Models;
using Xunit;

namespace BattleshipStateMangementUnitTests
{
    public class BoardTests
    {
        public readonly Board _board;
        public BoardTests()
        {
            _board = new Board();
        }

        [Fact]
        public void AddShip_WithCorrectCoordinate_PlacesShipCorrectly()
        {
            //Arrange
            _board.CreateBoard();
            var position = new List<Coordinate>() { new Coordinate(0,0), new Coordinate(0, 1), new Coordinate(0, 2), new Coordinate(0, 3),
            new Coordinate(0,4)};
            var battleShip = new Ship
            {
                HitCount = 0,
                Name = "CARRIER",
                Size = (int)ShipType.CARRIER,
                Position = position
            };
            //Act
            _board.AddShip(battleShip);

            //Assert
            _board.getShips().FirstOrDefault().Equals(battleShip);
        }

        [Fact]
        public void AddShip_OnExistingShip_ThrowsExcepetion()
        {
            //Arrange
            _board.CreateBoard();
            var position = new List<Coordinate>() { new Coordinate(0,0), new Coordinate(0, 1), new Coordinate(0, 2), new Coordinate(0, 3),
            new Coordinate(0,4)};

            var battleShip = new Ship
            {
                HitCount = 0,
                Name = "BATTLESHIP",
                Size = (int)ShipType.BATTLESHIP,
                Position = position
            };
            var position2 = new List<Coordinate>() { new Coordinate(0,0), new Coordinate(0, 1)};
            var rowingBoat = new Ship
            {
                HitCount = 0,
                Name = "DESTORYER",
                Size = (int)ShipType.DESTORYER,
                Position = position2
            };
            
            //Act
            _board.AddShip(battleShip);
            var exception = Assert.Throws<ShipOccupiedException>(() => _board.AddShip(rowingBoat));
        
            //Assert
            _board.getShips().FirstOrDefault().Equals(battleShip);
            exception.Message.Equals("Ship already Occupied at: X: 0 Y:0");
        }


        [Fact]
        public void AttackShip_OnNonOccupiedCell_ReturnsMiss()
        {
            //Arrange
            _board.CreateBoard();
            var position = new List<Coordinate>() { new Coordinate(0,0), new Coordinate(0, 1), new Coordinate(0, 2), new Coordinate(0, 3),
            new Coordinate(0,4)};
            var battleShip = new Ship
            {
                HitCount = 0,
                Name = "BATTLESHIP",
                Size = (int)ShipType.BATTLESHIP,
                Position = position
            };

            //Act
            _board.AddShip(battleShip);
            var status = _board.AttackBoard(new Coordinate(10, 10));

            //Assert
            status.Equals(CellStatus.Miss);
        }


        [Fact]
        public void AttackShip_OnOccupiedShip_ReturnsHit()
        {
            //Arrange
            _board.CreateBoard();
            var position = new List<Coordinate>() { new Coordinate(0,0), new Coordinate(0, 1), new Coordinate(0, 2), new Coordinate(0, 3),
            new Coordinate(0,4)};
            var battleShip = new Ship
            {
                HitCount = 0,
                Name = "BATTLESHIP",
                Size = (int)ShipType.BATTLESHIP,
                Position = position
            };

            //Act
            _board.AddShip(battleShip);
            var status = _board.AttackBoard(new Coordinate(0, 0));

            //Assert
            status.Equals(CellStatus.Hit);
        }

        [Fact]
        public void AttackShip_OnAlreadyAttackedCell_ThrowsException()
        {
            //Arrange
            _board.CreateBoard();
            var position = new List<Coordinate>() { new Coordinate(0,0), new Coordinate(0, 1), new Coordinate(0, 2), new Coordinate(0, 3),
            new Coordinate(0,4)};
            var battleShip = new Ship
            {
                HitCount = 0,
                Name = "BATTLESHIP",
                Size = (int)ShipType.BATTLESHIP,
                Position = position
            };

            //Act
            _board.AddShip(battleShip);
            _board.AttackBoard(new Coordinate(0, 0));
            var ex = Assert.Throws<CellAttackedException>(() => _board.AttackBoard(new Coordinate(0,0)));
            
            //Assert
            ex.Message.Equals("Cell at: X:0 Y:0 already attacked");
        }
    }
}
