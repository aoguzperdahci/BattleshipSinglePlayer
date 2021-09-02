using Battleship.Entities;

namespace Battleship.SinglePlayer
{
    public static class PlaygroundController
    {
        public static bool IsFinished(this PlaygroundEntity playGround)
        {
            foreach (ShipEntity ship in playGround.Ships)
            {
                foreach (SquareEntity square in ship.ShipParts)
                {
                    if (square.Type != ShipType.Hit)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static void Enabled(this PlaygroundEntity playGround, bool enable)
        {
            foreach (SquareEntity square in playGround.Squares)
            {
                if (square.Type == ShipType.Hit)
                    continue;

                square.Enabled = enable;
            }
        }
    }
}
