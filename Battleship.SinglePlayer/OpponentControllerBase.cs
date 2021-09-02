using Battleship.Entities;

namespace Battleship.SinglePlayer
{
    public abstract class OpponentControllerBase : GameControllerBase, IOpponentController
    {
        protected OpponentControllerBase(PlaygroundEntity playerPlayground, PlaygroundEntity opponentPlayground) : base(playerPlayground, opponentPlayground)
        {
        }

        public abstract bool Fire();
        public abstract void PlaceShips();
        public abstract void ShowShips();
    }
}
