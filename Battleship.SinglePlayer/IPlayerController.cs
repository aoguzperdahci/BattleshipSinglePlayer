using Battleship.Entities;

namespace Battleship.SinglePlayer
{
    public interface IPlayerController : IGameController
    {
        public int DeleteShip(SquareEntity square);
    }
}
