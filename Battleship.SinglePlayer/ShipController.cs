using Battleship.Entities;

namespace Battleship.SinglePlayer
{
    public static class ShipController
    {
        public static void Clear(this ShipEntity ship)
        {
            foreach (SquareEntity square in ship.ShipParts)
            {
                square.ResetState();
            }
        }

        public static bool IsSank(this ShipEntity ship)
        {
            foreach (SquareEntity square in ship.ShipParts)
            {
                if (square.Type != ShipType.Hit)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
