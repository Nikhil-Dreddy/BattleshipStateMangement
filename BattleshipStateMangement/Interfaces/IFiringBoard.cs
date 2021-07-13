using BattleshipStateMangement.Models;

namespace BattleshipStateMangement.Interfaces
{
    public interface IFiringBoard
    {
        public CellStatus AttackBoard(Coordinate coordinate);

        public bool AllShipsSunk();
    }
}
