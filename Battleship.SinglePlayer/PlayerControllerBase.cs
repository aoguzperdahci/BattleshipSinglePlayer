using Battleship.Entities;

namespace Battleship.SinglePlayer
{
    public abstract class PlayerControllerBase : GameControllerBase, IPlayerController
    {
        protected PlayerControllerBase(PlaygroundEntity playerPlayground, PlaygroundEntity opponentPlayground) : base(playerPlayground, opponentPlayground)
        {
        }

        public abstract int DeleteShip(SquareEntity square);
    }
}
