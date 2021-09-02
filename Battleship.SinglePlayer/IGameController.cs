using Battleship.Entities;

namespace Battleship.SinglePlayer
{
    public interface IGameController
    {
        PlaygroundEntity PlayerPlayground { get; set; }
        PlaygroundEntity OpponentPlaygroud { get; set; }
        public Finished AttackTurn();
        public bool AddShip(int shipLength, int headX, int headY, ShipType type);
        public void RotateShip(SquareEntity square);
        public bool IsPlaceable(int shipLength, int headX, int headY, ShipRotation rotation);
    }
}