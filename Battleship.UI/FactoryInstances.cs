using Battleship.Entities;
using Battleship.SinglePlayer;
using System.Drawing;

namespace Battleship.UI
{
    public static class FactoryInstances
    {
        private static Size _size = new Size(Battleship.ComponentSize, Battleship.ComponentSize);

        public static SquareEntity SquareInitialze(int x, int y)
        {
            return new SquareEntity(x, y, _size);
        }

        public static IPlayerController CreatePlayerController(PlaygroundEntity playerPlayground, PlaygroundEntity opponentPlayground)
        {
            return new PlayerController(playerPlayground, opponentPlayground);
        }

        public static IOpponentController CreateOpponentController(PlaygroundEntity playerPlayground, PlaygroundEntity opponentPlayground)
        {
            return new AIController(playerPlayground, opponentPlayground);
        }

    }
}
